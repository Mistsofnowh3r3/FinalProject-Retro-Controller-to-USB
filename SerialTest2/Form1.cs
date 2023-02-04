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


namespace SerialTest2
{
    
    public partial class Form1 : Form
    {
        private SerialPort _serialPort = new System.IO.Ports.SerialPort("COM11", 9600);

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
            string indata = _serialPort.ReadExisting();
            Console.WriteLine("Data received: " + indata);
            tb_serialread.Invoke(new Action(() => tb_serialread.Text = indata));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
            _serialPort.Open();
        
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


            _serialPort.Write("SREQ"); //request serial operation

            for (int i = 0; i < 20; i++) // wait for a acknowledge and if not time out
            {
            }
            //send to serial


        }
    }
}
