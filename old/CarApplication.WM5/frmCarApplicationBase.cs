using System;
using System.Collections.Generic;
using System.Drawing;
using Nucleo.GoodGuide.Controls;
using Nucleo.Windows.Forms.DynamicForms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmCarApplicationBase : frmDynamicBase
  {
    public frmCarApplicationBase()
    {
      InitializeComponent();

      AddNavigationControl(btnBack);
      AddNavigationControl(btnMap);
      AddNavigationControl(btnSoftKey1);
      AddNavigationControl(btnSoftKey2);
      AddNavigationControl(btnSoftKey3);
      AddNavigationControl(btnSoftKey4);
      AddNavigationControl(btnSoftKey5);

      softKeys.Add(btnSoftKey1);
      softKeys.Add(btnSoftKey2);
      softKeys.Add(btnSoftKey3);
      softKeys.Add(btnSoftKey4);
      softKeys.Add(btnSoftKey5);

      LoadResources();
    }

    public override void BuildPresentation(FormDefinition formDefinition, object formData)
    {
      CarApplication.Instance.WriteLog(this, ">> BuildPresentation");
      base.BuildPresentation(formDefinition, formData);

      CarApplication.Instance.WriteLog(this, string.Format("formDefinition.GraphicResourceName: {0}", formDefinition.GraphicResourceName));
      CarApplication.Instance.WriteLog(this, string.Format("formDefinition.Text: {0}", formDefinition.Text));

      if (string.IsNullOrEmpty(formDefinition.GraphicResourceName) == false)
      {
        CarApplication.Instance.WriteLog(this, "  >> formDefinition.GraphicResourceName");

        pbMainHeading.Image = CarApplication.Instance.ImageManager[formDefinition.GraphicResourceName];
        pbMainHeading.Visible = true;
        lblMainHeading.Visible = false;
      }
      else
        if (string.IsNullOrEmpty(formDefinition.Text) == false)
        {
          CarApplication.Instance.WriteLog(this, "  >> formDefinition.Text");

          lblMainHeading.Text = formDefinition.Text;
          lblMainHeading.Visible = true;
          pbMainHeading.Visible = false;
        }

      for (Int32 ControlDefinitionNo = 0; ControlDefinitionNo < formDefinition.Controls.Count; ControlDefinitionNo++)
      {
        ControlDefinition ControlDefinition = formDefinition.Controls[ControlDefinitionNo];

        MultilineTextButton SoftKey = softKeys.Find(delegate(MultilineTextButton item) { return item.Name == ControlDefinition.Name; });
        if (SoftKey == null)
          continue;

        SoftKey.Text = ControlDefinition.Text;
        SoftKey.Visible = true;
        SoftKey.Enabled = true;
      }
    }

    protected MultilineTextButton BtnBack
    {
      get { return btnBack; }
    }
    protected MultilineTextButton BtnMap
    {
      get { return btnMap; }
    }
    protected MultilineTextButton BtnSoftKey1
    {
      get { return btnSoftKey1; }
    }
    protected MultilineTextButton BtnSoftKey2
    {
      get { return btnSoftKey2; }
    }
    protected MultilineTextButton BtnSoftKey3
    {
      get { return btnSoftKey3; }
    }
    protected MultilineTextButton BtnSoftKey4
    {
      get { return btnSoftKey4; }
    }
    protected MultilineTextButton BtnSoftKey5
    {
      get { return btnSoftKey5; }
    }

    protected Image HeadingImage
    {
      set
      {
        pbMainHeading.Image = value;
      }
    }
    protected string HeadingText
    {
      set
      {
        lblMainHeading.Text = value;
      }
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnMap.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgMap;

      BtnSoftKey1.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      BtnSoftKey1.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      BtnSoftKey2.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      BtnSoftKey2.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      BtnSoftKey3.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      BtnSoftKey3.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      BtnSoftKey4.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      BtnSoftKey4.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;

      BtnSoftKey5.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgActiveSoftkey;
      BtnSoftKey5.InactiveBackgroundImage = CarApplication.Instance.ImageManager.ImgInactiveSoftkey;
    }
    protected override void NavigationControlClicked(System.Windows.Forms.Control navigationControl, ref bool handled, ref object clickData)
    {
      CarApplication.Instance.AudibleFeedback();

      base.NavigationControlClicked(navigationControl, ref handled, ref clickData);
    }

    private readonly List<MultilineTextButton> softKeys = new List<MultilineTextButton>();
  }
  
}