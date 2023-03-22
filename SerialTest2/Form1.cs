using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ccAdapterRemapper
{

    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);



        #region variables


        // Theme Color variables
        private readonly string baseHexColor = "#007FFF";
        private Color baseColor;
        private Color baseColorStore;
        private Color buttonDarkColor;
        private Color buttonLightColor;
        private Color backDarkColor;
        private Color backLightColor;
        private Color textColor;
        private readonly string focusColor = "#0078d7"; // Color of selected box
        private readonly string greenColor = "#47FF59";
        private string needsSaving = "#ff6347"; //send remap active color


        // When true, bypass the unsent remap check on app exit
        private bool justRestart = false;



        // string variable to store the key name
        private string key;

        // Arrays for storing the current and previous mappings
        private int[] onload_NES_btns = new int[8];
        private int[] onload_SNES_btns = new int[12];
        private int[] onload_N64_btns = new int[18];
        private int[] working_NES_btns = new int[8];
        private int[] working_SNES_btns = new int[12];
        private int[] working_N64_btns = new int[18];


        // Serial port stuff
        private readonly SerialPort _serialPort = new SerialPort("COM0", 9600);


        // Funny variables
        // Funny AI motto
        private readonly string[] mottoArray = new string[] {
            "Redefine to your heart's desire",
            "Revise to your liking",
            "Reinvent as you please",
            "Rearrange to your satisfaction",
            "Reimagine without limitations",
            "Reconstruct to your own specifications",
            "Re-envision to your heart's content",
            "Reshape without bounds",
            "Reconfigure to your unique vision",
            "Reinterpret to your own taste",
            "Remodel according to your desires",
            "Revolutionize to your own accord",
            "Rebuild to your specifications",
            "Reinvent as you see fit",
            "Rearrange to your liking",
            "Reconfigure to your preferences",
            "Reimagine in your own way",
            "Transform to your personal style",
            "Recreate to your satisfaction",
            "Transmute your thoughts to crystalline form",
            "Revamp in your own manner"
        };



        private int didNo = 0;

        #endregion variables





        public Form1()
        {
            // Intilize the serial port
            _serialPort.Encoding = Encoding.UTF8;
            _serialPort.DtrEnable = true;
            _serialPort.ReadTimeout = 2000;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Only add the debug page if a DEBUG PARAM is set
            tabControl.TabPages.Remove(tabPageDebug);
            if (ccAdapterRemapper.Params.IsParamSet("DEBUG"))
            {
                tabControl.TabPages.Add(tabPageDebug);
            }


            // Check for previously saved controller mappings. Load them if they exist.
            LoadArrays();

            // Randomly select one of the funny mottos
            Random random = new Random();
            lb_motto.Text = mottoArray[random.Next(0, mottoArray.Length)];



            LoadTexboxes();//Poulate the textboxes with the data from the working arrays




            // Set the text of the ComboBox
            _ = cb_portlist.Items.Add("Select a port");
            cb_portlist.Text = "Select a port";

            foreach (Control c in tabControl.Controls)
            {
                foreach (Control x in c.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        x.Enabled = false;
                    }
                }
            }
            // iterate through every control contained in the tabcontrol and hide the caret and selction of the textboxes for aesthetic
            // GotFocus can not be set in the properties of a textbox, have to do this like this
            foreach (Control c in tabControl.Controls)
            {
                foreach (Control x in c.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        x.GotFocus += HideCaretAndSelection; 
                    }
                }
            }


            //Check if the Pastel Theme button was set
            cb_pastel.Checked = ccAdapterRemapper.Params.IsParamSet("PB");

            // CHeck if there is a saved color in the PARAMs
            baseColor = ccAdapterRemapper.Params.IsParamSet("COLOR")
                ? ColorTranslator.FromHtml(ccAdapterRemapper.Params.ReadParam("COLOR"))
                : ColorTranslator.FromHtml(baseHexColor);
            baseColorStore = baseColor; // create a backup of the baseColor

            ColorShifter(baseColor);// Create the colors 
            UpdateColors();// Set the app colors
        }

        private void LoadTexboxes()
        {
            //load all textboxes with text
            tb_NES_A.Text = RevKeyToKey(working_NES_btns[0]);
            tb_NES_B.Text = RevKeyToKey(working_NES_btns[1]);
            tb_NES_SELECT.Text = RevKeyToKey(working_NES_btns[2]);
            tb_NES_START.Text = RevKeyToKey(working_NES_btns[3]);
            tb_NES_UP.Text = RevKeyToKey(working_NES_btns[4]);
            tb_NES_DOWN.Text = RevKeyToKey(working_NES_btns[5]);
            tb_NES_LEFT.Text = RevKeyToKey(working_NES_btns[6]);
            tb_NES_RIGHT.Text = RevKeyToKey(working_NES_btns[7]);

            tb_SNES_B.Text = RevKeyToKey(working_SNES_btns[0]);
            tb_SNES_Y.Text = RevKeyToKey(working_SNES_btns[1]);
            tb_SNES_SELECT.Text = RevKeyToKey(working_SNES_btns[2]);
            tb_SNES_START.Text = RevKeyToKey(working_SNES_btns[3]);
            tb_SNES_UP.Text = RevKeyToKey(working_SNES_btns[4]);
            tb_SNES_DOWN.Text = RevKeyToKey(working_SNES_btns[5]);
            tb_SNES_LEFT.Text = RevKeyToKey(working_SNES_btns[6]);
            tb_SNES_RIGHT.Text = RevKeyToKey(working_SNES_btns[7]);
            tb_SNES_A.Text = RevKeyToKey(working_SNES_btns[8]);
            tb_SNES_X.Text = RevKeyToKey(working_SNES_btns[9]);
            tb_SNES_L.Text = RevKeyToKey(working_SNES_btns[10]);
            tb_SNES_R.Text = RevKeyToKey(working_SNES_btns[11]);

            tb_N64_A.Text = RevKeyToKey(working_N64_btns[0]);
            tb_N64_B.Text = RevKeyToKey(working_N64_btns[1]);
            tb_N64_Z.Text = RevKeyToKey(working_N64_btns[2]);
            tb_N64_START.Text = RevKeyToKey(working_N64_btns[3]);
            tb_N64_DPADUP.Text = RevKeyToKey(working_N64_btns[4]);
            tb_N64_DPADDOWN.Text = RevKeyToKey(working_N64_btns[5]);
            tb_N64_DPADLEFT.Text = RevKeyToKey(working_N64_btns[6]);
            tb_N64_DPADRIGHT.Text = RevKeyToKey(working_N64_btns[7]);
            tb_N64_CPADDOWN.Text = RevKeyToKey(working_N64_btns[8]);
            tb_N64_CPADLEFT.Text = RevKeyToKey(working_N64_btns[9]);
            tb_N64_L.Text = RevKeyToKey(working_N64_btns[10]);
            tb_N64_R.Text = RevKeyToKey(working_N64_btns[11]);
            tb_N64_JOYSTICKUP.Text = RevKeyToKey(working_N64_btns[12]);
            tb_N64_JOYSTICKDOWN.Text = RevKeyToKey(working_N64_btns[13]);
            tb_N64_JOYSTICKLEFT.Text = RevKeyToKey(working_N64_btns[14]);
            tb_N64_JOYSTICKRIGHT.Text = RevKeyToKey(working_N64_btns[15]);
            tb_N64_CPADUP.Text = RevKeyToKey(working_N64_btns[16]);
            tb_N64_CPADRIGHT.Text = RevKeyToKey(working_N64_btns[17]);
        }

        private void LoadArrays()
        {
            // Check for previously saved controller mappings. Load them if they exist.
            //if there is a param for the nes buttons already load them, other
            working_NES_btns = ccAdapterRemapper.Params.IsParamSet("NESBUTTONS") ? 
                Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("NESBUTTONS").Split(','), int.Parse) : 
                Enumerable.Repeat(0, working_NES_btns.Length).ToArray();
            Array.Copy(working_NES_btns, onload_NES_btns, working_NES_btns.Length);

            working_SNES_btns = ccAdapterRemapper.Params.IsParamSet("SNESBUTTONS") ? 
                Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("SNESBUTTONS").Split(','), int.Parse) : 
                Enumerable.Repeat(0, working_SNES_btns.Length).ToArray();
            Array.Copy(working_SNES_btns, onload_SNES_btns, working_SNES_btns.Length);

            working_N64_btns = ccAdapterRemapper.Params.IsParamSet("N64BUTTONS") ? 
                Array.ConvertAll(ccAdapterRemapper.Params.ReadParam("N64BUTTONS").Split(','), int.Parse) : 
                Enumerable.Repeat(0, working_N64_btns.Length).ToArray();
            Array.Copy(working_N64_btns, onload_N64_btns, working_N64_btns.Length);
        }

        private void TabStopHack(bool setEnable) //When pentering a key in a textbox, quickly disable tabstop so that the box can accept the TAB key
        {
            foreach (Control c in tabControl.Controls)
            {
                foreach (Control x in c.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        x.TabStop = setEnable;
                    }
                }
            }
        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) //When Serial data is recieved display it in the Console
        {
            _ = tb_console.Invoke(new Action(() => tb_console.Text = _serialPort.ReadExisting()));
        }

        // Modification of https://stackoverflow.com/a/1626232
        private void ColorShifter(Color color)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            double hue = color.GetHue();
            double saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            double value = 1;


            backDarkColor = ColorFromHSV(hue, saturation * .438, value * .188);
            backLightColor = ColorFromHSV(hue, saturation * .421, value * .243);
            buttonDarkColor = ColorFromHSV(hue, saturation * .435, value * .298);
            buttonLightColor = ColorFromHSV(hue, saturation * .324, value * .435);
        }

        //https://stackoverflow.com/a/1626232
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = (hue / 60) - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - (f * saturation)));
            int t = Convert.ToInt32(value * (1 - ((1 - f) * saturation)));

            return hi == 0
                ? Color.FromArgb(255, v, t, p)
                : hi == 1
                    ? Color.FromArgb(255, q, v, p)
                    : hi == 2
                                    ? Color.FromArgb(255, p, v, t)
                                    : hi == 3 ? Color.FromArgb(255, p, q, v) : hi == 4 ? Color.FromArgb(255, t, p, v) : Color.FromArgb(255, v, p, q);
        }

        private void UpdateColors() // Updates the current color base color or pastel color
        {
            textColor = Color.White;

            if (cb_pastel.Checked) // if the pastel box is checked, set the colors as pastel
            {
                baseColor = ColorTranslator.FromHtml("#F492A5");
                buttonDarkColor = ColorTranslator.FromHtml("#57cbe6");
                buttonLightColor = ColorTranslator.FromHtml("#95DAF8");
                backDarkColor = baseColor;
                backLightColor = ColorTranslator.FromHtml("#F9CBD0");
                textColor = Color.Black;
                needsSaving = "#40FF91";
                ccAdapterRemapper.Params.SetParam("PB");// save to PARAMs
            }
            else // Otherwise generate and load colors based off baseColor
            {
                needsSaving = "#ff6347";
                baseColor = baseColorStore; // reload the baseColor from baseColorStore incase base color was overwrote with pastel
                ColorShifter(baseColor);
                ccAdapterRemapper.Params.RemoveParam("PB");// save to PARAMs
            }
            ColorControls(this); // this being the form itself
        }

        private void ColorControls(Control parent) // Colors controls
        {
            if (parent.Tag.ToString().Contains("dark1"))
            {
                if (parent.Tag.ToString().Contains("dark1"))
                {
                    parent.BackColor = backDarkColor;
                }
            }

            foreach (Control control in parent.Controls) // for each control in the controls of the parent(form)
            {
                if (control is TabControl control1) // if the control is a tab
                {
                    foreach (TabPage tabPage in control1.TabPages) // then for each tabpage in the tabcontrol
                    {
                        if (tabPage.Tag.ToString().Contains("dark1"))
                        {
                            tabPage.BackColor = backDarkColor;
                        }
                        if (tabPage.Tag.ToString().Contains("dark2"))
                        {
                            tabPage.BackColor = backLightColor;
                        }
                        //ColorControls(tabPage);
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
                if (control.HasChildren) // Recursively color all the controls 
                {
                    ColorControls(control);
                }
            }
        }

        private int KeyToKey(string key)
        {
            switch (key)
            {
                // Converts KeysConverter().ConvertToString(e.KeyCode) output to int values that the Arduino Keyboard.h can use
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

        private string RevKeyToKey(int key)
        {
            switch (key)
            {
                // Converts Arduino Keyboard.h key ints to the key.
                // Notably does not convert them back into KeysConverter().ConvertToString(e.KeyCode) names,
                // instead gives them more standard and understandable names ( , instead of OEMCOMMA, ] instead of Oem6, etc. )
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
                default: return "Not set!";
            }

        }

        private void ThrowError(string message, int severity) // Error message thrower
        {
            switch (severity)
            {
                case 1:  //low severity (Unsupported key)
                    // Play sound and show error in the console 
                    SystemSounds.Exclamation.Play();
                    tb_console.Text = message;
                    break;

                case 2: //IO Error
                    // Play sound and show error in a pop up message
                    SystemSounds.Exclamation.Play();
                    _ = MessageBox.Show(new Form { TopMost = true }, $"Error! Program will restart.\nException message:  | {message} |");
                    justRestart = true; // Skip unsent mappings check
                    System.Windows.Forms.Application.Restart(); // EUGGGGH Just restart the whole thing I guess.
                    break;

                default:
                    // If severity is set otherwise just put message in console
                    tb_console.Text = message;
                    break;
            }
        }

        private void OpenCloseCOM(object sender, EventArgs e) //Attempt to open the selected COM port
        {
            string port = (string)cb_portlist.SelectedItem;
            if (port == null || !port.StartsWith("COM")) // Don't let user open or close a port if the selected item is not a port
            {
                ClosePort();
                btn_stopstart.Enabled = false;
                return;
            }
            try // try to open or close the serial port
            {
                if (_serialPort.IsOpen && port.StartsWith("COM"))
                {
                    ClosePort();
                }
                else
                {
                    OpenPort();
                }
            }
            catch (Exception ex) // If an exception
            {
                ThrowError(ex.Message, 2); //Throw the exception as an error instead
            }

        }

        private void ClosePort()
        {
            _serialPort.Close();
            LoadArrays();
            LoadTexboxes();
            btn_sendremap.Enabled = false;
            btn_peek.Enabled = false;
            btn_poke.Enabled = false;
            btn_stopstart.Text = "Open selected.";
            
            
            btn_sendremap.BackColor = buttonDarkColor;
            foreach (Control c in tabControl.Controls)
            {
                foreach (Control x in c.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        x.Enabled = false;
                    }
                }
            }
        }

        private void OpenPort()
        {
            _serialPort.Open();
            btn_stopstart.Text = "Close selected.";
            btn_sendremap.BackColor = buttonLightColor;
            btn_peek.Enabled = true;
            btn_poke.Enabled = true;
            foreach (Control c in tabControl.Controls)
            {
                foreach (Control x in c.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        x.Enabled = true;
                    }
                }
            }
            //btn_sendremap.Enabled = true;
            // btn_sendremap.BackColor = ColorTranslator.FromHtml(needsSaving);
        }


        private void Portlist_DropDown(object sender, EventArgs e) //Handles the COMlist combobox port dropdown
        {
            // Get a list of serial port names.
            _ = SerialPort.GetPortNames();
            //Finally poluate the combobox with the avialable COM ports
            foreach (string s in SerialPort.GetPortNames())
            {
                if (!cb_portlist.Items.Contains(s)) //Only add an item if it is not already on the list
                {
                    _ = cb_portlist.Items.Add(s);
                }
            }
        }

        private void Portlist_SelectedIndexChanged(object sender, EventArgs e) //Handles the COMlist combobox selections
        {
            string port = (string)cb_portlist.SelectedItem;
            if (port == null || !port.StartsWith("COM")) // Close and disable if the selected item is not a port.
            {
                ClosePort();
                btn_stopstart.BackColor = buttonDarkColor;
                btn_stopstart.Enabled = false;
                return;
            }
            _serialPort.Close(); // Ensure port is closed before opening a new one.
            // Change the serial port's COM
            _serialPort.PortName = port;
            btn_stopstart.Enabled = true;
            btn_stopstart.BackColor = ColorTranslator.FromHtml(needsSaving);
        }

        private void Sendremap_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabControl.SelectedTab.Text)
                {
                    case "NES":
                        for (int i = 0; i < 8; i++)
                        {
                            if (working_NES_btns[i] != onload_NES_btns[i])
                            {
                                _serialPort.Write("PO" + "," + i + "," + working_NES_btns[i] + "," + "NES" + "!"); // send a remap only if the current is different then before (minimize eeprom writes)
                            }

                            Thread.Sleep(30); //buffer, might not actually be needed
                        }
                        ccAdapterRemapper.Params.SetParam("NESBUTTONS", string.Join(",", working_NES_btns));// Save the keys select to the PARAMs
                        Array.Copy(working_NES_btns, onload_NES_btns, onload_NES_btns.Length); //update the onload array                      
                        break;

                    case "SNES":
                        for (int i = 0; i < 12; i++)
                        {
                            if (working_SNES_btns[i] != onload_SNES_btns[i])
                            {
                                _serialPort.Write("PO" + "," + i + "," + working_SNES_btns[i] + "," + "SNES" + "!"); // send a remap
                            }

                            Thread.Sleep(30);
                        }
                        ccAdapterRemapper.Params.SetParam("SNESBUTTONS", string.Join(",", working_SNES_btns));
                        Array.Copy(working_SNES_btns, onload_SNES_btns, onload_SNES_btns.Length); //update the onload array
                        break;
                    case "N64":
                        for (int i = 0; i < 18; i++)
                        {
                            if (working_N64_btns[i] != onload_N64_btns[i])
                            {
                                _serialPort.Write("PO" + "," + i + "," + working_N64_btns[i] + "," + "N64" + "!"); // send a remap
                            }

                            Thread.Sleep(30);
                        }
                        ccAdapterRemapper.Params.SetParam("N64BUTTONS", string.Join(",", working_N64_btns));
                        Array.Copy(working_N64_btns, onload_N64_btns, onload_N64_btns.Length); //update the onload array
                        break;
                }
                btn_sendremap.Enabled = false;
                btn_sendremap.BackColor = buttonDarkColor;
            }
            catch (Exception ex) // If an exception
            {
                //If there is an exception it is that the port does not exist so.
                ThrowError(ex.Message, 2); //Throw the exception as an error instead
            }
        }

        private void TextBoxGather(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.SelectionStart = 0;
            key = new KeysConverter().ConvertToString(e.KeyCode);

            if (KeyToKey(key) != 0)
            {

                textBox.Text = RevKeyToKey(KeyToKey(key)); // this is dumb, yet it is probably the easiest way to do this
                tb_console.Text = "Key: " + textBox.Text + " || KeyCode: " + KeyToKey(key) ; // Display the key in the console 

                //NES ONES
                if (textBox.Tag.ToString().Contains("JNESA"))
                {
                    working_NES_btns[0] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("JNESB"))
                {
                    working_NES_btns[1] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("JNESSELECT"))
                {
                    working_NES_btns[2] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("JNESSTART"))
                {
                    working_NES_btns[3] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("JNESUP"))
                {
                    working_NES_btns[4] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("JNESDOWN"))
                {
                    working_NES_btns[5] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("JNESLEFT"))
                {
                    working_NES_btns[6] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("JNESRIGHT"))
                {
                    working_NES_btns[7] = KeyToKey(key);
                }


                //SNES ONES
                if (textBox.Tag.ToString().Contains("SNESB"))
                {
                    working_SNES_btns[0] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESY"))
                {
                    working_SNES_btns[1] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESSELECT"))
                {
                    working_SNES_btns[2] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESSTART"))
                {
                    working_SNES_btns[3] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESDUP"))
                {
                    working_SNES_btns[4] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESDDOWN"))
                {
                    working_SNES_btns[5] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESDLEFT"))
                {
                    working_SNES_btns[6] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESDRIGHT"))
                {
                    working_SNES_btns[7] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESA"))
                {
                    working_SNES_btns[8] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESX"))
                {
                    working_SNES_btns[9] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESL"))
                {
                    working_SNES_btns[10] = KeyToKey(key);
                }

                if (textBox.Tag.ToString().Contains("SNESR"))
                {
                    working_SNES_btns[11] = KeyToKey(key);
                }


                //N64 buttons
                if (textBox.Tag.ToString().Contains("N64A"))
                {
                    working_N64_btns[0] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64B"))
                {
                    working_N64_btns[1] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64Z"))
                {
                    working_N64_btns[2] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64START"))
                {
                    working_N64_btns[3] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64DUP"))
                {
                    working_N64_btns[4] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64DDOWN"))
                {
                    working_N64_btns[5] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64DLEFT"))
                {
                    working_N64_btns[6] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64DRIGHT"))
                {
                    working_N64_btns[7] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64CDOWN"))
                {
                    working_N64_btns[8] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64CLEFT"))
                {
                    working_N64_btns[9] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64L"))
                {
                    working_N64_btns[10] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64R"))
                {
                    working_N64_btns[11] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64CUP"))
                {
                    working_N64_btns[12] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64CRIGHT"))
                {
                    working_N64_btns[13] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64JUP"))
                {
                    working_N64_btns[14] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64JDOWN"))
                {
                    working_N64_btns[15] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64JLEFT"))
                {
                    working_N64_btns[16] = KeyToKey(key);
                }
                if (textBox.Tag.ToString().Contains("N64JRIGHT"))
                {
                    working_N64_btns[17] = KeyToKey(key);
                }

                if (_serialPort.IsOpen)
                {
                    btn_sendremap.Enabled = true;
                    btn_sendremap.BackColor = ColorTranslator.FromHtml(needsSaving);
                }


            }
            else
            {
                ThrowError("Unsupported Key.", 1);
                return;
            }
            TabStopHack(true);

            _ = SelectNextControl(ActiveControl, true, true, true, true);
        }

        private void HideCaretAndSelection(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = ColorTranslator.FromHtml(focusColor);
            _ = HideCaret(textBox.Handle);
        }
        private void FocusLost(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = buttonLightColor;
        }

        private void Peek_Click(object sender, EventArgs e) // Manual peek 
        {
            if (!_serialPort.IsOpen)
            {
                ThrowError("COM Port is not open!", 2);
                return;
            }
            int adr = (int)nup_address.Value;
            int val = 0;
            _serialPort.Write("PE," + adr.ToString() + "," + val.ToString() + "!");
        }

        private void Poke_Click(object sender, EventArgs e) // Manual poke 
        {
            if (!_serialPort.IsOpen)
            {
                ThrowError("COM Port is not open!", 2);
                return;
            }
            int adr = (int)nup_address.Value;
            int val = (int)nup_value.Value;
            _serialPort.Write("PO," + adr.ToString() + "," + val.ToString() + "!");
            tb_console.Text = "PO," + adr.ToString() + "," + val.ToString() + "!";
        }

        private void PreviewKeyDownThis(object sender, PreviewKeyDownEventArgs e)
        {
            TabStopHack(false);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!justRestart)
            {
                if ((!working_NES_btns.SequenceEqual(onload_NES_btns)) || (!working_SNES_btns.SequenceEqual(onload_SNES_btns)) || (!working_N64_btns.SequenceEqual(onload_N64_btns)))
                {
                    DialogResult window = MessageBox.Show(
                        "There are unsent mappings, are you sure you want to close?",
                        "",
                        MessageBoxButtons.YesNo);

                    e.Cancel = window == DialogResult.No;
                }
            }
        }

        private void Pastel_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColors();
        }

        private void ColorPick_Click(object sender, EventArgs e) // User selected theme
        {
            ColorDialog MyDialog = new ColorDialog
            {
                // Sets the initial color select to the current color.
                Color = baseColor
            };

            // Update the color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                baseColor = MyDialog.Color;
                baseColorStore = baseColor;
                ColorShifter(baseColor);
                UpdateColors();
                ccAdapterRemapper.Params.SetParam("COLOR", ColorTranslator.ToHtml(baseColor));

            }
        }

        private void ParamWipebtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This will COMPLETELY wipe the contents of the PARAMs folder and clear ALL settings, are you sure?", "", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    ccAdapterRemapper.Params.RemoveAllParam();
                    _ = MessageBox.Show("It was as if it was never there.", "");
                    justRestart = true;
                    ccAdapterRemapper.Params.SetParam("DEBUG");
                    System.Windows.Forms.Application.Restart();
                    break;
                case DialogResult.No:
                    _ = didNo >= 3 ? MessageBox.Show("How odd.", "") : MessageBox.Show("Then it was left as is.", "");
                    didNo++;
                    break;
            }

        }
    }
}
