using StudioAdvanced.Utilities;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace StudioAdvanced.Views
{
    /// <summary>
    /// Interaction logic for WinEditSysParameters.xaml
    /// </summary>
    public partial class WinEditSysParameters : Window
    {
        private bool IsEditing;
        private bool IsInserting;
        private SystemParametersDataContext systemParameters;
        public WinEditSysParameters()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
            IsInserting = false;
            systemParameters = new SystemParametersDataContext();
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            var result = from systemParameter in systemParameters.SystemParameters
                select systemParameter;
            dgParameters.ItemsSource = result.ToList();
        }

        private void dgParameters_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            IsInserting = true;
        }

        private void dgParameters_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IsEditing = true;
        }

        private void dgParameters_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && !IsEditing)
            {
                if (dgParameters.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete the selected item/items?", "Confirm Delete",
                            MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    foreach (DataRow selectedItem in dgParameters.SelectedItems)
                    {
                        Data.Instance.DeleteSysParam(Convert.ToInt32(selectedItem["ParameterID"]));
                    }
                }
            }
            LoadDataGrid();
        }

        private void dgParameters_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                SystemParametersDataContext context = new SystemParametersDataContext();
                SystemParameter sysParams = e.Row.DataContext as SystemParameter;

                var matchedData = (from sp in context.SystemParameters
                    where sp.ParameterID == sysParams.ParameterID
                    select sp).SingleOrDefault();

                if (matchedData == null)
                {
                    SystemParameter sysParam = new SystemParameter();
                    sysParam.Description = sysParams.Description;
                    sysParam.Value = sysParams.Value;
                    sysParam.MiscDescription = sysParams.MiscDescription;
                    sysParam.Created = DateTime.Now;
                    context.SystemParameters.InsertOnSubmit(sysParam);
                    context.SubmitChanges();
                }
                else
                {
                    matchedData.Description = sysParams.Description;
                    matchedData.Value = sysParams.Value;
                    matchedData.MiscDescription = sysParams.MiscDescription;
                    context.SubmitChanges();
                }
            }
            LoadDataGrid();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
