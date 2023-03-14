using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Net.Mime.MediaTypeNames;

namespace ccAdapterRemapper
{

    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);



        #region variables


        // Theme Color variables
        int baseHexColor = 0x1B2530;
        Color baseColor;
        Color buttonDarkColor;
        Color buttonLightColor;
        Color backDarkColor;
        Color backLightColor;
        Color textColor;
        string focusColor = "#0078d7";
        string needsSaving = "#ff6347";

        


        // string variable to store the key name
        private string key;

        // Arrays for storing the current and previous mappings
        int[] onload_NES_btns = new int[8];
        int[] onload_SNES_btns = new int[12];
        int[] onload_N64_btns = new int[18];
        int[] working_NES_btns = new int[8];
        int[] working_SNES_btns = new int[12];
        int[] working_N64_btns = new int[18];


        // Serial port stuff
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


            //Poulate the textboxes with the data from the working arrays
            tb_NES_A.Text = revKeyToKey(working_NES_btns[0]);
            tb_NES_B.Text = revKeyToKey(working_NES_btns[1]);
            tb_NES_SELECT.Text = revKeyToKey(working_NES_btns[2]);
            tb_NES_START.Text = revKeyToKey(working_NES_btns[3]);
            tb_NES_UP.Text = revKeyToKey(working_NES_btns[4]);
            tb_NES_DOWN.Text = revKeyToKey(working_NES_btns[5]);
            tb_NES_LEFT.Text = revKeyToKey(working_NES_btns[6]);
            tb_NES_RIGHT.Text = revKeyToKey(working_NES_btns[7]);

            tb_SNES_B.Text = revKeyToKey(working_SNES_btns[0]);
            tb_SNES_Y.Text = revKeyToKey(working_SNES_btns[1]);
            tb_SNES_SELECT.Text = revKeyToKey(working_SNES_btns[2]);
            tb_SNES_START.Text = revKeyToKey(working_SNES_btns[3]);
            tb_SNES_UP.Text = revKeyToKey(working_SNES_btns[4]);
            tb_SNES_DOWN.Text = revKeyToKey(working_SNES_btns[5]);
            tb_SNES_LEFT.Text = revKeyToKey(working_SNES_btns[6]);
            tb_SNES_RIGHT.Text = revKeyToKey(working_SNES_btns[7]);
            tb_SNES_A.Text = revKeyToKey(working_SNES_btns[8]);
            tb_SNES_X.Text = revKeyToKey(working_SNES_btns[9]);
            tb_SNES_L.Text = revKeyToKey(working_SNES_btns[10]);
            tb_SNES_R.Text = revKeyToKey(working_SNES_btns[11]);

            cb_portlist.Items.Add("Select a port");
            cb_portlist.Text = "Select a port";


            //Hiding the caret and selction of the textboxes for aesthetic
            //NES
            tb_NES_A.GotFocus +=   hideCaretAndSelection;
            tb_NES_B.GotFocus +=   hideCaretAndSelection;
            tb_NES_SELECT.GotFocus +=   hideCaretAndSelection;
            tb_NES_START.GotFocus +=   hideCaretAndSelection;
            tb_NES_UP.GotFocus +=   hideCaretAndSelection;
            tb_NES_DOWN.GotFocus +=   hideCaretAndSelection;
            tb_NES_LEFT.GotFocus +=   hideCaretAndSelection;
            tb_NES_RIGHT.GotFocus +=   hideCaretAndSelection;

            //SNES
            tb_SNES_B.GotFocus +=   hideCaretAndSelection;
            tb_SNES_Y.GotFocus +=   hideCaretAndSelection;
            tb_SNES_SELECT.GotFocus +=   hideCaretAndSelection;
            tb_SNES_START.GotFocus +=   hideCaretAndSelection;
            tb_SNES_UP.GotFocus +=   hideCaretAndSelection;
            tb_SNES_DOWN.GotFocus +=   hideCaretAndSelection;
            tb_SNES_LEFT.GotFocus +=   hideCaretAndSelection;
            tb_SNES_RIGHT.GotFocus +=   hideCaretAndSelection;
            tb_SNES_A.GotFocus +=   hideCaretAndSelection;
            tb_SNES_X.GotFocus +=   hideCaretAndSelection;
            tb_SNES_L.GotFocus +=   hideCaretAndSelection;
            tb_SNES_R.GotFocus +=   hideCaretAndSelection;

            //Check if the Pastel Theme button was set
            pastelCB.Checked = ccAdapterRemapper.Params.IsParamSet("PB");


            updateColors();// Set the app colors
        }

        void neatHack(bool yup) //Part of a solution to disable the cursor and text selection in the textboxes
        {
            tb_NES_A.TabStop = yup;
            tb_NES_B.TabStop = yup;
            tb_NES_SELECT.TabStop = yup;
            tb_NES_START.TabStop = yup;
            tb_NES_UP.TabStop = yup;
            tb_NES_DOWN.TabStop = yup;
            tb_NES_LEFT.TabStop = yup;
            tb_NES_RIGHT.TabStop = yup;

            tb_SNES_B.TabStop = yup;
            tb_SNES_Y.TabStop = yup;
            tb_SNES_SELECT.TabStop = yup;
            tb_SNES_START.TabStop = yup;
            tb_SNES_UP.TabStop = yup;
            tb_SNES_DOWN.TabStop = yup;
            tb_SNES_LEFT.TabStop = yup;
            tb_SNES_RIGHT.TabStop = yup; 
            tb_SNES_A.TabStop = yup;
            tb_SNES_X.TabStop = yup;
            tb_SNES_L.TabStop = yup;
            tb_SNES_R.TabStop = yup;

        }        


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) //When Serial data is recieved display it in the Console
        {
            tb_console.Invoke(new Action(() => tb_console.Text = _serialPort.ReadExisting()));
        }

        void updateColors()
        {
            baseColor = ColorTranslator.FromHtml("#" + baseHexColor.ToString("X"));
            buttonDarkColor = ColorTranslator.FromHtml("#" + (baseHexColor + 0x80B0E).ToString("X"));
            buttonLightColor = ColorTranslator.FromHtml("#" + (baseHexColor + 0x30383F).ToString("X"));
            backDarkColor = baseColor;
            backLightColor = ColorTranslator.FromHtml("#" + (baseHexColor + 0x11171C).ToString("X"));
            textColor = Color.White;

            if (pastelCB.Checked)
            {
                baseColor = ColorTranslator.FromHtml("#F492A5");
                buttonDarkColor = ColorTranslator.FromHtml("#57cbe6");
                buttonLightColor = ColorTranslator.FromHtml("#95DAF8");
                backDarkColor = baseColor;
                backLightColor = ColorTranslator.FromHtml("#F9CBD0");
                textColor = Color.Black;
                ccAdapterRemapper.Params.SetParam("PB");// save to PARAMs
            }
            else ccAdapterRemapper.Params.RemoveParam("PB");// save to PARAMs

            ColorControls(this);
        }

        private void ColorControls(Control parent)
        {
            if (parent is Form && parent.Tag != null && parent.Tag.ToString().Contains("dark1"))
            {
                if (parent.Tag.ToString().Contains("dark1"))
                {
                    parent.BackColor = backDarkColor;
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
                            if (tabPage.Tag.ToString().Contains("dark1"))
                            {
                                tabPage.BackColor = backDarkColor;
                            }
                            if (tabPage.Tag.ToString().Contains("dark2"))
                            {
                                tabPage.BackColor = backLightColor;
                            }
                        }

                        ColorControls(tabPage);
                    }
                }
                else if (control.Tag != null)
                {
                    if (control.Tag.ToString().Contains("dark1"))
                    {
                        control.BackColor = backDarkColor;
                    }
                    if (control.Tag.ToString().Contains("dark2"))
                    {
                        control.BackColor = backLightColor;
                    }
                    if (control.Tag.ToString().Contains("light1"))
                    {
                        control.BackColor = buttonDarkColor;
                        control.ForeColor = textColor;
                    }
                    if (control.Tag.ToString().Contains("light2"))
                    {
                        control.BackColor = buttonLightColor;
                        control.ForeColor = textColor;
                    }
                    if (control.Tag.ToString().Contains("label"))
                    {
                        control.ForeColor = textColor;
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

        void throwError(String message, int severity)
        {
            switch (severity)
            {
                case 1 : 

                    SystemSounds.Exclamation.Play();
                    tb_console.Text = message;
                    break;

                case 2 : //IO Error

                    SystemSounds.Exclamation.Play();
                    MessageBox.Show(new Form { TopMost = true },$"I/O Error! Port will be closed.\nException message:  | {message} |");
                    break;

                default :
                    tb_console.Text = message;
                    break;
            }
        }


        private void openCOM(object sender, EventArgs e) //Attempt to open the selected COM port
        {
            try // try to open or close the serial port
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                    btn_stopstart.Text = "Open";
                    btn_sendremap.Enabled = false;
                    btn_sendremap.BackColor = buttonDarkColor;
                }
                else
                {
                    _serialPort.Open();
                    btn_stopstart.Text = "Close";
                    btn_sendremap.Enabled = true;
                    btn_sendremap.BackColor = ColorTranslator.FromHtml(needsSaving);
                }
            }
            catch (Exception ex) // If an exception
            {   
                cleanPort();
                throwError(ex.Message, 2); //Throw the exception as an error instead
            }
            finally
            { 
                _serialPort.Dispose(); // finally dipose of the serialport 
            }

        }

        void cleanPort()
        {
            btn_stopstart.Enabled = false;
            cb_portlist.Items.Clear(); 
            cb_portlist.Items.Add("Select a port"); 
            cb_portlist.Text = "Select a port";
            btn_stopstart.Text = "Open";
            btn_sendremap.Enabled = false;
            btn_sendremap.BackColor = buttonDarkColor;
        }

        private void cb_portlist_DropDown(object sender, EventArgs e) //Handles the COMlist combobox port dropdown
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();
            
            //Check if the defult text is currently the selected one
            if ((string)cb_portlist.SelectedItem == "Select a port") 
            {
                cb_portlist.Items.Clear();
            }
            //Check if there are even any COM ports available
            if (ports == null || ports.Length == 0)
            {
                cb_portlist.Items.Clear();
                cb_portlist.Items.Add("Select a port");
                cb_portlist.Text = "Select a port";
                return;
            }

            //Finally poluate the combobox with the avialable COM ports
            foreach (string s in SerialPort.GetPortNames())
            {
                if (!cb_portlist.Items.Contains(s)) //Only add an item if it is not already on the list
                {
                    cb_portlist.Items.Add(s);
                }                
            }
        }

        private void cb_portlist_DropDownClosed(object sender, EventArgs e)
        {
            String port = (string)cb_portlist.SelectedItem;
            if (port == null || !port.StartsWith("COM"))  //If the selected item in the combobox is not correct
            {
                cb_portlist.Items.Add("Select a port");
                cb_portlist.Text = "Select a port";
            }
        }


        private void cb_portlist_SelectedIndexChanged(object sender, EventArgs e) //Handles the COMlist combobox selections
        {
            String port = (string)cb_portlist.SelectedItem;
            if (port == null || !port.StartsWith("COM")) 
            {
                cb_portlist.Text = "Select a port";
                return;
            }
            _serialPort.Close();
            btn_stopstart.Text = "Open!";
            btn_stopstart.Enabled = true;
            // Change the serial port's COM
            _serialPort.PortName = port;
        }

        private void btn_sendremap_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabControl1.SelectedTab.Text)
                {
                    case "NES" : 
                        for (int i = 0; i < 8; i++) 
                        {
                            if (working_NES_btns[i] != onload_NES_btns[i]) _serialPort.Write("PO" + "," + i + "," + working_NES_btns[i] + "NES" + "!"); // send a remap only if the current is different then before (minimize eeprom writes)
                            Thread.Sleep(30); //buffer, might not actually be needed
                        }
                        ccAdapterRemapper.Params.SetParam("NESBUTTONS", String.Join(",", working_NES_btns));// Save the keys select to the PARAMs
                        Array.Copy(onload_NES_btns, working_NES_btns, onload_NES_btns.Length); //update the onload array
                        //btn_sendremap.BackColor = ColorTranslator.FromHtml(light2);
                        //btn_sendremap.Enabled = false;
                        break;

                    case "SNES" : 
                        for (int i = 0; i < 12; i++) 
                        {
                            _serialPort.Write("PO" + "," + i + "," + working_SNES_btns[i] + "SNES" + "!"); // send a remap
                    
                            Thread.Sleep(30);
                        }
                        ccAdapterRemapper.Params.SetParam("SNESBUTTONS", String.Join(",", working_SNES_btns));
                        Array.Copy(onload_SNES_btns, working_SNES_btns, onload_SNES_btns.Length); //update the onload array
                        break;
                    case "N64" : 
                        //ccAdapterRemapper.Params.SetParam("N64BUTTONS", String.Join(",", working_N64_btns));
                        break;
                }
            }
            catch (Exception ex) // If an exception
            {   
                //If there is an exception it is that the port does not exist so.
                cleanPort();
                throwError(ex.Message, 2); //Throw the exception as an error instead
            }
            finally
            { 
                _serialPort.Dispose(); // finally dipose of the serialport 
            }



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


                //NES ONES
                if (textBox.Tag.ToString().Contains("JNESA")) working_NES_btns[0] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("JNESB")) working_NES_btns[1] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("JNESSELECT")) working_NES_btns[2] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("JNESSTART")) working_NES_btns[3] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("JNESUP")) working_NES_btns[4] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("JNESDOWN")) working_NES_btns[5] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("JNESLEFT")) working_NES_btns[6] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("JNESRIGHT")) working_NES_btns[7] = keyToKey(key);


                //SNES ONES
                if (textBox.Tag.ToString().Contains("SNESB")) working_SNES_btns[0] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESY")) working_SNES_btns[1] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESSELECT")) working_SNES_btns[2] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESSTART")) working_SNES_btns[3] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESUP")) working_SNES_btns[4] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESDOWN")) working_SNES_btns[5] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESLEFT")) working_SNES_btns[6] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESRIGHT")) working_SNES_btns[7] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESA")) working_SNES_btns[8] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESX")) working_SNES_btns[9] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESL")) working_SNES_btns[10] = keyToKey(key);
                if (textBox.Tag.ToString().Contains("SNESR")) working_SNES_btns[11] = keyToKey(key);
                //working_NES_btns[0] = keyToKey(key);
            
            }
            else {
                throwError("Unsupported Key.", 1);
                return;
            }
            neatHack(true);

            SelectNextControl(ActiveControl, true, true, true, true);
        }

        void tbFocusLost(object sender, EventArgs e)    
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
            textBox.BackColor = buttonLightColor;
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
                    throwError("COM Port is not open!", 2);
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
                    throwError("COM Port is not open!", 2);
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((!working_NES_btns.SequenceEqual(onload_NES_btns)) || (!working_SNES_btns.SequenceEqual(onload_SNES_btns)) || (!working_N64_btns.SequenceEqual(onload_N64_btns)))
            {
                var window = MessageBox.Show(
                    "There are unsent mappings, are you sure you want to close?", 
                    "", 
                    MessageBoxButtons.YesNo);

                e.Cancel = (window == DialogResult.No);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _serialPort.Close(); // ensure serial port is closed on exit
        }

        private void pastelCB_CheckedChanged(object sender, EventArgs e)
        {
            updateColors();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }   
}
