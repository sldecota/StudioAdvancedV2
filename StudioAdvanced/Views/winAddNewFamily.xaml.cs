using System;
using System.Collections.Generic;
using System.Windows;
using StudioAdvanced.Models;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winAddNewFamily.xaml
    /// </summary>
    public partial class winAddNewFamily : Window
    {
        public winAddNewFamily()
        {
            InitializeComponent();
            List<string> stateList = new List<string>();
            stateList.Add("NY");
            stateList.Add("PA");
            stateList.Add("OH");
            cmbState.ItemsSource = stateList;
            txtFirstName.Focus();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var fmly = new FamilyModel();
            fmly.FirstName = txtFirstName.Text;
            fmly.LastName = txtLastName.Text;
            fmly.Address = txtAddress.Text;
            fmly.City = txtCity.Text;
            fmly.State = cmbState.Text;
            fmly.ZipCode = txtPostal.Text;
            fmly.PrimaryPhone = txtPrimaryPhone.Text;
            fmly.SecondaryPhone = txtSecondaryPhone.Text;
            fmly.EmailAddress = txtEmail.Text;

            try
            {
                Data.Instance.AddFamily(fmly);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            DialogResult = true;

            Close();
        }
    }
}
