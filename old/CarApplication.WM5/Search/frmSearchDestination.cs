using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSearchDestination : Form
  {
    public static DialogResult Search(ILogger logger, IDestinationSearchDataProvider dataProvider, ref string initialSearchCriteria)
    {
      frmSearchDestination frm = new frmSearchDestination(logger,dataProvider, initialSearchCriteria);
      DialogResult result = frm.ShowDialog();
      if (result == DialogResult.Cancel)
        return result;

      initialSearchCriteria = frm.dataProvider.SearchCriteria;

      frm.Dispose();

      return result;
    }

    private frmSearchDestination(ILogger logger, IDestinationSearchDataProvider dataProvider, string searchCriteria)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(dataProvider, "dataProvider");

      InitializeComponent();
      LoadResources();

      this.dataProvider = dataProvider;
      this.logger = logger;
      this.initialSearchCriteria = searchCriteria;

      this.dataProvider.DataChanged += dataProvider_DataUpdated;
      this.keyboardController.PropertyChanged += keyboardController_PropertyChanged;

      dataChangedDelegate = DataChanged;

      keyboardController.RegisterKeyboard("Numeric", "123", kb123);
      keyboardController.RegisterKeyboard("Alpha", "ABC", kbAbc);

      edtSearch.DataBindings.Add("Text", keyboardController, "Text", true, DataSourceUpdateMode.OnPropertyChanged);
      btnNextKeyboard.DataBindings.Add("Visible", keyboardController, "HasMultipleKeyboards", true, DataSourceUpdateMode.OnPropertyChanged);
      btnNextKeyboard.DataBindings.Add("Text", keyboardController, "NextKeyboardDisplayText", true, DataSourceUpdateMode.OnPropertyChanged);
      DataBindings.Add("CurrentKeyboard", keyboardController, "CurrentKeyboard", true, DataSourceUpdateMode.Never);

      keyboardController.SelectKeyboard("Alpha");
    }

    public IKeyboard CurrentKeyboard
    {
      get { return currentKeyboard; }
      set
      {
        if (currentKeyboard == value)
          return;

        currentKeyboard = value;

        kb123.Visible = false;
        kbAbc.Visible = false;

        ((Control)currentKeyboard).Visible = true;
      }
    }

    private void LoadResources()
    {
///*
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnNext.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgNext;

      kb123.Image = CarApplication.Instance.ImageManager.Img123Keyboard;
      kb123.LoadKeyDefinitions(CarApplication.Instance.TemplateManager.GetKeyDefinitions("Nucleo.GoodGuide.CarApplicationResources.Templates.Keyboards.123Keyboard.xml"));

      kbAbc.Image = CarApplication.Instance.ImageManager.ImgAbcKeyboard;
      kbAbc.LoadKeyDefinitions(CarApplication.Instance.TemplateManager.GetKeyDefinitions("Nucleo.GoodGuide.CarApplicationResources.Templates.Keyboards.AbcKeyboard.xml"));
//*/
/*
      Assembly ResourceAssembly = Assembly.LoadFrom(string.Format("{0}CarApplicationResources.WM5.dll", Nucleo.Path.ExecutablePath));
      ImageManager im = new ImageManager(ResourceAssembly, "");
      TemplateManager tm = new TemplateManager(ResourceAssembly);
      
      btnBack.ActiveBackgroundImage = im.ImgBack;
      btnNext.ActiveBackgroundImage = im.ImgNext;
 
      kb123.LoadKeyDefinitions(tm.GetKeyDefinitions("Nucleo.GoodGuide.CarApplicationResources.Templates.Keyboards.123Keyboard.xml"));
      kb123.Image = im.Img123Keyboard;

      kbAbc.LoadKeyDefinitions(tm.GetKeyDefinitions("Nucleo.GoodGuide.CarApplicationResources.Templates.Keyboards.AbcKeyboard.xml"));
      kbAbc.Image = im.ImgAbcKeyboard;
*/
    }
    private void DataChanged()
    {
      if (InvokeRequired)
      {
        Invoke(dataChangedDelegate);
      }
      else
      {
        btnNext.Visible = dataProvider.Data.Count > 0;

        if (dataProvider.Data.Count == 0)
          edtStreet.Text = string.Empty;
        else
        {
          edtStreet.Text = string.Format("{0}, {1}", dataProvider.Data[0].Code, dataProvider.Data[0].DestinationTypeDescription);

          if ((dataProvider.Data.Count < 5) && (initialSearchCriteria != keyboardController.Text))
            DialogResult = DialogResult.OK;
        }
      }
    }

    private void frmSearchByStreet_Load(object sender, EventArgs e)
    {
      WaitCursor.Show(false);
      keyboardController.Text = initialSearchCriteria;
    }
    private void frmSearchByStreet_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.keyboardController.PropertyChanged -= keyboardController_PropertyChanged;
      this.dataProvider.DataChanged -= dataProvider_DataUpdated;
    }
    private void dataProvider_DataUpdated(object sender, EventArgs e)
    {
      DataChanged();
    }

    private void btnNextKeyboard_Click(object sender, EventArgs e)
    {
      keyboardController.SelectNextKeyboard();
    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btnNext_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }
    private void keyboardController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "Text")
      {
        try
        {
          dataProvider.SearchCriteria = keyboardController.Text;
        }
        catch (Exception exc)
        {
          logger.Write(this,"Error getting matching data",exc);
        }
      }
    }

    #region Fields
    private readonly MultipleLayoutKeyboardController keyboardController = new MultipleLayoutKeyboardController();
    private readonly ILogger logger;
    private readonly IDestinationSearchDataProvider dataProvider;
    private readonly string initialSearchCriteria;
    private IKeyboard currentKeyboard;

    private readonly MethodDelegate dataChangedDelegate;
    #endregion

  }
}