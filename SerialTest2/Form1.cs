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

        int[] init_NES_btns = new int[8];

        int[] init_SNES_btns = new int[12];

        int[] init_N64_btns = new int[18];


        string[] init_NES_btns_string = new string[8];

        string[] init_SNES_btns_string = new string[12];

        //string[] init_N64_btns_string = new string[18];



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
            if (!_serialPort.IsOpen)
                {
                    throwError("COM Port is not open!");
                    return;
                }
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
            SerialTest2.Params.SetParam("N64BUTTONS", String.Join(",", init_N64_btns));
            //N64BUTTONS
            //COLORS
            //AKA "STEAL" CODE FROM RTC
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            String a = SerialTest2.Params.ReadParam("NESBUTTONS");
            String b = SerialTest2.Params.ReadParam("SNESBUTTONS");
            String c = SerialTest2.Params.ReadParam("N64BUTTONS");

            if (SerialTest2.Params.IsParamSet("NESBUTTONS"))    //if there is a param for the nes buttons already
            {
                this.init_NES_btns = Array.ConvertAll(a.Split(','), int.Parse); // load it in
            }
            if (SerialTest2.Params.IsParamSet("SNESBUTTONS"))    //if there is a param for the nes buttons already
            {
                this.init_SNES_btns = Array.ConvertAll(b.Split(','), int.Parse); // load it in
            }
            if (SerialTest2.Params.IsParamSet("N64BUTTONS"))    //if there is a param for the nes buttons already
            {
                this.init_N64_btns = Array.ConvertAll(b.Split(','), int.Parse); // load it in
            }


            //load the button states as text into the thing
            populateStingArrays();
            populateTextBoxes();
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
                case "Tab": return 179;
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


        string revKeyToKey(int key)
        {
            switch (key)
            {
                //why yes, this was painful to do manually
                case 177: return "Escape";
                case 96: return "`";
                case 49: return "1";
                case 50: return "2";
                case 51: return "3";
                case 52: return "4";
                case 53: return "5";
                case 54: return "6";
                case 55: return "7";
                case 56: return "8";
                case 57: return "9";
                case 48: return "0";
                case 45: return "-";
                case 61: return "=";
                case 178: return "Backspace";
                case 81: return "Q";
                case 87: return "W";
                case 69: return "E";
                case 82: return "R";
                case 84: return "T";
                case 89: return "Y";
                case 85: return "U";
                case 73: return "I";
                case 79: return "O";
                case 80: return "P";
                case 91: return "[";
                case 93: return "]";
                case 176: return "Enter";
                case 179: return "Tab";
                case 65: return "A";
                case 83: return "S";
                case 68: return "D";
                case 70: return "F";
                case 71: return "G";
                case 72: return "H";
                case 74: return "J";
                case 75: return "K";
                case 76: return "L";
                case 59: return ";";
                case 39: return "'";
                case 92: return "\\";
                case 129: return "Shift";
                case 90: return "Z";
                case 88: return "X";
                case 67: return "C";
                case 86: return "V";
                case 66: return "B";
                case 78: return "N";
                case 77: return "M";
                case 44: return ",";
                case 46: return ".";
                case 47: return "/";
                case 128: return "ControlKey";
                //case : return "LWin";
                case 130: return "Alt";
                case 32: return "Space";
                //case : return "Apps";
                //case : return "Ins";
                //case : return "Del";
                //case : return "Home";
                //case : return "End";
                //case : return "PgUp";
                //case : return "PgDn";
                case 218: return "Up";
                case 216: return "Left";
                case 217: return "Down";
                case 215: return "Right";
                default: return " ";
            }

        }

        void populateStingArrays()
        {
            
            for (int p = 0; p < 8; p++) init_NES_btns_string[p] = revKeyToKey(init_NES_btns[p]);
            
            for (int s = 0; s < 12; s++) init_SNES_btns_string[s] = revKeyToKey(init_SNES_btns[s]);

            //for (int b = 0; b < 18; b++) init_N64_btns_string[b] = revKeyToKey(init_N64_btns[b]);

        }

        void populateTextBoxes()
        {
            tb_NES_A.Text = init_NES_btns_string[0];
            tb_NES_B.Text = init_NES_btns_string[1];
            tb_NES_SELECT.Text = init_NES_btns_string[2];
            tb_NES_START.Text = init_NES_btns_string[3];
            tb_NES_UP.Text = init_NES_btns_string[4];
            tb_NES_DOWN.Text = init_NES_btns_string[5];
            tb_NES_LEFT.Text = init_NES_btns_string[6];
            tb_NES_RIGHT.Text = init_NES_btns_string[7];
        }

        void neatHack(bool yup)
        {
            tb_NES_A.TabStop = yup;
            tb_NES_B.TabStop = yup;
            tb_NES_SELECT.TabStop = yup;
            tb_NES_START.TabStop = yup;
            tb_NES_UP.TabStop = yup;
            tb_NES_DOWN.TabStop = yup;
            tb_NES_LEFT.TabStop = yup;
            tb_NES_RIGHT.TabStop = yup;
        }

        // textbox KeyDown event handler
        private void tb_NES_A_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_A.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[0] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void tb_NES_B_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_B.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[1] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void tb_NES_SELECT_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_SELECT.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[2] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void tb_NES_START_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_START.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[3] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void tb_NES_UP_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_UP.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[4] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void tb_NES_DOWN_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_DOWN.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[5] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void tb_NES_LEFT_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_LEFT.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[6] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void tb_NES_RIGHT_KeyDown(object sender, KeyEventArgs e)
        {
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                tb_NES_RIGHT.Text = revKeyToKey(keyToKey(key));
                tb_console.Text =   "Key: " + keyToKey(key);
                init_NES_btns[7] = keyToKey(key);
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);
            SelectNextControl(ActiveControl, true, true, true, true);
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
        }

        private void peek_Click(object sender, EventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                    throwError("COM Port is not open!");
                    return;
            }
            int adr = (int)ADR.Value;
            int val = 0;
            _serialPort.Write("PE," + adr.ToString() + "," + val.ToString() + "!");
        }

        private void Poke_Click(object sender, EventArgs e)
        {
            if (!_serialPort.IsOpen)
            {
                    throwError("COM Port is not open!");
                    return;
            }
            int adr = (int)ADR.Value;
            int val = (int)VAL.Value;
            _serialPort.Write("PO," + adr.ToString() + "," + val.ToString() + "!");
            tb_console.Text = ("PO," + adr.ToString() + "," + val.ToString() + "!");
        }

        private void tb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            neatHack(false);
        }

        void throwError(String message)
        {
            tb_console.Text = message;
            //MessageBox.Show(message); 
        }

        ///need to keep track of what keys were changed in remap to minimize r/w of eeprom
    }
}
