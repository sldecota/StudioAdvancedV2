using System;
using System.Collections.Generic;
using System.Windows;
using StudioAdvanced.Models;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winAddNewClass.xaml
    /// </summary>
    public partial class winAddNewClass : Window
    {
        private List<string> days;
        public winAddNewClass()
        {
            InitializeComponent();
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
            cbxClassDay.ItemsSource = Data.Instance.GetWeekDays().Tables[0].DefaultView;
            cbxClassDay.DisplayMemberPath = "WeekDayName";
            cbxClassDay.SelectedValuePath = "WeekDayID";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBoxes();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var _class = new ClassModel();
            _class.ClassName = txtClassName.Text;
            _class.RoutineName = txtRoutine.Text;
            _class.ClassTime = txtClassTime.Text;
            _class.DayOfWeek = (int)cbxClassDay.SelectedValue;
            _class.TeacherId = Convert.ToInt32(cbxTeacher.SelectedValue);
            _class.ClassroomId = Convert.ToInt32(cbxClassroom.SelectedValue);
            _class.ClassLevelID = Convert.ToInt32(cbxClassLevel.SelectedValue);
            DialogResult = Data.Instance.AddClass(_class);
            Close();
        }
    }
}
