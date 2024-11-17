using FluentMigrator.Runner.VersionTableInfo;

namespace Links.Data.Migrations;

[VersionTableMetaData]
public class CreateTabelaVersionInfoMetadata : DefaultVersionTableMetaData
{
    public override string TableName => "__migrations_metadata";
}
