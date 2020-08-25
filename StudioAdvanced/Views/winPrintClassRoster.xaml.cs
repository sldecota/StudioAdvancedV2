using Microsoft.Reporting.WinForms;
using StudioAdvanced.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudioAdvanced.PrintRosterDataSetTableAdapters;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPrintClassRoster.xaml
    /// </summary>
    public partial class winPrintClassRoster : Window
    {
        public winPrintClassRoster()
        {
            InitializeComponent();
        }

        public int ClassID { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsRoster = new PrintRosterDataSet();
            dsRoster.BeginInit();
            dsRoster.EnforceConstraints = false;
            reportData.Name = "dsRoster";
            reportData.Value = dsRoster.up_PrintClassRoster;
            ReportClassRoster.ProcessingMode = ProcessingMode.Local;
            ReportClassRoster.LocalReport.DataSources.Add(reportData);
            ReportClassRoster.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptClassRoster.rdlc";
            ReportClassRoster.LocalReport.ReportPath = @"../../Reports/rptClassRoster.rdlc";
            var printClassRosterTableAdapter = new up_PrintClassRosterTableAdapter() { ClearBeforeFill = true };
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            printClassRosterTableAdapter.Fill(dsRoster.up_PrintClassRoster, ClassID);
            dsRoster.EndInit();
            ReportClassRoster.RefreshReport();
        }
    }
}
