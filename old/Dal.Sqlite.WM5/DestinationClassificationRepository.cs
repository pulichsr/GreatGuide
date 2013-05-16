using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class DestinationClassificationRepository: 
    ObjectDal<WrapperObjects.DestinationClassification, WrapperObjects.DestinationClassifications>, 
    IDestinationClassificationRepository
  {
    public DestinationClassificationRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IDestinationClassificationRepository
    public void Insert(DestinationClassificationDataset.DestinationClassificationRow row)
    {
      WrapperObjects.DestinationClassification wrapperObject = new WrapperObjects.DestinationClassification(DestinationClassificationConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(DestinationClassificationDataset.DestinationClassificationDataTable table)
    {
      foreach (DestinationClassificationDataset.DestinationClassificationRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public DestinationClassificationDataset.DestinationClassificationDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);
      return GetHelper.GetTable<DestinationClassificationDataset, DestinationClassificationDataset.DestinationClassificationDataTable>(Connector, sql);
    }
    public DestinationClassificationDataset.DestinationClassificationRow GetByCode(string code)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where CODE = {1}", DbMetadata.SelectViewName, Connector.FormatParameterName("CODE"));
      AddCommandParameter(command,"CODE",code);

      DestinationClassificationDataset.DestinationClassificationDataTable table = GetHelper.GetTable<DestinationClassificationDataset, DestinationClassificationDataset.DestinationClassificationDataTable>(Connector, command);
      command.Dispose();

      if (table.Rows.Count == 0)
        return null;

      DestinationClassificationDataset.DestinationClassificationRow row = table[0];

      return row;
    }

    #endregion
  
  }
}