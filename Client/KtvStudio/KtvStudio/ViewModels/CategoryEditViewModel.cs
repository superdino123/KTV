using Helpers.Commands;
using KtvStudio.Views;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KtvStudio.ViewModels
{
    public class CategoryEditViewModel : ViewModelBase
    {
        #region NotifyProperty

        #region SelectedCategorySource

        private DataTable _SelectedCategorySource = new DataTable();

        public DataTable SelectedCategorySource
        {
            get { return _SelectedCategorySource; }
            set
            {
                if (_SelectedCategorySource != null && _SelectedCategorySource.Equals(value)) return;
                _SelectedCategorySource = value;
                RaisePropertyChanged("SelectedCategorySource");
                songInfoEditUc.AddSelectedCategory();
            }
        }

        #endregion

        #region ClientViewModel

        private ClientViewModel _ClientViewModel;

        public ClientViewModel ClientViewModel
        {
            get { return _ClientViewModel; }
            set
            {
                if (_ClientViewModel != null && _ClientViewModel.Equals(value)) return;
                _ClientViewModel = value;
                RaisePropertyChanged("ClientViewModel");

            }
        }

        #endregion

        public SongInfoEditUc songInfoEditUc;

        #region SelectedCategory

        public string selectedCategoryId;
        public string selectedCategoryName;
        
        #endregion

        #endregion

        #region Constrcutor

        public CategoryEditViewModel(ClientViewModel clientViewModel)
        {
            ClientViewModel = clientViewModel;
            if(!SelectedCategorySource.Columns.Contains("id"))
                SelectedCategorySource.Columns.Add("id");
            if (!SelectedCategorySource.Columns.Contains("categoryname"))
                SelectedCategorySource.Columns.Add("categoryname");
        }

        #endregion

        #region Commond
        
        #region AddCategoryCmd

        private RelayCommand _AddCategoryCmd;

        public ICommand AddCategoryCmd
        {
            get { return _AddCategoryCmd ?? (_AddCategoryCmd = new RelayCommand(param => OnAddCategory(), param => CanAddCategory())); }
        }

        public void OnAddCategory()
        {
            if (string.IsNullOrEmpty(selectedCategoryId) || string.IsNullOrEmpty(selectedCategoryName) ||
                (ClientViewModel.SongInfoEditItem.Category != null && 
                    (ClientViewModel.SongInfoEditItem.Category.Equals(selectedCategoryId) ||
                    ClientViewModel.SongInfoEditItem.Category.StartsWith(selectedCategoryId + ",") ||
                    ClientViewModel.SongInfoEditItem.Category.EndsWith("," + selectedCategoryId) ||
                    ClientViewModel.SongInfoEditItem.Category.Contains("," + selectedCategoryId + ","))
                ))
                return;
            DataRow row = SelectedCategorySource.NewRow();
            row["id"] = selectedCategoryId;
            row["categoryname"] = selectedCategoryName;
            SelectedCategorySource.Rows.Add(row);
            songInfoEditUc.AddSelectedCategory();

            if (string.IsNullOrEmpty(ClientViewModel.SongInfoEditItem.Category))
                ClientViewModel.SongInfoEditItem.Category = selectedCategoryId;
            else
                ClientViewModel.SongInfoEditItem.Category = ClientViewModel.SongInfoEditItem.Category + "," + selectedCategoryId;
        }

        public bool CanAddCategory()
        {
            return true;
        }

        #endregion
        
        #endregion

        #region Method

        public DataTable GetCategorySourceNameByCode() {
            DataTable result = new DataTable();
            result.Columns.Add("id");
            result.Columns.Add("categoryname");
            if (ClientViewModel.SongInfoEditItem.Category == null || string.IsNullOrEmpty(ClientViewModel.SongInfoEditItem.Category))
                return result;

            string code = ClientViewModel.SongInfoEditItem.Category;
            string[] codes = code.Split(',');
            int[] codesInt = new int[codes.Length];
            for (int i = 0; i < codes.Length; i++)
            {
                codesInt[i] = int.Parse(codes[i]);
            }

            for (int i = 0; i < codesInt.Length; i++)
            {
                for (int j = 0; j < ClientViewModel.CategorySource.Rows.Count; j++)
                {
                    if (codesInt[i] == (int)ClientViewModel.CategorySource.Rows[j]["id"])
                    {
                        DataRow row = result.NewRow();
                        row["id"] = codesInt[i];
                        row["categoryname"] = ClientViewModel.CategorySource.Rows[j]["categoryname"];
                        result.Rows.Add(row);
                    }
                }
            }
            return result;
        }

        public void AddSelectedCategory(MetroTabControl tabControl)
        {
            tabControl.Items.Clear();
            for (int i = 0; i < SelectedCategorySource.Rows.Count; i++)
            {
                MetroTabItem item = new MetroTabItem() {
                    CloseButtonEnabled = true,
                    Header = SelectedCategorySource.Rows[i]["categoryname"].ToString(),
                    Tag = SelectedCategorySource.Rows[i]["id"].ToString(),
                    Padding = new Thickness(20, 3, 20, 3),
                    BorderThickness = new Thickness(1),
                    BorderBrush = new SolidColorBrush(Colors.DeepSkyBlue),

                };
                ControlsHelper.SetHeaderFontSize(item, 14.0);
                  
                tabControl.Items.Add(item);
            }
        }

        public void DeleteSelectedCategory(string categoryId)
        {
            for (int i = 0; i < SelectedCategorySource.Rows.Count; i++)
            {
                if (SelectedCategorySource.Rows[i]["id"].Equals(categoryId)) {
                    if (ClientViewModel.SongInfoEditItem.Category.Equals(SelectedCategorySource.Rows[i]["id"]))
                        ClientViewModel.SongInfoEditItem.Category = string.Empty;
                    else
                        ClientViewModel.SongInfoEditItem.Category = ClientViewModel.SongInfoEditItem.Category.Replace($",{SelectedCategorySource.Rows[i]["id"]}", "").Replace($"{SelectedCategorySource.Rows[i]["id"]}", "");
                    SelectedCategorySource.Rows.RemoveAt(i);
                    break;
                }
            }
        }

        #endregion

    }
}