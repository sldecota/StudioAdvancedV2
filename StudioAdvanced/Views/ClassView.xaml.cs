using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StudioAdvanced.Models;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for ClassView.xaml
    /// </summary>
    public partial class ClassView : UserControl
    {
        DataTable classDetails = new DataTable();
        DataTable classRoster = new DataTable();
        private readonly List<string> days;

        public ClassView()
        {
            InitializeComponent();
        }

        public void RefreshClassGrid()
        {
            LoadClassGrid();
        }

        public bool IsClassSelected { get; set; }
        public bool IsStudentSelected { get; set; }

        public int ClassID { get; set; }
        private int StudentID { get; set; }

        public void DeleteClass()
        {
            Data.Instance.DeleteClass(ClassID);
            classDetails.Clear();
            classRoster.Clear();
            txtClassName.Clear();
            txtClassTime.Clear();
            txtRoutineName.Clear();
            cbxClassLevel.Text = "";
            cbxTeacher.Text = "";
            cbxClassroom.Text = "";
            cbxDay.Text = "";
            IsClassSelected = false;
        }

        public void UpdateClassDetails()
        {
            var _class = new ClassModel();
            _class.ClassId = ClassID;
            _class.ClassLevelID = Convert.ToInt32(cbxClassLevel.SelectedValue);
            _class.ClassName = txtClassName.Text;
            _class.RoutineName = txtRoutineName.Text;
            _class.ClassTime = txtClassTime.Text;
            _class.ClassroomId = Convert.ToInt32(cbxClassroom.SelectedValue);
            _class.TeacherId = Convert.ToInt32(cbxTeacher.SelectedValue);
            _class.DayOfWeek = (int)cbxDay.SelectedValue;

            Data.Instance.UpdateClass(_class);
            LoadClassGrid();
            LoadClassDetails(_class.ClassId);
        }

        public void RemoveStudent()
        {
            Data.Instance.DeleteRegistration(StudentID, ClassID);
            LoadClassDetails(ClassID);
        }

        private void LoadClassGrid()
        {
            var ds = new DataSet();
            ds = Data.Instance.GetAllClasses();
            if (txtFilter.Text != string.Empty && txtFilter.Text != "")
            {
                var filteredRows = from myRow in ds.Tables[0].AsEnumerable()
                    where (myRow.Field<string>("Last Name").ToLower().StartsWith(txtFilter.Text.ToLower()))
                    select myRow;
                dgClasses.ItemsSource = filteredRows.Any() ? filteredRows.CopyToDataTable().DefaultView : null;
            }
            else
            {
                dgClasses.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void FillComboBoxes()
        {
            cbxClassLevel.ItemsSource = Data.Instance.GetClassLevels().Tables[0].DefaultView;
            cbxClassLevel.DisplayMemberPath = "Description";
            cbxClassLevel.SelectedValuePath = "ClassLevelID";
            cbxTeacher.ItemsSource = Data.Instance.GetTeacherNames().Tables[0].DefaultView;
            cbxTeacher.DisplayMemberPath = "Name";
            cbxTeacher.SelectedValuePath = "ID";
            cbxClassroom.ItemsSource = Data.Instance.GetClassrooms().Tables[0].DefaultView;
            cbxClassroom.DisplayMemberPath = "Description";
            cbxClassroom.SelectedValuePath = "ClassroomID";
            cbxDay.ItemsSource = Data.Instance.GetWeekDays().Tables[0].DefaultView;
            cbxDay.DisplayMemberPath = "WeekDayName";
            cbxDay.SelectedValuePath = "WeekDayID";
        }

        private void LoadClassDetails(int classId)
        {
            classDetails.Clear();
            classRoster.Clear();
            classDetails = Data.Instance.GetClass(classId).Tables[0];
            classRoster = Data.Instance.GetClass(classId).Tables[1];

            ClassID = Convert.ToInt32(classDetails.Rows[0]["ID"]);
            txtClassName.Text = classDetails.Rows[0]["ClassName"].ToString();
            txtRoutineName.Text = classDetails.Rows[0]["ClassRoutine"].ToString();
            txtClassTime.Text = classDetails.Rows[0]["ClassTime"].ToString();
            cbxTeacher.Text = classDetails.Rows[0]["Teacher"].ToString();
            cbxDay.SelectedValue = (int)classDetails.Rows[0]["ClassDay"];
            cbxClassLevel.SelectedValue = (int)classDetails.Rows[0]["ClassLevelID"];
            cbxClassroom.SelectedValue = (int)classDetails.Rows[0]["ClassroomID"];

            dgRoster.ItemsSource = classRoster.DefaultView;
            IsClassSelected = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadClassGrid();
            FillComboBoxes();
            IsClassSelected = false;
        }

        private void dgClasses_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var drv = (DataRowView)dgClasses.SelectedItem;
            var _classId = Convert.ToInt32(drv["ID"]);
            LoadClassDetails(_classId);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadClassGrid();
        }

        private void dgRoster_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsStudentSelected = true;
            var drv = (DataRowView)dgRoster.SelectedItem;
            StudentID = Convert.ToInt32(drv.Row["StudentID"]);
        }
    }
}
