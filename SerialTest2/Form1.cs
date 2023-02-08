using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace SerialTest2
{

    public partial class Form1 : Form
    {

        int NES_A = 65;
        int NES_B = 66;
        int NES_SELECT = 32;
        int NES_START = 13;
        int NES_UP = 38;
        int NES_DOWN = 40;
        int NES_LEFT = 37;
        int NES_RIGHT = 39;
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
                                tabPage.BackColor = Color.FromArgb(244, 146, 165);
                            }
                            if (tabPage.Tag.ToString().Contains("background2"))
                            {
                                tabPage.BackColor = Color.FromArgb(249, 203, 208);
                            }
                        }

                        ColorControls(tabPage);
                    }
                }
                else if (control.Tag != null)
                {
                    if (control.Tag.ToString().Contains("background1"))
                    {
                        control.BackColor = Color.FromArgb(244, 146, 165);
                    }
                    if (control.Tag.ToString().Contains("background2"))
                    {
                        control.BackColor = Color.FromArgb(249, 203, 208);
                    }
                    if (control.Tag.ToString().Contains("button2"))
                    {
                        control.BackColor = Color.FromArgb(110, 130, 183);
                    }
                    if (control.Tag.ToString().Contains("button1"))
                    {
                        control.BackColor = Color.FromArgb(149, 218, 248);
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

        private void button8_Click(object sender, EventArgs e)
        {
            _serialPort.Write("3, a!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            handShakeNow();
            remapNow("SNES");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _serialPort.Write("STOP!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ColorControls(this);
        }
        private void btn_NES_A_Pressed(object sender, KeyEventArgs e)
        {
            btn_NES_A.Clear();
            btn_NES_A.Text = ((char)e.KeyValue).ToString();
            NES_A = e.KeyValue;
        }

        private void btn_saveSettings_Click(object sender, EventArgs e)
        {
            //save all settings to a PARAM folder 
            //NESBUTTONS
            //SNESBUTTONS
            //N64BUTTONS
            //COLORS
            //AKA "STEAL" CODE FROM RTC
        }
    }
}
