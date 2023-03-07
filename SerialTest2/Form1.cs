using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.Mime.MediaTypeNames;

namespace ccAdapterRemapper
{

    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);



        #region variables

        //serious colors (aka RTCV)
        string background1Color = "#23303e";

        string background2Color = "#2c3c4c";

        string button2Color = "#4b5d6f";

        string button1Color = "#4b5d6f";

        string focusColor = "#0078d7";

        string needsSaving = "#ff6347";

        string textColor = "#ffffff";




        //fun colors
        //string background1Color = "#F492A5";

        //string background2Color = "#F9CBD0";

        //string button2Color = "#57cbe6";

        //string button1Color = "#95DAF8";

        //string focusColor = "#4efcee";

        //string needsSaving = "#60fca6";

        //string greenDark = "#2f734e";


        // string variable to store the key name
        private string key;

        int[] onload_NES_btns = new int[8];

        int[] onload_SNES_btns = new int[12];

        int[] onload_N64_btns = new int[18];

        int[] working_NES_btns = new int[8];

        int[] working_SNES_btns = new int[12];

        int[] working_N64_btns = new int[18];

        string[] working_NES_btns_string = new string[8];

        string[] working_SNES_btns_string = new string[12];

        string[] working_N64_btns_string = new string[18];


        SerialPort _serialPort = new System.IO.Ports.SerialPort("COM0", 9600);


        private string serialNow;
        private readonly object _sync = new object();

        #endregion variables





        public Form1()
        {
            _serialPort.Encoding = Encoding.UTF8;
            _serialPort.DtrEnable = true;
            _serialPort.ReadTimeout = 2000;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (ccAdapterRemapper.Params.IsParamSet("NESBUTTONS"))    //if there is a param for the nes buttons already
            {
                this.working_NES_btns = Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("NESBUTTONS").Split(','), int.Parse); // load it in
                this.onload_NES_btns = Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("NESBUTTONS").Split(','), int.Parse); // load it in
            }
            if (ccAdapterRemapper.Params.IsParamSet("SNESBUTTONS"))    //if there is a param for the nes buttons already
            {
                this.working_SNES_btns = Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("SNESBUTTONS").Split(','), int.Parse); // load it in
                this.onload_SNES_btns = Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("SNESBUTTONS").Split(','), int.Parse); // load it in
            }
            if (ccAdapterRemapper.Params.IsParamSet("N64BUTTONS"))    //if there is a param for the nes buttons already
            {
                this.working_N64_btns = Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("N64BUTTONS").Split(','), int.Parse); // load it in
                this.onload_N64_btns = Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("N64BUTTONS").Split(','), int.Parse); // load it in
            }


            //load the button states as text into the thing
            populateStingArrays();
            populateTextBoxes();
            cb_portlist.Items.Add("Select a port");
            cb_portlist.Text = "Select a port";



            //tb_NES_A.Cursor = Cursors.Arrow;
            tb_NES_A.GotFocus +=   hideCaretAndSelection;
            tb_NES_B.GotFocus +=   hideCaretAndSelection;
            tb_NES_SELECT.GotFocus +=   hideCaretAndSelection;
            tb_NES_START.GotFocus +=   hideCaretAndSelection;
            tb_NES_UP.GotFocus +=   hideCaretAndSelection;
            tb_NES_DOWN.GotFocus +=   hideCaretAndSelection;
            tb_NES_LEFT.GotFocus +=   hideCaretAndSelection;
            tb_NES_RIGHT.GotFocus +=   hideCaretAndSelection;
            ColorControls(this);
        }

        void populateStingArrays()
        {
            
            for (int p = 0; p < 8; p++) working_NES_btns_string[p] = revKeyToKey(working_NES_btns[p]);
            
            for (int s = 0; s < 12; s++) working_SNES_btns_string[s] = revKeyToKey(working_SNES_btns[s]);

            //for (int b = 0; b < 18; b++) working_N64_btns_string[b] = revKeyToKey(onload_N64_btns[b]);

        }

        void populateTextBoxes()
        {
            tb_NES_A.Text = working_NES_btns_string[0];
            tb_NES_B.Text = working_NES_btns_string[1];
            tb_NES_SELECT.Text = working_NES_btns_string[2];
            tb_NES_START.Text = working_NES_btns_string[3];
            tb_NES_UP.Text = working_NES_btns_string[4];
            tb_NES_DOWN.Text = working_NES_btns_string[5];
            tb_NES_LEFT.Text = working_NES_btns_string[6];
            tb_NES_RIGHT.Text = working_NES_btns_string[7];
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

        


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            serialNow = _serialPort.ReadExisting();
            tb_serialread.Invoke(new Action(() => tb_serialread.Text = serialNow));
        }

        private void openCOM(object sender, EventArgs e)
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

        private void ColorControls(Control parent)
        {
            if (parent is Form && parent.Tag != null && parent.Tag.ToString().Contains("background1"))
            {
                if (parent.Tag.ToString().Contains("background1"))
                {
                    parent.BackColor = ColorTranslator.FromHtml(background1Color);
                }
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
                        control.ForeColor = ColorTranslator.FromHtml(textColor);
                    }
                    if (control.Tag.ToString().Contains("button1"))
                    {
                        control.BackColor = ColorTranslator.FromHtml(button1Color);
                        control.ForeColor = ColorTranslator.FromHtml(textColor);
                    }
                    if (control.Tag.ToString().Contains("label"))
                    {
                        control.ForeColor = ColorTranslator.FromHtml(textColor);
                    }


                }

                if (control.HasChildren)
                {
                    ColorControls(control);
                }
            }
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
                // this time chatgpt came to my rescue
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

        void throwError(String message)
        {
            SystemSounds.Exclamation.Play();  
            tb_console.Text = message;
            //MessageBox.Show(message); 
        }


        private void cb_portlist_DropDown(object sender, EventArgs e)
        {
            // Get a list of serial port names.
            var selectedItem = cb_portlist.SelectedItem;
            cb_portlist.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string s in SerialPort.GetPortNames())
            {
                cb_portlist.Items.Add(s);
            }
            cb_portlist.SelectedItem = selectedItem;
        }

        private void cb_portlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            String port = (string)cb_portlist.SelectedItem;
            if (port == null || !port.StartsWith("COM")) 
            {

                return;
            }
            _serialPort.Close();
            btn_stopstart.Text = "Open!";
            btn_stopstart.Enabled = true;
            // Change the serial port's COM
            _serialPort.PortName = port;
        }

        private void btn_saveSettings_Click(object sender, EventArgs e)
        {
            //ccAdapterRemapper.Params.IsParamSet("NESBUTTONS");
            //save all settings to a PARAM folder 
            btn_saveSettings.BackColor = ColorTranslator.FromHtml(button2Color);
            btn_saveSettings.Enabled = false;
            ccAdapterRemapper.Params.SetParam("NESBUTTONS", String.Join(",", working_NES_btns));
            ccAdapterRemapper.Params.SetParam("SNESBUTTONS", String.Join(",", working_SNES_btns));
            ccAdapterRemapper.Params.SetParam("N64BUTTONS", String.Join(",", working_N64_btns));

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
                if (working_NES_btns[i] != onload_NES_btns[i]) // if the button mapping is different then it was on load
                {
                    _serialPort.Write("PO" + "," + i + "," + working_NES_btns[i] + "!"); // send a remap
                }
                
                Thread.Sleep(30);
            }

            Array.Copy(onload_NES_btns, working_NES_btns, onload_NES_btns.Length); //update the onload array


        }

        private void textBoxGather(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            textBox.SelectionStart = 0;
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (keyToKey(key) != 0)
            {
                
                textBox.Text = revKeyToKey(keyToKey(key)); // this is dumb, yet it is probably the easiest way to do this
                tb_console.Text =   "Key: " + keyToKey(key);
                btn_saveSettings.BackColor = ColorTranslator.FromHtml(needsSaving);
                btn_saveSettings.Enabled = true;

                if (textBox.Tag.ToString().Contains("NESA")) working_NES_btns[0] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("NESB")) working_NES_btns[1] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("NESSELECT")) working_NES_btns[2] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("NESSTART")) working_NES_btns[3] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("NESUP")) working_NES_btns[4] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("NESDOWN")) working_NES_btns[5] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("NESLEFT")) working_NES_btns[6] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("NESRIGHT")) working_NES_btns[7] = keyToKey(key);

                    
                //working_NES_btns[0] = keyToKey(key);
            
            }
            else {
                throwError("Unsupported Key.");
                return;
            }
            neatHack(true);

            SelectNextControl(ActiveControl, true, true, true, true);
        }

        void tbFocusLost(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            textBox.BackColor = ColorTranslator.FromHtml(button2Color);
        }

        private void hideCaretAndSelection(object sender, EventArgs e) {
                System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
                textBox.BackColor = ColorTranslator.FromHtml(focusColor);
                textBox.SelectionStart = 0;
                textBox.SelectionStart = tb_NES_A.TextLength;
                HideCaret(textBox.Handle);
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



        ///need to keep track of what keys were changed in remap to minimize r/w of eeprom
    }
}
