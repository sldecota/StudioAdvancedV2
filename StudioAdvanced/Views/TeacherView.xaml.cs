using StudioAdvanced.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StudioAdvanced.Models;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for TeacherView.xaml
    /// </summary>
    public partial class TeacherView : UserControl
    {
        private DataTable teacherDetails = new DataTable();
        private DataTable classList = new DataTable();
        public TeacherView()
        {
            InitializeComponent();
            List<string> stateList = new List<string>();
            stateList.Add("NY");
            stateList.Add("PA");
            stateList.Add("OH");
            cbxState.ItemsSource = stateList;
        }
        public bool IsTeacherSelected { get; set; }

        public void RefreshTeacherGrid()
        {
            LoadTeacherGrid();
        }

        public void UpdateTeacherDetails()
        {
            var teach = new TeacherModel();
            teach.TeacherId = Convert.ToInt32(txtTeacherID.Text);
            teach.FirstName = txtFName.Text;
            teach.LastName = txtLName.Text;
            teach.Address = txtAddress.Text;
            teach.City = txtCity.Text;
            teach.State = cbxState.Text;
            teach.Postal = txtPostal.Text;
            teach.Email = txtEmail.Text;
            if (dtpBirthday.SelectedDate != null) teach.Birthday = (DateTime) dtpBirthday.SelectedDate;
            teach.PayRate = Convert.ToDecimal(txtPayRate.Text.Substring(1));
            teach.PrimaryPhone = txtPPhone.Text;
            teach.SecondaryPhone = txtSPhone.Text;

            Data.Instance.UpdateTeacher(teach);
            LoadTeacherGrid();
            LoadTeacherDetails(teach.TeacherId);
        }

        public void DeleteTeacher()
        {
            Data.Instance.DeleteTeacher(Convert.ToInt32(txtTeacherID.Text));
            LoadTeacherGrid();
            teacherDetails.Clear();
            classList.Clear();
            txtFName.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtPostal.Text = string.Empty;
            txtPPhone.Text = string.Empty;
            txtSPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cbxState.Text = string.Empty;
            txtTeacherID.Text = string.Empty;
            txtPayRate.Clear();

            dgClasses.ItemsSource = classList.DefaultView;
            IsTeacherSelected = false;
        }

        private void LoadTeacherGrid()
        {
            var ds = new DataSet();
            ds = Data.Instance.GetAllTeachers();
            if (txtFilter.Text != string.Empty && txtFilter.Text != "")
            {
                var filteredRows = from myRow in ds.Tables[0].AsEnumerable()
                    where (myRow.Field<string>("Last Name").ToLower().StartsWith(txtFilter.Text.ToLower()))
                    select myRow;
                dgTeachers.ItemsSource = filteredRows.Any() ? filteredRows.CopyToDataTable().DefaultView : null;
            }
            else
            {
                dgTeachers.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void LoadTeacherDetails(int _teacherId)
        {
            teacherDetails.Clear();
            classList.Clear();
            teacherDetails = Data.Instance.GetTeacher(_teacherId).Tables[0];
            classList = Data.Instance.GetTeacher(_teacherId).Tables[1];

            var _payRate = Convert.ToDecimal(teacherDetails.Rows[0]["PayRate"]);

            txtFName.Text = teacherDetails.Rows[0]["First Name"].ToString();
            txtLName.Text = teacherDetails.Rows[0]["Last Name"].ToString();
            txtAddress.Text = teacherDetails.Rows[0]["Address"].ToString();
            txtCity.Text = teacherDetails.Rows[0]["City"].ToString();
            cbxState.Text = teacherDetails.Rows[0]["StateProvince"].ToString();
            txtPostal.Text = teacherDetails.Rows[0]["Postal"].ToString();
            txtPPhone.Text = teacherDetails.Rows[0]["PrimaryPhone"].ToString();
            txtSPhone.Text = teacherDetails.Rows[0]["SecondaryPhone"].ToString();
            txtEmail.Text = teacherDetails.Rows[0]["Email"].ToString();
            txtTeacherID.Text = teacherDetails.Rows[0]["ID"].ToString();
            txtPayRate.Text = string.Format(new CultureInfo("en-US"), "{0:C}", _payRate);
            dtpBirthday.SelectedDate = (DateTime) teacherDetails.Rows[0]["Birthday"];

            dgClasses.ItemsSource = classList.DefaultView;

            IsTeacherSelected = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTeacherGrid();
            IsTeacherSelected = false;
        }

        private void dgTeachers_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var drv = (DataRowView) dgTeachers.SelectedItem;
            var _teacherId = Convert.ToInt32(drv["ID"]);
            LoadTeacherDetails(_teacherId);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadTeacherGrid();
        }
    }
}
