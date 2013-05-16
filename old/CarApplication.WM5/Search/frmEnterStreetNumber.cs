using System;
using System.Reflection;
using System.Windows.Forms;
using Nucleo.Windows.Forms;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;


namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmEnterStreetNumber : Form
  {
    public static DialogResult EnterNumber(DataObjects.Street street,out Int16 number)
    {
      number = 0;

      frmEnterStreetNumber frm = new frmEnterStreetNumber(street);
      DialogResult result = frm.ShowDialog();
      if (result == DialogResult.Cancel)
        return result;
     
      number = frm.Number;

      frm.Dispose();

      return result;
    }

    private frmEnterStreetNumber(DataObjects.Street street)
    {
      Guard.ArgumentNotNull(street, "street");

      InitializeComponent();
      LoadResources();

      this.street = street;

      edtSearch.DataBindings.Add("Text", keyboardController, "Text", true, DataSourceUpdateMode.OnPropertyChanged);

      keyboardController.RegisterKeyboard("Numeric", "123", kb123);
      keyboardController.SelectKeyboard("Numeric");
    }

    private Int16 Number
    {
      get { return number; }
    }
    private void LoadResources()
    {
///*
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnNext.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgNext;

      kb123.Image = CarApplication.Instance.ImageManager.Img123Keyboard;
      kb123.LoadKeyDefinitions(CarApplication.Instance.TemplateManager.GetKeyDefinitions("Nucleo.GoodGuide.CarApplicationResources.Templates.Keyboards.123Keyboard.xml"));
//*/
/*
      Assembly ResourceAssembly = Assembly.LoadFrom(string.Format("{0}CarApplicationResources.WM5.dll", Nucleo.Path.ExecutablePath));
      ImageManager im = new ImageManager(ResourceAssembly, "");
      TemplateManager tm = new TemplateManager(ResourceAssembly);

      btnBack.ActiveBackgroundImage = im.ImgBack;
      btnNext.ActiveBackgroundImage = im.ImgNext;

      kb123.LoadKeyDefinitions(tm.GetKeyDefinitions("Nucleo.GoodGuide.CarApplicationResources.Templates.Keyboards.123Keyboard.xml"));
      kb123.Image = im.Img123Keyboard;
*/
    }

    private void frmEnterStreetNumber_Load(object sender, EventArgs e)
    {
      edtStreetNumbers.Text = street.StreetNumbers;
    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
    private void btnNext_Click(object sender, EventArgs e)
    {
      try
      {
        number = Convert.ToInt16(keyboardController.Text);
      }
      catch
      {
        return;
      }

      DialogResult = DialogResult.OK;
    }
    private void edtSearch_TextChanged(object sender, EventArgs e)
    {
      btnNext.Visible = edtSearch.Text.Trim().Length > 0;
    }

    #region Fields
    private readonly MultipleLayoutKeyboardController keyboardController = new MultipleLayoutKeyboardController();
    private readonly DataObjects.Street street;
   private Int16 number;
    #endregion

  }
}