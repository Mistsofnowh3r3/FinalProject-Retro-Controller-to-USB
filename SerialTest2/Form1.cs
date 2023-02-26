using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace SerialTest2
{

    public partial class Form1 : Form
    {

        string background1Color = "#F492A5";

        string background2Color = "#F9CBD0";

        string button2Color = "#6E82B7";

        string button1Color = "#95DAF8";

        // string variable to store the key name
        private string key;


        int[] init_SNES_btns = new int[] {
    'z',     //B
    'x',      //Y
    0xb0,      //SELECT
    32,      //START
    0xDA,      //UP   arrow keys will be wrong
    0xD9,      //DOWN
    0xD8,      //LEFT
    0xD7,      //RIGHT
    'a',      //A
    's',      //X
    'q',      //L
    'w',      //R
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
            remapNow();
        }
        private void remapNow()
        {
            for (int i = 0; i < 8; i++) 
            {
                _serialPort.Write("PO" + "," + i + "," + init_NES_btns[i] + "!");
                Thread.Sleep(30);
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

        }


        int keyToKey(String key)
        {
            switch (key)
            {
                //why yes, this was painful to do manually
                case "Escape": return 177;
                case "Oemtilde": return 96;
                case "1": return 49;
                case "2": return 50;
                case "3": return 51;
                case "4": return 52;
                case "5": return 53;
                case "6": return 54;
                case "7": return 55;
                case "8": return 56;
                case "9": return 57;
                case "0": return 48;
                case "OemMinus": return 45;
                case "Oemplus": return 61;
                case "Back": return 178;
                case "Q": return 81;
                case "W": return 87;
                case "E": return 69;
                case "R": return 82;
                case "T": return 84;
                case "Y": return 89;
                case "U": return 85;
                case "I": return 73;
                case "O": return 79;
                case "P": return 80;
                case "OemOpenBrackets": return 91;
                case "Oem6": return 93;
                case "Enter": return 176;
                //case "Capital": return
                case "A": return 65;
                case "S": return 83;
                case "D": return 68;
                case "F": return 70;
                case "G": return 71;
                case "H": return 72;
                case "J": return 74;
                case "K": return 75;
                case "L": return 76;
                case "Oem1": return 59;
                case "Oem7": return 39;
                case "Oem5": return 92;
                case "ShiftKey": return 129;
                case "Z": return 90;
                case "X": return 88;
                case "C": return 67;
                case "V": return 86;
                case "B": return 66;
                case "N": return 78;
                case "M": return 77;
                case "Oemcomma": return 44;
                case "OemPeriod": return 46;
                case "OemQuestion": return 47;
                case "ControlKey": return 128;
                //case "LWin": return
                case "Menu": return 130;
                case "Space": return 32;
                //case "Apps": return
                //case "Ins": return
                //case "Del": return
                //case "Home": return
                //case "End": return
                //case "PgUp": return
                //case "PgDn": return
                case "Up": return 218;
                case "Left": return 216;
                case "Down": return 217;
                case "Right": return 215;
                default: return 0;
            }

        }


        // textbox KeyDown event handler
        private void tb_NES_A_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_A.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[0] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
        }

        private void tb_NES_B_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_B.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[1] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
        }

        private void tb_NES_SELECT_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_SELECT.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[2] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
        }

        private void tb_NES_START_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_START.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[3] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
        }

        private void tb_NES_UP_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_UP.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[4] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
        }

        private void tb_NES_DOWN_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_DOWN.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[5] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
        }

        private void tb_NES_LEFT_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_LEFT.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[6] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
        }

        private void tb_NES_RIGHT_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            tb_NES_RIGHT.Text = key;
            tb_console.Text =   "Key: " + keyToKey(key);

            if (keyToKey(key) != 0)
            {
                init_NES_btns[7] = keyToKey(key);
            }
            // prevent the key from being processed by the control
            e.Handled = true;
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

        private void peek_Click(object sender, EventArgs e)
        {
            int adr = (int)ADR.Value;
            int val = 0;
            _serialPort.Write("PE," + adr.ToString() + "," + val.ToString() + "!");
        }

        private void Poke_Click(object sender, EventArgs e)
        {
            int adr = (int)ADR.Value;
            int val = (int)VAL.Value;
            _serialPort.Write("PO," + adr.ToString() + "," + val.ToString() + "!");
            tb_console.Text = ("PO," + adr.ToString() + "," + val.ToString() + "!");
        }

        ///need to keep track of what keys were changed in remap to minimize r/w of eeprom
    }
}
