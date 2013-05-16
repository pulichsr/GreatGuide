using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmCommentMore : frmCarApplicationBase
  {
    public static MoreResults ShowDetail(string comment)
    {
      frmCommentMore frm = new frmCommentMore(comment);

      frm.ShowDialog();
      MoreResults Result = frm.MoreResult;
      frm.Dispose();

      return Result;
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

    private frmCommentMore(string comment)
    {
      InitializeComponent();

      edtComment.Text = comment;
    }

    protected override void NavigationControlClicked(System.Windows.Forms.Control navigationControl, ref bool handled, ref object clickData)
    {
      #region btnBack
      if (navigationControl == BtnBack)
        Close();
      #endregion

      #region btnMap
      if (navigationControl == BtnMap)
      {
        handled = true;
        MoreResult = MoreResults.ViewMap;
        return;
      }
      #endregion
    }

    private MoreResults moreResult = MoreResults.None;
  }
}