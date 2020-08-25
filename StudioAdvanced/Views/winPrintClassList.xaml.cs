using Microsoft.Reporting.WinForms;
using System.Windows;
using StudioAdvanced.StudioAdvancedDataSet2TableAdapters;
using StudioAdvanced.Utilities;
using System.Data;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPrintClassList.xaml
    /// </summary>
    public partial class winPrintClassList : Window
    {
        public winPrintClassList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsClassList = new StudioAdvancedDataSet2();
            dsClassList.BeginInit();
            reportData.Name = "dsClassList";
            reportData.Value = dsClassList.up_PrintClassList;
            ReportClassList.ProcessingMode = ProcessingMode.Local;
            ReportClassList.LocalReport.DataSources.Add(reportData);
            ReportClassList.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptClassList.rdlc";
            ReportClassList.LocalReport.ReportPath = @"../../Reports/rptClassList.rdlc";
            var printClassListTableAdapter = new up_PrintClassListTableAdapter {ClearBeforeFill = true};
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            printClassListTableAdapter.Fill(dsClassList.up_PrintClassList);
            dsClassList.EndInit();
            ReportClassList.RefreshReport();
        }
    }
}
