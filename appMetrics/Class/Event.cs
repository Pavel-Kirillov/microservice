using agent.Class;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace appMetrics.Class
{
    internal class Event : Metrics
    {
        private readonly SeriesCollection ColumnSeriesValues;
        private readonly List<string> Labels;
        private readonly TextBlock PercentTextBlock;

        public object Dispatcher { get; private set; }

        public Event(SeriesCollection ColumnSeriesValues, List<string> Labels, string metricAddress, TextBlock PercentTextBlock)
        {
            this.ColumnSeriesValues = ColumnSeriesValues;
            this.Labels = Labels;
            this.metricAddress = metricAddress;
            this.PercentTextBlock = PercentTextBlock;
            Thread thread = new Thread(RefreshMetrics);
            thread.IsBackground = true;
            thread.Start();
        }
        private MetricReaponse SetValue(TimeSpan fromTime, TimeSpan toTime)
        {

            List<MetricReaponse> response;
            response = GetMetricsAgent(fromTime, toTime);
            if (response.Count > 0) return response[response.Count - 1];
            else return null;

        }
        public void RefreshMetrics()
        {
            long localDateTmp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 5;
            TimeSpan localDate = TimeSpan.FromSeconds(localDateTmp * 5);

            for (int i = 10; i > 0; i--)
            {
                MetricReaponse reaponse = SetValue(localDate - TimeSpan.FromSeconds(5 * i), localDate - TimeSpan.FromSeconds(5 * (i - 1)));
                double value;
                if (reaponse != null)
                {
                    value = reaponse.Value;
                    ColumnSeriesValues[0].Values.Add(value);
                    Labels.Add(TimeSpan.FromSeconds(reaponse.Time).ToString(@"hh\:mm\:ss"));
                }
                else
                {
                    value = 0;
                    ColumnSeriesValues[0].Values.Add(value);
                    Labels.Add((localDate - TimeSpan.FromSeconds(5 * i)).ToString(@"hh\:mm\:ss"));
                }
                PercentTextBlock.Dispatcher.BeginInvoke((Action)(() =>
                {
                    PercentTextBlock.Text = Convert.ToString(value);
                }));

            }

            while (true)
            {
                localDate = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                if (localDate.Seconds % 5 == 0)
                {
                    if (ColumnSeriesValues[0].Values.Count >= 10)
                    {
                        ColumnSeriesValues[0].Values.RemoveAt(0);
                        Labels.RemoveAt(0);
                    }
                    MetricReaponse reaponse = SetValue(localDate - TimeSpan.FromSeconds(5), localDate);

                    if (reaponse != null)
                    {
                        double value = reaponse.Value;
                        ColumnSeriesValues[0].Values.Add(value);
                        Labels.Add(TimeSpan.FromSeconds(reaponse.Time).ToString(@"hh\:mm\:ss"));

                        PercentTextBlock.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            PercentTextBlock.Text = Convert.ToString(value);
                        }));
                    }
                }
                Thread.Sleep(1000);
            }
        }

    }
}
