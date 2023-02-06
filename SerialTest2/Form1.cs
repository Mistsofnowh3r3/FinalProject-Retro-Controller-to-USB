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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_sendremap_Click(object sender, EventArgs e)
        {
            //check the button boxes
            //add all the buttons to change in an array




            for (int i = 0; i < 20; i++) // wait for a acknowledge and if not time out
            {
            }
            //send to serial


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _serialPort.Write("SREQ!"); //request serial operation
            while (true)
            {
                tb_console.Text = serialNow;
                if (serialNow == "SACK") ;
                {
                    tb_console.Text = "SREQ acknowledged.";
                    _serialPort.Write("REMAP!"); //request remap
                    while (true)
                    {
                        if (serialNow == "REMAPACK") ;
                        {
                            _serialPort.Write("NES!"); // state NES
                            while (true)
                            {
                                if (serialNow == "NESACK") ;
                                {
                                    tb_console.Text = "The handshake was a sucess!";
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _serialPort.Write("REMAP!"); //request remap
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

        private void button6_Click(object sender, EventArgs e)
        {
            _serialPort.Write("NES!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _serialPort.Write("SNES!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _serialPort.Write("N64!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _serialPort.Write("3, a!");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            string serialNow = _serialPort.ReadExisting();
            tb_console.Text = serialNow;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            _serialPort.Write("SREQ!"); //request serial operation
            tb_console.Text = serialNow;
        }
    }
}
