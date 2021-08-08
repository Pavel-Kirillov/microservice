using FluentMigrator;

namespace agent.Class
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            string[] table = new string[5] { "cpumetrics", "dotnetmetrics", "hddmetrics", "networkmetrics", "rammetrics" };
            for (int i = 0; i < table.Length; i++)
            {
                Create.Table(table[i])
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64()
                .WithColumn("Value").AsInt64()
                .WithColumn("Time").AsInt64();
            }
            Create.Table("agents")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("AgentId").AsInt64()
            .WithColumn("AgentAddress").AsString();
        }
        public override void Down()
        {
            string[] table = new string[6] { "cpumetrics", "dotnetmetrics", "hddmetrics", "networkmetrics", "rammetrics", "agents" };
            for (int i = 0; i < table.Length; i++)
            {
                Delete.Table(table[i]);
            }
        }
    }
}