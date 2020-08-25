using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using StudioAdvanced.PrintAccoutDetailsDataSetTableAdapters;
using StudioAdvanced.PrintStatementDataSetTableAdapters;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPrintAccountDetails.xaml
    /// </summary>
    public partial class winPrintAccountDetails : Window
    {
        public winPrintAccountDetails()
        {
            InitializeComponent();
        }

        public int FamilyID { get; set; }
        public string EmailAddress { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsStatement = new PrintAccountDetailsDataSet();
            dsStatement.BeginInit();
            reportData.Name = "dsStatement";
            reportData.Value = dsStatement.up_PrintAccountDetails;
            ReportStatement.ProcessingMode = ProcessingMode.Local;
            ReportStatement.LocalReport.DataSources.Clear();
            ReportStatement.LocalReport.DataSources.Add(reportData);
            ReportStatement.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptAccountDetails.rdlc";
            ReportStatement.LocalReport.ReportPath = @"../../Reports/rptAccountDetails.rdlc";
            var printAccountDetailsTableAdapter = new up_PrintAccountDetailsTableAdapter { ClearBeforeFill = true };
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            printAccountDetailsTableAdapter.Fill(dsStatement.up_PrintAccountDetails, FamilyID);
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
