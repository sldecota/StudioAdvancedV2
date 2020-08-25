using StudioAdvanced.Utilities;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winPayTypes.xaml
    /// </summary>
    public partial class winPayTypes : Window
    {
        private bool IsEditing;
        private bool IsInserting;
        private DataTable payTypes;
        public winPayTypes()
        {
            InitializeComponent();
        }

        private void dgPayTypes_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            IsInserting = true;
        }

        private void dgPayTypes_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IsEditing = true;
        }

        private void dgPayTypes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !IsEditing)
            {
                if (dgPayTypes.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete the selected item/items?", "Confirm Delete",
                            MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    foreach (DataRow selectedItem in dgPayTypes.SelectedItems)
                    {
                        Data.Instance.DeletePayType(Convert.ToInt32(selectedItem["PayTypeID"]));
                    }
                }
            }
            LoadDataGrid();
        }

        private void dgPayTypes_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var _row = e.Row.Item as DataRow;
            var _payTypeId = Convert.ToInt32(_row["PayTypeID"]);
            var _description = _row["Description"].ToString();
            if (IsInserting)
            {
                Data.Instance.EditPayTypes(_payTypeId, _description);
            }
            else if (IsEditing)
            {
                _payTypeId = -1;
                Data.Instance.EditPayTypes(_payTypeId, _description);
            }
            IsEditing = false;
            IsInserting = false;
            LoadDataGrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
            IsInserting = false;
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            payTypes = Data.Instance.GetPayTypes().Tables[0];
            dgPayTypes.ItemsSource = payTypes.DefaultView;
            dgPayTypes.DisplayMemberPath = "Description";
            dgPayTypes.SelectedValuePath = "PayTypeID";
        }
    }
}
