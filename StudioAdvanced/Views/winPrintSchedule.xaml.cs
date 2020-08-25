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
using Microsoft.Reporting.WinForms;
using StudioAdvanced.Utilities;
using StudioAdvanced.PrintStudentScheduleDataSetTableAdapters;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPrintSchedule.xaml
    /// </summary>
    public partial class winPrintSchedule : Window
    {
        public winPrintSchedule()
        {
            InitializeComponent();
        }

        public int StudentID { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reportData = new ReportDataSource();
            var dsSchedule = new PrintStudentScheduleDataSet();
            dsSchedule.BeginInit();
            dsSchedule.EnforceConstraints = false;
            reportData.Name = "dsSchedule";
            reportData.Value = dsSchedule.up_PrintStudentSchedule;
            ReportStudentSchedule.ProcessingMode = ProcessingMode.Local;
            ReportStudentSchedule.LocalReport.DataSources.Add(reportData);
            ReportStudentSchedule.LocalReport.ReportEmbeddedResource = "StudioAdvanced.rptStudentSchedule.rdlc";
            ReportStudentSchedule.LocalReport.ReportPath = @"../../Reports/rptStudentSchedule.rdlc";
            var printStudentScheduleTableAdapter = new up_PrintStudentScheduleTableAdapter() { ClearBeforeFill = true };
            if (Data.Instance.Connection.State == ConnectionState.Open)
            {
                Data.Instance.Connection.Close();
            }
            printStudentScheduleTableAdapter.Fill(dsSchedule.up_PrintStudentSchedule, StudentID);
            dsSchedule.EndInit();
            ReportStudentSchedule.RefreshReport();
        }
    }
}
