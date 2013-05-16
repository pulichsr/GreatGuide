namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmEnterLatLong
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.edtLatitudeDegrees = new System.Windows.Forms.TextBox();
      this.edtLongitudeDegrees = new System.Windows.Forms.TextBox();
      this.lblHeading = new System.Windows.Forms.Label();
      this.lblError = new System.Windows.Forms.Label();
      this.pnlDms = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.edtLongitudeSeconds = new System.Windows.Forms.TextBox();
      this.edtLatitudeSeconds = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.edtLongitudeMinutes = new System.Windows.Forms.TextBox();
      this.edtLatitudeMinutes = new System.Windows.Forms.TextBox();
      this.pnlDdd = new System.Windows.Forms.Panel();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.edtLongitudeFractions = new System.Windows.Forms.TextBox();
      this.edtLatitudeFractions = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.edtLongitudeHemisphere = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.edtLatitudeHemisphere = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.btnBackspace = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.btnOk = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton11 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton12 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton13 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton14 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton15 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton16 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton17 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton18 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton19 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.keyboardButton20 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnNextFormat = new System.Windows.Forms.PictureBox();
      this.btnPreviousFormat = new System.Windows.Forms.PictureBox();
      this.edtFormat = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.label12 = new System.Windows.Forms.Label();
      this.pnlDms.SuspendLayout();
      this.pnlDdd.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
      this.label1.Location = new System.Drawing.Point(7, 92);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(87, 20);
      this.label1.Text = "Latitude";
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
      this.label2.Location = new System.Drawing.Point(7, 138);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(87, 20);
      this.label2.Text = "Longitude";
      // 
      // edtLatitudeDegrees
      // 
      this.edtLatitudeDegrees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLatitudeDegrees.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLatitudeDegrees.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLatitudeDegrees.ForeColor = System.Drawing.Color.White;
      this.edtLatitudeDegrees.Location = new System.Drawing.Point(101, 85);
      this.edtLatitudeDegrees.MaxLength = 2;
      this.edtLatitudeDegrees.Name = "edtLatitudeDegrees";
      this.edtLatitudeDegrees.Size = new System.Drawing.Size(50, 35);
      this.edtLatitudeDegrees.TabIndex = 83;
      this.edtLatitudeDegrees.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // edtLongitudeDegrees
      // 
      this.edtLongitudeDegrees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLongitudeDegrees.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLongitudeDegrees.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLongitudeDegrees.ForeColor = System.Drawing.Color.White;
      this.edtLongitudeDegrees.Location = new System.Drawing.Point(101, 131);
      this.edtLongitudeDegrees.MaxLength = 3;
      this.edtLongitudeDegrees.Name = "edtLongitudeDegrees";
      this.edtLongitudeDegrees.Size = new System.Drawing.Size(50, 35);
      this.edtLongitudeDegrees.TabIndex = 84;
      this.edtLongitudeDegrees.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // lblHeading
      // 
      this.lblHeading.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.lblHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.lblHeading.Location = new System.Drawing.Point(95, 0);
      this.lblHeading.Name = "lblHeading";
      this.lblHeading.Size = new System.Drawing.Size(290, 25);
      this.lblHeading.Text = "Enter Latitude and Longitude";
      this.lblHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblError
      // 
      this.lblError.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
      this.lblError.ForeColor = System.Drawing.Color.Firebrick;
      this.lblError.Location = new System.Drawing.Point(7, 230);
      this.lblError.Name = "lblError";
      this.lblError.Size = new System.Drawing.Size(275, 25);
      // 
      // pnlDms
      // 
      this.pnlDms.BackColor = System.Drawing.Color.White;
      this.pnlDms.Controls.Add(this.label4);
      this.pnlDms.Controls.Add(this.label3);
      this.pnlDms.Controls.Add(this.label7);
      this.pnlDms.Controls.Add(this.label8);
      this.pnlDms.Controls.Add(this.edtLongitudeSeconds);
      this.pnlDms.Controls.Add(this.edtLatitudeSeconds);
      this.pnlDms.Controls.Add(this.label5);
      this.pnlDms.Controls.Add(this.label6);
      this.pnlDms.Controls.Add(this.edtLongitudeMinutes);
      this.pnlDms.Controls.Add(this.edtLatitudeMinutes);
      this.pnlDms.Location = new System.Drawing.Point(154, 79);
      this.pnlDms.Name = "pnlDms";
      this.pnlDms.Size = new System.Drawing.Size(150, 90);
      // 
      // label4
      // 
      this.label4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label4.Location = new System.Drawing.Point(0, 43);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(15, 20);
      this.label4.Text = "o";
      // 
      // label3
      // 
      this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label3.Location = new System.Drawing.Point(0, -3);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(15, 20);
      this.label3.Text = "o";
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label7.Location = new System.Drawing.Point(133, 46);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(10, 10);
      this.label7.Text = "\"";
      // 
      // label8
      // 
      this.label8.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label8.Location = new System.Drawing.Point(133, 0);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(10, 10);
      this.label8.Text = "\"";
      // 
      // edtLongitudeSeconds
      // 
      this.edtLongitudeSeconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLongitudeSeconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLongitudeSeconds.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLongitudeSeconds.ForeColor = System.Drawing.Color.White;
      this.edtLongitudeSeconds.Location = new System.Drawing.Point(98, 52);
      this.edtLongitudeSeconds.MaxLength = 2;
      this.edtLongitudeSeconds.Name = "edtLongitudeSeconds";
      this.edtLongitudeSeconds.Size = new System.Drawing.Size(35, 35);
      this.edtLongitudeSeconds.TabIndex = 105;
      this.edtLongitudeSeconds.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // edtLatitudeSeconds
      // 
      this.edtLatitudeSeconds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLatitudeSeconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLatitudeSeconds.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLatitudeSeconds.ForeColor = System.Drawing.Color.White;
      this.edtLatitudeSeconds.Location = new System.Drawing.Point(98, 6);
      this.edtLatitudeSeconds.MaxLength = 2;
      this.edtLatitudeSeconds.Name = "edtLatitudeSeconds";
      this.edtLatitudeSeconds.Size = new System.Drawing.Size(35, 35);
      this.edtLatitudeSeconds.TabIndex = 104;
      this.edtLatitudeSeconds.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // label5
      // 
      this.label5.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label5.Location = new System.Drawing.Point(68, 46);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(10, 10);
      this.label5.Text = "\'";
      // 
      // label6
      // 
      this.label6.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label6.Location = new System.Drawing.Point(68, 0);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(10, 10);
      this.label6.Text = "\'";
      // 
      // edtLongitudeMinutes
      // 
      this.edtLongitudeMinutes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLongitudeMinutes.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLongitudeMinutes.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLongitudeMinutes.ForeColor = System.Drawing.Color.White;
      this.edtLongitudeMinutes.Location = new System.Drawing.Point(33, 52);
      this.edtLongitudeMinutes.MaxLength = 2;
      this.edtLongitudeMinutes.Name = "edtLongitudeMinutes";
      this.edtLongitudeMinutes.Size = new System.Drawing.Size(35, 35);
      this.edtLongitudeMinutes.TabIndex = 103;
      this.edtLongitudeMinutes.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // edtLatitudeMinutes
      // 
      this.edtLatitudeMinutes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLatitudeMinutes.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLatitudeMinutes.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLatitudeMinutes.ForeColor = System.Drawing.Color.White;
      this.edtLatitudeMinutes.Location = new System.Drawing.Point(33, 6);
      this.edtLatitudeMinutes.MaxLength = 2;
      this.edtLatitudeMinutes.Name = "edtLatitudeMinutes";
      this.edtLatitudeMinutes.Size = new System.Drawing.Size(35, 35);
      this.edtLatitudeMinutes.TabIndex = 102;
      this.edtLatitudeMinutes.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // pnlDdd
      // 
      this.pnlDdd.BackColor = System.Drawing.Color.White;
      this.pnlDdd.Controls.Add(this.label9);
      this.pnlDdd.Controls.Add(this.label10);
      this.pnlDdd.Controls.Add(this.label13);
      this.pnlDdd.Controls.Add(this.label14);
      this.pnlDdd.Controls.Add(this.edtLongitudeFractions);
      this.pnlDdd.Controls.Add(this.edtLatitudeFractions);
      this.pnlDdd.Location = new System.Drawing.Point(154, 79);
      this.pnlDdd.Name = "pnlDdd";
      this.pnlDdd.Size = new System.Drawing.Size(150, 90);
      // 
      // label9
      // 
      this.label9.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label9.Location = new System.Drawing.Point(116, 43);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(15, 20);
      this.label9.Text = "o";
      // 
      // label10
      // 
      this.label10.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label10.Location = new System.Drawing.Point(116, -3);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(15, 20);
      this.label10.Text = "o";
      // 
      // label13
      // 
      this.label13.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label13.Location = new System.Drawing.Point(12, 65);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(10, 20);
      this.label13.Text = ".";
      // 
      // label14
      // 
      this.label14.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.label14.Location = new System.Drawing.Point(12, 19);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(10, 20);
      this.label14.Text = ".";
      // 
      // edtLongitudeFractions
      // 
      this.edtLongitudeFractions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLongitudeFractions.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLongitudeFractions.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLongitudeFractions.ForeColor = System.Drawing.Color.White;
      this.edtLongitudeFractions.Location = new System.Drawing.Point(33, 52);
      this.edtLongitudeFractions.MaxLength = 5;
      this.edtLongitudeFractions.Name = "edtLongitudeFractions";
      this.edtLongitudeFractions.Size = new System.Drawing.Size(80, 35);
      this.edtLongitudeFractions.TabIndex = 103;
      this.edtLongitudeFractions.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // edtLatitudeFractions
      // 
      this.edtLatitudeFractions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLatitudeFractions.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtLatitudeFractions.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold);
      this.edtLatitudeFractions.ForeColor = System.Drawing.Color.White;
      this.edtLatitudeFractions.Location = new System.Drawing.Point(33, 6);
      this.edtLatitudeFractions.MaxLength = 5;
      this.edtLatitudeFractions.Name = "edtLatitudeFractions";
      this.edtLatitudeFractions.Size = new System.Drawing.Size(80, 35);
      this.edtLatitudeFractions.TabIndex = 102;
      this.edtLatitudeFractions.GotFocus += new System.EventHandler(this.Editor_GotFocus);
      // 
      // label11
      // 
      this.label11.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
      this.label11.Location = new System.Drawing.Point(7, 46);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(87, 20);
      this.label11.Text = "Format";
      // 
      // edtLongitudeHemisphere
      // 
      this.edtLongitudeHemisphere.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLongitudeHemisphere.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
      this.edtLongitudeHemisphere.ForeColor = System.Drawing.Color.White;
      this.edtLongitudeHemisphere.Location = new System.Drawing.Point(310, 131);
      this.edtLongitudeHemisphere.Name = "edtLongitudeHemisphere";
      this.edtLongitudeHemisphere.Size = new System.Drawing.Size(30, 35);
      this.edtLongitudeHemisphere.TabIndex = 118;
      this.edtLongitudeHemisphere.MouseUp += new System.Windows.Forms.MouseEventHandler(this.edtLongitudeHemisphere_MouseUp);
      // 
      // edtLatitudeHemisphere
      // 
      this.edtLatitudeHemisphere.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtLatitudeHemisphere.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
      this.edtLatitudeHemisphere.ForeColor = System.Drawing.Color.White;
      this.edtLatitudeHemisphere.Location = new System.Drawing.Point(310, 85);
      this.edtLatitudeHemisphere.Name = "edtLatitudeHemisphere";
      this.edtLatitudeHemisphere.Size = new System.Drawing.Size(30, 35);
      this.edtLatitudeHemisphere.TabIndex = 117;
      this.edtLatitudeHemisphere.MouseUp += new System.Windows.Forms.MouseEventHandler(this.edtLatitudeHemisphere_MouseUp);
      // 
      // btnBackspace
      // 
      this.btnBackspace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnBackspace.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnBackspace.ForeColor = System.Drawing.Color.White;
      this.btnBackspace.Location = new System.Drawing.Point(288, 230);
      this.btnBackspace.Name = "btnBackspace";
      this.btnBackspace.Size = new System.Drawing.Size(88, 30);
      this.btnBackspace.TabIndex = 104;
      this.btnBackspace.Text = "<";
      this.btnBackspace.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnBackspace_MouseUp);
      // 
      // btnOk
      // 
      this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnOk.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnOk.ForeColor = System.Drawing.Color.White;
      this.btnOk.Location = new System.Drawing.Point(384, 230);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(88, 30);
      this.btnOk.TabIndex = 78;
      this.btnOk.Text = "OK";
      this.btnOk.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // keyboardButton11
      // 
      this.keyboardButton11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton11.ForeColor = System.Drawing.Color.White;
      this.keyboardButton11.Location = new System.Drawing.Point(430, 184);
      this.keyboardButton11.Name = "keyboardButton11";
      this.keyboardButton11.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton11.TabIndex = 77;
      this.keyboardButton11.Text = "0";
      this.keyboardButton11.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton12
      // 
      this.keyboardButton12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton12.ForeColor = System.Drawing.Color.White;
      this.keyboardButton12.Location = new System.Drawing.Point(101, 184);
      this.keyboardButton12.Name = "keyboardButton12";
      this.keyboardButton12.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton12.TabIndex = 76;
      this.keyboardButton12.Text = "3";
      this.keyboardButton12.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton13
      // 
      this.keyboardButton13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton13.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton13.ForeColor = System.Drawing.Color.White;
      this.keyboardButton13.Location = new System.Drawing.Point(148, 184);
      this.keyboardButton13.Name = "keyboardButton13";
      this.keyboardButton13.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton13.TabIndex = 75;
      this.keyboardButton13.Text = "4";
      this.keyboardButton13.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton14
      // 
      this.keyboardButton14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton14.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton14.ForeColor = System.Drawing.Color.White;
      this.keyboardButton14.Location = new System.Drawing.Point(195, 184);
      this.keyboardButton14.Name = "keyboardButton14";
      this.keyboardButton14.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton14.TabIndex = 74;
      this.keyboardButton14.Text = "5";
      this.keyboardButton14.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton15
      // 
      this.keyboardButton15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton15.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton15.ForeColor = System.Drawing.Color.White;
      this.keyboardButton15.Location = new System.Drawing.Point(242, 184);
      this.keyboardButton15.Name = "keyboardButton15";
      this.keyboardButton15.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton15.TabIndex = 73;
      this.keyboardButton15.Text = "6";
      this.keyboardButton15.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton16
      // 
      this.keyboardButton16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton16.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton16.ForeColor = System.Drawing.Color.White;
      this.keyboardButton16.Location = new System.Drawing.Point(289, 184);
      this.keyboardButton16.Name = "keyboardButton16";
      this.keyboardButton16.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton16.TabIndex = 72;
      this.keyboardButton16.Text = "7";
      this.keyboardButton16.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton17
      // 
      this.keyboardButton17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton17.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton17.ForeColor = System.Drawing.Color.White;
      this.keyboardButton17.Location = new System.Drawing.Point(336, 184);
      this.keyboardButton17.Name = "keyboardButton17";
      this.keyboardButton17.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton17.TabIndex = 71;
      this.keyboardButton17.Text = "8";
      this.keyboardButton17.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton18
      // 
      this.keyboardButton18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton18.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton18.ForeColor = System.Drawing.Color.White;
      this.keyboardButton18.Location = new System.Drawing.Point(383, 184);
      this.keyboardButton18.Name = "keyboardButton18";
      this.keyboardButton18.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton18.TabIndex = 70;
      this.keyboardButton18.Text = "9";
      this.keyboardButton18.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton19
      // 
      this.keyboardButton19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton19.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton19.ForeColor = System.Drawing.Color.White;
      this.keyboardButton19.Location = new System.Drawing.Point(54, 184);
      this.keyboardButton19.Name = "keyboardButton19";
      this.keyboardButton19.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton19.TabIndex = 69;
      this.keyboardButton19.Text = "2";
      this.keyboardButton19.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // keyboardButton20
      // 
      this.keyboardButton20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton20.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton20.ForeColor = System.Drawing.Color.White;
      this.keyboardButton20.Location = new System.Drawing.Point(7, 184);
      this.keyboardButton20.Name = "keyboardButton20";
      this.keyboardButton20.Size = new System.Drawing.Size(40, 37);
      this.keyboardButton20.TabIndex = 68;
      this.keyboardButton20.Text = "1";
      this.keyboardButton20.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnKeyboard_MouseUp);
      // 
      // btnBack
      // 
      this.btnBack.BackColor = System.Drawing.SystemColors.Window;
      this.btnBack.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnBack.ForeColor = System.Drawing.Color.White;
      this.btnBack.LineSeparation = ((short)(1));
      this.btnBack.Location = new System.Drawing.Point(4, 2);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(59, 42);
      this.btnBack.TabIndex = 52;
      this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // btnNextFormat
      // 
      this.btnNextFormat.Location = new System.Drawing.Point(398, 29);
      this.btnNextFormat.Name = "btnNextFormat";
      this.btnNextFormat.Size = new System.Drawing.Size(80, 44);
      this.btnNextFormat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNextFormat_MouseUp);
      // 
      // btnPreviousFormat
      // 
      this.btnPreviousFormat.Location = new System.Drawing.Point(103, 29);
      this.btnPreviousFormat.Name = "btnPreviousFormat";
      this.btnPreviousFormat.Size = new System.Drawing.Size(80, 44);
      this.btnPreviousFormat.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPreviousFormat_MouseUp);
      // 
      // edtFormat
      // 
      this.edtFormat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.edtFormat.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.edtFormat.ForeColor = System.Drawing.Color.White;
      this.edtFormat.LineSeparation = ((short)(1));
      this.edtFormat.Location = new System.Drawing.Point(185, 29);
      this.edtFormat.Name = "edtFormat";
      this.edtFormat.Size = new System.Drawing.Size(211, 44);
      this.edtFormat.TabIndex = 135;
      this.edtFormat.Text = "DMS";
      this.edtFormat.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // label12
      // 
      this.label12.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
      this.label12.Location = new System.Drawing.Point(356, 105);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(114, 44);
      this.label12.Text = "Tap field to enter values";
      // 
      // frmEnterLatLong
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.label12);
      this.Controls.Add(this.edtFormat);
      this.Controls.Add(this.btnNextFormat);
      this.Controls.Add(this.btnPreviousFormat);
      this.Controls.Add(this.label11);
      this.Controls.Add(this.edtLongitudeHemisphere);
      this.Controls.Add(this.edtLatitudeHemisphere);
      this.Controls.Add(this.lblError);
      this.Controls.Add(this.btnBackspace);
      this.Controls.Add(this.lblHeading);
      this.Controls.Add(this.edtLongitudeDegrees);
      this.Controls.Add(this.edtLatitudeDegrees);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.keyboardButton11);
      this.Controls.Add(this.keyboardButton12);
      this.Controls.Add(this.keyboardButton13);
      this.Controls.Add(this.keyboardButton14);
      this.Controls.Add(this.keyboardButton15);
      this.Controls.Add(this.keyboardButton16);
      this.Controls.Add(this.keyboardButton17);
      this.Controls.Add(this.keyboardButton18);
      this.Controls.Add(this.keyboardButton19);
      this.Controls.Add(this.keyboardButton20);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.pnlDdd);
      this.Controls.Add(this.pnlDms);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmEnterLatLong";
      this.Text = "frmDisclaimer";
      this.Load += new System.EventHandler(this.frmEnterLatLong_Load);
      this.pnlDms.ResumeLayout(false);
      this.pnlDdd.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton11;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton12;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton13;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton14;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton15;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton16;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton17;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton18;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton19;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton20;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnOk;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox edtLatitudeDegrees;
    private System.Windows.Forms.TextBox edtLongitudeDegrees;
    private System.Windows.Forms.Label lblHeading;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnBackspace;
    private System.Windows.Forms.Label lblError;
    private Nucleo.GoodGuide.Controls.KeyboardButton edtLatitudeHemisphere;
    private Nucleo.GoodGuide.Controls.KeyboardButton edtLongitudeHemisphere;
    private System.Windows.Forms.Panel pnlDms;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox edtLongitudeSeconds;
    private System.Windows.Forms.TextBox edtLatitudeSeconds;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox edtLongitudeMinutes;
    private System.Windows.Forms.TextBox edtLatitudeMinutes;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel pnlDdd;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.TextBox edtLongitudeFractions;
    private System.Windows.Forms.TextBox edtLatitudeFractions;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.PictureBox btnNextFormat;
    private System.Windows.Forms.PictureBox btnPreviousFormat;
    private Nucleo.GoodGuide.Controls.MultilineTextButton edtFormat;
    private System.Windows.Forms.Label label12;

  }
}