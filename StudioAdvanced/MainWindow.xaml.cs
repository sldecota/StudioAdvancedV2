using System;
using StudioAdvanced.ViewModels;
using System.Windows;
using System.Windows.Input;
using StudioAdvanced.Models;
using StudioAdvanced.Utilities;
using StudioAdvanced.Views;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace StudioAdvanced
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random _random = new Random();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            AddHotKeys();
        }

        private void AddHotKeys()
        {
            RoutedCommand paymentCommand = new RoutedCommand();
            paymentCommand.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(paymentCommand, btnPayment_Click));

            RoutedCommand statementCommand = new RoutedCommand();
            statementCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(statementCommand, btnPrintStatement_Click));
        }

        private void ExitApplication_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPrintStatement_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                if (!uc.IsStudentSelected) return;
                var printForm = new winPrintStatement();
                printForm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                printForm.ShowDialog();
            }
            else if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                if (!uc.IsFamilySelected) return;
                var printForm = new winPrintStatement();
                printForm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                printForm.ShowDialog();
            }
            else
            {
                var frm = new winStatement();
                frm.ShowDialog();
            }
        }

        private void btnPrintAccountDetails_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                if (!uc.IsStudentSelected) return;
                var printForm = new winPrintAccountDetails();
                printForm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                printForm.ShowDialog();
            }
            else if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                if (!uc.IsFamilySelected) return;
                var printForm = new winPrintAccountDetails();
                printForm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                printForm.ShowDialog();
            }
        }

        private void btnPrintFundraisingStatement_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                if (!uc.IsStudentSelected) return;
                var printForm = new winPrintFundraisingStatement();
                printForm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                printForm.ShowDialog();
            }
            else if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                if (!uc.IsFamilySelected) return;
                var printForm = new winPrintFundraisingStatement();
                printForm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                printForm.ShowDialog();
            }
            //else
            //{
            //    var frm = new winStatement();
            //    frm.ShowDialog();
            //}
        }

        private void btnAddFamily_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winAddNewFamily();
            frm.ShowDialog();
            if (!frm.DialogResult.HasValue || !frm.DialogResult.Value) return;
            var uc = cntCurrentView.Content as FamilyView;
            uc.RefreshGrid();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winRegistration();
            var uc = cntCurrentView.Content as StudentView;
            if (!uc.IsStudentSelected) return;
            frm.StudentID = Convert.ToInt32(uc.txtStudentID.Text);
            frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
            frm.FirstName = uc.txtFName.Text;
            frm.LastName = uc.txtLName.Text;
            frm.ShowDialog();
            if (!frm.DialogResult.HasValue || !frm.DialogResult.Value) return;
            uc.RefreshStudentDetails(Convert.ToInt32(uc.txtStudentID.Text));
        }

        private void btnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winAddNewTeacher();
            var uc = cntCurrentView.Content as TeacherView;
            frm.ShowDialog();
            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {
                uc.RefreshTeacherGrid();
            }
        }

        private void btnAddClass_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winAddNewClass();
            var uc = cntCurrentView.Content as ClassView;
            frm.ShowDialog();
            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {
               uc.RefreshClassGrid();
            }
        }

        private void btnSaveFamily_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as FamilyView;
            if (!uc.IsFamilySelected) return;
            if (MessageBox.Show("Update Family Information?", "Confirm Save", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.UpdateFamilyDetails();
        }

        private void btnDeleteFamily_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as FamilyView;
            if (!uc.IsFamilySelected) return;
            if (MessageBox.Show("Delete Family?", "Confirm Delete", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.DeleteFamily();
        }

        private void btnAddStudentFmly_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winAddNewStudent();
            var uc = cntCurrentView.Content as FamilyView;
            if (!uc.IsFamilySelected) return;
            frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
            frm.ShowDialog();
            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {
                uc.RefreshDetails();
            }
        }

        private void btnSaveStudent_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as StudentView;
            if (!uc.IsStudentSelected) return;
            if (MessageBox.Show("Update Student Information?", "Confirm Save", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.UpdateStudentDetails();
        }

        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as StudentView;
            if (!uc.IsStudentSelected) return;
            if (MessageBox.Show("Delete Student?", "Confirm Delete", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.DeleteStudent();
        }

        private void btnSaveTeacher_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as TeacherView;
            if (!uc.IsTeacherSelected) return;
            if (MessageBox.Show("Update Teacher Information?", "Confirm Save", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.UpdateTeacherDetails();
        }

        private void btnSaveClass_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as ClassView;
            if (!uc.IsClassSelected) return;
            if (MessageBox.Show("Update Class Details?", "Confirm Save", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.UpdateClassDetails();
        }

        private void btnDeleteClass_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as ClassView;
            if (!uc.IsClassSelected) return;
            if (MessageBox.Show("Delete selected class?", "Confirm Delete", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.DeleteClass();
        }

        private void btnRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            var uc = cntCurrentView.Content as ClassView;
            if (!uc.IsClassSelected && !uc.IsStudentSelected) return;
            if (MessageBox.Show("Remove the selected student from this class?", "Confirm Removal", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            uc.RemoveStudent();
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                var frm = new winPayment();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.LastName = uc.txtLName.Text;
                frm.ShowDialog();
            }
            else if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                var frm = new winPayment();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.ShowDialog();
            }
            else
            {
                var frm = new winPayment();
                frm.ShowDialog();
            }
        }

        private void btnApplyCredit_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                var frm = new winCredit();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.LastName = uc.txtLName.Text;
                frm.ShowDialog();
            }
            else if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                var frm = new winCredit();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.ShowDialog();
            }
            else
            {
                var frm = new winCredit();
                frm.ShowDialog();
            }
        }

        private void btnAddFundraising_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                var frm = new winAddFundraising();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.LastName = uc.txtLName.Text;
                frm.ShowDialog();
            }
            else if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                var frm = new winAddFundraising();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.ShowDialog();
            }
            else
            {
                var frm = new winAddFundraising();
                frm.ShowDialog();
            }
        }

        private void btnDeductFundraising_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                var frm = new winDeductFundraising();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.LastName = uc.txtLName.Text;
                frm.ShowDialog();
            }
            else if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                var frm = new winDeductFundraising();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.ShowDialog();
            }
            else
            {
                var frm = new winDeductFundraising();
                frm.ShowDialog();
            }
        }

        private void YearEnd_Click(object sender, RoutedEventArgs e)
        {
            Data.Instance.YearEnd();
        }

        private void btnAddCharge_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                var frm = new winCharge();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.LastName = uc.txtLName.Text;
                frm.ShowDialog();
            }
            else if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                var frm = new winCharge();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.ShowDialog();
            }
            else
            {
                var frm = new winCharge();
                frm.ShowDialog();
            }
        }

        private void btnAdjustCharge_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is FamilyView)
            {
                var uc = cntCurrentView.Content as FamilyView;
                var frm = new winEditStatement();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.LastName = uc.txtLName.Text;
                frm.ShowDialog();
            }
            else if (cntCurrentView.Content is StudentView)
            {
                var uc = cntCurrentView.Content as StudentView;
                var frm = new winEditStatement();
                frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
                frm.ShowDialog();
            }
            //else
            //{
            //    var frm = new winCharge();
            //    frm.ShowDialog();
            //}
        }

        private void RecitalWizard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditTransTypes_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winTransTypes();
            frm.ShowDialog();
        }

        private void EditPayTypes_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winPayTypes();
            frm.ShowDialog();
        }

        private void btnPrintDaysheet_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winDaysheet();
            frm.ShowDialog();
        }

        private void btnPrintClassList_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winPrintClassList();
            frm.ShowDialog();
        }

        private void btnPrintClassRoster_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is ClassView)
            {
                var frm = new winPrintClassRoster();
                var uc = cntCurrentView.Content as ClassView;
                if (!uc.IsClassSelected) return;
                frm.ClassID = uc.ClassID;
                frm.ShowDialog();
            }
        }

        private void btnPrintSchedule_Click(object sender, RoutedEventArgs e)
        {
            if (cntCurrentView.Content is StudentView)
            {
                var frm = new winPrintSchedule();
                var uc = cntCurrentView.Content as StudentView;
                if (!uc.IsStudentSelected) return;
                frm.StudentID = Convert.ToInt32(uc.txtStudentID.Text);
                frm.ShowDialog();
            }
        }

        private void btnEditFamilyTuition_Click(object sender, RoutedEventArgs e)
        {
            var frm = new winEditTuition();
            var uc = cntCurrentView.Content as FamilyView;
            if (!uc.IsFamilySelected) return;
            frm.FamilyID = Convert.ToInt32(uc.txtFamilyID.Text);
            frm.ShowDialog();
        }

        private void btnRecitalFee_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Add recital fee to all active families?", "Confirm Recital Fee", MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            Data.Instance.AddRecitalFee();
        }

        private void btnRegistrationFee_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Add registration fee to all registered dancers?", "Confirm Registration Fee",
                    MessageBoxButton.YesNo) !=
                MessageBoxResult.Yes) return;
            Data.Instance.AddRegistrationFee();
        }

        private void EditSysParams_Click(object sender, RoutedEventArgs e)
        {
            var frm = new WinEditSysParameters();
            frm.ShowDialog();
        }

        //private void GenerateRecitalOrder()
        //{
        //    List<int> _classList = new List<int>();
        //    var dt = Data.Instance.GetClassIDList().Tables[0];
        //    foreach (DataRow dataRow in dt.Rows)
        //    {
        //        _classList.Add(Convert.ToInt32(dataRow[0]));
        //    }
        //    List<int> _orderList = new List<int>();
        //    _orderList.Add(_classList[_random.Next(_classList.Count)]);
        //    List<int> _studentList1 = new List<int>();
        //    var dt1 = Data.Instance.GetStudentIDList(_orderList[0]);
        //    foreach (DataRow dataRow in dt1.Rows)
        //    {
        //        _studentList1.Add(Convert.ToInt32(dataRow[0]));
        //    }
        //    var _filteredClassList = _classList.Except(_orderList).ToList();
        //    var _nextClassID = _filteredClassList[_random.Next(_filteredClassList.Count)];
        //    List<int> _studentList2 = new List<int>();
        //    var dt2 = Data.Instance.GetStudentIDList(_nextClassID);
        //    foreach (DataRow dataRow in dt2.Rows)
        //    {
        //        _studentList2.Add(Convert.ToInt32(dataRow[0]));
        //    }
        //    foreach (var i in _studentList2)
        //    {
        //        if (_studentList1.Contains(i))
        //        {
        //            _sharedStudent = true;
        //            return;
        //        }
        //        else
        //        {
        //            _orderList.Add(_nextClassID);
        //        }
        //    }
        //}
    }
}
