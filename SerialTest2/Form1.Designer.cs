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
            this.btn_sendremap = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.button8 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_console = new System.Windows.Forms.TextBox();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_serialread
            // 
            this.tb_serialread.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_serialread.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.tb_serialread.Location = new System.Drawing.Point(235, 42);
            this.tb_serialread.Multiline = true;
            this.tb_serialread.Name = "tb_serialread";
            this.tb_serialread.ReadOnly = true;
            this.tb_serialread.Size = new System.Drawing.Size(122, 20);
            this.tb_serialread.TabIndex = 0;
            this.tb_serialread.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_stopstart
            // 
            this.btn_stopstart.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_stopstart.Enabled = false;
            this.btn_stopstart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_stopstart.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.btn_stopstart.ForeColor = System.Drawing.Color.White;
            this.btn_stopstart.Location = new System.Drawing.Point(12, 12);
            this.btn_stopstart.Name = "btn_stopstart";
            this.btn_stopstart.Size = new System.Drawing.Size(144, 50);
            this.btn_stopstart.TabIndex = 1;
            this.btn_stopstart.Text = "Select A Port";
            this.btn_stopstart.UseVisualStyleBackColor = false;
            this.btn_stopstart.Click += new System.EventHandler(this.button1_Click);
            // 
            // cb_portlist
            // 
            this.cb_portlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_portlist.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.cb_portlist.FormattingEnabled = true;
            this.cb_portlist.Location = new System.Drawing.Point(202, 11);
            this.cb_portlist.Name = "cb_portlist";
            this.cb_portlist.Size = new System.Drawing.Size(155, 23);
            this.cb_portlist.TabIndex = 3;
            this.cb_portlist.DropDown += new System.EventHandler(this.cb_portlist_DropDown);
            this.cb_portlist.SelectedIndexChanged += new System.EventHandler(this.cb_portlist_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(164, 15);
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
            this.label2.Location = new System.Drawing.Point(162, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Serial Read:";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPage1.Controls.Add(this.btn_sendremap);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 343);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NES";
            // 
            // btn_sendremap
            // 
            this.btn_sendremap.Location = new System.Drawing.Point(663, 310);
            this.btn_sendremap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sendremap.Name = "btn_sendremap";
            this.btn_sendremap.Size = new System.Drawing.Size(102, 28);
            this.btn_sendremap.TabIndex = 1;
            this.btn_sendremap.Text = "Send Remapping";
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
            this.tabPage3.Text = "N64";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.button8);
            this.tabPage4.Controls.Add(this.button1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(768, 343);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Other";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send Request to remap NES";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(12, 69);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 369);
            this.tabControl1.TabIndex = 5;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(353, 6);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(122, 52);
            this.button8.TabIndex = 6;
            this.button8.Text = "Remap Start to t";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "No Words Yet";
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
            this.tb_console.Size = new System.Drawing.Size(311, 23);
            this.tb_console.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Text = "Serial Tester";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_console;
    }
}

