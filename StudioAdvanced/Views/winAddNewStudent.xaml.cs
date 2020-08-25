using System;
using System.Windows;
using StudioAdvanced.Models;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winAddNewStudent.xaml
    /// </summary>
    public partial class winAddNewStudent : Window
    {
        public winAddNewStudent()
        {
            InitializeComponent();
            txtFirstName.Focus();
        }
        public int FamilyID { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var stud = new StudentModel();
            stud.FamilyId = FamilyID;
            stud.FirstName = txtFirstName.Text;
            stud.LastName = txtLastName.Text;
            if (dtpBirthday.SelectedDate != null) stud.Birthday = (DateTime) dtpBirthday.SelectedDate;
            if (chbCompetition.IsChecked != null) stud.Competition = (bool)chbCompetition.IsChecked;
            if (chbAssistant.IsChecked != null) stud.IsAssistant = (bool) chbAssistant.IsChecked;
            if (cbxMBDiscount.IsChecked != null) stud.MBDiscount = (bool) cbxMBDiscount.IsChecked;
            stud.Notes = txtNotes.Text;
            DialogResult = Data.Instance.AddStudent(stud);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
