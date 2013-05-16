using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Controls;
using Nucleo.Windows.Forms.DynamicForms;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmEightButton : frmCarApplicationBase
  {

    public frmEightButton()
    {
      InitializeComponent();

      AddNavigationControl(btn1);
      AddNavigationControl(btn2);
      AddNavigationControl(btn3);
      AddNavigationControl(btn4);
      AddNavigationControl(btn5);
      AddNavigationControl(btn6);
      AddNavigationControl(btn7);
      AddNavigationControl(btn8);

      buttons.Add(btn1);
      buttons.Add(btn2);
      buttons.Add(btn3);
      buttons.Add(btn4);
      buttons.Add(btn5);
      buttons.Add(btn6);
      buttons.Add(btn7);
      buttons.Add(btn8);
    }

    public override void BuildPresentation(FormDefinition formDefinition, object formData)
    {
      base.BuildPresentation(formDefinition, formData);

      for (Int32 ControlDefinitionNo = 0;ControlDefinitionNo < formDefinition.Controls.Count;ControlDefinitionNo++)
      {
        ControlDefinition ControlDefinition = formDefinition.Controls[ControlDefinitionNo];

        TwoLineMenuButton Button = buttons.Find(delegate(TwoLineMenuButton item) { return item.Name == ControlDefinition.Name; });
        if (Button == null)
          continue;

        Button.Text = ControlDefinition.Text;
        Button.DescriptionText = ControlDefinition.Description;
        Button.Visible = true;
        Button.BackColor = ControlDefinition.Color;
        Button.ActiveBackgroundImage = CarApplication.Instance.ImageManager[ControlDefinition.GraphicResourceName];
      }

    }

    protected override void NavigationControlClicked(System.Windows.Forms.Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled, ref clickData);
      if (handled == true)
      {
        return;
      }

      if (navigationControl == BtnMap)
      {
        handled = true;
        Close();
        return;
      }
    }

    private readonly List<TwoLineMenuButton> buttons = new List<TwoLineMenuButton>();
  }
}