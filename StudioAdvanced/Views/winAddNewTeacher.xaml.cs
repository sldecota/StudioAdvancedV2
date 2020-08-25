using System;
using System.Collections.Generic;
using System.Windows;
using StudioAdvanced.Models;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winAddNewTeacher.xaml
    /// </summary>
    public partial class winAddNewTeacher : Window
    {
        public winAddNewTeacher()
        {
            InitializeComponent();
            List<string> stateList = new List<string>();
            stateList.Add("NY");
            stateList.Add("PA");
            stateList.Add("OH");
            cmbState.ItemsSource = stateList;
            txtFirstName.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var teach = new TeacherModel();
            teach.FirstName = txtFirstName.Text;
            teach.LastName = txtLastName.Text;
            teach.Address = txtAddress.Text;
            teach.City = txtCity.Text;
            teach.State = cmbState.Text;
            teach.Postal = txtPostal.Text;
            teach.PrimaryPhone = txtPrimaryPhone.Text;
            teach.SecondaryPhone = txtSecondaryPhone.Text;
            teach.Email = txtEmail.Text;
            teach.PayRate = txtPayRate.Text != string.Empty ? Convert.ToDecimal(txtPayRate.Text) : 0;
            if (dtpBirthday.SelectedDate != null) teach.Birthday = (DateTime) dtpBirthday.SelectedDate;

            DialogResult = Data.Instance.AddTeacher(teach);

            Close();
        }
    }
}
