using KtvStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KtvStudio.Views
{
    /// <summary>
    /// SongInfoEditUc.xaml 的交互逻辑
    /// </summary>
    public partial class SongInfoEditUc : UserControl, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        #region CategoryEditViewModel

        private CategoryEditViewModel _CategoryEditViewModel;

        public CategoryEditViewModel CategoryEditViewModel
        {
            get { return _CategoryEditViewModel; }
            set
            {
                if (_CategoryEditViewModel != null && _CategoryEditViewModel.Equals(value)) return;
                _CategoryEditViewModel = value;
                RaisePropertyChanged("CategoryEditViewModel");

            }
        }

        #endregion

        public SongInfoEditUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                ClientViewModel = (ClientViewModel)DataContext;
                CategoryEditViewModel = new CategoryEditViewModel(ClientViewModel);
            };
        }

        private void FirstCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedValue != null)
            {
                string code = comboBox.SelectedValue.ToString();
                ClientViewModel.SecondCategorySourceView.RowFilter = $"fatherid = '{code}'";
            }
        }

        private void SecondCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedValue != null)
            {
                string code = comboBox.SelectedValue.ToString();
                CategoryEditViewModel.selectedCategoryId = code;
                CategoryEditViewModel.selectedCategoryName = ((DataRowView)comboBox.SelectedItem)["categoryname"].ToString();
            }
        }

        private void SelectedCategoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedIndex != -1)
            {
                CategoryEditViewModel.selectedDataGridCategory = ((DataRowView)dataGrid.SelectedItem).Row;
            }
        }

        private void MusicNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //根据已选择的分类代码获取其中文名称
            CategoryEditViewModel.SelectedCategorySource = CategoryEditViewModel.GetCategorySourceNameByCode();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vedio.Play();
        }
    }
}