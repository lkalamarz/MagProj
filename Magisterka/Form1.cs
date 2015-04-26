using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;



namespace Magisterka
{
    public partial class Form1 : Form
    {
        static int baza = 0x8000;
        static int baza1 = 0x8001;
        static int baza2 = 0x8002;
        static int baza3 = 0x8003;
        static int baza4 = 0x8004;
        static int baza5 = 0x8005;
        bool cmd = false;
        byte[] bufor;
        int wynik = 0;
        int[] led = new int[8];
        int[] led2 = new int[8];
        int[] led3 = new int[8];
        int[] led4 = new int[8];
        int[] led5 = new int[8];
        int[] led6 = new int[8];
        SerialPort com1 = new SerialPort();
        inputs Inputs;
        outputs Outputs;
        bool connection = false;
        public Form1()
        {
            InitializeComponent();
            serial_init();
            inputs_init();
            outputs_init();
            while (connection)
                Serial_response();
            while (connection)
            {
                for (int i = 0; i < 8; i++)
                {
                    //led[i] = Inputs.input_read(baza, i);
                    led[i] = Outputs.outputs_read(baza1);
                    led2[i] = Inputs.input_read((baza1), i);
                textBox1.AppendText(i.ToString() + " " + led[i].ToString()  + " " + led2[i].ToString() + "gowno"+ "\r\n");
            }
               
            }
        }


        /// <summary>
        /// Buttons events handlers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                com1.PortName = listBox1.SelectedItem.ToString();
                try
                {
                    com1.Parity = (Parity)comboBox1.SelectedItem;
                    com1.PortName = listBox1.SelectedItem.ToString();
                    com1.StopBits = (StopBits)comboBox2.SelectedItem;
                    com1.DataBits = (int)comboBox3.SelectedItem;
                    com1.BaudRate = (int)comboBox4.SelectedItem;
                  //  com1.
                    com1.Open();
                    MessageBox.Show("Connected");
                  //  timer1.Start();
                    button2.Enabled = true;
                    button1.Enabled = false;
                    connection = true;
                    timer1.Enabled = true;
                 //   PortUse = true;
                  //  textBox1.Text = "";
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else MessageBox.Show("Set all properties");
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            connection = false;
            timer1.Enabled = false;
            com1.Close();
        }



       /// <summary>
       /// Serial ports methods
       /// </summary>

        private void serial_init()
        {
            listBox1.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())
            {
                listBox1.Items.Add(s);
            }

            for (int i = 0; i < 5; i++)
            {
                comboBox1.Items.Add((Parity)i);
            }
            comboBox1.SelectedItem = Parity.None;

            for (int i = 0; i < 4; i++)
            {
                comboBox2.Items.Add((StopBits)i);
            }
            comboBox2.SelectedItem = StopBits.One;

            for (int i = 0; i < 17; i++)
            {
                comboBox3.Items.Add(i);
            }
            comboBox3.SelectedItem = 8;

            comboBox4.Items.Add(1200);
            comboBox4.Items.Add(2400);
            comboBox4.Items.Add(4800);
            comboBox4.Items.Add(9600);
            comboBox4.Items.Add(14400);
            comboBox4.Items.Add(19200);
            comboBox4.Items.Add(28800);
            comboBox4.Items.Add(38400);
            comboBox4.Items.Add(57600);
            comboBox4.Items.Add(115200);
            comboBox4.SelectedItem = 115200;

        }

        private void Serial_response()
        { 
        int bytesAmount = com1.BytesToRead;
        if (bytesAmount > 0)
        {
            //textBox1.AppendText(">>");
            bufor = new byte[bytesAmount];
            com1.Read(bufor, 0, bytesAmount);
            string dane = System.Text.ASCIIEncoding.UTF8.GetString(bufor);
            textBox1.AppendText(dane.ToString()+ "\r\n");
        }
        }

        private void inputs_init()
        { Inputs = new inputs(com1,baza,timer2); }


        private void outputs_init()
        { Outputs = new outputs(com1,baza,timer1); }

        private void set_leds()
        {
            for (int i = 7; i >= 0; i--)
            {
                if (wynik - (int)Math.Pow(2, i) >= 0)
                {
                    led[i] = 1;
                    wynik -= (int)Math.Pow(2, i);
                }
                else
                    led[i] = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //int k = Inputs.input_read(baza, 4);
            //textBox1.AppendText(k.ToString() + "\r\n");
            //Outputs.output_set(baza1, 4, true);
            Outputs.BuzzerSet(0);
          // int[] h = new int[8];
           // for (int i = 0; i < 8; i++)
           // {
            //    h[i] = 0;
           // }
           // Outputs.LEDSet(h);
            //MessageBox.Show("Read outputvalue = " + Outputs.BuzzerRead().ToString());
           // Outputs.output_set(baza1, 5, false);
           // MessageBox.Show(k.ToString());
        //    Serial_response();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // int[] h = new int[8];
          //  for (int i = 0; i < 8; i++)
           // {
            //   h[i] = 1;
           // }
           // Outputs.LEDSet(h);
            Outputs.BuzzerSet(1);
            //int k = Inputs.input_read(baza, 5);
            //textBox1.AppendText(k.ToString() + "\r\n");
           // Outputs.output_set(baza1, 4, false);
           // Outputs.output_set(baza1, 5, true);
         //   MessageBox.Show(k.ToString());
          //  Serial_response();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
         /*   textBox1.Text = "";
            int j = 0;
        //   wynik = Inputs.inputs_read(baza1);
            if (cmd == false)
                wynik = Outputs.outputs_read(baza1);
            else
                wynik = Inputs.inputs_read(baza1);*/
           
         /*   for (int i = 0; i < 8; i++)
            {
                led[i] = Inputs.input_read(baza1, i);
                Inputs.Delay(670000);
                if (led[6] == 1)
                {
                    int bp = 9;
                }
                if (led[7] == 1)
                {
                    int bp = 9;
                }
                textBox1.AppendText(i.ToString() + " " + led[i].ToString() + "\r\n");
                
             //   led2[i] = Inputs.input_read((baza1), i);
             //   led3[i] = Inputs.input_read(baza2, i);
              //  led4[i] = Inputs.input_read(baza3, i);
               // led5[i] = Inputs.input_read(baza4, i);
              //  led6[i] = Inputs.input_read(baza5, i);
               // textBox1.Text += led2[i].ToString() + " " + /*led3[i].ToString() + " " + led4[i].ToString() + " " + led5[i].ToString() + " " + led6[i].ToString() +*/
            //}*/
            
          /*  for (int i = 0; i < 8; i++)
                led[i] = Inputs.input_read(baza, i);
           */
           
            //textBox1.AppendText(wynik.ToString() + "\r\n");

          
        }

        private void label6_Click(object sender, EventArgs e)
        {
            int k = Inputs.input_read(baza, 4);
            label6.Text = k.ToString();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            int k = Inputs.input_read(baza, 5);
            label7.Text = k.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            int k = Inputs.input_read(baza, 6);
            label8.Text = k.ToString();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            int k = Inputs.input_read(baza, 7);
            label9.Text = k.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            foreach (string s in SerialPort.GetPortNames() )
            {
                listBox1.Items.Add(s);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            cmd = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
           
            wynik = Outputs.outputs_read(baza1);
            set_leds();
            for (int i = 0; i < 8; i++)
            {
                textBox1.AppendText(i.ToString() + " - " + led[i].ToString() + "\r\n");

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            int[] x = {1,1,1,1,1,1,1,1};
            Outputs.LEDSet(x);

        }
    
    }

}
