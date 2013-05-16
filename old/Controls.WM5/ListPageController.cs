using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Nucleo.GoodGuide.Controls
{
  public class ListPageController
  {
    public ListPageController()
    {
    }
    public ListPageController(Int16 itemsPerPage)
    {
      this.itemsPerPage = itemsPerPage;
    }
    public ListPageController(Int16 itemsPerPage,IList datasource)
    {
      this.itemsPerPage = itemsPerPage;
      this.dataSource = datasource;
    }

    public event EventHandler PageNumberChanged;

    public object DataSource
    {
      get { return dataSource; }
      set
      {
        dataSource = value;

        if (dataSource is IList)
          dataSourceCount = ((IList)dataSource).Count;
        if (dataSource is IListSource)
          dataSourceCount = ((IListSource)dataSource).GetList().Count;

        pageNumber = 1;
        BuildPageItems();
      }
    }
    public IList PageData
    {
      get { return displayedObjects; }
    }
    public Int16 ItemsPerPage
    {
      get { return itemsPerPage; }
      set
      {
        itemsPerPage = value;
        BuildPageItems();
      }
    }
    public short PageNumber
    {
      get { return pageNumber; }
      set
      {
        if (value < 1)
          return;
        if (value > PageCount)
          return;

        pageNumber = value;
        BuildPageItems();
        OnPageNumberChanged();
      }
    }
    public Int16 PageCount
    {
      get
      {
        if (dataSource == null)
          return 1;

        if (dataSourceCount == 0)
          return 1;

        if (dataSourceCount % itemsPerPage == 0)
          return (Int16)(dataSourceCount / itemsPerPage);
        else
          return (Int16)((dataSourceCount / itemsPerPage) + 1);
      }
    }

    public void FirstPage()
    {
      PageNumber = 1;
    }
    public void PreviousPage()
    {
      PageNumber--;
    }
    public void NextPage()
    {
      PageNumber++;
    }

    private void BuildPageItems()
    {
      displayedObjects.Clear();

      if (dataSource == null)
        return;

      Int32 PageOffset = (pageNumber - 1) * itemsPerPage;

      for (Int32 ItemNo = 0; ItemNo < itemsPerPage;ItemNo++)
      {
        Int32 Index = PageOffset + ItemNo;
        if (Index >= dataSourceCount)
          continue;

        object Item = null;
        if (dataSource is IList)
          Item = ((IList)dataSource)[Index];
        if (dataSource is IListSource)
          Item = ((IListSource)dataSource).GetList()[Index];
        
        displayedObjects.Add(Item);        
      }
    }

    private void OnPageNumberChanged()
    {
      if (PageNumberChanged == null)
        return;

      PageNumberChanged(this,new EventArgs());
    }

    private object dataSource = null;
    private Int16 itemsPerPage = 1;
    private Int16 pageNumber = 1;
    private Int32 dataSourceCount = 0;
    private readonly List<object> displayedObjects = new List<object>();
  }
}
