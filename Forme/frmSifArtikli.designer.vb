<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSifArtikli
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.dgvPrikaz = New System.Windows.Forms.DataGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblOpis1 = New System.Windows.Forms.Label()
        Me.btnNovaFirma = New System.Windows.Forms.Button()
        Me.btnIzborFirma = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TextBox0 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label0 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ToolStrip4 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mnuOpcije_KnjiziSravnjenje = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpcije_FinNalog = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpcije_KolNalog = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpcije_BrisanjeNaloga = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpcije_StampaPredracuna = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOpcije_StampaPred_Uspravno = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOpcije_StampaPred_Polozeno = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpcije_PrebaciURacunVeleprodaje = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblDodOpc = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
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
        Me.StatusStrip2 = New System.Windows.Forms.StatusStrip()
        Me.lblNaslov = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnIsprazniPolja = New System.Windows.Forms.Button()
        Me.btnIzmeni = New System.Windows.Forms.Button()
        Me.btnUpisi = New System.Windows.Forms.Button()
        Me.btnBrisi = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.StatusStrip3 = New System.Windows.Forms.StatusStrip()
        Me.lblNaslovStavke = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dgvStavke = New System.Windows.Forms.DataGridView()
        CType(Me.dgvPrikaz, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ToolStrip4.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.StatusStrip2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.StatusStrip3.SuspendLayout()
        CType(Me.dgvStavke, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvPrikaz
        '
        Me.dgvPrikaz.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvPrikaz.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPrikaz.Location = New System.Drawing.Point(11, 24)
        Me.dgvPrikaz.Name = "dgvPrikaz"
        Me.dgvPrikaz.Size = New System.Drawing.Size(761, 171)
        Me.dgvPrikaz.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblOpis1)
        Me.Panel3.Controls.Add(Me.btnNovaFirma)
        Me.Panel3.Controls.Add(Me.btnIzborFirma)
        Me.Panel3.Controls.Add(Me.DateTimePicker1)
        Me.Panel3.Controls.Add(Me.StatusStrip1)
        Me.Panel3.Controls.Add(Me.TextBox0)
        Me.Panel3.Controls.Add(Me.TextBox1)
        Me.Panel3.Controls.Add(Me.TextBox2)
        Me.Panel3.Controls.Add(Me.TextBox3)
        Me.Panel3.Controls.Add(Me.TextBox4)
        Me.Panel3.Controls.Add(Me.TextBox5)
        Me.Panel3.Controls.Add(Me.TextBox6)
        Me.Panel3.Controls.Add(Me.TextBox7)
        Me.Panel3.Controls.Add(Me.Label0)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Location = New System.Drawing.Point(11, 201)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(400, 236)
        Me.Panel3.TabIndex = 29
        '
        'lblOpis1
        '
        Me.lblOpis1.AutoSize = True
        Me.lblOpis1.ForeColor = System.Drawing.Color.Blue
        Me.lblOpis1.Location = New System.Drawing.Point(259, 35)
        Me.lblOpis1.Name = "lblOpis1"
        Me.lblOpis1.Size = New System.Drawing.Size(32, 13)
        Me.lblOpis1.TabIndex = 49
        Me.lblOpis1.Text = "opis1"
        Me.lblOpis1.Visible = False
        '
        'btnNovaFirma
        '
        Me.btnNovaFirma.Location = New System.Drawing.Point(221, 28)
        Me.btnNovaFirma.Name = "btnNovaFirma"
        Me.btnNovaFirma.Size = New System.Drawing.Size(32, 20)
        Me.btnNovaFirma.TabIndex = 38
        Me.btnNovaFirma.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnNovaFirma, "Dodaj novi")
        Me.btnNovaFirma.UseVisualStyleBackColor = True
        Me.btnNovaFirma.Visible = False
        '
        'btnIzborFirma
        '
        Me.btnIzborFirma.Location = New System.Drawing.Point(185, 28)
        Me.btnIzborFirma.Name = "btnIzborFirma"
        Me.btnIzborFirma.Size = New System.Drawing.Size(30, 20)
        Me.btnIzborFirma.TabIndex = 34
        Me.btnIzborFirma.TabStop = False
        Me.btnIzborFirma.Text = ". . ."
        Me.ToolTip1.SetToolTip(Me.btnIzborFirma, "Izaberi postojeći")
        Me.btnIzborFirma.UseVisualStyleBackColor = True
        Me.btnIzborFirma.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(359, 210)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(19, 20)
        Me.DateTimePicker1.TabIndex = 33
        Me.DateTimePicker1.TabStop = False
        Me.DateTimePicker1.Visible = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.AutoSize = False
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.StatusStrip1.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(398, 22)
        Me.StatusStrip1.TabIndex = 29
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = False
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(5, 3, 5, 2)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(373, 17)
        Me.lblStatus.Spring = True
        '
        'TextBox0
        '
        Me.TextBox0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox0.Location = New System.Drawing.Point(127, 28)
        Me.TextBox0.Name = "TextBox0"
        Me.TextBox0.Size = New System.Drawing.Size(52, 20)
        Me.TextBox0.TabIndex = 0
        Me.TextBox0.Tag = ""
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(127, 54)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(52, 20)
        Me.TextBox1.TabIndex = 3
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Location = New System.Drawing.Point(127, 81)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(53, 20)
        Me.TextBox2.TabIndex = 4
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Location = New System.Drawing.Point(127, 106)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(53, 20)
        Me.TextBox3.TabIndex = 5
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Location = New System.Drawing.Point(128, 132)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(51, 20)
        Me.TextBox4.TabIndex = 1
        '
        'TextBox5
        '
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox5.Location = New System.Drawing.Point(127, 158)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(53, 20)
        Me.TextBox5.TabIndex = 2
        '
        'TextBox6
        '
        Me.TextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox6.Location = New System.Drawing.Point(127, 184)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(139, 20)
        Me.TextBox6.TabIndex = 6
        Me.TextBox6.Visible = False
        '
        'TextBox7
        '
        Me.TextBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox7.Location = New System.Drawing.Point(127, 210)
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(139, 20)
        Me.TextBox7.TabIndex = 7
        Me.TextBox7.Visible = False
        '
        'Label0
        '
        Me.Label0.AutoSize = True
        Me.Label0.Location = New System.Drawing.Point(4, 35)
        Me.Label0.Name = "Label0"
        Me.Label0.Size = New System.Drawing.Size(59, 13)
        Me.Label0.TabIndex = 23
        Me.Label0.Text = "Šifra artikla"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Ulaz/povećanje"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Izlaz/Smanjenje"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "MP cena"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 139)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(117, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Knjigovodstveno stanje"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 165)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Popisno stanje"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 191)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Label6"
        Me.Label6.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 217)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Label7"
        Me.Label7.Visible = False
        '
        'ToolStrip4
        '
        Me.ToolStrip4.AutoSize = False
        Me.ToolStrip4.BackColor = System.Drawing.Color.White
        Me.ToolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip4.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1})
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.Size = New System.Drawing.Size(98, 55)
        Me.ToolStrip4.TabIndex = 0
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.AutoSize = False
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpcije_KnjiziSravnjenje, Me.mnuOpcije_FinNalog, Me.mnuOpcije_KolNalog, Me.mnuOpcije_BrisanjeNaloga, Me.mnuOpcije_StampaPredracuna, Me.mnuOpcije_PrebaciURacunVeleprodaje})
        Me.ToolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Margin = New System.Windows.Forms.Padding(3)
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(70, 50)
        Me.ToolStripDropDownButton1.Text = "Izveštaji"
        Me.ToolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'mnuOpcije_KnjiziSravnjenje
        '
        Me.mnuOpcije_KnjiziSravnjenje.Name = "mnuOpcije_KnjiziSravnjenje"
        Me.mnuOpcije_KnjiziSravnjenje.Size = New System.Drawing.Size(220, 22)
        Me.mnuOpcije_KnjiziSravnjenje.Text = "Knjiženje"
        '
        'mnuOpcije_FinNalog
        '
        Me.mnuOpcije_FinNalog.Name = "mnuOpcije_FinNalog"
        Me.mnuOpcije_FinNalog.Size = New System.Drawing.Size(220, 22)
        Me.mnuOpcije_FinNalog.Text = "Finansijski nalog"
        '
        'mnuOpcije_KolNalog
        '
        Me.mnuOpcije_KolNalog.Name = "mnuOpcije_KolNalog"
        Me.mnuOpcije_KolNalog.Size = New System.Drawing.Size(220, 22)
        Me.mnuOpcije_KolNalog.Text = "Količinski nalog"
        '
        'mnuOpcije_BrisanjeNaloga
        '
        Me.mnuOpcije_BrisanjeNaloga.Name = "mnuOpcije_BrisanjeNaloga"
        Me.mnuOpcije_BrisanjeNaloga.Size = New System.Drawing.Size(220, 22)
        Me.mnuOpcije_BrisanjeNaloga.Text = "Brisanje naloga"
        '
        'mnuOpcije_StampaPredracuna
        '
        Me.mnuOpcije_StampaPredracuna.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOpcije_StampaPred_Uspravno, Me.btnOpcije_StampaPred_Polozeno})
        Me.mnuOpcije_StampaPredracuna.Name = "mnuOpcije_StampaPredracuna"
        Me.mnuOpcije_StampaPredracuna.Size = New System.Drawing.Size(220, 22)
        Me.mnuOpcije_StampaPredracuna.Text = "Štampaj predračun"
        '
        'btnOpcije_StampaPred_Uspravno
        '
        Me.btnOpcije_StampaPred_Uspravno.Name = "btnOpcije_StampaPred_Uspravno"
        Me.btnOpcije_StampaPred_Uspravno.Size = New System.Drawing.Size(124, 22)
        Me.btnOpcije_StampaPred_Uspravno.Text = "Uspravno"
        '
        'btnOpcije_StampaPred_Polozeno
        '
        Me.btnOpcije_StampaPred_Polozeno.Name = "btnOpcije_StampaPred_Polozeno"
        Me.btnOpcije_StampaPred_Polozeno.Size = New System.Drawing.Size(124, 22)
        Me.btnOpcije_StampaPred_Polozeno.Text = "Položeno"
        '
        'mnuOpcije_PrebaciURacunVeleprodaje
        '
        Me.mnuOpcije_PrebaciURacunVeleprodaje.Name = "mnuOpcije_PrebaciURacunVeleprodaje"
        Me.mnuOpcije_PrebaciURacunVeleprodaje.Size = New System.Drawing.Size(220, 22)
        Me.mnuOpcije_PrebaciURacunVeleprodaje.Text = "Prebaci u račun veleprodaje"
        '
        'lblDodOpc
        '
        Me.lblDodOpc.AutoSize = True
        Me.lblDodOpc.Location = New System.Drawing.Point(13, 55)
        Me.lblDodOpc.Name = "lblDodOpc"
        Me.lblDodOpc.Size = New System.Drawing.Size(79, 13)
        Me.lblDodOpc.TabIndex = 1
        Me.lblDodOpc.Text = "Dodatne opcije"
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.Add(Me.lblDodOpc)
        Me.Panel4.Controls.Add(Me.ToolStrip4)
        Me.Panel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel4.Location = New System.Drawing.Point(417, 440)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(100, 70)
        Me.Panel4.TabIndex = 32
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblStandOpc)
        Me.Panel1.Controls.Add(Me.ToolStrip3)
        Me.Panel1.Location = New System.Drawing.Point(523, 440)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(250, 70)
        Me.Panel1.TabIndex = 33
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
        'StatusStrip2
        '
        Me.StatusStrip2.AutoSize = False
        Me.StatusStrip2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.StatusStrip2.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblNaslov})
        Me.StatusStrip2.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip2.Name = "StatusStrip2"
        Me.StatusStrip2.Size = New System.Drawing.Size(784, 22)
        Me.StatusStrip2.TabIndex = 34
        Me.StatusStrip2.Text = "StatusStrip2"
        '
        'lblNaslov
        '
        Me.lblNaslov.AutoSize = False
        Me.lblNaslov.Margin = New System.Windows.Forms.Padding(5, 3, 5, 2)
        Me.lblNaslov.Name = "lblNaslov"
        Me.lblNaslov.Size = New System.Drawing.Size(759, 17)
        Me.lblNaslov.Spring = True
        Me.lblNaslov.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Panel2.Location = New System.Drawing.Point(11, 440)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(400, 70)
        Me.Panel2.TabIndex = 35
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
        Me.btnIsprazniPolja.TabIndex = 11
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
        Me.btnIzmeni.TabIndex = 9
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
        Me.btnUpisi.TabIndex = 8
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
        Me.btnBrisi.TabIndex = 10
        Me.btnBrisi.Text = "Briši"
        Me.btnBrisi.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(148, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Rad sa bazom"
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Controls.Add(Me.StatusStrip3)
        Me.Panel6.Controls.Add(Me.dgvStavke)
        Me.Panel6.Location = New System.Drawing.Point(418, 200)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(354, 238)
        Me.Panel6.TabIndex = 37
        '
        'StatusStrip3
        '
        Me.StatusStrip3.AutoSize = False
        Me.StatusStrip3.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.StatusStrip3.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStrip3.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblNaslovStavke})
        Me.StatusStrip3.Location = New System.Drawing.Point(0, 0)
        Me.StatusStrip3.Name = "StatusStrip3"
        Me.StatusStrip3.Size = New System.Drawing.Size(352, 22)
        Me.StatusStrip3.TabIndex = 5
        Me.StatusStrip3.Text = "StatusStrip3"
        '
        'lblNaslovStavke
        '
        Me.lblNaslovStavke.AutoSize = False
        Me.lblNaslovStavke.Margin = New System.Windows.Forms.Padding(5, 3, 5, 2)
        Me.lblNaslovStavke.Name = "lblNaslovStavke"
        Me.lblNaslovStavke.Size = New System.Drawing.Size(327, 17)
        Me.lblNaslovStavke.Spring = True
        '
        'dgvStavke
        '
        Me.dgvStavke.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvStavke.BackgroundColor = System.Drawing.SystemColors.ButtonFace
        Me.dgvStavke.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvStavke.Location = New System.Drawing.Point(3, 25)
        Me.dgvStavke.Name = "dgvStavke"
        Me.dgvStavke.Size = New System.Drawing.Size(346, 208)
        Me.dgvStavke.TabIndex = 33
        '
        'frmSifArtikli
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 512)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusStrip2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.dgvPrikaz)
        Me.Name = "frmSifArtikli"
        Me.Text = "Pattern form"
        CType(Me.dgvPrikaz, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.StatusStrip2.ResumeLayout(False)
        Me.StatusStrip2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.StatusStrip3.ResumeLayout(False)
        Me.StatusStrip3.PerformLayout()
        CType(Me.dgvStavke, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected Friend WithEvents dgvPrikaz As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Protected Friend WithEvents TextBox0 As System.Windows.Forms.TextBox
    Protected Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Protected Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Protected Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Protected Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Protected Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Protected Friend WithEvents Label0 As System.Windows.Forms.Label
    Protected Friend WithEvents Label1 As System.Windows.Forms.Label
    Protected Friend WithEvents Label2 As System.Windows.Forms.Label
    Protected Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip4 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents lblDodOpc As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblStandOpc As System.Windows.Forms.Label
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnZatvori As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPretrazi As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents PretragaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PočetnoStanjeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnStampaj As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents PregledŠtampeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PDFToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PolozajToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip2 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblNaslov As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnNovaFirma As System.Windows.Forms.Button
    Friend WithEvents btnIzborFirma As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblOpis1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents btnBrisi As Button
    Friend WithEvents btnUpisi As Button
    Friend WithEvents btnIzmeni As Button
    Friend WithEvents btnIsprazniPolja As Button
    Friend WithEvents mnuOpcije_KnjiziSravnjenje As ToolStripMenuItem
    Friend WithEvents mnuOpcije_FinNalog As ToolStripMenuItem
    Friend WithEvents mnuOpcije_KolNalog As ToolStripMenuItem
    Friend WithEvents mnuOpcije_BrisanjeNaloga As ToolStripMenuItem
    Friend WithEvents Panel6 As Panel
    Friend WithEvents StatusStrip3 As StatusStrip
    Friend WithEvents lblNaslovStavke As ToolStripStatusLabel
    Friend WithEvents dgvStavke As DataGridView
    Protected Friend WithEvents TextBox4 As TextBox
    Protected Friend WithEvents TextBox5 As TextBox
    Protected Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents mnuOpcije_StampaPredracuna As ToolStripMenuItem
    Friend WithEvents mnuOpcije_PrebaciURacunVeleprodaje As ToolStripMenuItem
    Friend WithEvents btnOpcije_StampaPred_Uspravno As ToolStripMenuItem
    Friend WithEvents btnOpcije_StampaPred_Polozeno As ToolStripMenuItem
End Class

