using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ChartsTest
{
    /// <summary>
    /// CrosshairCursorControl.xaml 的交互逻辑
    /// </summary>
    public partial class CrosshairCursorControl : Window
    {
        public CrosshairCursorControl()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,7 }
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 }
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0 //straight lines, 1 really smooth lines
            });

            //modifying any series values will also animate and update the chart
            SeriesCollection[2].Values.Add(5d);

            DataContext = this;
        }


        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}
