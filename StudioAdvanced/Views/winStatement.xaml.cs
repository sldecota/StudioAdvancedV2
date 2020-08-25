using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winStatement.xaml
    /// </summary>
    public partial class winStatement : Window
    {
        private DataTable familyNames;
        IList<int> selectedRow = new List<int>();
        public winStatement()
        {
            InitializeComponent();
            familyNames = Data.Instance.GetFamilyNames().Tables[0];
            cbxFamilies.ItemsSource = familyNames.DefaultView;
            cbxFamilies.DisplayMemberPath = "Last Name";
            cbxFamilies.SelectedValuePath = "ID";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winPrintStatement();
            frm.FamilyID = selectedRow[0];
            frm.ShowDialog();
            Close();
        }

        private void cbxFamilies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbx = (ComboBox) sender;
            if (cbx.SelectedItem != null)
            {
                selectedRow.Add((int)((DataRowView)cbx.SelectedItem).Row.ItemArray[0]);
            }
        }
    }
}
