using System;
using System.Windows;
using StudioAdvanced.Utilities;
using System.Data;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winDeductFundraising.xaml
    /// </summary>
    public partial class winDeductFundraising : Window
    {
        private DataTable families;
        public winDeductFundraising()
        {
            InitializeComponent();
        }
        public int FamilyID { get; set; }
        public string LastName { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            families = Data.Instance.GetFamilyNames().Tables[0];
            cbxFamilies.ItemsSource = families.DefaultView;
            cbxFamilies.DisplayMemberPath = "Last Name";
            cbxFamilies.SelectedValuePath = "ID";

            if (FamilyID < 1) return;
            cbxFamilies.SelectedValue = FamilyID;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cbxFamilies.Text) || string.IsNullOrEmpty(txtFundraisingAmount.Text) ||
                string.IsNullOrEmpty(txtComments.Text))
            {
                MessageBox.Show("Please confirm all data has been entered!", "Missing Information", MessageBoxButton.OK);
            }
            else
            {
                Data.Instance.AddFundraising(Convert.ToInt32(cbxFamilies.SelectedValue),
                    Convert.ToDecimal(txtFundraisingAmount.Text) * -1, txtComments.Text);
                Close();
            }
        }
    }
}
