using LiveCharts;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;

namespace appMetrics
{
    internal class Event : Metrics
    {
        private readonly SeriesCollection columnSeriesValues;
        private readonly List<string> labels;
        private readonly TextBlock percentTextBlock;

        public object Dispatcher { get; private set; }

        public Event(SeriesCollection columnSeriesValues, List<string> labels, string metricAddress, TextBlock percentTextBlock)
        {
            this.columnSeriesValues = columnSeriesValues;
            this.labels = labels;
            this.metricAddress = metricAddress;
            this.percentTextBlock = percentTextBlock;
            Thread thread = new Thread(RefreshMetrics)
            {
                IsBackground = true
            };
            thread.Start();
        }
        private MetricResponse SetValue(TimeSpan fromTime, TimeSpan toTime)
        {

            List<MetricResponse> response;
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
                MetricResponse reaponse = SetValue(localDate - TimeSpan.FromSeconds(5 * i), localDate - TimeSpan.FromSeconds(5 * (i - 1)));
                double value;
                if (reaponse != null)
                {
                    value = reaponse.Value;
                    columnSeriesValues[0].Values.Add(value);
                    labels.Add(TimeSpan.FromSeconds(reaponse.Time).ToString(@"hh\:mm\:ss"));
                }
                else
                {
                    value = 0;
                    columnSeriesValues[0].Values.Add(value);
                    labels.Add((localDate - TimeSpan.FromSeconds(5 * i)).ToString(@"hh\:mm\:ss"));
                }
                percentTextBlock.Dispatcher.BeginInvoke((Action)(() =>
                {
                    percentTextBlock.Text = Convert.ToString(value);
                }));

            }

            while (true)
            {
                localDate = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                if (localDate.Seconds % 5 == 0)
                {
                    if (columnSeriesValues[0].Values.Count >= 10)
                    {
                        columnSeriesValues[0].Values.RemoveAt(0);
                        labels.RemoveAt(0);
                    }
                    MetricResponse reaponse = SetValue(localDate - TimeSpan.FromSeconds(5), localDate);

                    if (reaponse != null)
                    {
                        double value = reaponse.Value;
                        columnSeriesValues[0].Values.Add(value);
                        labels.Add(TimeSpan.FromSeconds(reaponse.Time).ToString(@"hh\:mm\:ss"));

                        percentTextBlock.Dispatcher.BeginInvoke((Action)(() =>
                        {
                            percentTextBlock.Text = Convert.ToString(value);
                        }));
                    }
                }
                Thread.Sleep(1000);
            }
        }

    }
}
