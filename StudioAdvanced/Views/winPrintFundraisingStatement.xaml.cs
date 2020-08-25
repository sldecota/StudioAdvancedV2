using System.Data;
using System.Windows;
using Microsoft.Reporting.WinForms;
using StudioAdvanced.PrintFundraisingStatementDataSetTableAdapters;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPrintFundraisingStatement.xaml
    /// </summary>
    public partial class winPrintFundraisingStatement : Window
    {
        public winPrintFundraisingStatement()
        {
            InitializeComponent();
        }

        public int FamilyID { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsStatement = new PrintFundraisingStatementDataSet();
            dsStatement.BeginInit();
            reportData.Name = "dsFundraisingStatement";
            reportData.Value = dsStatement.up_PrintFundraisingStatement;
            ReportStatement.ProcessingMode = ProcessingMode.Local;
            ReportStatement.LocalReport.DataSources.Clear();
            ReportStatement.LocalReport.DataSources.Add(reportData);
            ReportStatement.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptFundraisingStatement.rdlc";
            ReportStatement.LocalReport.ReportPath = @"../../Reports/rptFundraisingStatement.rdlc";
            var printStatementTableAdapter = new up_PrintFundraisingStatementTableAdapter { ClearBeforeFill = true };
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            printStatementTableAdapter.Fill(dsStatement.up_PrintFundraisingStatement, FamilyID);
            dsStatement.EndInit();
            ReportStatement.RefreshReport();
        }
    }
}
