using BusStation.UI.Services.Abstract;
using BusStation.UI.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using DataVis = System.Windows.Controls.DataVisualization;

namespace BusStation.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ChartView.xaml
    /// </summary>
    public partial class ChartView : UserControl
    {
        public ChartView(IBusDataService busDataService, IRepairmentDataService repairmentDataService)
        {
            InitializeComponent();
            DataContext = new ChartViewModel(busDataService, repairmentDataService);
        }

        private void ChartsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChartsListBox.SelectedIndex == -1)
            {
                return;
            }

            MainChart.Visibility = Visibility.Visible;

            switch(ChartsListBox.SelectedIndex) 
            {
                case 0:
                    MakeBusColorsChartAsync();
                    break; 
                case 1:
                    MakeRepairmentYearsChartAsync();
                    break;
            }
        }

        private async void MakeBusColorsChartAsync()
        {
            int chartNumber = 0;

            await ((ChartViewModel)DataContext).LoadBusColorsWithCount();
            List<KeyValuePair<string, int>> elements = new List<KeyValuePair<string, int>>();

            foreach (var c in ((ChartViewModel)DataContext).BusColorsWithCount)
            {
                elements.Add(new KeyValuePair<string, int>(c.Color, c.BusCount));
            }

            ((PieSeries)MainChart.Series[chartNumber]).ItemsSource = elements;
            MakeChartActive(chartNumber);
        }
        
        private async void MakeRepairmentYearsChartAsync()
        {
            int chartNumber = 1;

            await((ChartViewModel)DataContext).LoadRepairmentYearsWithCount();
            List<KeyValuePair<int, int>> elements = new List<KeyValuePair<int, int>>();
            
            foreach (var y in ((ChartViewModel)DataContext).RepairmentYearsWithCount)
            {
                elements.Add(new KeyValuePair<int, int>(y.Year, y.RepairmentsCount));
            }

            ((LineSeries)MainChart.Series[chartNumber]).ItemsSource = elements;
            MakeChartActive(chartNumber);
        }

        private void MakeChartActive(int index) 
        {
            for (int i = 0; i < MainChart.Series.Count; i++)
            {
                if (i == index)
                {
                    ((UIElement)MainChart.Series[i]).Visibility = Visibility.Visible;
                    continue;
                }

                ((UIElement)MainChart.Series[i]).Visibility = Visibility.Collapsed;
            }
            
        }
    }
}
