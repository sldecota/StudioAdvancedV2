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

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winAddFundraising.xaml
    /// </summary>
    public partial class winAddFundraising : Window
    {
        private DataTable families;

        public winAddFundraising()
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
                    Convert.ToDecimal(txtFundraisingAmount.Text), txtComments.Text);
                Close();
            }
        }
    }
}
