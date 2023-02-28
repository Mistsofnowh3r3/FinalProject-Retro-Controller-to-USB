namespace SerialTest2
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tb_serialread = new System.Windows.Forms.TextBox();
            this.btn_stopstart = new System.Windows.Forms.Button();
            this.cb_portlist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tb_NES_RIGHT = new System.Windows.Forms.TextBox();
            this.tb_NES_LEFT = new System.Windows.Forms.TextBox();
            this.tb_NES_DOWN = new System.Windows.Forms.TextBox();
            this.tb_NES_UP = new System.Windows.Forms.TextBox();
            this.tb_NES_START = new System.Windows.Forms.TextBox();
            this.tb_NES_SELECT = new System.Windows.Forms.TextBox();
            this.tb_NES_B = new System.Windows.Forms.TextBox();
            this.tb_NES_A = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_sendremap = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.VAL = new System.Windows.Forms.NumericUpDown();
            this.Poke = new System.Windows.Forms.Button();
            this.ADR = new System.Windows.Forms.NumericUpDown();
            this.peek = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_console = new System.Windows.Forms.TextBox();
            this.btn_saveSettings = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ADR)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_serialread
            // 
            this.tb_serialread.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_serialread.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_serialread.Location = new System.Drawing.Point(182, 42);
            this.tb_serialread.Multiline = true;
            this.tb_serialread.Name = "tb_serialread";
            this.tb_serialread.ReadOnly = true;
            this.tb_serialread.Size = new System.Drawing.Size(196, 52);
            this.tb_serialread.TabIndex = 0;
            this.tb_serialread.TabStop = false;
            // 
            // btn_stopstart
            // 
            this.btn_stopstart.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_stopstart.Enabled = false;
            this.btn_stopstart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_stopstart.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btn_stopstart.ForeColor = System.Drawing.Color.White;
            this.btn_stopstart.Location = new System.Drawing.Point(21, 11);
            this.btn_stopstart.Name = "btn_stopstart";
            this.btn_stopstart.Size = new System.Drawing.Size(118, 46);
            this.btn_stopstart.TabIndex = 1;
            this.btn_stopstart.TabStop = false;
            this.btn_stopstart.Tag = "button2";
            this.btn_stopstart.Text = "Select A Port";
            this.btn_stopstart.UseVisualStyleBackColor = false;
            this.btn_stopstart.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_portlist
            // 
            this.cb_portlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_portlist.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.cb_portlist.FormattingEnabled = true;
            this.cb_portlist.Location = new System.Drawing.Point(182, 11);
            this.cb_portlist.Name = "cb_portlist";
            this.cb_portlist.Size = new System.Drawing.Size(196, 23);
            this.cb_portlist.TabIndex = 3;
            this.cb_portlist.TabStop = false;
            this.cb_portlist.DropDown += new System.EventHandler(this.cb_portlist_DropDown);
            this.cb_portlist.SelectedIndexChanged += new System.EventHandler(this.cb_portlist_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(144, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(146, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log:";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPage1.Controls.Add(this.tb_NES_RIGHT);
            this.tabPage1.Controls.Add(this.tb_NES_LEFT);
            this.tabPage1.Controls.Add(this.tb_NES_DOWN);
            this.tabPage1.Controls.Add(this.tb_NES_UP);
            this.tabPage1.Controls.Add(this.tb_NES_START);
            this.tabPage1.Controls.Add(this.tb_NES_SELECT);
            this.tabPage1.Controls.Add(this.tb_NES_B);
            this.tabPage1.Controls.Add(this.tb_NES_A);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.label26);
            this.tabPage1.Controls.Add(this.label27);
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.btn_sendremap);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = "background2";
            this.tabPage1.Text = "NES";
            // 
            // tb_NES_RIGHT
            // 
            this.tb_NES_RIGHT.AcceptsReturn = true;
            this.tb_NES_RIGHT.AcceptsTab = true;
            this.tb_NES_RIGHT.BackColor = System.Drawing.Color.White;
            this.tb_NES_RIGHT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_RIGHT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_RIGHT.Location = new System.Drawing.Point(524, 206);
            this.tb_NES_RIGHT.Name = "tb_NES_RIGHT";
            this.tb_NES_RIGHT.ReadOnly = true;
            this.tb_NES_RIGHT.ShortcutsEnabled = false;
            this.tb_NES_RIGHT.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_RIGHT.TabIndex = 7;
            this.tb_NES_RIGHT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_RIGHT_KeyDown);
            this.tb_NES_RIGHT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // tb_NES_LEFT
            // 
            this.tb_NES_LEFT.AcceptsReturn = true;
            this.tb_NES_LEFT.AcceptsTab = true;
            this.tb_NES_LEFT.BackColor = System.Drawing.Color.White;
            this.tb_NES_LEFT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_LEFT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_LEFT.Location = new System.Drawing.Point(524, 178);
            this.tb_NES_LEFT.Name = "tb_NES_LEFT";
            this.tb_NES_LEFT.ReadOnly = true;
            this.tb_NES_LEFT.ShortcutsEnabled = false;
            this.tb_NES_LEFT.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_LEFT.TabIndex = 6;
            this.tb_NES_LEFT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_LEFT_KeyDown);
            this.tb_NES_LEFT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // tb_NES_DOWN
            // 
            this.tb_NES_DOWN.AcceptsReturn = true;
            this.tb_NES_DOWN.AcceptsTab = true;
            this.tb_NES_DOWN.BackColor = System.Drawing.Color.White;
            this.tb_NES_DOWN.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_DOWN.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_DOWN.Location = new System.Drawing.Point(524, 150);
            this.tb_NES_DOWN.Name = "tb_NES_DOWN";
            this.tb_NES_DOWN.ReadOnly = true;
            this.tb_NES_DOWN.ShortcutsEnabled = false;
            this.tb_NES_DOWN.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_DOWN.TabIndex = 5;
            this.tb_NES_DOWN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_DOWN_KeyDown);
            this.tb_NES_DOWN.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // tb_NES_UP
            // 
            this.tb_NES_UP.AcceptsReturn = true;
            this.tb_NES_UP.AcceptsTab = true;
            this.tb_NES_UP.BackColor = System.Drawing.Color.White;
            this.tb_NES_UP.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_UP.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_UP.Location = new System.Drawing.Point(524, 121);
            this.tb_NES_UP.Name = "tb_NES_UP";
            this.tb_NES_UP.ReadOnly = true;
            this.tb_NES_UP.ShortcutsEnabled = false;
            this.tb_NES_UP.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_UP.TabIndex = 4;
            this.tb_NES_UP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_UP_KeyDown);
            this.tb_NES_UP.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // tb_NES_START
            // 
            this.tb_NES_START.AcceptsReturn = true;
            this.tb_NES_START.AcceptsTab = true;
            this.tb_NES_START.BackColor = System.Drawing.Color.White;
            this.tb_NES_START.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_START.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_START.Location = new System.Drawing.Point(524, 93);
            this.tb_NES_START.Name = "tb_NES_START";
            this.tb_NES_START.ReadOnly = true;
            this.tb_NES_START.ShortcutsEnabled = false;
            this.tb_NES_START.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_START.TabIndex = 3;
            this.tb_NES_START.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_START_KeyDown);
            this.tb_NES_START.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // tb_NES_SELECT
            // 
            this.tb_NES_SELECT.AcceptsReturn = true;
            this.tb_NES_SELECT.AcceptsTab = true;
            this.tb_NES_SELECT.BackColor = System.Drawing.Color.White;
            this.tb_NES_SELECT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_SELECT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_SELECT.Location = new System.Drawing.Point(524, 63);
            this.tb_NES_SELECT.Name = "tb_NES_SELECT";
            this.tb_NES_SELECT.ReadOnly = true;
            this.tb_NES_SELECT.ShortcutsEnabled = false;
            this.tb_NES_SELECT.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_SELECT.TabIndex = 2;
            this.tb_NES_SELECT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_SELECT_KeyDown);
            this.tb_NES_SELECT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // tb_NES_B
            // 
            this.tb_NES_B.AcceptsReturn = true;
            this.tb_NES_B.AcceptsTab = true;
            this.tb_NES_B.BackColor = System.Drawing.Color.White;
            this.tb_NES_B.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_B.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_B.Location = new System.Drawing.Point(524, 35);
            this.tb_NES_B.Name = "tb_NES_B";
            this.tb_NES_B.ReadOnly = true;
            this.tb_NES_B.ShortcutsEnabled = false;
            this.tb_NES_B.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_B.TabIndex = 1;
            this.tb_NES_B.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_B_KeyDown);
            this.tb_NES_B.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // tb_NES_A
            // 
            this.tb_NES_A.AcceptsReturn = true;
            this.tb_NES_A.AcceptsTab = true;
            this.tb_NES_A.BackColor = System.Drawing.Color.White;
            this.tb_NES_A.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_A.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_A.Location = new System.Drawing.Point(524, 6);
            this.tb_NES_A.Name = "tb_NES_A";
            this.tb_NES_A.ReadOnly = true;
            this.tb_NES_A.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_NES_A.ShortcutsEnabled = false;
            this.tb_NES_A.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_A.TabIndex = 0;
            this.tb_NES_A.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_NES_A_KeyDown);
            this.tb_NES_A.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.tb_PreviewKeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(477, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 15);
            this.label9.TabIndex = 62;
            this.label9.Text = "RIGHT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(486, 182);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 15);
            this.label10.TabIndex = 61;
            this.label10.Text = "LEFT";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(474, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 15);
            this.label11.TabIndex = 60;
            this.label11.Text = "DOWN";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(496, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 15);
            this.label12.TabIndex = 59;
            this.label12.Text = "UP";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(476, 95);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(42, 15);
            this.label25.TabIndex = 58;
            this.label25.Text = "START";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(472, 66);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(46, 15);
            this.label26.TabIndex = 57;
            this.label26.Text = "SELECT";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(504, 37);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 15);
            this.label27.TabIndex = 56;
            this.label27.Text = "B";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(503, 8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 15);
            this.label28.TabIndex = 47;
            this.label28.Text = "A";
            // 
            // btn_sendremap
            // 
            this.btn_sendremap.Location = new System.Drawing.Point(367, 310);
            this.btn_sendremap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sendremap.Name = "btn_sendremap";
            this.btn_sendremap.Size = new System.Drawing.Size(395, 28);
            this.btn_sendremap.TabIndex = 1;
            this.btn_sendremap.TabStop = false;
            this.btn_sendremap.Tag = "button1";
            this.btn_sendremap.Text = "Send to EEPROM";
            this.btn_sendremap.UseVisualStyleBackColor = true;
            this.btn_sendremap.Click += new System.EventHandler(this.btn_sendremap_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 6);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(361, 333);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 343);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "background2";
            this.tabPage2.Text = "SNES";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(768, 343);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Tag = "background2";
            this.tabPage3.Text = "N64";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.VAL);
            this.tabPage4.Controls.Add(this.Poke);
            this.tabPage4.Controls.Add(this.ADR);
            this.tabPage4.Controls.Add(this.peek);
            this.tabPage4.Controls.Add(this.button5);
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.button2);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Controls.Add(this.button4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(768, 343);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Tag = "background2";
            this.tabPage4.Text = "Other";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(164, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 15);
            this.label13.TabIndex = 59;
            this.label13.Text = "Val";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(164, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 58;
            this.label3.Text = "address";
            // 
            // VAL
            // 
            this.VAL.Location = new System.Drawing.Point(167, 96);
            this.VAL.Name = "VAL";
            this.VAL.Size = new System.Drawing.Size(120, 22);
            this.VAL.TabIndex = 14;
            this.VAL.TabStop = false;
            // 
            // Poke
            // 
            this.Poke.Location = new System.Drawing.Point(6, 64);
            this.Poke.Name = "Poke";
            this.Poke.Size = new System.Drawing.Size(122, 52);
            this.Poke.TabIndex = 13;
            this.Poke.TabStop = false;
            this.Poke.Text = "Poke Memory.";
            this.Poke.UseVisualStyleBackColor = true;
            this.Poke.Click += new System.EventHandler(this.Poke_Click);
            // 
            // ADR
            // 
            this.ADR.Location = new System.Drawing.Point(167, 37);
            this.ADR.Name = "ADR";
            this.ADR.Size = new System.Drawing.Size(120, 22);
            this.ADR.TabIndex = 12;
            this.ADR.TabStop = false;
            // 
            // peek
            // 
            this.peek.Location = new System.Drawing.Point(6, 6);
            this.peek.Name = "peek";
            this.peek.Size = new System.Drawing.Size(122, 52);
            this.peek.TabIndex = 11;
            this.peek.TabStop = false;
            this.peek.Text = "Peek Memory.";
            this.peek.UseVisualStyleBackColor = true;
            this.peek.Click += new System.EventHandler(this.peek_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(512, 66);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(122, 52);
            this.button5.TabIndex = 10;
            this.button5.TabStop = false;
            this.button5.Text = "Summon a box";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(512, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 52);
            this.button3.TabIndex = 10;
            this.button3.TabStop = false;
            this.button3.Text = "Summon Color Picker";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(640, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 52);
            this.button2.TabIndex = 9;
            this.button2.TabStop = false;
            this.button2.Tag = "button1";
            this.button2.Text = "Make NES A be 66 in the array";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(640, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 52);
            this.button1.TabIndex = 8;
            this.button1.TabStop = false;
            this.button1.Tag = "button1";
            this.button1.Text = "What value is the first of the NES array?";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(640, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(122, 52);
            this.button4.TabIndex = 8;
            this.button4.TabStop = false;
            this.button4.Tag = "button1";
            this.button4.Text = "Make it pretty";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(12, 100);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 369);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(380, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Debug Console";
            // 
            // tb_console
            // 
            this.tb_console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_console.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_console.Location = new System.Drawing.Point(473, 11);
            this.tb_console.Multiline = true;
            this.tb_console.Name = "tb_console";
            this.tb_console.ReadOnly = true;
            this.tb_console.Size = new System.Drawing.Size(311, 83);
            this.tb_console.TabIndex = 6;
            this.tb_console.TabStop = false;
            // 
            // btn_saveSettings
            // 
            this.btn_saveSettings.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_saveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_saveSettings.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btn_saveSettings.ForeColor = System.Drawing.Color.White;
            this.btn_saveSettings.Location = new System.Drawing.Point(21, 63);
            this.btn_saveSettings.Name = "btn_saveSettings";
            this.btn_saveSettings.Size = new System.Drawing.Size(118, 31);
            this.btn_saveSettings.TabIndex = 50;
            this.btn_saveSettings.TabStop = false;
            this.btn_saveSettings.Tag = "button2";
            this.btn_saveSettings.Text = "Save Settings";
            this.btn_saveSettings.UseVisualStyleBackColor = false;
            this.btn_saveSettings.Click += new System.EventHandler(this.btn_saveSettings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 486);
            this.Controls.Add(this.btn_saveSettings);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_console);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_portlist);
            this.Controls.Add(this.btn_stopstart);
            this.Controls.Add(this.tb_serialread);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Tag = "background1";
            this.Text = "Serial Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ADR)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_serialread;
        private System.Windows.Forms.Button btn_stopstart;
        private System.Windows.Forms.ComboBox cb_portlist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_sendremap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_console;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_saveSettings;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_NES_A;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.NumericUpDown ADR;
        private System.Windows.Forms.Button peek;
        private System.Windows.Forms.Button Poke;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown VAL;
        private System.Windows.Forms.TextBox tb_NES_RIGHT;
        private System.Windows.Forms.TextBox tb_NES_LEFT;
        private System.Windows.Forms.TextBox tb_NES_DOWN;
        private System.Windows.Forms.TextBox tb_NES_UP;
        private System.Windows.Forms.TextBox tb_NES_START;
        private System.Windows.Forms.TextBox tb_NES_SELECT;
        private System.Windows.Forms.TextBox tb_NES_B;
        private System.Windows.Forms.Button button5;
    }
}

