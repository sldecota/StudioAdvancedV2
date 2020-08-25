using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows;
using Microsoft.Reporting.WinForms;
using StudioAdvanced.PrintStatementDataSetTableAdapters;
using StudioAdvanced.Utilities;
using System.Text.RegularExpressions;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPrintStatement.xaml
    /// </summary>
    public partial class winPrintStatement : Window
    {
        public winPrintStatement()
        {
            InitializeComponent();
        }

        public int FamilyID { get; set; }
        public string EmailAddress { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsStatement = new PrintStatementDataSet();
            dsStatement.BeginInit();
            reportData.Name = "dsStatement";
            reportData.Value = dsStatement.up_PrintStatement;
            ReportStatement.ProcessingMode = ProcessingMode.Local;
            ReportStatement.LocalReport.DataSources.Clear();
            ReportStatement.LocalReport.DataSources.Add(reportData);
            ReportStatement.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptStatement.rdlc";
            ReportStatement.LocalReport.ReportPath = @"../../Reports/rptStatement.rdlc";
            var printStatementTableAdapter = new up_PrintStatementTableAdapter {ClearBeforeFill = true};
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            printStatementTableAdapter.Fill(dsStatement.up_PrintStatement, FamilyID);
            dsStatement.EndInit();
            ReportStatement.RefreshReport();
            txtEmail.Text = Data.Instance.GetFamilyEmail(FamilyID);
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(txtEmail.Text,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Please enter a valid email address", "Invalid Email Address");
                return;
            }
            var username = Data.Instance.GetEmailAddress();
            var password = Data.Instance.GetEmailPassword();
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            NetworkCredential cred = new NetworkCredential(username, password);

            byte[] bytes = ReportStatement.LocalReport.Render
            ("PDF", null, out mimeType, out encoding, out extension, out
                streamids, out warnings);

            MemoryStream memoryStream = new MemoryStream(bytes);
            memoryStream.Seek(0, SeekOrigin.Begin);

            MailMessage message = new MailMessage();
            Attachment attachment = new Attachment(memoryStream, "Statement.PDF");
            message.Attachments.Add(attachment);

            message.From = new MailAddress(username);
            message.To.Add(txtEmail.Text);

            message.Subject = "Stages Account Statement";
            message.IsBodyHtml = true;

            message.Body = "Please find your current statement attached.";

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

            memoryStream.Close();
            memoryStream.Dispose();
            Close();
        }
    }
}
