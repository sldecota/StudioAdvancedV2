using System.Data;
using System.IO;
using System.Windows;
using Microsoft.Reporting.WinForms;
using StudioAdvanced.Models;
using StudioAdvanced.PrintReceiptTableAdapters;
using StudioAdvanced.Utilities;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winReceipt.xaml
    /// </summary>
    public partial class winReceipt : Window
    {
        public winReceipt()
        {
            InitializeComponent();
        }
        
        public PaymentModel Payment { get; set; }
        public int FamilyID { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsReceipt = new PrintReceipt();
            dsReceipt.BeginInit();
            reportData.Name = "dsReceipt";
            reportData.Value = dsReceipt.up_PrintReceipt;
            Receipt.ProcessingMode = ProcessingMode.Local;
            Receipt.LocalReport.DataSources.Clear();
            Receipt.LocalReport.DataSources.Add(reportData);
            Receipt.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptReceipt.rdlc";
            Receipt.LocalReport.ReportPath = @".../.../Reports/rptReceipt.rdlc";
            var printReceiptTableAdapter = new up_PrintReceiptTableAdapter {ClearBeforeFill = true};
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            printReceiptTableAdapter.Fill(dsReceipt.up_PrintReceipt);
            dsReceipt.EndInit();
            Receipt.RefreshReport();
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

            byte[] bytes = Receipt.LocalReport.Render
            ("PDF", null, out mimeType, out encoding, out extension, out
                streamids, out warnings);

            MemoryStream memoryStream = new MemoryStream(bytes);
            memoryStream.Seek(0, SeekOrigin.Begin);

            MailMessage message = new MailMessage();
            Attachment attachment = new Attachment(memoryStream, "Receipt.PDF");
            message.Attachments.Add(attachment);

            message.From = new MailAddress(username);
            message.To.Add(txtEmail.Text);

            message.Subject = "Stages Payment Receipt";
            message.IsBodyHtml = true;

            message.Body = "Please find your receipt attached.";

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
        }
    }
}
