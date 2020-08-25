using StudioAdvanced.Utilities;
using System;
using System.Data;
using System.Windows;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winCredit.xaml
    /// </summary>
    public partial class winCredit : Window
    {
        private DataTable families;
        public winCredit()
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
            if (string.IsNullOrEmpty(cbxFamilies.Text) || string.IsNullOrEmpty(txtCreditAmount.Text) ||
                string.IsNullOrEmpty(txtComments.Text))
            {
                MessageBox.Show("Please confirm all data has been entered!", "Missing Information", MessageBoxButton.OK);
            }
            else
            {
                Data.Instance.ApplyCredit(Convert.ToInt32(cbxFamilies.SelectedValue),
                    Convert.ToDecimal(txtCreditAmount.Text), txtComments.Text);
                Close();
            }
        }
    }
}
