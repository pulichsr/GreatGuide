using System;
using System.Data;
using Nucleo.Data;
using Nucleo.Data.DataAccess;
using Nucleo.GoodGuide.Dal.Sqlite.DatasetConverters;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using WrapperObjects = Nucleo.GoodGuide.Dal.Sqlite.WrapperObjects;

namespace Nucleo.GoodGuide.Dal.Sqlite
{
  public class ItineraryDayRepository: 
    ObjectDal<WrapperObjects.ItineraryDay, WrapperObjects.ItineraryDays>,
    IItineraryDayRepository
  {
    public ItineraryDayRepository(IDbConnector connector, ILogger logger)
      : base(connector)
    {
      this.logger = logger;
    }

    #region IItineraryDayRepository
    public void Insert(ItineraryDayDataset.ItineraryDayRow row)
    {
      WrapperObjects.ItineraryDay wrapperObject = new WrapperObjects.ItineraryDay(ItineraryDayConverter.ToDataOject(row));

      ValidationResult result = wrapperObject.DataObject.Validate();
      if (result.IsValid == false)
        throw new DataException(result.ValidationErrors.ToString());

      base.Insert(wrapperObject);
    }
    public void Insert(ItineraryDayDataset.ItineraryDayDataTable table)
    {
      foreach (ItineraryDayDataset.ItineraryDayRow row in table.Rows)
        Insert(row);
    }

    public void DeleteAll()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("delete from {0}", this.DbMetadata.TableName));
      Connector.ExecuteNonQuery(command);
      command.Dispose();
    }

    public ItineraryDayDataset.ItineraryDayDataTable Get()
    {
      string sql = string.Format("select * from {0}",DbMetadata.SelectViewName);

      return GetHelper.GetTable<ItineraryDayDataset, ItineraryDayDataset.ItineraryDayDataTable>(Connector, sql);
    }
    public ItineraryDayDataset.ItineraryDayRow GetById(int id)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where ID = {1}", DbMetadata.SelectViewName, id);

      ItineraryDayDataset.ItineraryDayDataTable table = GetHelper.GetTable<ItineraryDayDataset, ItineraryDayDataset.ItineraryDayDataTable>(Connector, command);
      command.Dispose();

      if (table.Rows.Count == 0)
        return null;

      ItineraryDayDataset.ItineraryDayRow row = table[0];
      table.RemoveItineraryDayRow(row);

      return row;
    }
    public DateTime? GetFirstDay()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("select min(ITINERARY_DAT) from {0} ", DbMetadata.TableName));
      object value = Connector.ExecuteScalar(command);
      command.Dispose();

      if (value == null)
        return null;

      return (DateTime)DateTime.ParseExact((string)value,"yyyy-MM-dd HH:mm:ss",null);
    }
    public DateTime? GetLastDay()
    {
      IDbCommand command = Connector.CreateCommand(string.Format("select max(ITINERARY_DAT) from {0} ", DbMetadata.TableName));
      object value = Connector.ExecuteScalar(command);
      command.Dispose();

      if (value == null)
        return null;

      return (DateTime)DateTime.ParseExact((string)value, "yyyy-MM-dd HH:mm:ss", null);
    }
    #endregion

    public ItineraryDayDataset.ItineraryDayRow GetByDate(DateTime date)
    {
      IDbCommand command = Connector.CreateCommand();
      command.CommandText = string.Format("select * from {0} where ITINERARYDAT = {1}", DbMetadata.SelectViewName, Connector.FormatParameterName("DATE"));
      AddCommandParameter(command,"DATE",typeof(string),date.ToString("yyyy-MM-dd 00:00:00"));

      ItineraryDayDataset.ItineraryDayDataTable table = GetHelper.GetTable<ItineraryDayDataset, ItineraryDayDataset.ItineraryDayDataTable>(Connector, command);
      command.Dispose();

      if (table.Rows.Count == 0)
        return null;

      ItineraryDayDataset.ItineraryDayRow row = table[0];

      return row;
    }

    private readonly ILogger logger;
  }
}