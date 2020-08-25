using System;
using System.Data;
using System.Windows.Controls;
using StudioAdvanced.Utilities;
using System.Linq;
using StudioAdvanced.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using StudioAdvanced.ViewModels;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for FamilyView.xaml
    /// </summary>
    public partial class FamilyView : UserControl
    {
        private DataTable familyDetails = new DataTable();
        private DataTable studentList = new DataTable();
        private MainWindow mainWindow;
        public FamilyView()
        {
            InitializeComponent();
            List<string> stateList = new List<string>();
            stateList.Add("NY");
            stateList.Add("PA");
            stateList.Add("OH");
            cbxState.ItemsSource = stateList;
        }

        public void UpdateFamilyDetails()
        {
            var fmly = new FamilyModel();
            fmly.FirstName = txtFName.Text;
            fmly.LastName = txtLName.Text;
            fmly.Address = txtAddress.Text;
            fmly.City = txtCity.Text;
            fmly.ZipCode = txtPostal.Text;
            fmly.PrimaryPhone = txtPPhone.Text;
            fmly.SecondaryPhone = txtSPhone.Text;
            fmly.EmailAddress = txtEmail.Text;
            fmly.State = cbxState.Text;
            fmly.FamilyId = Convert.ToInt32(txtFamilyID.Text);

            Data.Instance.UpdateFamily(fmly);
            LoadFamilyGrid();
            LoadFamilyDetails(fmly.FamilyId);
        }

        public void DeleteFamily()
        {
            Data.Instance.DeleteFamily(Convert.ToInt32(txtFamilyID.Text));
            LoadFamilyGrid();
            familyDetails.Clear();
            studentList.Clear();
            txtFName.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtPostal.Text = string.Empty;
            txtPPhone.Text = string.Empty;
            txtSPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cbxState.Text = string.Empty;
            txtFamilyID.Text = string.Empty;

            dgStudents.ItemsSource = studentList.DefaultView;
            IsFamilySelected = false;
        }

        public bool IsFamilySelected { get; set; }

        public void RefreshDetails()
        {
            LoadFamilyDetails(Convert.ToInt32(txtFamilyID.Text));
        }

        public void RefreshGrid()
        {
            LoadFamilyGrid();
        }
        private void LoadFamilyGrid()
        {
            var ds = new DataSet();
            ds = Data.Instance.GetFamilies();
            if (txtFilter.Text != string.Empty && txtFilter.Text != "")
            {
                var filteredRows = from myRow in ds.Tables[0].AsEnumerable()
                    where (myRow.Field<string>("Last Name").ToLower().StartsWith(txtFilter.Text.ToLower()))
                    select myRow;
                dgFamilies.ItemsSource = filteredRows.Any() ? filteredRows.CopyToDataTable().DefaultView : null;
            }
            else
            {
                dgFamilies.ItemsSource = ds.Tables[0].DefaultView;
            }
        }

        private void LoadFamilyDetails(int _familyID)
        {
            familyDetails.Clear();
            familyDetails = Data.Instance.GetFamily(_familyID).Tables[0];
            studentList = Data.Instance.GetFamily(_familyID).Tables[1];

            txtFName.Text = familyDetails.Rows[0]["FName"].ToString();
            txtLName.Text = familyDetails.Rows[0]["LName"].ToString();
            txtAddress.Text = familyDetails.Rows[0]["Address"].ToString();
            txtCity.Text = familyDetails.Rows[0]["City"].ToString();
            txtPostal.Text = familyDetails.Rows[0]["ZipPostal"].ToString();
            txtPPhone.Text = familyDetails.Rows[0]["PrimaryPhone"].ToString();
            txtSPhone.Text = familyDetails.Rows[0]["SecondaryPhone"].ToString();
            txtEmail.Text = familyDetails.Rows[0]["Email"].ToString();
            cbxState.Text = familyDetails.Rows[0]["StateProvince"].ToString();
            txtFamilyID.Text = familyDetails.Rows[0]["FamilyID"].ToString();
            txtFundraising.Text = string.Format("{0:C}", familyDetails.Rows[0]["Fundraising"]);

            dgStudents.ItemsSource = studentList.DefaultView;
            IsFamilySelected = true;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LoadFamilyGrid();
            IsFamilySelected = false;
            mainWindow = (MainWindow)Window.GetWindow(this);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadFamilyGrid();
        }

        private void dgFamilies_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var drv = (DataRowView) dgFamilies.SelectedItem;
            var _familyID = Convert.ToInt32(drv["ID"]);
            LoadFamilyDetails(_familyID);
        }

        private void dgStudents_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                    (e.Column as DataGridTextColumn).Binding.StringFormat = "d";
        }

        private void dgStudents_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgStudents.SelectedItem == null) return;
            DataRowView drv = dgStudents.SelectedItem as DataRowView;
            DataRow row = drv.Row;
            var _studentID = Convert.ToInt32(row["StudentID"]);
            //var _dataContext = (MainWindowViewModel)DataContext;
            var _parent = Application.Current.MainWindow as MainWindow;
            _parent.exStudent.IsExpanded = true;
            var _student = _parent.cntCurrentView.Content as StudentView;
            _student.RefreshStudentDetails(_studentID);
        }
    }
}
