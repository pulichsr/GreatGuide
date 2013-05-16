using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class DestinationUpdateBll : ISyncTarget
  {
    public const Int32 UndefinedId = -1;

    public DestinationUpdateBll(ILogger logger,IDestinationRepository destinationRepository, DestinationBll destinationBll)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(destinationRepository, "destinationRepository");
      Guard.ArgumentNotNull(destinationBll, "destinationBll");

      this.logger = logger;
      this.destinationRepository = destinationRepository;
      this.destinationBll = destinationBll;
    }

    public string DatasetName
    {
      get { return "Destination"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      logger.Write(this, ">> ISyncTarget.Insert");

      DestinationDataset.DestinationDataTable data = (DestinationDataset.DestinationDataTable)newData.Tables["Destination"];
      if (data != null)
      {
        for (Int32 RowNo = 0; RowNo < data.Rows.Count; RowNo++)
        {
          if (data[RowNo].IsIdNull() == true)
          {
            logger.Write(this,string.Format("   Row {0} Id is null",RowNo));
            continue;
          }

          logger.Write(this, string.Format("   New destination: Id:{0}, Code:{1}, Version:{2}", data[RowNo].Id, data[RowNo].Code, data[RowNo].VersionNo));

          DestinationDataset.DestinationRow currentDestination = destinationRepository.GetById(data[RowNo].Id + offset);

          if (currentDestination != null)
          {
            logger.Write(this, string.Format("   Current destination: Id:{0}, Code:{1}, Version:{2}", currentDestination.Id, currentDestination.Code, currentDestination.VersionNo));

            if (data[RowNo].VersionNo <= currentDestination.VersionNo)
            {
              logger.Write(this,"   New destination is lower or equal than current version. Skipping");
              continue;
            }
            else
            {
              logger.Write(this, "   New destination is higher than current version. Deleting");
              destinationRepository.DeleteById(currentDestination.Id + offset);
            }
          }
          else
          {
            logger.Write(this,"Current destination not found");
          }

          data[RowNo].Id += offset;

          if (data[RowNo].IsClassificationIdNull() == false) data[RowNo].ClassificationId += offset;
          if (data[RowNo].IsCollectionIdNull() == false) data[RowNo].CollectionId += offset;
          if (data[RowNo].IsDestinationTypeIdNull() == false) data[RowNo].DestinationTypeId += offset;
          if (data[RowNo].IsMasterAreaIdNull() == false) data[RowNo].MasterAreaId += offset;
          if (data[RowNo].IsThemeIdNull() == false) data[RowNo].ThemeId += offset;

          logger.Write(this, "   Inserting");
          destinationBll.Insert(data[RowNo]);
        }

        data.Dispose();
      }
    }
    public void DeleteAll()
    {}

    private readonly ILogger logger;
    private readonly IDestinationRepository destinationRepository = null;
    private readonly DestinationBll destinationBll = null;
  }

}
