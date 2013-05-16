using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmDestinationMore : frmCarApplicationBase
  {
    public enum MorePages
    {
      None,
      More1,
      More2,
      More3
    }

    public static MoreResults ShowMore(DestinationDataset.DestinationRow destination,Boolean isRecentDestination)
    {
      frmDestinationMore frm = new frmDestinationMore(destination,isRecentDestination);

      frm.ShowDialog();
      MoreResults Result = frm.MoreResult;
      frm.Dispose();

      return Result;
    }

    public MorePages Page
    {
      get { return page; }
      set
      {
        if (page == value)
          return;

        if (page != MorePages.None)
          SetPageVisible(page, false);

        page = value;

        SetPageVisible(page, true);

        UpdateControls();
      }
    }
    public MoreResults MoreResult
    {
      get { return moreResult; }
      set
      {
        moreResult = value;
        DialogResult = System.Windows.Forms.DialogResult.None;
      }
    }

    protected frmDestinationMore(DestinationDataset.DestinationRow destination,Boolean isRecentDestination)
    {
      InitializeComponent();

      this.destination = destination;
      this.isRecentDestination = isRecentDestination;

      canShowMore1 = DestinationBll.CanShowMore1(destination);
      canShowMore2 = DestinationBll.CanShowMore2(destination);
      canShowMore3 = DestinationBll.CanShowMore3(destination);

      isInMySelection = CarApplication.Instance.MySelectionBll.IsInMySelection(destination.Id);

      destinationType = CarApplication.Instance.DestinationTypeBll.GetById(destination.DestinationTypeId);

      HeadingText = destination.Code;
      lblMainHeading2a.Text = destination.Location;
      lblMainHeading2c.Text = destination.ClassificationCode;

      #region Softkeys
      BtnSoftKey1.Enabled = true;
      SetIsInMySelection(isInMySelection);

      if ((destination.IsLatitudeNull() == false) && (destination.IsLatitudeNull() == false))
      {
        BtnSoftKey2.Enabled = true;
        BtnSoftKey3.Enabled = true;
      }
      else
      {
        BtnSoftKey2.Enabled = false;
        BtnSoftKey3.Enabled = false;
      }
      BtnSoftKey2.Enabled = false;  // TEMP

      BtnSoftKey2.Text = "VIEW|MAP";
      BtnSoftKey3.Text = "TAKE ME|THERE";
      BtnSoftKey4.Text = "PREVIOUS|PAGE";
      BtnSoftKey5.Text = "NEXT|PAGE";
      #endregion

      #region More1
      pnlMore1.Text = destination.ShortDescription;
      if (destinationType != null)
      {
        pnlMore1.Comment1Label = destinationType.Comment1Label;
      }
      pnlMore1.Comment1 = destination.Comment1;
      pnlMore1.CommentBackgroundImage = CarApplication.Instance.ImageManager.ImgDestinationMore1CommentLine;
      if (string.IsNullOrEmpty(destination.Image1Filename) == false)
        pnlMore1.Image = CarApplication.Instance.ImageManager.GetDestinationImage(destination.Image1Filename);
      #endregion

      #region More2
      pnlMore2.Text = destination.LongDescription;
      if (destinationType != null)
      {       
        pnlMore2.Comment1Label = destinationType.Comment2Label;
        pnlMore2.Comment2Label = destinationType.Comment3Label;
        pnlMore2.Comment3Label = destinationType.Comment4Label;
      }
      pnlMore2.Comment1 = destination.Comment2;
      pnlMore2.Comment2 = destination.Comment3;
      pnlMore2.Comment3 = destination.Comment4;
      pnlMore2.CommentBackgroundImage = CarApplication.Instance.ImageManager.ImgDestinationMore1CommentLine;
      if (string.IsNullOrEmpty(destination.Image2Filename) == false)
        pnlMore2.Image = CarApplication.Instance.ImageManager.GetDestinationImage(destination.Image2Filename);
      #endregion

      #region More3
      pnlMore3.Text = destination.Address;
      pnlMore3.Comment1Label = "Booking: ";
      pnlMore3.Comment2Label = "Language: ";
      pnlMore3.Comment3Label = "Comment: ";
      pnlMore3.Comment1 = destination.Booking;
      pnlMore3.Comment2 = destination.Language;
      pnlMore3.Comment3 = destination.Comment;
      pnlMore3.TelNo = destination.TelephoneNo;
      pnlMore3.CellNo = destination.CellNo;
      pnlMore3.CommentBackgroundImage = CarApplication.Instance.ImageManager.ImgDestinationMore3CommentLine;
      pnlMore3.NumberBackgroundImage = CarApplication.Instance.ImageManager.ImgDestinationMore1CommentLine;
      #endregion

      if (canShowMore1 == true)
      {
        Page = MorePages.More1;
        return;
      }
      if (canShowMore2 == true)
      {
        Page = MorePages.More2;
        return;
      }
      if (canShowMore3 == true)
      {
        Page = MorePages.More3;
        return;
      }
    }

    protected void SetIsInMySelection(Boolean value)
    {
      isInMySelection = value;

      if (isInMySelection == false)
        BtnSoftKey1.Text = "ADD TO MY|SELECTION";
      else
        BtnSoftKey1.Text = "REMOVE FROM|SELECTION";      
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      #region btnBack
      if (navigationControl == BtnBack)
      {
        handled = true;
        MoreResult = MoreResults.Back;
        return;
      }
      #endregion

      #region btnMap
      if (navigationControl == BtnMap)
      {
        handled = true;
        MoreResult = MoreResults.ViewMap;
        return;
      }
      #endregion

      #region Softkey1 - My Selection
      if (navigationControl == BtnSoftKey1)
      {
        handled = true;
        ToggleMySelection();
        return;
      }
      #endregion

      #region Softkey3 - Take Me There
      if (navigationControl == BtnSoftKey3)
      {
        handled = true;
        TakeMeThere();
        MoreResult = MoreResults.TakeMeThere;
      }
      #endregion

      #region Softkey4 - Previous Page
      if (navigationControl == BtnSoftKey4)
      {
        handled = true;
        PreviousPage();
        return;
      }
      #endregion

      #region Softkey5 - Next Page
      if (navigationControl == BtnSoftKey5)
      {
        handled = true;
        NextPage();
        return;
      }
      #endregion
    }

    private void ToggleMySelection()
    {
      if (isInMySelection == true)
      {
        try
        {
          CarApplication.Instance.MySelectionBll.Remove(destination.Id);
          isInMySelection = false;
          SetIsInMySelection(isInMySelection);
        }
        catch
        {
        }
      }
      else
      {
        try
        {
          CarApplication.Instance.MySelectionBll.Add(destination.Id);
          isInMySelection = true;
          SetIsInMySelection(isInMySelection);
        }
        catch
        {
        }
      }
    }
    private void TakeMeThere()
    {
      CarApplication.Instance.NavigateTo(destination,isRecentDestination);
    }
    private void PreviousPage()
    {
      switch (page)
      {
        case MorePages.More2:
          if (canShowMore1 == true)
            Page = MorePages.More1;
          break;
        case MorePages.More3:
          if (canShowMore2 == true)
            Page = MorePages.More2;
          else
            if (canShowMore1 == true)
              Page = MorePages.More1;
          break;
      }
    }
    private void NextPage()
    {
      switch (page)
      {
        case MorePages.More1:
          if (canShowMore2 == true)
            Page = MorePages.More2;
          else
            if (canShowMore3 == true)
              Page = MorePages.More3;
          break;
        case MorePages.More2:
          if (canShowMore3 == true)
            Page = MorePages.More3;
          break;
      }
    }

    private void SetPageVisible(MorePages morePage,Boolean visible)
    {
      switch (morePage)
      {
        case MorePages.More1:
          pnlMore1.Visible = visible;
          break;
        case MorePages.More2:
          pnlMore2.Visible = visible;
          break;
        case MorePages.More3:
          pnlMore3.Visible = visible;
          break;
      }
    }
    private void UpdateControls()
    {
      switch(page)
      {
        case MorePages.More1:
          BtnSoftKey4.Enabled = false;
          BtnSoftKey5.Enabled = (canShowMore2 == true) || (canShowMore3 == true);
          break;
        case MorePages.More2:
          BtnSoftKey4.Enabled = canShowMore1;
          BtnSoftKey5.Enabled = canShowMore3;
          break;
        case MorePages.More3:
          BtnSoftKey4.Enabled = (canShowMore1 == true) || (canShowMore2 == true);
          BtnSoftKey5.Enabled = false;
          break;
      }
    }

    private MorePages page = MorePages.None;
    private MoreResults moreResult = MoreResults.None;
    private Boolean isInMySelection = false;
    private readonly Boolean canShowMore1 = false;
    private readonly Boolean canShowMore2 = false;
    private readonly Boolean canShowMore3 = false;
    private readonly DestinationDataset.DestinationRow destination;
    private readonly DestinationTypeDataset.DestinationTypeRow destinationType;
    private readonly Boolean isRecentDestination;
  }
}