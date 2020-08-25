using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPayment.xaml
    /// </summary>
    public partial class winPayment : Window
    {
        private DataTable families;
        private DataTable payTypes;
        private DataTable accountSummary;
        public winPayment()
        {
            InitializeComponent();
        }
        public string LastName { get; set; }
        public int FamilyID { get; set; }

        private void FillComboBoxes()
        {
            families = Data.Instance.GetFamilyNames().Tables[0];
            payTypes = Data.Instance.GetPayTypes().Tables[0];
            accountSummary = Data.Instance.PrintStatement(FamilyID).Tables[0];
            cbxFamilies.ItemsSource = families.DefaultView;
            cbxFamilies.DisplayMemberPath = "Last Name";
            cbxFamilies.SelectedValuePath = "ID";
            cbxPayTypes.ItemsSource = payTypes.DefaultView;
            cbxPayTypes.DisplayMemberPath = "Description";
            cbxPayTypes.SelectedValuePath = "ID";
            dgAccountSummary.ItemsSource = accountSummary.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            FillComboBoxes();
            if (FamilyID < 1) return;
            cbxFamilies.SelectedValue = FamilyID;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cbxFamilies.Text) || string.IsNullOrEmpty(cbxPayTypes.Text) ||
                string.IsNullOrEmpty(txtAmountPaid.Text))
            {
                MessageBox.Show("Please confirm all data has been entered!", "Missing Information", MessageBoxButton.OK);
            }
            else
            {
                Data.Instance.ApplyPayment(Convert.ToInt32(cbxFamilies.SelectedValue),
                    Convert.ToDecimal(txtAmountPaid.Text), Convert.ToInt32(cbxPayTypes.SelectedValue), txtComments.Text, dtpPaymentDate.DisplayDate);
                var frm = new winReceipt();
                frm.FamilyID = FamilyID;
                frm.ShowDialog();
                Close();
            }
        }

        private void dgAccountSummary_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "d";
            if (e.PropertyType == typeof(System.Decimal))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "c2";
        }
    }
}
