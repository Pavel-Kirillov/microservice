using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace appMetrics.Graff
{
    public partial class RamGraff : UserControl
    {
        public SeriesCollection ColumnSeriesValues { get; set; }
        public List<string> Labels { get; set; }
        public readonly Axis maxValue;
        public RamGraff()
        {

            InitializeComponent();

            ColumnSeriesValues = new SeriesCollection
            {
                new ColumnSeries{ Values = new ChartValues<double>{ } }
            };
            DataContext = this;
            Labels = new List<string>();

            new Event(ColumnSeriesValues, Labels, $"api/metrics/ram/available/agent", percentTextBlock);
        }

        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
            timePowerChart.Update(true);
        }

    }
}
