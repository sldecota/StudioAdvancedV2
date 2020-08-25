using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Expression.Prototyping.Data;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winEditStatement.xaml
    /// </summary>
    public partial class winEditStatement : Window
    {
        public winEditStatement()
        {
            InitializeComponent();
        }
        private DataTable families;
        private DataTable transTypes;
        private DataTable accountSummary;
        
        public string LastName { get; set; }
        public int FamilyID { get; set; }

        private void FillComboBoxes()
        {
            families = Data.Instance.GetFamilyNames().Tables[0];
            transTypes = Data.Instance.GetTransTypes().Tables[0];
            accountSummary = Data.Instance.GetAccountSummary(FamilyID).Tables[0];
            cbxFamilies.ItemsSource = families.DefaultView;
            cbxFamilies.DisplayMemberPath = "Last Name";
            cbxFamilies.SelectedValuePath = "ID";
            cbxTransTypes.ItemsSource = transTypes.DefaultView;
            cbxTransTypes.DisplayMemberPath = "Description";
            cbxTransTypes.SelectedValuePath = "ID";
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
            if (string.IsNullOrEmpty(txtAmountCharged.Text))
            {
                MessageBox.Show("Please confirm all data has been entered!", "Missing Information", MessageBoxButton.OK);
            }
            else
            {
                Data.Instance.UpdateAccountCharge(Convert.ToInt32(txtTransID.Text),
                    Convert.ToDecimal(txtAmountCharged.Text.Substring(1, txtAmountCharged.Text.Length - 1)), txtComments.Text);
                MessageBox.Show("Changes Successfully Saved", "Changes Saved", MessageBoxButton.OK);
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

        private void DgAccountSummary_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dr = (DataRowView)e.AddedItems[0];
            txtTransID.Text = dr.Row["TransactionID"].ToString();
            txtAmountCharged.Text = string.Format("{0:C}", dr.Row["AmountDue"]);
            txtComments.Text = dr.Row["Comments"].ToString();
            cbxTransTypes.SelectedIndex = (int)dr.Row["TransTypeID"]-1;
            dtpTransDate.SelectedDate = (DateTime) dr.Row["DueDate"];
        }
    }
}
