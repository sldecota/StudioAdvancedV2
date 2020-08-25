using StudioAdvanced.Utilities;
using System;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using StudioAdvanced.Models;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        private DataTable studentDetails = new DataTable();
        private DataTable classList = new DataTable();
        public StudentView()
        {
            InitializeComponent();
        }
        public static int GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        public bool IsStudentSelected { get; set; }

        public void UpdateStudentDetails()
        {
            var stud = new StudentModel();

            stud.StudentId = Convert.ToInt32(txtStudentID.Text);
            stud.FirstName = txtFName.Text;
            stud.LastName = txtLName.Text;
            stud.Notes = txtNotes.Text;
            if (dtpBirthday.SelectedDate != null) stud.Birthday = (DateTime) dtpBirthday.SelectedDate;
            if (cbxCompetition.IsChecked != null) stud.Competition = (bool)cbxCompetition.IsChecked;
            if (cbxAssist.IsChecked != null) stud.IsAssistant = (bool)cbxAssist.IsChecked;
            if (cbxMBDiscount.IsChecked != null) stud.MBDiscount = (bool) cbxMBDiscount.IsChecked;
            stud.AssistantCredit = Convert.ToInt32(txtCredit.Text);

            Data.Instance.UpdateStudent(stud);
            LoadStudentGrid();
            LoadStudentDetails(stud.StudentId);
        }

        public void DeleteStudent()
        {
            Data.Instance.DeleteStudent(Convert.ToInt32(txtStudentID.Text));
            LoadStudentGrid();
            studentDetails.Clear();
            classList.Clear();

            txtFamilyID.Clear();
            txtStudentID.Clear();
            txtAge.Clear();
            txtFName.Clear();
            txtLName.Clear();
            txtFilter.Clear();
            txtNotes.Clear();
            txtCredit.Clear();
            cbxCompetition.IsChecked = false;
            cbxAssist.IsChecked = false;
            cbxMBDiscount.IsChecked = false;
            dtpBirthday.SelectedDate = null;
            dtpBirthday.DisplayDate = DateTime.Today;

            dgClasses.ItemsSource = classList.DefaultView;
            IsStudentSelected = false;
        }

        private void LoadStudentGrid()
        {
            var ds = new DataSet();
            ds = Data.Instance.GetStudents();
            if (txtFilter.Text != string.Empty && txtFilter.Text != "")
            {
                var filteredRows = from myRow in ds.Tables[0].AsEnumerable()
                    where (myRow.Field<string>("Last Name").ToLower().StartsWith(txtFilter.Text.ToLower()))
                    select myRow;
                dgStudents.ItemsSource = filteredRows.Any() ? filteredRows.CopyToDataTable().DefaultView : null;
            }
            else
            {
                dgStudents.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void LoadStudentDetails(int studentId)
        {
            studentDetails.Clear();
            classList.Clear();
            studentDetails = Data.Instance.GetStudent(studentId).Tables[0];
            classList = Data.Instance.GetStudent(studentId).Tables[1];

            txtStudentID.Text = studentDetails.Rows[0]["StudentID"].ToString();
            txtFName.Text = studentDetails.Rows[0]["First Name"].ToString();
            txtLName.Text = studentDetails.Rows[0]["Last Name"].ToString();
            txtParentName.Text = studentDetails.Rows[0]["Parent Name"].ToString();
            txtNotes.Text = studentDetails.Rows[0]["Notes"].ToString();
            dtpBirthday.SelectedDate = (DateTime) studentDetails.Rows[0]["Birthday"];
            txtAge.Text = GetAge((DateTime)studentDetails.Rows[0]["Birthday"]).ToString();
            txtFamilyID.Text = studentDetails.Rows[0]["FamilyID"].ToString();
            cbxCompetition.IsChecked = (bool) studentDetails.Rows[0]["Competition"];
            cbxMBDiscount.IsChecked = (bool)studentDetails.Rows[0]["MBDiscount"];
            cbxAssist.IsChecked = (bool) studentDetails.Rows[0]["Assistant"];
            txtCredit.Text = studentDetails.Rows[0]["AssistantCredit"].ToString();

            dgClasses.ItemsSource = classList.DefaultView;

            IsStudentSelected = true;
        }

        public void RefreshStudentDetails(int studentId)
        {
            studentDetails.Clear();
            classList.Clear();
            studentDetails = Data.Instance.GetStudent(studentId).Tables[0];
            classList = Data.Instance.GetStudent(studentId).Tables[1];

            txtStudentID.Text = studentDetails.Rows[0]["StudentID"].ToString();
            txtFName.Text = studentDetails.Rows[0]["First Name"].ToString();
            txtLName.Text = studentDetails.Rows[0]["Last Name"].ToString();
            txtParentName.Text = studentDetails.Rows[0]["Parent Name"].ToString();
            txtNotes.Text = studentDetails.Rows[0]["Notes"].ToString();
            dtpBirthday.SelectedDate = (DateTime)studentDetails.Rows[0]["Birthday"];
            txtAge.Text = GetAge((DateTime)studentDetails.Rows[0]["Birthday"]).ToString();
            txtFamilyID.Text = studentDetails.Rows[0]["FamilyID"].ToString();
            cbxCompetition.IsChecked = (bool)studentDetails.Rows[0]["Competition"];
            cbxMBDiscount.IsChecked = (bool) studentDetails.Rows[0]["MBDiscount"];
            cbxAssist.IsChecked = (bool)studentDetails.Rows[0]["Assistant"];
            txtCredit.Text = studentDetails.Rows[0]["AssistantCredit"].ToString();

            dgClasses.ItemsSource = classList.DefaultView;

            IsStudentSelected = true;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadStudentGrid();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadStudentGrid();
            IsStudentSelected = false;
        }

        private void dgStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "d";
        }

        private void dgStudents_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var drv = (DataRowView) dgStudents.SelectedItem;
            var _studentId = Convert.ToInt32(drv.Row["StudentID"]);
            LoadStudentDetails(_studentId);
        }
    }
}
