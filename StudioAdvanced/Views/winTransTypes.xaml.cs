using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StudioAdvanced.Utilities;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for winTransTypes.xaml
    /// </summary>
    public partial class winTransTypes : Window
    {
        private bool IsEditing;
        private bool IsInserting;
        private TransTypesDBDataContext transTypes;
        public winTransTypes()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
            IsInserting = false;
            transTypes = new TransTypesDBDataContext();
            LoadDataGrid();
        }

        private void dgTransTypes_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            IsInserting = true;
        }

        private void dgTransTypes_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IsEditing = true;
        }

        private void dgTransTypes_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                TransTypesDBDataContext context = new TransTypesDBDataContext();
                TransType tType = e.Row.DataContext as TransType;

                var matchedData = (from tt in context.GetTable<TransType>()
                    where tt.TransTypeID == tType.TransTypeID
                    select tt).SingleOrDefault();
                if (matchedData == null)
                {
                    TransType trans = new TransType();
                    trans.Description = tType.Description;
                    context.TransTypes.InsertOnSubmit(trans);
                    context.SubmitChanges();
                }
                else
                {
                    matchedData.Description = tType.Description;
                    context.SubmitChanges();
                }
            }
            
            //var _row = e.Row.Item as DataRow;
            //var _transTypeId = Convert.ToInt32(_row["TransTypeID"]);
            //var _description = _row["Description"].ToString();
            //if (IsInserting)
            //{
            //    _transTypeId = -1;
            //    Data.Instance.EditTransTypes(_transTypeId, _description);
            //}
            //else if (IsEditing)
            //{
            //    Data.Instance.EditTransTypes(_transTypeId, _description);
            //}
            //IsEditing = false;
            //IsInserting = false;
            LoadDataGrid();
        }

        private void dgTransTypes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !IsEditing)
            {
                if (dgTransTypes.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete the selected item/items?", "Confirm Delete",
                            MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    foreach (DataRow selectedItem in dgTransTypes.SelectedItems)
                    {
                        Data.Instance.DeleteTransType(Convert.ToInt32(selectedItem["TransTypeID"]));
                    }
                }
            }
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            var result = from transType in transTypes.TransTypes
                select transType;
            dgTransTypes.ItemsSource = result.ToList();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
