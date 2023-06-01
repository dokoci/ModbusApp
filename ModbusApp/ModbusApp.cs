using System;
using System.Drawing;
using System.Windows.Forms;
using EasyModbus;

namespace ModbusApp
{
    public partial class ModbusApp : Form
    {
        private ModbusClient modbus;
        
        public ModbusApp()
        {
            InitializeComponent();
            modbus = new ModbusClient("192.168.1.50", 502);
            //modbus = new ModbusClient("192.168.1.50", 502);

            //modbus = new ModbusClient();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                modbus.Connect();
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Unable to connect to Server.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            //modbus.WriteMultipleCoils(0, new bool[] { true, true, true, true, true, true, true, true, true, true });
            bool[] readCoils = modbus.ReadCoils(1, 10);
            //bool t = readCoils[4];
            //if (t)
            //{
            //    panel2.BackColor = Color.LimeGreen;
            //}
            //else
            //{
            //    panel1.BackColor = Color.OrangeRed;
            //}
            if (readCoils[1])
            {
                panel3.BackColor = Color.LimeGreen;
            }
            else
            {
                panel3.BackColor = Color.OrangeRed;
            }

            if (readCoils[5])
            {
                //door2Lbl.BackColor = Color.LimeGreen;
                panel2.BackColor = Color.LimeGreen;
            }
            else
            {
                //door2Lbl.BackColor = Color.OrangeRed;
                panel2.BackColor = Color.OrangeRed;
            }
            if (readCoils[3])
            {
                //door1Lbl.BackColor = Color.LimeGreen;
                panel1.BackColor = Color.LimeGreen;
            }
            else
            {
                //door1Lbl.BackColor = Color.OrangeRed;
                panel2.BackColor = Color.OrangeRed;
            }
            //timer1.Enabled = true; 

            listBox1.Items.Clear();

            for (int i = 0; i < readCoils.Length; i++)
            {
                listBox1.Items.Add(readCoils[i]);
            }
        }

        private void CoilValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modbus.Connect();
                if (coilAddress.SelectedIndex > -1 && coilAddress.SelectedIndex > -1)
                {
                    int address = Convert.ToInt32(coilAddress.SelectedItem);
                    bool value = Convert.ToBoolean(coilValue.SelectedItem);
                    if (value)
                    {
                        modbus.WriteSingleCoil(address, true);
                        groupBox1.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        modbus.WriteSingleCoil(address, false);
                        groupBox1.BackColor = Color.Red;
                    }
                }
                modbus.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Unable to connect to Server.");
            }
            
        }

        private void Readbtn_Click(object sender, EventArgs e)
        {
            try
            {
                modbus.Connect();
                bool[] readCoils = modbus.ReadCoils(1, 10);
                listBox1.Items.Clear();

                for (int i = 0; i < readCoils.Length; i++)
                {
                    listBox1.Items.Add(readCoils[i]);
                }

                modbus.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Unable to connect to Server.");
            }
        }

        private void coilAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                modbus.Connect();
                if (coilValue.SelectedIndex > -1 && coilAddress.SelectedIndex > -1)
                {
                    int address = Convert.ToInt32(coilAddress.SelectedItem);
                    bool value = Convert.ToBoolean(coilValue.SelectedItem);
                    if (value)
                    {
                        modbus.WriteSingleCoil(address, true);
                        groupBox1.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        modbus.WriteSingleCoil(address, false);
                        groupBox1.BackColor = Color.Red;
                    }
                }
                modbus.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Unable to connect to Server.");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                modbus.Connect();
                bool[] readDiscrete = modbus.ReadDiscreteInputs(0, 10);
                listBox2.Items.Clear();

                for (int i = 0; i < readDiscrete.Length; i++)
                {
                    listBox2.Items.Add(readDiscrete[i]);
                }

                modbus.Disconnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to connect to Server.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                modbus.Connect();
                int[] readInputReg = modbus.ReadInputRegisters(0, 10);
                listBox3.Items.Clear();
                for (int i = 0; i < readInputReg.Length; i++)
                {
                    listBox3.Items.Add(readInputReg[i]);
                }
                modbus.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Unable to connect to Server.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                modbus.Connect();
                int[] readHoldReg = modbus.ReadHoldingRegisters(0, 9);
                listBox4.Items.Clear();
                for (int i = 0; i < readHoldReg.Length; i++)
                {
                    listBox4.Items.Add(readHoldReg[i]);
                }
                modbus.Disconnect();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Unable to connect to Server.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();
            coilAddress.SelectedIndex = -1;
            coilValue.SelectedIndex = -1;
            groupBox1.BackColor = Color.Transparent;
            if (modbus.Connected)
            {
                modbus.Disconnect();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            modbus.WriteSingleCoil(1, true);
        }
    }
}
