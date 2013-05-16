using System.Data;
using System.Windows.Forms;
using Nucleo.GoodGuide.Datasets.Datasets;
using Resco.Controls.SmartGrid;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmDestinationListSearch : frmAlphaSearchBase
  {
    public static DialogResult Search(DestinationDataset.DestinationDataTable destinations,out DestinationDataset.DestinationRow destination)
    {
      destinations.DefaultView.RowFilter = string.Empty;

      frmDestinationListSearch frm = new frmDestinationListSearch(destinations);
      DialogResult Result = frm.ShowDialog();
      destination = frm.destination;

      frm.Dispose();

      return Result;
    }

    protected override void EnteredTextChanged(string text)
    {
      destinations.DefaultView.RowFilter = string.Format("Code like '%{0}%'",text);
      DataSource = destinations;
    }
    protected override void OkClicked()
    {
      destination = (DestinationDataset.DestinationRow)(((DataRowView)SelectedRow).Row);
    }
    private frmDestinationListSearch(DestinationDataset.DestinationDataTable destinations)
    {
      InitializeComponent();

      SmartGrid.DefaultRowHeight = 25;

      Column CodeColumn = new Column(222,"","Code");
      Columns.Add(CodeColumn);
      Column LocationColumn = new Column(200, "", "Location");
      Columns.Add(LocationColumn);

      this.destinations = destinations;
      DataSource = destinations;
    }

    private readonly DestinationDataset.DestinationDataTable destinations = null;
    private DestinationDataset.DestinationRow destination = null;
  }
}