using System.Data;
using System.Data.SQLite;
using Nucleo.Data;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public static class GetHelper
  {
    public static TableType GetTable<DatasetType,TableType>(IDbConnector connector,string sql) 
      where DatasetType: DataSet,new()
      where TableType: DataTable,new()
    {
      IDbCommand command = connector.CreateCommand(sql);

      TableType table = GetTable<DatasetType, TableType>(connector, command);
      command.Dispose();

      return table;
    }
    public static TableType GetTable<DatasetType,TableType>(IDbConnector connector,IDbCommand command) 
      where DatasetType: DataSet,new()
      where TableType: DataTable,new()
    {
      SQLiteDataAdapter adapter = (SQLiteDataAdapter)connector.CreateDataAdapter(command);
      DatasetType dataset = new DatasetType();
      adapter.Fill(dataset, dataset.Tables[0].TableName);

      return (TableType)dataset.Tables[dataset.Tables[0].TableName];
    }
  }
}
