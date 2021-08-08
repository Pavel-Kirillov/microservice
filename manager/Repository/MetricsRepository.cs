using agent.Class;
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

        private const string ConnectionString = @"Data Source=metricsManager.db;Version=3;Pooling=True;Max Pool Size=100;";
        private string table = "";
        protected void SetTable(string table)
        {
            this.table = table;
        }
        public void Add(long agentID, long value, long time)
        {
            logger.Trace($"Add in table = {table} AgentId = {agentID} value = {value} time = {time}");
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute($"INSERT INTO {table} (AgentId, value, time) VALUES({agentID},{value}, {time})");
            }
        }
        public List<MetricReaponse> GetByTimePeriod(int agentID, TimeSpan fromTime, TimeSpan toTime)
        {
            logger.Trace($"GetAgentMetrics agentID = {agentID} table = {table} fromTime = {fromTime} toTime = {toTime}");

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<MetricReaponse>($"SELECT * FROM {table} WHERE AgentID = {agentID} AND time >= {fromTime.TotalSeconds} AND time <= {toTime.TotalSeconds}").ToList();
            }
        }
        public int GetLastTime()
        {
            int lastTime = 0;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    lastTime = connection.Query<int>($"SELECT MAX(time) FROM {table}").Single();
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            logger.Trace($"GetLastTime lastTime = {lastTime}");
            return lastTime;
        }
    }
}
