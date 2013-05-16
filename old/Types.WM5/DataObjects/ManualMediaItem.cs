using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class ManualMediaItem
  {
    public ManualMediaItem()
    {
    }
    public ManualMediaItem(string filename,string description,bool isFillerContent)
    {
      this.filename = filename;
      this.description = description;
      this.isFillerContent = isFillerContent;
    }

    public string Filename
    {
      get { return filename; }
      set { filename = value; }
    }
    public string Description
    {
      get { return description; }
      set { description = value; }
    }
    public bool IsFillerContent
    {
      get { return isFillerContent; }
      set { isFillerContent = value; }
    }

    private string filename;
    private string description;
    private Boolean isFillerContent;  
  }

  public class ManualMediaItems : List<ManualMediaItem>
  {}
}