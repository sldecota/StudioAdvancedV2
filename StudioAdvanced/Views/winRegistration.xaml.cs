using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winRegistration.xaml
    /// </summary>
    public partial class winRegistration : Window
    {
        public winRegistration()
        {
            InitializeComponent();
        }
        public int StudentID { get; set; }
        public int FamilyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private void LoadClassGrid()
        {
            var classTable = new DataTable();
            classTable = Data.Instance.GetAllClasses().Tables[0];
            dgClasses.ItemsSource = classTable.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadClassGrid();
            txtFirstName.Text = FirstName;
            txtLastName.Text = LastName;
            btnRegister.IsEnabled = false;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            //var drv = (DataRowView) dgClasses.SelectedItem;
            foreach (DataRowView selectedItem in dgClasses.SelectedItems)
            {
                Data.Instance.AddRegistration(StudentID, FamilyID, Convert.ToInt32(selectedItem.Row["ID"]));
            }
            DialogResult = true;
            Close();
        }

        private void dgClasses_MouseUp(object sender, MouseButtonEventArgs e)
        {
            btnRegister.IsEnabled = true;
        }
        private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataGridRow row = GetVisualParentByType(
                    (FrameworkElement)e.OriginalSource, typeof(DataGridRow)) as DataGridRow;

                row.IsSelected = !row.IsSelected;
                e.Handled = true;
            }
        }

        private void MouseEnterHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.OriginalSource is DataGridRow && e.LeftButton == MouseButtonState.Pressed)
            {
                DataGridRow row = e.OriginalSource as DataGridRow;

                row.IsSelected = !row.IsSelected;
                e.Handled = true;
            }
        }

        public static DependencyObject GetVisualParentByType(DependencyObject startObject, Type type)
        {
            DependencyObject parent = startObject;
            while (parent != null)
            {
                if (type.IsInstanceOfType(parent))
                    break;
                else
                    parent = VisualTreeHelper.GetParent(parent);
            }

            return parent;
        }
    }
}
