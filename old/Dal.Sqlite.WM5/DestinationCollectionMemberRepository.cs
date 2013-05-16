using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class DestinationCollectionMemberRepository: 
    ObjectDal<WrapperObjects.DestinationCollectionMember, WrapperObjects.DestinationCollectionMembers>,
    IDestinationCollectionMemberRepository
  {
    public DestinationCollectionMemberRepository(IDbConnector connector) : base(connector)
    {
    }

    #region IDestinationCollectionMemberRepository Members
    public void Insert(DestinationCollectionMemberDataset.DestinationCollectionMemberRow row)
    {
      WrapperObjects.DestinationCollectionMember wrapperObject = new WrapperObjects.DestinationCollectionMember(DestinationCollectionMemberConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable table)
    {
      foreach (DestinationCollectionMemberDataset.DestinationCollectionMemberRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<DestinationCollectionMemberDataset, DestinationCollectionMemberDataset.DestinationCollectionMemberDataTable>(Connector, sql);
    }

    #endregion
  }
}