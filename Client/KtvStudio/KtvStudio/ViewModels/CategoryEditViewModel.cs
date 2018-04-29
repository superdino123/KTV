using Helpers.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        #region SelectedCategory

        public string selectedCategoryId;
        public string selectedCategoryName;

        public DataRow selectedDataGridCategory;

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
            DataRow row = SelectedCategorySource.NewRow();
            row["id"] = selectedCategoryId;
            row["categoryname"] = selectedCategoryName;
            SelectedCategorySource.Rows.Add(row);

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
        
        #region DeleteCategoryCmd

        private RelayCommand _DeleteCategoryCmd;

        public ICommand DeleteCategoryCmd
        {
            get { return _DeleteCategoryCmd ?? (_DeleteCategoryCmd = new RelayCommand(param => OnDeleteCategory(), param => CanDeleteCategory())); }
        }

        public void OnDeleteCategory()
        {
            string code = selectedDataGridCategory["id"].ToString();
            for (int i = 0; i < SelectedCategorySource.Rows.Count; i++)
            {
                if (SelectedCategorySource.Rows[i]["id"].Equals(code)) {
                    if (ClientViewModel.SongInfoEditItem.Category.Equals(SelectedCategorySource.Rows[i]["id"]))
                        ClientViewModel.SongInfoEditItem.Category = string.Empty;
                    else
                        ClientViewModel.SongInfoEditItem.Category = ClientViewModel.SongInfoEditItem.Category.Replace($",{SelectedCategorySource.Rows[i]["id"]}", "").Replace($"{SelectedCategorySource.Rows[i]["id"]}", "");
                    SelectedCategorySource.Rows.RemoveAt(i);
                    break;
                }
            }
        }

        public bool CanDeleteCategory()
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
            if (ClientViewModel.SongInfoEditItem.Category == null)
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

        #endregion

    }
}