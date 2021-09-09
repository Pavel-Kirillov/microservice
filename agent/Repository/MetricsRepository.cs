using Dapper;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace agent.Repository
{
    public abstract class MetricsRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private const string connectionString = @"Data Source=metrics.db;Version=3;Pooling=True;Max Pool Size=100;";

        protected string table = "";

        public List<MetricResponse> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            logger.Trace($"GetAgentMetrics table = {table} fromTime = {fromTime} toTime = {toTime}");

            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            return connection.Query<MetricResponse>($"SELECT * FROM {table} WHERE time > {fromTime.TotalSeconds} AND time <= {toTime.TotalSeconds}").ToList();
        }
        public void Add(TimeSpan time, long value)
        {
            logger.Trace($"Add in table = {table} value = {value} time = {time}");
            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Execute($"INSERT INTO {table} (value, time) VALUES({value}, {time.TotalSeconds})");
        }
        public void Delete(TimeSpan fromTime, TimeSpan toTime)
        {
            logger.Trace($"Delete in table = {table} fromTime = {fromTime} toTime = {toTime}");
            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Execute($"DELETE FROM {table} WHERE time > {fromTime.TotalSeconds} AND time <= {toTime.TotalSeconds}");
        }
    }
}
