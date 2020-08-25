using StudioAdvanced.Utilities;
using System;
using System.Data;
using System.Windows;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winCharge.xaml
    /// </summary>
    public partial class winCharge : Window
    {
        private DataTable families;
        private DataTable transTypes;
        public winCharge()
        {
            InitializeComponent();
        }
        public int FamilyID { get; set; }
        public string LastName { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBoxes();
            if (FamilyID < 1) return;
            cbxFamilies.SelectedValue = FamilyID;
        }

        private void FillComboBoxes()
        {
            families = Data.Instance.GetFamilyNames().Tables[0];
            transTypes = Data.Instance.GetTransTypes().Tables[0];
            cbxFamilies.ItemsSource = families.DefaultView;
            cbxFamilies.DisplayMemberPath = "Last Name";
            cbxFamilies.SelectedValuePath = "ID";
            cbxTransTypes.ItemsSource = transTypes.DefaultView;
            cbxTransTypes.DisplayMemberPath = "Description";
            cbxTransTypes.SelectedValuePath = "TransTypeID";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cbxFamilies.Text) || string.IsNullOrEmpty(cbxTransTypes.Text) ||
                string.IsNullOrEmpty(txtAmountDue.Text))
            {
                MessageBox.Show("Please confirm all data has been entered!", "Missing Information", MessageBoxButton.OK);
            }
            else
            {
                Data.Instance.ApplyCharge(Convert.ToInt32(cbxFamilies.SelectedValue),
                     Convert.ToInt32(cbxTransTypes.SelectedValue), Convert.ToDecimal(txtAmountDue.Text), txtComments.Text);
            }
            Close();
        }
    }
}
