using System;

namespace Nucleo.GoodGuide.Types
{
  [Serializable]
  public class DestinationListFormData
  {
    public DestinationListFormData()
    {
    }
    public DestinationListFormData(string comment)
    {
      this.Comment = comment;
    }
    public DestinationListFormData(string destinationTypeCode,string classificationCode,string collectionCode)
    {
      this.destinationTypeCode = destinationTypeCode;
      this.classificationCode = classificationCode;
      this.collectionCode = collectionCode;
    }
    public DestinationListFormData(string destinationTypeCode,string classificationCode,string collectionCode,string comment,Boolean listAllMasterAreas)
    {
      this.destinationTypeCode = destinationTypeCode;
      this.classificationCode = classificationCode;
      this.collectionCode = collectionCode;
      this.comment = comment;
      this.listAllMasterAreas = listAllMasterAreas;
    }

    public string DestinationTypeCode
    {
      get { return destinationTypeCode; }
      set { destinationTypeCode = value; }
    }
    public string ClassificationCode
    {
      get { return classificationCode; }
      set { classificationCode = value; }
    }
    public string CollectionCode
    {
      get { return collectionCode; }
      set { collectionCode = value; }
    }
    public string Comment
    {
      get { return comment; }
      set { comment = value; }
    }
    public Boolean ListAllMasterAreas
    {
      get { return listAllMasterAreas; }
      set { listAllMasterAreas = value; }
    }


    private string destinationTypeCode;
    private string classificationCode;
    private string collectionCode;
    private string comment;
    private Boolean listAllMasterAreas = false;
  }
}