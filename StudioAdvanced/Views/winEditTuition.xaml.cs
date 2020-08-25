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
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winEditTuition.xaml
    /// </summary>
    public partial class winEditTuition : Window
    {
        private DataTable familyNames;
        IList<int> selectedRow = new List<int>();
        public winEditTuition()
        {
            InitializeComponent();
            familyNames = Data.Instance.GetFamilyNames().Tables[0];
            cbxFamily.ItemsSource = familyNames.DefaultView;
            cbxFamily.DisplayMemberPath = "Last Name";
            cbxFamily.SelectedValuePath = "ID";
            FamilyID = 0;
        }

        public int FamilyID { get; set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Data.Instance.UpdateTuition(Convert.ToInt32(cbxFamily.SelectedValue),
                Convert.ToDecimal(txtTuitionAmount.Text));
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (FamilyID > 0)
            {
                cbxFamily.SelectedValue = FamilyID;
                txtTuitionAmount.Text = Data.Instance.GetTuition(FamilyID);
            }
        }

        private void cbxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if (cbx.SelectedItem != null)
            {
                selectedRow.Add((int)((DataRowView)cbx.SelectedItem).Row.ItemArray[0]);
            }
            txtTuitionAmount.Text = Data.Instance.GetTuition(selectedRow[0]);
        }
    }
}
