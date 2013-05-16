using System;
using System.Windows.Forms;
using System.Drawing;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmEnterLatLong : Form
  {
    public enum LatitudeHemispheres
    {
      North,
      South,
    }
    public enum LongitudeHemispheres
    {
      East,
      West,
    }
    public enum EntryFormats
    {
      Dms,
      Ddd,
    }

    public static DialogResult EnterValues(out double latitude, out double longitude)
    {
      frmEnterLatLong frm = new frmEnterLatLong();

      DialogResult Result = frm.ShowDialog();
      latitude = frm.latitude;
      longitude = frm.longitude;

      return Result;
    }

    private EntryFormats EntryFormat
    {
      get { return entryFormat; }
      set
      {
        entryFormat = value;
        pnlDms.Visible = entryFormat == EntryFormats.Dms;
        pnlDdd.Visible = entryFormat == EntryFormats.Ddd;

        edtFormat.Text = EntryFormat == EntryFormats.Dms ? "DMS e.g. 34deg 20' 30\"" : "D.DDD e.g. 34.2034deg";
      }
    }

    private frmEnterLatLong()
    {
      InitializeComponent();

      LoadResources();
    }

    private TextBox FocusedControl
    {
      set
      {
        if (focusedControl != null)
          focusedControl.BackColor = Color.FromArgb(94,136,35); 

        focusedControl = value;

        if (focusedControl != null)
          focusedControl.BackColor = Color.FromArgb(134,94,113);
      }
    }
    private LatitudeHemispheres LatitudeHemisphere
    {
      set
      {
        latitudeHemisphere = value;

        switch (value)
        {
          case LatitudeHemispheres.South:
            edtLatitudeHemisphere.Text = "S";
            break;
          case LatitudeHemispheres.North:
            edtLatitudeHemisphere.Text = "N";
            break;
        }
      }
    }
    private LongitudeHemispheres LongitudeHemisphere
    {
      set
      {
        longitudeHemisphere = value;

        switch (value)
        {
          case LongitudeHemispheres.East:
            edtLongitudeHemisphere.Text = "E";
            break;
          case LongitudeHemispheres.West:
            edtLongitudeHemisphere.Text = "W";
            break;
        }
      }
    }

    private void LoadResources()
    {
      btnPreviousFormat.Image = CarApplication.Instance.ImageManager.ImgSelectorPrevious;
      btnNextFormat.Image = CarApplication.Instance.ImageManager.ImgSelectorNext;

      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
    }
    private Boolean ValidateValues()
    {
      Boolean Result = true;

      #region Latitude degrees
      if (edtLatitudeDegrees.Text.Length == 0)
      {
        edtLatitudeDegrees.Text = "0";
      }

      Int16 LatitudeDegrees;
      try
      {
        LatitudeDegrees = Convert.ToInt16(edtLatitudeDegrees.Text);
      }
      catch
      {
        lblError.Text = "Invalid latitude degrees";
        return false;
      }
      if (LatitudeDegrees > 90)
      {
        lblError.Text = "Invalid latitude degrees";
        return false;
      }
      #endregion

      #region Longitude degrees
      if (edtLongitudeDegrees.Text.Length == 0)
      {
        edtLongitudeDegrees.Text = "0";
      }
      Int16 LongitudeDegrees;
      try
      {
        LongitudeDegrees = Convert.ToInt16(edtLongitudeDegrees.Text);
      }
      catch
      {
        lblError.Text = "Invalid longitude degrees";
        return false;
      }
      if (LongitudeDegrees > 180)
      {
        lblError.Text = "Invalid longitude degrees";
        return false;
      }
      #endregion

      switch (EntryFormat)
      {
        case EntryFormats.Dms:

          #region Latitude minutes
          if (edtLatitudeMinutes.Text.Length == 0)
          {
            edtLatitudeMinutes.Text = "0";
          }
          Int16 LatitudeMinutes;
          try
          {
            LatitudeMinutes = Convert.ToInt16(edtLatitudeMinutes.Text);
          }
          catch
          {
            lblError.Text = "Invalid latitude minutes";
            return false;
          }
          if (LatitudeMinutes > 59)
          {
            lblError.Text = "Invalid latitude minutes";
            return false;
          }
          #endregion

          #region Latitude seconds
          if (edtLatitudeSeconds.Text.Length == 0)
          {
            edtLatitudeSeconds.Text = "0";
          }
          Int16 LatitudeSeconds;
          try
          {
            LatitudeSeconds = Convert.ToInt16(edtLatitudeSeconds.Text);
          }
          catch
          {
            lblError.Text = "Invalid latitude seconds";
            return false;
          }
          if (LatitudeSeconds > 59)
          {
            lblError.Text = "Invalid latitude seconds";
            return false;
          }
          #endregion

          #region Longitude minutes
          if (edtLongitudeMinutes.Text.Length == 0)
          {
            edtLongitudeMinutes.Text = "0";
          }
          Int16 LongitudeMinutes;
          try
          {
            LongitudeMinutes = Convert.ToInt16(edtLongitudeMinutes.Text);
          }
          catch
          {
            lblError.Text = "Invalid longitude minutes";
            return false;
          }
          if (LongitudeMinutes > 59)
          {
            lblError.Text = "Invalid longitude minutes";
            return false;
          }
          #endregion

          #region Longitude seconds
          if (edtLongitudeSeconds.Text.Length == 0)
          {
            edtLongitudeSeconds.Text = "0";
          }
          Int16 LongitudeSeconds;
          try
          {
            LongitudeSeconds = Convert.ToInt16(edtLongitudeSeconds.Text);
          }
          catch
          {
            lblError.Text = "Invalid longitude seconds";
            return false;
          }
          if (LongitudeSeconds > 59)
          {
            lblError.Text = "Invalid longitude seconds";
            return false;
          }
          #endregion

          latitude = LatitudeDegrees + (double)LatitudeMinutes / 60 + (double)LatitudeSeconds / 3600;
          longitude = LongitudeDegrees + (double)LongitudeMinutes / 60 + (double)LongitudeSeconds / 3600;
          break;
        case EntryFormats.Ddd:

          #region Latitude fractions
          if (edtLatitudeFractions.Text.Length == 0)
          {
            edtLatitudeFractions.Text = "0";
          }

          string fractionString = string.Format("0.{0}",edtLatitudeFractions.Text);
          double LatitudeFractions;
          try
          {
            LatitudeFractions = Convert.ToDouble(fractionString);
          }
          catch
          {
            lblError.Text = "Invalid latitude fractions";
            return false;
          }
          #endregion

          #region Longitude fractions
          if (edtLongitudeFractions.Text.Length == 0)
          {
            edtLongitudeFractions.Text = "0";
          }
          fractionString = string.Format("0.{0}", edtLongitudeFractions.Text);
          double LongitudeFractions;
          try
          {
            LongitudeFractions = Convert.ToDouble(fractionString);
          }
          catch
          {
            lblError.Text = "Invalid longitude fractions";
            return false;
          }
          #endregion

          latitude = LatitudeDegrees + LatitudeFractions;
          longitude = LongitudeDegrees + LongitudeFractions;
          break;
      }

      if (latitudeHemisphere == LatitudeHemispheres.South)
        latitude *= -1;
      if (longitudeHemisphere == LongitudeHemispheres.West)
        longitude *= -1;

      return Result;
    }
    private void ToggleLatitudeHemisphere()
    {
      switch (latitudeHemisphere)
      {
        case LatitudeHemispheres.South:
          LatitudeHemisphere = LatitudeHemispheres.North;
          break;
        case LatitudeHemispheres.North:
          LatitudeHemisphere = LatitudeHemispheres.South;
          break;
      }
    }
    private void ToggleLongitudeHemisphere()
    {
      switch (longitudeHemisphere)
      {
        case LongitudeHemispheres.East:
          LongitudeHemisphere = LongitudeHemispheres.West;
          break;
        case LongitudeHemispheres.West:
          LongitudeHemisphere = LongitudeHemispheres.East;
          break;
      }
    }
    private void ToggleEntryFormat()
    {
      switch(EntryFormat)
      {
        case EntryFormats.Dms:
          EntryFormat = EntryFormats.Ddd;
          break;
        case EntryFormats.Ddd:
          EntryFormat = EntryFormats.Dms;
          break;
      }
    }

    private void frmEnterLatLong_Load(object sender, EventArgs e)
    {
      FocusedControl = edtLatitudeDegrees;

      LatitudeHemisphere = LatitudeHemispheres.South;
      LongitudeHemisphere = LongitudeHemispheres.East;

      EntryFormat = entryFormat;
    }
    private void btnOK_Click(object sender, EventArgs e)
    {
      if (ValidateValues() == false)
        return;

      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
    private void btnKeyboard_MouseUp(object sender, MouseEventArgs e)
    {
      lblError.Text = string.Empty;

      if (focusedControl == null)
        return;

      if (focusedControl.Text.Length == focusedControl.MaxLength)
        return;

      focusedControl.Text += ((Control)sender).Text;
    }
    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btnBackspace_MouseUp(object sender, MouseEventArgs e)
    {
      lblError.Text = string.Empty;

      if (focusedControl == null)
        return;

      if (focusedControl.Text.Length == 0)
        return;

      focusedControl.Text = focusedControl.Text.Substring(0, focusedControl.Text.Length - 1);
    }
    private void Editor_GotFocus(object sender, EventArgs e)
    {
      if (sender is TextBox)
        FocusedControl = (TextBox)sender;
    }
    private void edtLongitudeHemisphere_MouseUp(object sender, MouseEventArgs e)
    {
      ToggleLongitudeHemisphere();
    }
    private void edtLatitudeHemisphere_MouseUp(object sender, MouseEventArgs e)
    {
      ToggleLatitudeHemisphere();
    }
    private void btnPreviousFormat_MouseUp(object sender, MouseEventArgs e)
    {
      ToggleEntryFormat();
    }
    private void btnNextFormat_MouseUp(object sender, MouseEventArgs e)
    {
      ToggleEntryFormat();
    }

    private TextBox focusedControl = null;
    private double latitude = 0;
    private double longitude = 0;
    private LatitudeHemispheres latitudeHemisphere;
    private LongitudeHemispheres longitudeHemisphere;
    private static EntryFormats entryFormat = EntryFormats.Dms;

  }
}