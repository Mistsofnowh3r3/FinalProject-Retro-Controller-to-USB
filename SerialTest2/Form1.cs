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
        // Calls a function in the user32.dll to allow textbox caret to be hidden
        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);

        #region variables

        // Theme Color variables
        readonly string baseHexColor = "#007FFF"; // The base color at the start.
        Color baseColor;
        Color baseColorStore;
        Color buttonDarkColor;
        Color buttonLightColor;
        Color backDarkColor;
        Color backLightColor;
        Color textColor;
        readonly string focusColor = "#0078d7"; // Color of selected box
        string needsSaving = "#ff6347"; //send remap active color

        // string variable to store a keyboard key name
        string key;

        // Arrays for storing the current and previous mappings
        int[] onload_NES_btns = new int[8];
        int[] onload_SNES_btns = new int[12];
        int[] onload_N64_btns = new int[18];
        int[] working_NES_btns = new int[8];
        int[] working_SNES_btns = new int[12];
        int[] working_N64_btns = new int[18];

        // Serial port stuff
        SerialPort _serialPort = new SerialPort("COM0", 9600);

        // Funny AI motto
        readonly string[] mottoArray = new string[] 
        {
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
            LoadArrays(); // Check for previously saved controller mappings. Load them if they exist.

            // Randomly select one of the funny mottos
            Random random = new Random();
            lb_motto.Text = mottoArray[random.Next(0, mottoArray.Length)];

            LoadTexboxes(); // Poulate the textboxes with the data from the working arrays

            // Set the text of the ComboBox
            _ = cb_portlist.Items.Add("Select a port");
            cb_portlist.Text = "Select a port";

            // Iterate through every textbox contained in the tabcontrol and disable it 
            // Also, assign HideCaretAndSelection to it's gotfocus event to hide the caret and selction of the textboxes for aesthetic
            foreach (Control c in tabControl.Controls)
            {
                foreach (Control x in c.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        x.Enabled = false;
                        x.GotFocus += HideCaretAndSelection;
                    }
                }
            }
            tb_console.GotFocus += HideCaretAndSelectionConsole; // Also give the console one


            // Check if the Pastel Theme button was set
            cb_pastel.Checked = ccAdapterRemapper.Params.IsParamSet("PB");

            if(!ccAdapterRemapper.Params.IsParamSet("DEBUG")) // Hide the debug page if the PARAM is not set.
            {
                tabControl.TabPages.Remove(tabPageDebug);
            }

            // Check if there is a saved color in the PARAMs
            baseColor = ccAdapterRemapper.Params.IsParamSet("COLOR")
                ? ColorTranslator.FromHtml(ccAdapterRemapper.Params.ReadParam("COLOR"))
                : ColorTranslator.FromHtml(baseHexColor);
            baseColorStore = baseColor; // Create a backup of the baseColor

            ColorShifter(baseColor);// Create the colors 
            UpdateColors();// Set the colors
        }

        // Load all textboxes with text
        // This code looks super ugly but, I really don't know how to do this better
        private void LoadTexboxes()
        {
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

        // Check for previously saved controller mappings. Load them if they exist, otherwise set the array to zero
        private void LoadArrays()
        {
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

        // Part of a hacky solution. Controls the tabstop of all textboxes in the tabcontrol
        // Used to allow the TAB key to be input in the textboxes, but to still allow the textboxes to be able to be cycled through
        private void TabStopHack(bool setEnable)
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

        // When Serial data is recieved display it in the Console
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            _ = tb_console.Invoke(new Action(() => tb_console.Text = _serialPort.ReadExisting()));
        }

        // Modification of https://stackoverflow.com/a/1626232
        private void ColorShifter(Color color)
        {
            double hue = color.GetHue(); // Get the base hue of the color
            double saturation = color.GetSaturation(); // Get the saturation of the color

            // Set each color from the base color
            backDarkColor = ColorFromHSV(hue, saturation * .438, 1 * .188);
            backLightColor = ColorFromHSV(hue, saturation * .421, 1 * .243);
            buttonDarkColor = ColorFromHSV(hue, saturation * .435, 1 * .298);
            buttonLightColor = ColorFromHSV(hue, saturation * .324, 1 * .435);
        }

        // https://stackoverflow.com/a/1626232
        // Takes a H (Hue), S (Saturation), and V (Value) and creates a color
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

        // Updates the current base color or makes the colors pastel.
        private void UpdateColors()
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

        // Colors all controls using a tag based system
        private void ColorControls(Control parent)
        {
            if (parent.Tag.ToString().Contains("dark1"))
                parent.BackColor = backDarkColor; // Colors the form's background

            foreach (Control control in parent.Controls) // Go through every control in the form
            {
                if (control.Tag != null)
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
                if (control.HasChildren) // This method on the children of every control encountered 
                {
                    ColorControls(control);
                }
            }

            // Manually keep these color active if they were
            if (btn_stopstart.Enabled == true) btn_stopstart.BackColor = ColorTranslator.FromHtml(needsSaving);
            if (btn_write.Enabled == true) btn_write.BackColor = ColorTranslator.FromHtml(needsSaving);

        }

        // Converts KeysConverter().ConvertToString(e.KeyCode) output to int values that the Arduino Keyboard.h library can use
        private int KeyToKey(string key)
        {
            switch (key)
            {
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
                case "Menu": return 130;
                case "Space": return 32;
                case "Up": return 218;
                case "Left": return 216;
                case "Down": return 217;
                case "Right": return 215;
                default: return 0; // All other keys give zero (Unsupported)
            }

        }
       
        // Converts Arduino Keyboard.h library key ints to C# key values
        // Notably does not convert them back into KeysConverter().ConvertToString(e.KeyCode) names,
        // instead gives them more standard and understandable names ( , instead of OEMCOMMA, ] instead of Oem6, etc. )
        private string RevKeyToKey(int key)
        {
            switch (key)
            {
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
                case 130: return "Alt";
                case 32: return "Space";
                case 218: return "Up";
                case 216: return "Left";
                case 217: return "Down";
                case 215: return "Right";
                default: return "Not set!"; // All other keys are unsupported
            }

        }

        // Error message thrower
        private void ThrowError(string message, int severity)
        {
            switch (severity)
            {
                case 1:  // Low severity (Unsupported key)
                    // Play sound and show error in the console 
                    SystemSounds.Exclamation.Play();
                    tb_console.Text = message;
                    break;

                case 2: //IO Error
                    // Play sound and show error in a pop up message, also panic restart.
                    SystemSounds.Exclamation.Play();
                    MessageBox.Show(new Form { TopMost = true }, $"Grievous error! Program will restart.\nException message: | {message} |");
                    System.Windows.Forms.Application.Restart(); // Panic restart
                    break;

                default:
                    // If severity is set otherwise just put message in console
                    tb_console.Text = message;
                    break;
            }
        }

        // Attempt to open or close the selected COM port
        private void OpenCloseCOM(object sender, EventArgs e)
        {
            string port = (string)cb_portlist.SelectedItem;
            if (port == null || !port.StartsWith("COM")) // Don't do actions if the selected item is not a COM port
            {
                ClosePort(); // Close the current open port
                btn_stopstart.Enabled = false; // disable the stop start button
                return;
            }
            try // Try to open or close the serial port
            {
                if (_serialPort.IsOpen) // If open, close the port
                {
                    ClosePort();
                }
                else // Else open it
                {
                    OpenPort();
                }
            }
            catch (Exception ex) // If an exception
            {
                ThrowError(ex.Message, 2); // Throw the exception as an error instead
            }

        }

        // Close the serial port and other things
        private void ClosePort()
        {
            _serialPort.Close();
            LoadArrays(); // Reload the arrays from PARAMs
            LoadTexboxes(); // Load the reloaded arrays into the textboxes

            // Reset most controls
            btn_write.Enabled = false;
            btn_peek.Enabled = false;
            btn_poke.Enabled = false;
            btn_stopstart.Text = "Open selected.";
            btn_write.BackColor = buttonDarkColor;

            // Disable every textbox
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

        // Open the serial port and other things
        private void OpenPort()
        {
            _serialPort.Open();

            // Enable most controls
            btn_stopstart.Text = "Close selected.";
            btn_write.BackColor = buttonDarkColor;
            btn_peek.Enabled = true;
            btn_poke.Enabled = true;

            // Enable every textbox
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
        }

        // Handles the combobox portlist dropdown
        private void Portlist_DropDown(object sender, EventArgs e)
        {
            _ = SerialPort.GetPortNames(); // Get a list of serial port names

            // Poluate the combobox with the avialable COM ports
            foreach (string s in SerialPort.GetPortNames())
            {
                if (!cb_portlist.Items.Contains(s)) // Only add an item if it is not already on the list
                {
                    _ = cb_portlist.Items.Add(s);
                }
            }
        }

        // Handles the combobox portlist item selection
        private void Portlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            string port = (string)cb_portlist.SelectedItem;

            // Close and disable if the selected item is not a port.
            if (port == null || !port.StartsWith("COM"))
            {
                ClosePort();
                btn_stopstart.BackColor = buttonDarkColor;
                btn_stopstart.Enabled = false;
                return;
            }

            _serialPort.Close(); // Ensure port is closed before opening a new one.

            _serialPort.PortName = port; // Change the serial port's COM
            btn_stopstart.Enabled = true;
            btn_stopstart.BackColor = ColorTranslator.FromHtml(needsSaving);
        }

        // Handles the write button being clicked
        private void Write_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabControl.SelectedTab.Text) // Action depending on which tab is open
                {
                    case "NES":
                        for (int i = 0; i < 8; i++)
                        {
                            // Send a remap only if the current key is different then before (minimize eeprom writes)
                            if (working_NES_btns[i] != onload_NES_btns[i])
                            {
                                // Sends a write command
                                _serialPort.Write("PO" + "," + i + "," + working_NES_btns[i] + "," + "NES" + "!");
                            }

                            Thread.Sleep(30); // Buffer, might not actually be needed
                        }
                        ccAdapterRemapper.Params.SetParam("NESBUTTONS", string.Join(",", working_NES_btns)); // Save the keys select to the PARAMs
                        Array.Copy(working_NES_btns, onload_NES_btns, onload_NES_btns.Length); // Update the onload array                      
                        break;

                    case "SNES":
                        for (int i = 0; i < 12; i++)
                        {
                            // Send a remap only if the current key is different then before (minimize eeprom writes)
                            if (working_SNES_btns[i] != onload_SNES_btns[i])
                            {
                                // Sends a write command
                                _serialPort.Write("PO" + "," + i + "," + working_SNES_btns[i] + "," + "SNES" + "!");
                            }

                            Thread.Sleep(30); // Buffer, might not actually be needed
                        }
                        ccAdapterRemapper.Params.SetParam("SNESBUTTONS", string.Join(",", working_SNES_btns)); // Save the keys select to the PARAMs
                        Array.Copy(working_SNES_btns, onload_SNES_btns, onload_SNES_btns.Length); // Update the onload array 
                        break;

                    case "N64":
                        for (int i = 0; i < 18; i++)
                        {
                            // Send a remap only if the current key is different then before (minimize eeprom writes)
                            if (working_N64_btns[i] != onload_N64_btns[i])
                            {
                                // Sends a write command
                                _serialPort.Write("PO" + "," + i + "," + working_N64_btns[i] + "," + "N64" + "!");
                            }

                            Thread.Sleep(30); // Buffer, might not actually be needed
                        }
                        ccAdapterRemapper.Params.SetParam("N64BUTTONS", string.Join(",", working_N64_btns)); // Save the keys select to the PARAMs
                        Array.Copy(working_N64_btns, onload_N64_btns, onload_N64_btns.Length); // Update the onload array 
                        break;
                }

                btn_write.Enabled = false;
                btn_write.BackColor = buttonDarkColor;
            }
            catch (Exception ex) // If an exception
            {
                ThrowError(ex.Message, 2); //Throw the exception as an error instead
            }
        }

        // Handles keypresses in textboxes
        private void TextBoxGather(object sender, KeyEventArgs e)
        {
            TextBox textBox = sender as TextBox; // The sender is a textbox

            key = new KeysConverter().ConvertToString(e.KeyCode); // Get the keycode of the key entered

            // If the key is not zero, send it to the workign array otherwise the key is unsupported.
            if (KeyToKey(key) != 0)
            {
                // This is dumb, yet it is probably the easiest way to do this.
                // Sets the texboxes text to the name of the key
                textBox.Text = RevKeyToKey(KeyToKey(key));

                tb_console.Text = "Key: " + textBox.Text + " || KeyCode: " + KeyToKey(key); // Display the key in the console 

                #region nesKeys
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
                #endregion nesKeys

                #region snesKeys
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
                #endregion snesKeys

                #region n64Keys
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
                #endregion n64Keys

                btn_write.Enabled = true;
                btn_write.BackColor = ColorTranslator.FromHtml(needsSaving);


            }
            else
            {
                ThrowError("Unsupported Key.", 1);
                return;
            }

            TabStopHack(true); // Make it so the textboxes can be tabbed to

            _ = SelectNextControl(ActiveControl, true, true, true, true); // Auto move to the next textbox
        }

        // Happens when a textbox get's focus
        private void HideCaretAndSelection(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox; // The sender is a textbox
            textBox.BackColor = ColorTranslator.FromHtml(focusColor); // Don't color the console on click
            _ = HideCaret(textBox.Handle); // Hide the caret
        }

        // Happens when a textbox get's focus
        private void HideCaretAndSelectionConsole(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox; // The sender is a textbox
            _ = HideCaret(textBox.Handle); // Hide the caret
        }

        // Called when a textbox loses focus
        private void FocusLost(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox; // The sender is a textbox
            textBox.BackColor = buttonLightColor;
        }

        // Manual peek button
        private void Peek_Click(object sender, EventArgs e)
        {
            // Check that the port is open
            if (!_serialPort.IsOpen)
            {
                ThrowError("COM Port is not open!", 2); // throw an error if not
            }
            int adr = (int)nup_address.Value; // From the address numeric up down
            int val = 0; // Value does not matter, not writing
            _serialPort.Write("PE," + adr.ToString() + "," + val.ToString() + "!"); // Send the read command
        }

        // Manual poke button
        private void Poke_Click(object sender, EventArgs e)
        {
            // Check that the port is open
            if (!_serialPort.IsOpen)
            {
                ThrowError("COM Port is not open!", 2); // throw an error if not
            }
            int adr = (int)nup_address.Value; // From the address numeric up down
            int val = (int)nup_value.Value; // From the vlaue numeric up down
            _serialPort.Write("PO," + adr.ToString() + "," + val.ToString() + "!"); // Send the write command
        }

        // Happens when a key is pressed in a textbox
        private void PreviewKeyDownThis(object sender, PreviewKeyDownEventArgs e)
        {
            TabStopHack(false); // Disable the tabstop of every textbox to allow TAB key input
        }

        // Pastel button clicked
        private void Pastel_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColors();
        }

        // Color picker button clicked
        private void ColorPick_Click(object sender, EventArgs e)
        {
            // Open a color dialog
            ColorDialog MyDialog = new ColorDialog
            {
                // Sets the initial color select to the current color.
                Color = baseColor
            };

            // Update the theme color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                baseColor = MyDialog.Color;
                baseColorStore = baseColor;
                ColorShifter(baseColor);
                UpdateColors();
                ccAdapterRemapper.Params.SetParam("COLOR", ColorTranslator.ToHtml(baseColor));

            }
        }

        // Debug button to wipe all PARAMs
        private void ParamWipebtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("This will COMPLETELY wipe the contents of the PARAMs folder and clear ALL settings (DOES NOT WIPE THE ARDUINO MEMORY, save the EEPROM writes), are you sure?", "", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.Yes:
                    ccAdapterRemapper.Params.RemoveAllParam();
                    _ = MessageBox.Show("It was as if it was never there.", "");
                    ccAdapterRemapper.Params.SetParam("DEBUG"); // Keep the debug PARAM
                    System.Windows.Forms.Application.Restart();
                    break;
                case DialogResult.No:
                    break;
            }
        }

        // Switchings tabs reloads the arrays and textboxes and disables the write button
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadArrays(); // Reload the arrays from PARAMs
            LoadTexboxes(); // Load the reloaded arrays into the textboxes
            btn_write.Enabled = false;
            btn_write.BackColor = buttonDarkColor;
        }
    }
}
