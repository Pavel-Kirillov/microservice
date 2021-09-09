using Dapper;
using NLog;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace manager.Repository
{
    public class AgentRepository
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private const string connectionString = @"Data Source=metricsManager.db;Version=3;Pooling=True;Max Pool Size=100;";
        private const string table = "agents";
        public List<AgentInfo> GetAgents()
        {
            logger.Trace($"GetAgents");
            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            List<AgentInfo> str = connection.Query<AgentInfo>($"SELECT * FROM {table}").ToList();
            return str;
        }
        public void AddAgent(int agentID, string agentUrl)
        {
            logger.Trace($"AddAgent ID = {agentID} url = {agentUrl}");
            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Execute($"INSERT INTO {table} (AgentId, AgentAddress) VALUES({agentID},\"{agentUrl}\")");
        }
        public void DeleteAgent(int agentID)
        {
            logger.Trace($"DeleteAgent ID = {agentID}");
            using SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Execute($"DELETE FROM {table} WHERE AgentId = {agentID}");
        }

    }
}
