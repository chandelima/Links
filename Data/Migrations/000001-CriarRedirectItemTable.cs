using FluentMigrator;

namespace Links.Data.Migrations;

[Migration(000_001)]
public class CriarTabelaParametrosMigration : Migration
{
    private string _tableName = "links";

    public override void Up()
    {
        if (!Schema.Table(_tableName).Exists())
        {
            Create.Table(_tableName)
                .WithColumn("route").AsString(128).PrimaryKey().NotNullable()
                .WithColumn("redirect").AsString(1024).NotNullable();
        }
    }

    public override void Down()
    {
        if (Schema.Table(_tableName).Exists())
        {
            Delete.Table(_tableName);
        }
    }
}
