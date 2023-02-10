using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace SerialTest2
{

    public partial class Form1 : Form
    {


        string background1Color = "#F492A5";

        string background2Color = "#F9CBD0";

        string button2Color = "#6E82B7";

        string button1Color = "#95DAF8";


        int[] init_SNES_btns = new int[] { 
            66,     //B
            89,      //Y
            32,      //SELECT
            13,      //START
            38,      //UP   arrow keys will be wrong
            0,      //DOWN
            0,      //LEFT
            0,      //RIGHT
            0,      //A
            0,      //X
            0,      //L
            0,      //R
        };

        int[] init_NES_btns = new int[] {
            0,     //A
            0,      //B
            0,      //SELECT
            0,      //START
            0,      //UP
            0,      //DOWN
            0,      //LEFT
            0,      //RIGHT
        };

        SerialPort _serialPort = new System.IO.Ports.SerialPort("COM0", 9600);


        private string serialNow;
        private readonly object _sync = new object();
        public Form1()
        {
            _serialPort.Encoding = Encoding.UTF8;
            _serialPort.DtrEnable = true;
            _serialPort.ReadTimeout = 2000;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            InitializeComponent();
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            serialNow = _serialPort.ReadExisting();
            tb_serialread.Invoke(new Action(() => tb_serialread.Text = serialNow));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                btn_stopstart.Text = "Open";
            }
            else
            {
                _serialPort.Open();
                btn_stopstart.Text = "Close";
            }


        }

        private void btn_sendremap_Click(object sender, EventArgs e)
        {
            //check the button boxes
            //add all the buttons to change in an array




            handShakeNow();
            remapNow("NES");


        }

        private void handShakeNow()
        {
            _serialPort.Write("SREQ!"); //request serial operation
            while (true)
            {
                tb_console.Text = serialNow;
                if (serialNow == "SACK") ;
                {
                    tb_console.Text = "SREQ acknowledged.";
                    return;
                }
            }
        }
        private void remapNow(string console)
        {
            _serialPort.Write("REMAP!"); //request remap
            while (true)
            {
                if (serialNow == "REMAPACK") ;
                {
                    switch (console)
                    {
                        case "NES":
                            _serialPort.Write("NES!"); // state NES
                            while (true)
                            {
                                if (serialNow == "NESACK") ;
                                {
                                    tb_console.Text = "Remap a button on the NES controller? You got it boss!";
                                    return;
                                }
                            }
                        case "SNES":
                            _serialPort.Write("SNES!"); // state NES
                            while (true)
                            {
                                if (serialNow == "SNESACK") ;
                                {
                                    tb_console.Text = "Remap a button on the SNES controller? You got it boss!";
                                    return;
                                }
                            }
                        case "N64":
                            _serialPort.Write("N64!"); // state NES
                            while (true)
                            {
                                if (serialNow == "N64SACK") ;
                                {
                                    tb_console.Text = "Remap a button on the N64 controller? You got it boss!";
                                    return;
                                }
                            }
                    }
                }
            }
        }

        private void ColorControls(Control parent)
        {
            if (parent is Form && parent.Tag != null && parent.Tag.ToString().Contains("background1"))
            {
                parent.BackColor = Color.FromArgb(244, 146, 165);
            }

            foreach (Control control in parent.Controls)
            {
                if (control is TabControl)
                {
                    foreach (TabPage tabPage in ((TabControl)control).TabPages)
                    {
                        if (tabPage.Tag != null)
                        {
                            if (tabPage.Tag.ToString().Contains("background1"))
                            {
                                tabPage.BackColor = ColorTranslator.FromHtml(background1Color);
                            }
                            if (tabPage.Tag.ToString().Contains("background2"))
                            {
                                tabPage.BackColor = ColorTranslator.FromHtml(background2Color);
                            }
                        }

                        ColorControls(tabPage);
                    }
                }
                else if (control.Tag != null)
                {
                    if (control.Tag.ToString().Contains("background1"))
                    {
                        control.BackColor = ColorTranslator.FromHtml(background1Color);
                    }
                    if (control.Tag.ToString().Contains("background2"))
                    {
                        control.BackColor = ColorTranslator.FromHtml(background2Color);
                    }
                    if (control.Tag.ToString().Contains("button2"))
                    {
                        control.BackColor = ColorTranslator.FromHtml(button2Color);
                    }
                    if (control.Tag.ToString().Contains("button1"))
                    {
                        control.BackColor = ColorTranslator.FromHtml(button1Color);
                    }
                }

                if (control.HasChildren)
                {
                    ColorControls(control);
                }
            }
        }

        private void cb_portlist_DropDown(object sender, EventArgs e)
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            var selectedItem = cb_portlist.SelectedItem;
            cb_portlist.Items.Clear();
            if (selectedItem != null)
            {
                cb_portlist.Items.Add(selectedItem);
                cb_portlist.SelectedItem = selectedItem;
            }


            // Display each port name to the console.
            foreach (string s in SerialPort.GetPortNames())
            {
                cb_portlist.Items.Add(s);
            }
        }

        private void cb_portlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            String port = (string)cb_portlist.SelectedItem;
            _serialPort.Close();
            btn_stopstart.Text = "Open!";
            btn_stopstart.Enabled = true;
            // Change the serial port's COM
            _serialPort.PortName = port;
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            _serialPort.Write("STOP!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorControls(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tb_console.Text = "It's: " + init_NES_btns[0];
        }
        private void btn_saveSettings_Click(object sender, EventArgs e)
        {
            //SerialTest2.Params.IsParamSet("NESBUTTONS");
            //save all settings to a PARAM folder 
            SerialTest2.Params.SetParam("NESBUTTONS", String.Join(",", init_NES_btns));
            SerialTest2.Params.SetParam("SNESBUTTONS", String.Join(",", init_SNES_btns));
            //N64BUTTONS
            //COLORS
            //AKA "STEAL" CODE FROM RTC
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {

            //somehow, hide curosr
           // TextBox textbox = sender as TextBox;
           
            //textbox.Enabled = false;
            //textbox.Enabled = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            String a = SerialTest2.Params.ReadParam("NESBUTTONS");
            String b = SerialTest2.Params.ReadParam("SNESBUTTONS");

            if (SerialTest2.Params.IsParamSet("NESBUTTONS"))    //if there is a param for the nes buttons already
            {
                this.init_NES_btns = Array.ConvertAll(a.Split(','), int.Parse); // load it in
            }
            if (SerialTest2.Params.IsParamSet("SNESBUTTONS"))    //if there is a param for the nes buttons already
            {
                this.init_SNES_btns = Array.ConvertAll(b.Split(','), int.Parse); // load it in
            }


            //load the button states as text into the thing
            btn_NES_A.Text = ((char)init_NES_btns[0]).ToString();
            btn_NES_B.Text = ((char)init_NES_btns[1]).ToString();
            btn_NES_SELECT.Text = ((char)init_NES_btns[2]).ToString();
            btn_NES_START.Text = ((char)init_NES_btns[3]).ToString();
            btn_NES_UP.Text = ((char)init_NES_btns[4]).ToString();
            btn_NES_DOWN.Text = ((char)init_NES_btns[5]).ToString();
            btn_NES_LEFT.Text = ((char)init_NES_btns[6]).ToString();
            btn_NES_RIGHT.Text = ((char)init_NES_btns[7]).ToString();
        }
        private void btn_NES_A_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_A.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[0] = e.KeyValue;
            tb_console.Text =   "Key: " + e.KeyValue.ToString();
        }
        private void btn_NES_B_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_B.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[1] = e.KeyValue;
            tb_console.Text = "Key: " + e.KeyValue.ToString();
        }
        private void btn_NES_SELECT_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_SELECT.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[2] = e.KeyValue;
            tb_console.Text = "Key: " + e.KeyValue.ToString();
        }
        private void btn_NES_START_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_START.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[3] = e.KeyValue;
            tb_console.Text = "Key: " + e.KeyValue.ToString();
        }
        private void btn_NES_UP_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_UP.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[4] = e.KeyValue;
            tb_console.Text = "Key: " + e.KeyValue.ToString();
        }
        private void btn_NES_DOWN_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_DOWN.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[5] = e.KeyValue;
            tb_console.Text = "Key: " + e.KeyValue.ToString();
        }
        private void btn_NES_LEFT_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_LEFT.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[6] = e.KeyValue;
            tb_console.Text = "Key: " + e.KeyValue.ToString();
        }
        private void btn_NES_RIGHT_Pressed(object sender, KeyEventArgs e)
        {
            
            btn_NES_RIGHT.Text = ((char)e.KeyValue).ToString();
            init_NES_btns[7] = e.KeyValue;
            tb_console.Text = "Key: " + e.KeyValue.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                btn_stop.ForeColor = MyDialog.Color;
        }
    }
}
