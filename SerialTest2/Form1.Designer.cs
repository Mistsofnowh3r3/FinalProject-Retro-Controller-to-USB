﻿namespace ccAdapterRemapper
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
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageNES = new System.Windows.Forms.TabPage();
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
            this.tabPageSNES = new System.Windows.Forms.TabPage();
            this.tb_SNES_R = new System.Windows.Forms.TextBox();
            this.tb_SNES_L = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tb_SNES_X = new System.Windows.Forms.TextBox();
            this.tb_SNES_A = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tb_SNES_RIGHT = new System.Windows.Forms.TextBox();
            this.tb_SNES_LEFT = new System.Windows.Forms.TextBox();
            this.tb_SNES_DOWN = new System.Windows.Forms.TextBox();
            this.tb_SNES_UP = new System.Windows.Forms.TextBox();
            this.tb_SNES_START = new System.Windows.Forms.TextBox();
            this.tb_SNES_SELECT = new System.Windows.Forms.TextBox();
            this.tb_SNES_Y = new System.Windows.Forms.TextBox();
            this.tb_SNES_B = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPageN64 = new System.Windows.Forms.TabPage();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.btn_wipeParams = new System.Windows.Forms.Button();
            this.btn_colorPick = new System.Windows.Forms.Button();
            this.cb_pastel = new System.Windows.Forms.CheckBox();
            this.lb_val = new System.Windows.Forms.Label();
            this.lb_Address = new System.Windows.Forms.Label();
            this.nup_value = new System.Windows.Forms.NumericUpDown();
            this.btn_poke = new System.Windows.Forms.Button();
            this.nup_address = new System.Windows.Forms.NumericUpDown();
            this.btn_peek = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.lb_debug = new System.Windows.Forms.Label();
            this.tb_console = new System.Windows.Forms.TextBox();
            this.btn_sendremap = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_motto = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tab = new System.Windows.Forms.TabPage();
            this.tabPageNES.SuspendLayout();
            this.tabPageSNES.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_value)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_address)).BeginInit();
            this.tabControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_serialread
            // 
            this.tb_serialread.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_serialread.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_serialread.Location = new System.Drawing.Point(219, 285);
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
            this.btn_stopstart.FlatAppearance.BorderSize = 0;
            this.btn_stopstart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stopstart.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btn_stopstart.ForeColor = System.Drawing.Color.White;
            this.btn_stopstart.Location = new System.Drawing.Point(12, 12);
            this.btn_stopstart.Name = "btn_stopstart";
            this.btn_stopstart.Size = new System.Drawing.Size(114, 46);
            this.btn_stopstart.TabIndex = 1;
            this.btn_stopstart.TabStop = false;
            this.btn_stopstart.Tag = "light1";
            this.btn_stopstart.Text = "Select A Port";
            this.btn_stopstart.UseVisualStyleBackColor = false;
            this.btn_stopstart.Click += new System.EventHandler(this.OpenCOM);
            // 
            // cb_portlist
            // 
            this.cb_portlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_portlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_portlist.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.cb_portlist.FormattingEnabled = true;
            this.cb_portlist.Location = new System.Drawing.Point(12, 64);
            this.cb_portlist.Name = "cb_portlist";
            this.cb_portlist.Size = new System.Drawing.Size(114, 23);
            this.cb_portlist.TabIndex = 3;
            this.cb_portlist.TabStop = false;
            this.cb_portlist.Tag = "light2";
            this.cb_portlist.DropDown += new System.EventHandler(this.Portlist_DropDown);
            this.cb_portlist.SelectedIndexChanged += new System.EventHandler(this.Portlist_SelectedIndexChanged);
            this.cb_portlist.DropDownClosed += new System.EventHandler(this.Portlist_DropDownClosed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(216, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Serial Log:";
            // 
            // tabPageNES
            // 
            this.tabPageNES.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPageNES.Controls.Add(this.tb_NES_RIGHT);
            this.tabPageNES.Controls.Add(this.tb_NES_LEFT);
            this.tabPageNES.Controls.Add(this.tb_NES_DOWN);
            this.tabPageNES.Controls.Add(this.tb_NES_UP);
            this.tabPageNES.Controls.Add(this.tb_NES_START);
            this.tabPageNES.Controls.Add(this.tb_NES_SELECT);
            this.tabPageNES.Controls.Add(this.tb_NES_B);
            this.tabPageNES.Controls.Add(this.tb_NES_A);
            this.tabPageNES.Controls.Add(this.label9);
            this.tabPageNES.Controls.Add(this.label10);
            this.tabPageNES.Controls.Add(this.label11);
            this.tabPageNES.Controls.Add(this.label12);
            this.tabPageNES.Controls.Add(this.label25);
            this.tabPageNES.Controls.Add(this.label26);
            this.tabPageNES.Controls.Add(this.label27);
            this.tabPageNES.Controls.Add(this.label28);
            this.tabPageNES.Location = new System.Drawing.Point(4, 22);
            this.tabPageNES.Name = "tabPageNES";
            this.tabPageNES.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNES.Size = new System.Drawing.Size(421, 260);
            this.tabPageNES.TabIndex = 0;
            this.tabPageNES.Tag = "dark2";
            this.tabPageNES.Text = "NES";
            // 
            // tb_NES_RIGHT
            // 
            this.tb_NES_RIGHT.AcceptsReturn = true;
            this.tb_NES_RIGHT.AcceptsTab = true;
            this.tb_NES_RIGHT.BackColor = System.Drawing.Color.White;
            this.tb_NES_RIGHT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_RIGHT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_RIGHT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_RIGHT.Location = new System.Drawing.Point(57, 216);
            this.tb_NES_RIGHT.Name = "tb_NES_RIGHT";
            this.tb_NES_RIGHT.ReadOnly = true;
            this.tb_NES_RIGHT.ShortcutsEnabled = false;
            this.tb_NES_RIGHT.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_RIGHT.TabIndex = 7;
            this.tb_NES_RIGHT.Tag = "JNESRIGHTlight2";
            this.tb_NES_RIGHT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_RIGHT.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_RIGHT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_NES_LEFT
            // 
            this.tb_NES_LEFT.AcceptsReturn = true;
            this.tb_NES_LEFT.AcceptsTab = true;
            this.tb_NES_LEFT.BackColor = System.Drawing.Color.White;
            this.tb_NES_LEFT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_LEFT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_LEFT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_LEFT.Location = new System.Drawing.Point(57, 188);
            this.tb_NES_LEFT.Name = "tb_NES_LEFT";
            this.tb_NES_LEFT.ReadOnly = true;
            this.tb_NES_LEFT.ShortcutsEnabled = false;
            this.tb_NES_LEFT.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_LEFT.TabIndex = 6;
            this.tb_NES_LEFT.Tag = "JNESLEFTlight2";
            this.tb_NES_LEFT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_LEFT.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_LEFT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_NES_DOWN
            // 
            this.tb_NES_DOWN.AcceptsReturn = true;
            this.tb_NES_DOWN.AcceptsTab = true;
            this.tb_NES_DOWN.BackColor = System.Drawing.Color.White;
            this.tb_NES_DOWN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_DOWN.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_DOWN.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_DOWN.Location = new System.Drawing.Point(57, 160);
            this.tb_NES_DOWN.Name = "tb_NES_DOWN";
            this.tb_NES_DOWN.ReadOnly = true;
            this.tb_NES_DOWN.ShortcutsEnabled = false;
            this.tb_NES_DOWN.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_DOWN.TabIndex = 5;
            this.tb_NES_DOWN.Tag = "JNESDOWNlight2";
            this.tb_NES_DOWN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_DOWN.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_DOWN.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_NES_UP
            // 
            this.tb_NES_UP.AcceptsReturn = true;
            this.tb_NES_UP.AcceptsTab = true;
            this.tb_NES_UP.BackColor = System.Drawing.Color.White;
            this.tb_NES_UP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_UP.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_UP.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_UP.Location = new System.Drawing.Point(57, 131);
            this.tb_NES_UP.Name = "tb_NES_UP";
            this.tb_NES_UP.ReadOnly = true;
            this.tb_NES_UP.ShortcutsEnabled = false;
            this.tb_NES_UP.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_UP.TabIndex = 4;
            this.tb_NES_UP.Tag = "JNESUPlight2";
            this.tb_NES_UP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_UP.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_UP.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_NES_START
            // 
            this.tb_NES_START.AcceptsReturn = true;
            this.tb_NES_START.AcceptsTab = true;
            this.tb_NES_START.BackColor = System.Drawing.Color.White;
            this.tb_NES_START.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_START.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_START.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_START.Location = new System.Drawing.Point(57, 103);
            this.tb_NES_START.Name = "tb_NES_START";
            this.tb_NES_START.ReadOnly = true;
            this.tb_NES_START.ShortcutsEnabled = false;
            this.tb_NES_START.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_START.TabIndex = 3;
            this.tb_NES_START.Tag = "JNESSTARTlight2";
            this.tb_NES_START.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_START.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_START.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_NES_SELECT
            // 
            this.tb_NES_SELECT.AcceptsReturn = true;
            this.tb_NES_SELECT.AcceptsTab = true;
            this.tb_NES_SELECT.BackColor = System.Drawing.Color.White;
            this.tb_NES_SELECT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_SELECT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_SELECT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_SELECT.Location = new System.Drawing.Point(57, 73);
            this.tb_NES_SELECT.Name = "tb_NES_SELECT";
            this.tb_NES_SELECT.ReadOnly = true;
            this.tb_NES_SELECT.ShortcutsEnabled = false;
            this.tb_NES_SELECT.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_SELECT.TabIndex = 2;
            this.tb_NES_SELECT.Tag = "JNESSELECTlight2";
            this.tb_NES_SELECT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_SELECT.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_SELECT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_NES_B
            // 
            this.tb_NES_B.AcceptsReturn = true;
            this.tb_NES_B.AcceptsTab = true;
            this.tb_NES_B.BackColor = System.Drawing.Color.White;
            this.tb_NES_B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_B.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_B.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_B.Location = new System.Drawing.Point(57, 45);
            this.tb_NES_B.Name = "tb_NES_B";
            this.tb_NES_B.ReadOnly = true;
            this.tb_NES_B.ShortcutsEnabled = false;
            this.tb_NES_B.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_B.TabIndex = 1;
            this.tb_NES_B.Tag = "JNESBlight2";
            this.tb_NES_B.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_B.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_B.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_NES_A
            // 
            this.tb_NES_A.AcceptsReturn = true;
            this.tb_NES_A.AcceptsTab = true;
            this.tb_NES_A.BackColor = System.Drawing.Color.White;
            this.tb_NES_A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_NES_A.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_NES_A.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_NES_A.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb_NES_A.Location = new System.Drawing.Point(57, 16);
            this.tb_NES_A.Name = "tb_NES_A";
            this.tb_NES_A.ReadOnly = true;
            this.tb_NES_A.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_NES_A.ShortcutsEnabled = false;
            this.tb_NES_A.Size = new System.Drawing.Size(103, 23);
            this.tb_NES_A.TabIndex = 0;
            this.tb_NES_A.Tag = "JNESAlight2";
            this.tb_NES_A.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_NES_A.Leave += new System.EventHandler(this.FocusLost);
            this.tb_NES_A.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(10, 218);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 15);
            this.label9.TabIndex = 62;
            this.label9.Tag = "label";
            this.label9.Text = "RIGHT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(18, 190);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 15);
            this.label10.TabIndex = 61;
            this.label10.Tag = "label";
            this.label10.Text = "LEFT";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(6, 162);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 15);
            this.label11.TabIndex = 60;
            this.label11.Tag = "label";
            this.label11.Text = "DOWN";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(28, 133);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 15);
            this.label12.TabIndex = 59;
            this.label12.Tag = "label";
            this.label12.Text = "UP";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Location = new System.Drawing.Point(8, 105);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(42, 15);
            this.label25.TabIndex = 58;
            this.label25.Tag = "label";
            this.label25.Text = "START";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Location = new System.Drawing.Point(5, 75);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(46, 15);
            this.label26.TabIndex = 57;
            this.label26.Tag = "label";
            this.label26.Text = "SELECT";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(36, 47);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 15);
            this.label27.TabIndex = 56;
            this.label27.Tag = "label";
            this.label27.Text = "B";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label28.ForeColor = System.Drawing.Color.Black;
            this.label28.Location = new System.Drawing.Point(36, 18);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 15);
            this.label28.TabIndex = 47;
            this.label28.Tag = "label";
            this.label28.Text = "A";
            // 
            // tabPageSNES
            // 
            this.tabPageSNES.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPageSNES.Controls.Add(this.tb_SNES_R);
            this.tabPageSNES.Controls.Add(this.tb_SNES_L);
            this.tabPageSNES.Controls.Add(this.label20);
            this.tabPageSNES.Controls.Add(this.label21);
            this.tabPageSNES.Controls.Add(this.tb_SNES_X);
            this.tabPageSNES.Controls.Add(this.tb_SNES_A);
            this.tabPageSNES.Controls.Add(this.label18);
            this.tabPageSNES.Controls.Add(this.label19);
            this.tabPageSNES.Controls.Add(this.tb_SNES_RIGHT);
            this.tabPageSNES.Controls.Add(this.tb_SNES_LEFT);
            this.tabPageSNES.Controls.Add(this.tb_SNES_DOWN);
            this.tabPageSNES.Controls.Add(this.tb_SNES_UP);
            this.tabPageSNES.Controls.Add(this.tb_SNES_START);
            this.tabPageSNES.Controls.Add(this.tb_SNES_SELECT);
            this.tabPageSNES.Controls.Add(this.tb_SNES_Y);
            this.tabPageSNES.Controls.Add(this.tb_SNES_B);
            this.tabPageSNES.Controls.Add(this.label5);
            this.tabPageSNES.Controls.Add(this.label6);
            this.tabPageSNES.Controls.Add(this.label7);
            this.tabPageSNES.Controls.Add(this.label8);
            this.tabPageSNES.Controls.Add(this.label14);
            this.tabPageSNES.Controls.Add(this.label15);
            this.tabPageSNES.Controls.Add(this.label16);
            this.tabPageSNES.Controls.Add(this.label17);
            this.tabPageSNES.Location = new System.Drawing.Point(4, 22);
            this.tabPageSNES.Name = "tabPageSNES";
            this.tabPageSNES.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSNES.Size = new System.Drawing.Size(421, 260);
            this.tabPageSNES.TabIndex = 1;
            this.tabPageSNES.Tag = "dark2";
            this.tabPageSNES.Text = "SNES";
            // 
            // tb_SNES_R
            // 
            this.tb_SNES_R.AcceptsReturn = true;
            this.tb_SNES_R.AcceptsTab = true;
            this.tb_SNES_R.BackColor = System.Drawing.Color.White;
            this.tb_SNES_R.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_R.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_R.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_R.Location = new System.Drawing.Point(190, 104);
            this.tb_SNES_R.Name = "tb_SNES_R";
            this.tb_SNES_R.ReadOnly = true;
            this.tb_SNES_R.ShortcutsEnabled = false;
            this.tb_SNES_R.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_R.TabIndex = 84;
            this.tb_SNES_R.Tag = "SNESRlight2";
            this.tb_SNES_R.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_R.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_R.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_L
            // 
            this.tb_SNES_L.AcceptsReturn = true;
            this.tb_SNES_L.AcceptsTab = true;
            this.tb_SNES_L.BackColor = System.Drawing.Color.White;
            this.tb_SNES_L.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_L.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_L.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_L.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb_SNES_L.Location = new System.Drawing.Point(190, 75);
            this.tb_SNES_L.Name = "tb_SNES_L";
            this.tb_SNES_L.ReadOnly = true;
            this.tb_SNES_L.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_SNES_L.ShortcutsEnabled = false;
            this.tb_SNES_L.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_L.TabIndex = 83;
            this.tb_SNES_L.Tag = "SNESLlight2";
            this.tb_SNES_L.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_L.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_L.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(169, 106);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 15);
            this.label20.TabIndex = 86;
            this.label20.Tag = "label";
            this.label20.Text = "R";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(169, 77);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(13, 15);
            this.label21.TabIndex = 85;
            this.label21.Tag = "label";
            this.label21.Text = "L";
            // 
            // tb_SNES_X
            // 
            this.tb_SNES_X.AcceptsReturn = true;
            this.tb_SNES_X.AcceptsTab = true;
            this.tb_SNES_X.BackColor = System.Drawing.Color.White;
            this.tb_SNES_X.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_X.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_X.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_X.Location = new System.Drawing.Point(190, 45);
            this.tb_SNES_X.Name = "tb_SNES_X";
            this.tb_SNES_X.ReadOnly = true;
            this.tb_SNES_X.ShortcutsEnabled = false;
            this.tb_SNES_X.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_X.TabIndex = 80;
            this.tb_SNES_X.Tag = "SNESXlight2";
            this.tb_SNES_X.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_X.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_X.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_A
            // 
            this.tb_SNES_A.AcceptsReturn = true;
            this.tb_SNES_A.AcceptsTab = true;
            this.tb_SNES_A.BackColor = System.Drawing.Color.White;
            this.tb_SNES_A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_A.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_A.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_A.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb_SNES_A.Location = new System.Drawing.Point(190, 16);
            this.tb_SNES_A.Name = "tb_SNES_A";
            this.tb_SNES_A.ReadOnly = true;
            this.tb_SNES_A.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_SNES_A.ShortcutsEnabled = false;
            this.tb_SNES_A.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_A.TabIndex = 79;
            this.tb_SNES_A.Tag = "SNESAlight2";
            this.tb_SNES_A.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_A.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_A.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(169, 47);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 15);
            this.label18.TabIndex = 82;
            this.label18.Tag = "label";
            this.label18.Text = "X";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(169, 18);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 15);
            this.label19.TabIndex = 81;
            this.label19.Tag = "label";
            this.label19.Text = "A";
            // 
            // tb_SNES_RIGHT
            // 
            this.tb_SNES_RIGHT.AcceptsReturn = true;
            this.tb_SNES_RIGHT.AcceptsTab = true;
            this.tb_SNES_RIGHT.BackColor = System.Drawing.Color.White;
            this.tb_SNES_RIGHT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_RIGHT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_RIGHT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_RIGHT.Location = new System.Drawing.Point(57, 216);
            this.tb_SNES_RIGHT.Name = "tb_SNES_RIGHT";
            this.tb_SNES_RIGHT.ReadOnly = true;
            this.tb_SNES_RIGHT.ShortcutsEnabled = false;
            this.tb_SNES_RIGHT.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_RIGHT.TabIndex = 70;
            this.tb_SNES_RIGHT.Tag = "SNESRIGHTlight2";
            this.tb_SNES_RIGHT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_RIGHT.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_RIGHT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_LEFT
            // 
            this.tb_SNES_LEFT.AcceptsReturn = true;
            this.tb_SNES_LEFT.AcceptsTab = true;
            this.tb_SNES_LEFT.BackColor = System.Drawing.Color.White;
            this.tb_SNES_LEFT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_LEFT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_LEFT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_LEFT.Location = new System.Drawing.Point(57, 188);
            this.tb_SNES_LEFT.Name = "tb_SNES_LEFT";
            this.tb_SNES_LEFT.ReadOnly = true;
            this.tb_SNES_LEFT.ShortcutsEnabled = false;
            this.tb_SNES_LEFT.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_LEFT.TabIndex = 69;
            this.tb_SNES_LEFT.Tag = "SNESLEFTlight2";
            this.tb_SNES_LEFT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_LEFT.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_LEFT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_DOWN
            // 
            this.tb_SNES_DOWN.AcceptsReturn = true;
            this.tb_SNES_DOWN.AcceptsTab = true;
            this.tb_SNES_DOWN.BackColor = System.Drawing.Color.White;
            this.tb_SNES_DOWN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_DOWN.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_DOWN.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_DOWN.Location = new System.Drawing.Point(57, 160);
            this.tb_SNES_DOWN.Name = "tb_SNES_DOWN";
            this.tb_SNES_DOWN.ReadOnly = true;
            this.tb_SNES_DOWN.ShortcutsEnabled = false;
            this.tb_SNES_DOWN.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_DOWN.TabIndex = 68;
            this.tb_SNES_DOWN.Tag = "SNESDOWNlight2";
            this.tb_SNES_DOWN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_DOWN.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_DOWN.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_UP
            // 
            this.tb_SNES_UP.AcceptsReturn = true;
            this.tb_SNES_UP.AcceptsTab = true;
            this.tb_SNES_UP.BackColor = System.Drawing.Color.White;
            this.tb_SNES_UP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_UP.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_UP.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_UP.Location = new System.Drawing.Point(57, 131);
            this.tb_SNES_UP.Name = "tb_SNES_UP";
            this.tb_SNES_UP.ReadOnly = true;
            this.tb_SNES_UP.ShortcutsEnabled = false;
            this.tb_SNES_UP.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_UP.TabIndex = 67;
            this.tb_SNES_UP.Tag = "SNESUPlight2";
            this.tb_SNES_UP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_UP.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_UP.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_START
            // 
            this.tb_SNES_START.AcceptsReturn = true;
            this.tb_SNES_START.AcceptsTab = true;
            this.tb_SNES_START.BackColor = System.Drawing.Color.White;
            this.tb_SNES_START.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_START.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_START.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_START.Location = new System.Drawing.Point(57, 103);
            this.tb_SNES_START.Name = "tb_SNES_START";
            this.tb_SNES_START.ReadOnly = true;
            this.tb_SNES_START.ShortcutsEnabled = false;
            this.tb_SNES_START.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_START.TabIndex = 66;
            this.tb_SNES_START.Tag = "SNESSTARTlight2";
            this.tb_SNES_START.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_START.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_START.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_SELECT
            // 
            this.tb_SNES_SELECT.AcceptsReturn = true;
            this.tb_SNES_SELECT.AcceptsTab = true;
            this.tb_SNES_SELECT.BackColor = System.Drawing.Color.White;
            this.tb_SNES_SELECT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_SELECT.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_SELECT.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_SELECT.Location = new System.Drawing.Point(57, 73);
            this.tb_SNES_SELECT.Name = "tb_SNES_SELECT";
            this.tb_SNES_SELECT.ReadOnly = true;
            this.tb_SNES_SELECT.ShortcutsEnabled = false;
            this.tb_SNES_SELECT.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_SELECT.TabIndex = 65;
            this.tb_SNES_SELECT.Tag = "SNESSELECTlight2";
            this.tb_SNES_SELECT.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_SELECT.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_SELECT.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_Y
            // 
            this.tb_SNES_Y.AcceptsReturn = true;
            this.tb_SNES_Y.AcceptsTab = true;
            this.tb_SNES_Y.BackColor = System.Drawing.Color.White;
            this.tb_SNES_Y.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_Y.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_Y.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_Y.Location = new System.Drawing.Point(57, 45);
            this.tb_SNES_Y.Name = "tb_SNES_Y";
            this.tb_SNES_Y.ReadOnly = true;
            this.tb_SNES_Y.ShortcutsEnabled = false;
            this.tb_SNES_Y.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_Y.TabIndex = 64;
            this.tb_SNES_Y.Tag = "SNESYlight2";
            this.tb_SNES_Y.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_Y.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_Y.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // tb_SNES_B
            // 
            this.tb_SNES_B.AcceptsReturn = true;
            this.tb_SNES_B.AcceptsTab = true;
            this.tb_SNES_B.BackColor = System.Drawing.Color.White;
            this.tb_SNES_B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_SNES_B.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tb_SNES_B.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_SNES_B.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tb_SNES_B.Location = new System.Drawing.Point(57, 16);
            this.tb_SNES_B.Name = "tb_SNES_B";
            this.tb_SNES_B.ReadOnly = true;
            this.tb_SNES_B.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tb_SNES_B.ShortcutsEnabled = false;
            this.tb_SNES_B.Size = new System.Drawing.Size(103, 23);
            this.tb_SNES_B.TabIndex = 63;
            this.tb_SNES_B.Tag = "SNESBlight2";
            this.tb_SNES_B.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxGather);
            this.tb_SNES_B.Leave += new System.EventHandler(this.FocusLost);
            this.tb_SNES_B.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyDownThis);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(10, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 15);
            this.label5.TabIndex = 78;
            this.label5.Tag = "label";
            this.label5.Text = "RIGHT";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(18, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 15);
            this.label6.TabIndex = 77;
            this.label6.Tag = "label";
            this.label6.Text = "LEFT";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(6, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 15);
            this.label7.TabIndex = 76;
            this.label7.Tag = "label";
            this.label7.Text = "DOWN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(28, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 75;
            this.label8.Tag = "label";
            this.label8.Text = "UP";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(8, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 15);
            this.label14.TabIndex = 74;
            this.label14.Tag = "label";
            this.label14.Text = "START";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(5, 75);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 15);
            this.label15.TabIndex = 73;
            this.label15.Tag = "label";
            this.label15.Text = "SELECT";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(36, 47);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 15);
            this.label16.TabIndex = 72;
            this.label16.Tag = "label";
            this.label16.Text = "Y";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(36, 18);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 15);
            this.label17.TabIndex = 71;
            this.label17.Tag = "label";
            this.label17.Text = "B";
            // 
            // tabPageN64
            // 
            this.tabPageN64.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPageN64.Location = new System.Drawing.Point(4, 22);
            this.tabPageN64.Name = "tabPageN64";
            this.tabPageN64.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageN64.Size = new System.Drawing.Size(421, 260);
            this.tabPageN64.TabIndex = 2;
            this.tabPageN64.Tag = "dark2";
            this.tabPageN64.Text = "N64";
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPageDebug.Controls.Add(this.btn_wipeParams);
            this.tabPageDebug.Controls.Add(this.btn_colorPick);
            this.tabPageDebug.Controls.Add(this.cb_pastel);
            this.tabPageDebug.Controls.Add(this.label2);
            this.tabPageDebug.Controls.Add(this.lb_val);
            this.tabPageDebug.Controls.Add(this.lb_Address);
            this.tabPageDebug.Controls.Add(this.nup_value);
            this.tabPageDebug.Controls.Add(this.tb_serialread);
            this.tabPageDebug.Controls.Add(this.btn_poke);
            this.tabPageDebug.Controls.Add(this.nup_address);
            this.tabPageDebug.Controls.Add(this.btn_peek);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDebug.Size = new System.Drawing.Size(421, 260);
            this.tabPageDebug.TabIndex = 3;
            this.tabPageDebug.Tag = "dark2";
            this.tabPageDebug.Text = "Debug";
            // 
            // btn_wipeParams
            // 
            this.btn_wipeParams.FlatAppearance.BorderSize = 0;
            this.btn_wipeParams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_wipeParams.Location = new System.Drawing.Point(6, 179);
            this.btn_wipeParams.Name = "btn_wipeParams";
            this.btn_wipeParams.Size = new System.Drawing.Size(122, 52);
            this.btn_wipeParams.TabIndex = 62;
            this.btn_wipeParams.Tag = "light2";
            this.btn_wipeParams.Text = "Wipe PARAM Folder";
            this.btn_wipeParams.UseVisualStyleBackColor = true;
            this.btn_wipeParams.Click += new System.EventHandler(this.ParamWipebtn_Click);
            // 
            // btn_colorPick
            // 
            this.btn_colorPick.FlatAppearance.BorderSize = 0;
            this.btn_colorPick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_colorPick.Location = new System.Drawing.Point(6, 122);
            this.btn_colorPick.Name = "btn_colorPick";
            this.btn_colorPick.Size = new System.Drawing.Size(122, 52);
            this.btn_colorPick.TabIndex = 61;
            this.btn_colorPick.Tag = "light2";
            this.btn_colorPick.Text = "Choose Color Theme";
            this.btn_colorPick.UseVisualStyleBackColor = true;
            this.btn_colorPick.Click += new System.EventHandler(this.ColorPick_Click);
            // 
            // cb_pastel
            // 
            this.cb_pastel.AutoSize = true;
            this.cb_pastel.Location = new System.Drawing.Point(8, 237);
            this.cb_pastel.Name = "cb_pastel";
            this.cb_pastel.Size = new System.Drawing.Size(92, 17);
            this.cb_pastel.TabIndex = 60;
            this.cb_pastel.Tag = "label";
            this.cb_pastel.Text = "Pastel Colors";
            this.cb_pastel.UseVisualStyleBackColor = true;
            this.cb_pastel.CheckedChanged += new System.EventHandler(this.Pastel_CheckedChanged);
            // 
            // lb_val
            // 
            this.lb_val.AutoSize = true;
            this.lb_val.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.lb_val.ForeColor = System.Drawing.Color.Black;
            this.lb_val.Location = new System.Drawing.Point(266, 16);
            this.lb_val.Name = "lb_val";
            this.lb_val.Size = new System.Drawing.Size(36, 15);
            this.lb_val.TabIndex = 59;
            this.lb_val.Tag = "label";
            this.lb_val.Text = "Value";
            // 
            // lb_Address
            // 
            this.lb_Address.AutoSize = true;
            this.lb_Address.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.lb_Address.ForeColor = System.Drawing.Color.Black;
            this.lb_Address.Location = new System.Drawing.Point(140, 17);
            this.lb_Address.Name = "lb_Address";
            this.lb_Address.Size = new System.Drawing.Size(49, 15);
            this.lb_Address.TabIndex = 58;
            this.lb_Address.Tag = "label";
            this.lb_Address.Text = "Address";
            // 
            // nup_value
            // 
            this.nup_value.Location = new System.Drawing.Point(269, 36);
            this.nup_value.Name = "nup_value";
            this.nup_value.Size = new System.Drawing.Size(120, 22);
            this.nup_value.TabIndex = 14;
            this.nup_value.TabStop = false;
            this.nup_value.Tag = "light2";
            // 
            // btn_poke
            // 
            this.btn_poke.Enabled = false;
            this.btn_poke.FlatAppearance.BorderSize = 0;
            this.btn_poke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_poke.Location = new System.Drawing.Point(6, 64);
            this.btn_poke.Name = "btn_poke";
            this.btn_poke.Size = new System.Drawing.Size(122, 52);
            this.btn_poke.TabIndex = 13;
            this.btn_poke.TabStop = false;
            this.btn_poke.Tag = "light2";
            this.btn_poke.Text = "Poke Memory.";
            this.btn_poke.UseVisualStyleBackColor = true;
            this.btn_poke.Click += new System.EventHandler(this.Poke_Click);
            // 
            // nup_address
            // 
            this.nup_address.Location = new System.Drawing.Point(143, 36);
            this.nup_address.Name = "nup_address";
            this.nup_address.Size = new System.Drawing.Size(120, 22);
            this.nup_address.TabIndex = 12;
            this.nup_address.TabStop = false;
            this.nup_address.Tag = "light2";
            // 
            // btn_peek
            // 
            this.btn_peek.Enabled = false;
            this.btn_peek.FlatAppearance.BorderSize = 0;
            this.btn_peek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_peek.Location = new System.Drawing.Point(6, 6);
            this.btn_peek.Name = "btn_peek";
            this.btn_peek.Size = new System.Drawing.Size(122, 52);
            this.btn_peek.TabIndex = 11;
            this.btn_peek.TabStop = false;
            this.btn_peek.Tag = "light2";
            this.btn_peek.Text = "Peek Memory.";
            this.btn_peek.UseVisualStyleBackColor = true;
            this.btn_peek.Click += new System.EventHandler(this.Peek_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageNES);
            this.tabControl.Controls.Add(this.tabPageSNES);
            this.tabControl.Controls.Add(this.tabPageN64);
            this.tabControl.Controls.Add(this.tabPageDebug);
            this.tabControl.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.tabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl.Location = new System.Drawing.Point(12, 136);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(429, 286);
            this.tabControl.TabIndex = 5;
            this.tabControl.TabStop = false;
            this.tabControl.Tag = "TAB";
            // 
            // lb_debug
            // 
            this.lb_debug.AutoSize = true;
            this.lb_debug.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.lb_debug.ForeColor = System.Drawing.Color.White;
            this.lb_debug.Location = new System.Drawing.Point(334, 90);
            this.lb_debug.Name = "lb_debug";
            this.lb_debug.Size = new System.Drawing.Size(88, 15);
            this.lb_debug.TabIndex = 7;
            this.lb_debug.Tag = "label";
            this.lb_debug.Text = "Debug Console";
            // 
            // tb_console
            // 
            this.tb_console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_console.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_console.Location = new System.Drawing.Point(250, 12);
            this.tb_console.Multiline = true;
            this.tb_console.Name = "tb_console";
            this.tb_console.ReadOnly = true;
            this.tb_console.Size = new System.Drawing.Size(172, 75);
            this.tb_console.TabIndex = 6;
            this.tb_console.TabStop = false;
            // 
            // btn_sendremap
            // 
            this.btn_sendremap.Enabled = false;
            this.btn_sendremap.FlatAppearance.BorderSize = 0;
            this.btn_sendremap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sendremap.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btn_sendremap.ForeColor = System.Drawing.Color.White;
            this.btn_sendremap.Location = new System.Drawing.Point(131, 12);
            this.btn_sendremap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sendremap.Name = "btn_sendremap";
            this.btn_sendremap.Size = new System.Drawing.Size(114, 75);
            this.btn_sendremap.TabIndex = 1;
            this.btn_sendremap.TabStop = false;
            this.btn_sendremap.Tag = "light2";
            this.btn_sendremap.Text = "Write current tab to EEPROM";
            this.btn_sendremap.UseVisualStyleBackColor = true;
            this.btn_sendremap.Click += new System.EventHandler(this.Sendremap_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_motto);
            this.panel1.Controls.Add(this.tb_console);
            this.panel1.Controls.Add(this.btn_sendremap);
            this.panel1.Controls.Add(this.lb_debug);
            this.panel1.Controls.Add(this.btn_stopstart);
            this.panel1.Controls.Add(this.cb_portlist);
            this.panel1.Location = new System.Drawing.Point(12, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 117);
            this.panel1.TabIndex = 64;
            this.panel1.Tag = "dark2";
            // 
            // lb_motto
            // 
            this.lb_motto.AutoSize = true;
            this.lb_motto.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Italic);
            this.lb_motto.ForeColor = System.Drawing.Color.White;
            this.lb_motto.Location = new System.Drawing.Point(9, 90);
            this.lb_motto.Name = "lb_motto";
            this.lb_motto.Size = new System.Drawing.Size(164, 15);
            this.lb_motto.TabIndex = 8;
            this.lb_motto.Tag = "label";
            this.lb_motto.Text = "Remap to your hearts content";
            // 
            // tab
            // 
            this.tab.Location = new System.Drawing.Point(4, 22);
            this.tab.Name = "tab";
            this.tab.Padding = new System.Windows.Forms.Padding(3);
            this.tab.Size = new System.Drawing.Size(421, 260);
            this.tab.TabIndex = 4;
            this.tab.Text = "tabPage1";
            this.tab.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(453, 434);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Tag = "dark1";
            this.Text = "Remapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPageNES.ResumeLayout(false);
            this.tabPageNES.PerformLayout();
            this.tabPageSNES.ResumeLayout(false);
            this.tabPageSNES.PerformLayout();
            this.tabPageDebug.ResumeLayout(false);
            this.tabPageDebug.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nup_value)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nup_address)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_serialread;
        private System.Windows.Forms.Button btn_stopstart;
        private System.Windows.Forms.ComboBox cb_portlist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageNES;
        private System.Windows.Forms.TabPage tabPageSNES;
        private System.Windows.Forms.TabPage tabPageN64;
        private System.Windows.Forms.TabPage tabPageDebug;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label lb_debug;
        private System.Windows.Forms.TextBox tb_console;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tb_NES_A;
        private System.Windows.Forms.NumericUpDown nup_address;
        private System.Windows.Forms.Button btn_peek;
        private System.Windows.Forms.Button btn_poke;
        private System.Windows.Forms.Label lb_val;
        private System.Windows.Forms.Label lb_Address;
        private System.Windows.Forms.NumericUpDown nup_value;
        private System.Windows.Forms.TextBox tb_NES_RIGHT;
        private System.Windows.Forms.TextBox tb_NES_LEFT;
        private System.Windows.Forms.TextBox tb_NES_DOWN;
        private System.Windows.Forms.TextBox tb_NES_UP;
        private System.Windows.Forms.TextBox tb_NES_START;
        private System.Windows.Forms.TextBox tb_NES_SELECT;
        private System.Windows.Forms.TextBox tb_NES_B;
        private System.Windows.Forms.Button btn_sendremap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_SNES_RIGHT;
        private System.Windows.Forms.TextBox tb_SNES_LEFT;
        private System.Windows.Forms.TextBox tb_SNES_DOWN;
        private System.Windows.Forms.TextBox tb_SNES_UP;
        private System.Windows.Forms.TextBox tb_SNES_START;
        private System.Windows.Forms.TextBox tb_SNES_SELECT;
        private System.Windows.Forms.TextBox tb_SNES_Y;
        private System.Windows.Forms.TextBox tb_SNES_B;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb_SNES_R;
        private System.Windows.Forms.TextBox tb_SNES_L;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tb_SNES_X;
        private System.Windows.Forms.TextBox tb_SNES_A;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.CheckBox cb_pastel;
        private System.Windows.Forms.Button btn_colorPick;
        private System.Windows.Forms.Label lb_motto;
        private System.Windows.Forms.Button btn_wipeParams;
        private System.Windows.Forms.TabPage tab;
    }
}

