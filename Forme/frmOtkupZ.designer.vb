<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOtkupZ
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.dgvPrikaz = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnIzborFirma2 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnIzborFirma1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblOpis1 = New System.Windows.Forms.Label()
        Me.btnNovaFirma = New System.Windows.Forms.Button()
        Me.btnIzborFirma = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox0 = New System.Windows.Forms.TextBox()
        Me.Label0 = New System.Windows.Forms.Label()
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblNaslov = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblDodOpc = New System.Windows.Forms.Label()
        Me.ToolStrip4 = New System.Windows.Forms.ToolStrip()
        Me.tsdbOpcije = New System.Windows.Forms.ToolStripDropDownButton()
        Me.tsbStavke = New System.Windows.Forms.ToolStripButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblStandOpc = New System.Windows.Forms.Label()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.btnZatvori = New System.Windows.Forms.ToolStripButton()
        Me.btnPretrazi = New System.Windows.Forms.ToolStripDropDownButton()
        Me.PretragaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PočetnoStanjeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnStampaj = New System.Windows.Forms.ToolStripDropDownButton()
        Me.PregledŠtampeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PDFToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PolozajToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnIsprazniPolja = New System.Windows.Forms.Button()
        Me.btnIzmeni = New System.Windows.Forms.Button()
        Me.btnUpisi = New System.Windows.Forms.Button()
        Me.btnBrisi = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DataSetBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.dgvPrikaz, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.ToolStrip4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.DataSetBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPrikaz
        '
        Me.dgvPrikaz.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPrikaz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPrikaz.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvPrikaz.Location = New System.Drawing.Point(12, 24)
        Me.dgvPrikaz.Name = "dgvPrikaz"
        Me.dgvPrikaz.Size = New System.Drawing.Size(829, 213)
        Me.dgvPrikaz.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.StatusStrip2)
        Me.Panel1.Location = New System.Drawing.Point(12, 243)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(829, 240)
        Me.Panel1.TabIndex = 4
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox5)
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Controls.Add(Me.Button6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.TextBox4)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.btnIzborFirma2)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TextBox3)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.btnIzborFirma1)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(451, 25)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(347, 136)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Poljoprivredni proizvojač"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(128, 89)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(100, 20)
        Me.TextBox5.TabIndex = 65
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(313, 89)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(32, 20)
        Me.Button5.TabIndex = 64
        Me.Button5.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Button5, "Dodaj novi")
        Me.Button5.UseVisualStyleBackColor = True
        Me.Button5.Visible = False
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(277, 89)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(30, 20)
        Me.Button6.TabIndex = 63
        Me.Button6.TabStop = False
        Me.Button6.Text = ". . ."
        Me.ToolTip1.SetToolTip(Me.Button6, "Izaberi postojeći")
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Label6"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(128, 53)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(100, 20)
        Me.TextBox4.TabIndex = 61
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(313, 53)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(32, 20)
        Me.Button3.TabIndex = 60
        Me.Button3.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Button3, "Dodaj novi")
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'btnIzborFirma2
        '
        Me.btnIzborFirma2.Location = New System.Drawing.Point(277, 53)
        Me.btnIzborFirma2.Name = "btnIzborFirma2"
        Me.btnIzborFirma2.Size = New System.Drawing.Size(30, 20)
        Me.btnIzborFirma2.TabIndex = 59
        Me.btnIzborFirma2.TabStop = False
        Me.btnIzborFirma2.Text = ". . ."
        Me.ToolTip1.SetToolTip(Me.btnIzborFirma2, "Izaberi postojeći")
        Me.btnIzborFirma2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Label5"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(128, 20)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(100, 20)
        Me.TextBox3.TabIndex = 57
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(313, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(32, 20)
        Me.Button1.TabIndex = 56
        Me.Button1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.Button1, "Dodaj novi")
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnIzborFirma1
        '
        Me.btnIzborFirma1.Location = New System.Drawing.Point(277, 20)
        Me.btnIzborFirma1.Name = "btnIzborFirma1"
        Me.btnIzborFirma1.Size = New System.Drawing.Size(30, 20)
        Me.btnIzborFirma1.TabIndex = 55
        Me.btnIzborFirma1.TabStop = False
        Me.btnIzborFirma1.Text = ". . ."
        Me.ToolTip1.SetToolTip(Me.btnIzborFirma1, "Izaberi postojeći")
        Me.btnIzborFirma1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Label4"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblOpis1)
        Me.GroupBox1.Controls.Add(Me.btnNovaFirma)
        Me.GroupBox1.Controls.Add(Me.btnIzborFirma)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBox0)
        Me.GroupBox1.Controls.Add(Me.Label0)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(392, 136)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Broj otkupnog lista i datum"
        '
        'lblOpis1
        '
        Me.lblOpis1.AutoSize = True
        Me.lblOpis1.ForeColor = System.Drawing.Color.Blue
        Me.lblOpis1.Location = New System.Drawing.Point(346, 95)
        Me.lblOpis1.Name = "lblOpis1"
        Me.lblOpis1.Size = New System.Drawing.Size(32, 13)
        Me.lblOpis1.TabIndex = 52
        Me.lblOpis1.Text = "opis1"
        Me.lblOpis1.Visible = False
        '
        'btnNovaFirma
        '
        Me.btnNovaFirma.Location = New System.Drawing.Point(308, 89)
        Me.btnNovaFirma.Name = "btnNovaFirma"
        Me.btnNovaFirma.Size = New System.Drawing.Size(32, 20)
        Me.btnNovaFirma.TabIndex = 51
        Me.btnNovaFirma.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnNovaFirma, "Dodaj novi")
        Me.btnNovaFirma.UseVisualStyleBackColor = True
        Me.btnNovaFirma.Visible = False
        '
        'btnIzborFirma
        '
        Me.btnIzborFirma.Location = New System.Drawing.Point(272, 89)
        Me.btnIzborFirma.Name = "btnIzborFirma"
        Me.btnIzborFirma.Size = New System.Drawing.Size(30, 20)
        Me.btnIzborFirma.TabIndex = 50
        Me.btnIzborFirma.TabStop = False
        Me.btnIzborFirma.Text = ". . ."
        Me.ToolTip1.SetToolTip(Me.btnIzborFirma, "Izaberi postojeći")
        Me.btnIzborFirma.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(272, 53)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(19, 20)
        Me.DateTimePicker1.TabIndex = 35
        Me.DateTimePicker1.TabStop = False
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(145, 89)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 20)
        Me.TextBox2.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Label3"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(145, 53)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Label2"
        '
        'TextBox0
        '
        Me.TextBox0.Location = New System.Drawing.Point(145, 20)
        Me.TextBox0.Name = "TextBox0"
        Me.TextBox0.Size = New System.Drawing.Size(100, 20)
        Me.TextBox0.TabIndex = 1
        '
        'Label0
        '
        Me.Label0.AutoSize = True
        Me.Label0.Location = New System.Drawing.Point(6, 27)
        Me.Label0.Name = "Label0"
        Me.Label0.Size = New System.Drawing.Size(39, 13)
        Me.Label0.TabIndex = 0
        Me.Label0.Text = "Label1"
        '
        'StatusStrip2
        '
        Me.StatusStrip2.AutoSize = False
        Me.StatusStrip2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.StatusStrip2.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(827, 22)
        Me.StatusStrip2.TabIndex = 4
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = False
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(5, 3, 5, 2)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(802, 17)
        Me.lblStatus.Spring = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblNaslov})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(854, 22)
        Me.StatusStrip1.TabIndex = 29
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblNaslov
        '
        Me.lblNaslov.AutoSize = False
        Me.lblNaslov.Margin = New System.Windows.Forms.Padding(5, 3, 5, 2)
        Me.lblNaslov.Name = "lblNaslov"
        Me.lblNaslov.Size = New System.Drawing.Size(829, 17)
        Me.lblNaslov.Spring = True
        Me.lblNaslov.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblDodOpc)
        Me.Panel2.Controls.Add(Me.ToolStrip4)
        Me.Panel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel2.Location = New System.Drawing.Point(418, 488)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(166, 70)
        Me.Panel2.TabIndex = 31
        '
        'lblDodOpc
        '
        Me.lblDodOpc.AutoSize = True
        Me.lblDodOpc.Location = New System.Drawing.Point(42, 55)
        Me.lblDodOpc.Name = "lblDodOpc"
        Me.lblDodOpc.Size = New System.Drawing.Size(79, 13)
        Me.lblDodOpc.TabIndex = 1
        Me.lblDodOpc.Text = "Dodatne opcije"
        '
        'ToolStrip4
        '
        Me.ToolStrip4.AutoSize = False
        Me.ToolStrip4.BackColor = System.Drawing.Color.White
        Me.ToolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip4.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsdbOpcije, Me.tsbStavke})
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.Size = New System.Drawing.Size(164, 55)
        Me.ToolStrip4.TabIndex = 0
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'tsdbOpcije
        '
        Me.tsdbOpcije.AutoSize = False
        Me.tsdbOpcije.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsdbOpcije.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsdbOpcije.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.tsdbOpcije.Name = "tsdbOpcije"
        Me.tsdbOpcije.Size = New System.Drawing.Size(70, 50)
        Me.tsdbOpcije.Text = "Opcije"
        Me.tsdbOpcije.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'tsbStavke
        '
        Me.tsbStavke.AutoSize = False
        Me.tsbStavke.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbStavke.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbStavke.Name = "tsbStavke"
        Me.tsbStavke.Size = New System.Drawing.Size(70, 50)
        Me.tsbStavke.Text = "Stavke"
        Me.tsbStavke.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblStandOpc)
        Me.Panel3.Controls.Add(Me.ToolStrip3)
        Me.Panel3.Location = New System.Drawing.Point(592, 488)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(250, 70)
        Me.Panel3.TabIndex = 32
        '
        'lblStandOpc
        '
        Me.lblStandOpc.AutoSize = True
        Me.lblStandOpc.Location = New System.Drawing.Point(81, 55)
        Me.lblStandOpc.Name = "lblStandOpc"
        Me.lblStandOpc.Size = New System.Drawing.Size(93, 13)
        Me.lblStandOpc.TabIndex = 20
        Me.lblStandOpc.Text = "Standardne opcije"
        '
        'ToolStrip3
        '
        Me.ToolStrip3.AutoSize = False
        Me.ToolStrip3.BackColor = System.Drawing.Color.White
        Me.ToolStrip3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnZatvori, Me.btnPretrazi, Me.btnStampaj})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStrip3.Size = New System.Drawing.Size(248, 55)
        Me.ToolStrip3.TabIndex = 19
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'btnZatvori
        '
        Me.btnZatvori.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnZatvori.AutoSize = False
        Me.btnZatvori.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnZatvori.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnZatvori.Margin = New System.Windows.Forms.Padding(3)
        Me.btnZatvori.Name = "btnZatvori"
        Me.btnZatvori.Size = New System.Drawing.Size(70, 50)
        Me.btnZatvori.Text = "Zatvori"
        Me.btnZatvori.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btnPretrazi
        '
        Me.btnPretrazi.AutoSize = False
        Me.btnPretrazi.AutoToolTip = False
        Me.btnPretrazi.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PretragaToolStripMenuItem, Me.PočetnoStanjeToolStripMenuItem})
        Me.btnPretrazi.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnPretrazi.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPretrazi.Margin = New System.Windows.Forms.Padding(3)
        Me.btnPretrazi.Name = "btnPretrazi"
        Me.btnPretrazi.Size = New System.Drawing.Size(70, 50)
        Me.btnPretrazi.Text = "Pretraži"
        Me.btnPretrazi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PretragaToolStripMenuItem
        '
        Me.PretragaToolStripMenuItem.Name = "PretragaToolStripMenuItem"
        Me.PretragaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PretragaToolStripMenuItem.Text = "Pretraga"
        '
        'PočetnoStanjeToolStripMenuItem
        '
        Me.PočetnoStanjeToolStripMenuItem.Name = "PočetnoStanjeToolStripMenuItem"
        Me.PočetnoStanjeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.PočetnoStanjeToolStripMenuItem.Text = "Početno stanje"
        '
        'btnStampaj
        '
        Me.btnStampaj.AutoSize = False
        Me.btnStampaj.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PregledŠtampeToolStripMenuItem, Me.PDFToolStripMenuItem, Me.WordToolStripMenuItem, Me.ExcelToolStripMenuItem, Me.PolozajToolStripMenuItem})
        Me.btnStampaj.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnStampaj.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnStampaj.Margin = New System.Windows.Forms.Padding(3)
        Me.btnStampaj.Name = "btnStampaj"
        Me.btnStampaj.Size = New System.Drawing.Size(70, 50)
        Me.btnStampaj.Text = "Štampaj"
        Me.btnStampaj.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PregledŠtampeToolStripMenuItem
        '
        Me.PregledŠtampeToolStripMenuItem.Name = "PregledŠtampeToolStripMenuItem"
        Me.PregledŠtampeToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.PregledŠtampeToolStripMenuItem.Text = "Pregled štampe"
        '
        'PDFToolStripMenuItem
        '
        Me.PDFToolStripMenuItem.Name = "PDFToolStripMenuItem"
        Me.PDFToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.PDFToolStripMenuItem.Text = "Export PDF"
        '
        'WordToolStripMenuItem
        '
        Me.WordToolStripMenuItem.Name = "WordToolStripMenuItem"
        Me.WordToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.WordToolStripMenuItem.Text = "Export Word"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.ExcelToolStripMenuItem.Text = "Export Excel"
        '
        'PolozajToolStripMenuItem
        '
        Me.PolozajToolStripMenuItem.Name = "PolozajToolStripMenuItem"
        Me.PolozajToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.PolozajToolStripMenuItem.Text = "Uspravno"
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.Panel5)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel4.Location = New System.Drawing.Point(12, 488)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(400, 70)
        Me.Panel4.TabIndex = 34
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.btnIsprazniPolja)
        Me.Panel5.Controls.Add(Me.btnIzmeni)
        Me.Panel5.Controls.Add(Me.btnUpisi)
        Me.Panel5.Controls.Add(Me.btnBrisi)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(398, 56)
        Me.Panel5.TabIndex = 50
        '
        'btnIsprazniPolja
        '
        Me.btnIsprazniPolja.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIsprazniPolja.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIsprazniPolja.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIsprazniPolja.Location = New System.Drawing.Point(295, 13)
        Me.btnIsprazniPolja.Name = "btnIsprazniPolja"
        Me.btnIsprazniPolja.Size = New System.Drawing.Size(95, 35)
        Me.btnIsprazniPolja.TabIndex = 3
        Me.btnIsprazniPolja.Text = "Isprazni polja"
        Me.btnIsprazniPolja.UseVisualStyleBackColor = True
        '
        'btnIzmeni
        '
        Me.btnIzmeni.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIzmeni.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIzmeni.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIzmeni.Location = New System.Drawing.Point(103, 13)
        Me.btnIzmeni.Name = "btnIzmeni"
        Me.btnIzmeni.Size = New System.Drawing.Size(90, 35)
        Me.btnIzmeni.TabIndex = 1
        Me.btnIzmeni.Text = "Izmeni"
        Me.btnIzmeni.UseVisualStyleBackColor = True
        '
        'btnUpisi
        '
        Me.btnUpisi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpisi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpisi.Location = New System.Drawing.Point(7, 13)
        Me.btnUpisi.Name = "btnUpisi"
        Me.btnUpisi.Size = New System.Drawing.Size(90, 35)
        Me.btnUpisi.TabIndex = 0
        Me.btnUpisi.Text = "Upiši"
        Me.btnUpisi.UseVisualStyleBackColor = True
        '
        'btnBrisi
        '
        Me.btnBrisi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBrisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrisi.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrisi.Location = New System.Drawing.Point(199, 13)
        Me.btnBrisi.Name = "btnBrisi"
        Me.btnBrisi.Size = New System.Drawing.Size(90, 35)
        Me.btnBrisi.TabIndex = 2
        Me.btnBrisi.Text = "Briši"
        Me.btnBrisi.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(163, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Rad sa bazom"
        '
        'DataSetBindingSource
        '
        Me.DataSetBindingSource.DataSource = GetType(System.Data.DataSet)
        Me.DataSetBindingSource.Position = 0
        '
        'frmOtkupZ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(854, 562)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.dgvPrikaz)
        Me.Name = "frmOtkupZ"
        Me.Text = "Pattern form"
        CType(Me.dgvPrikaz, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.DataSetBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected Friend WithEvents dgvPrikaz As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblNaslov As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip4 As System.Windows.Forms.ToolStrip
    Friend WithEvents lblDodOpc As System.Windows.Forms.Label
    Friend WithEvents tsdbOpcije As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblStandOpc As System.Windows.Forms.Label
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnZatvori As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPretrazi As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents btnStampaj As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents PregledŠtampeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PDFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PolozajToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PretragaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PočetnoStanjeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents tsbStavke As ToolStripButton
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents btnBrisi As Button
    Friend WithEvents btnUpisi As Button
    Friend WithEvents btnIzmeni As Button
    Friend WithEvents btnIsprazniPolja As Button
    Friend WithEvents DataSetBindingSource As BindingSource
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox0 As TextBox
    Friend WithEvents Label0 As Label
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Button1 As Button
    Friend WithEvents btnIzborFirma1 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents lblOpis1 As Label
    Friend WithEvents btnNovaFirma As Button
    Friend WithEvents btnIzborFirma As Button
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents btnIzborFirma2 As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox3 As TextBox
End Class

