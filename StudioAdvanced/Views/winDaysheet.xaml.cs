using Microsoft.Reporting.WinForms;
using System.Data;
using System.Windows;
using StudioAdvanced.StudioAdvancedDataSetTableAdapters;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winDaysheet.xaml
    /// </summary>
    public partial class winDaysheet : Window
    {
        public winDaysheet()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsDaysheet = new StudioAdvancedDataSet();
            dsDaysheet.BeginInit();
            reportData.Name = "dsDaysheet";
            reportData.Value = dsDaysheet.up_PrintDaySheet;
            ReportDaysheet.ProcessingMode = ProcessingMode.Local;
            ReportDaysheet.LocalReport.DataSources.Clear();
            ReportDaysheet.LocalReport.DataSources.Add(reportData);
            ReportDaysheet.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptDaysheet.rdlc";
            ReportDaysheet.LocalReport.ReportPath = @"../../Reports/rptDaysheet.rdlc";
            var daySheetTableAdapter = new up_PrintDaySheetTableAdapter {ClearBeforeFill = true};
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            daySheetTableAdapter.Fill(dsDaysheet.up_PrintDaySheet);
            dsDaysheet.EndInit();
            ReportDaysheet.RefreshReport();
        }
    }
}
