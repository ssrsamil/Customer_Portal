using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerPortal
{
    public partial class Form1 : Form
    {
        List<Tariff> tarifList = new List<Tariff>();
        List<Subscriber> subscriberList = new List<Subscriber>();
        List<Operation> operationList = new List<Operation>();

        Operation oper = new Operation();
        public static decimal preBalance;
  
        StringBuilder str = new StringBuilder();
        Thread t;

        string sourceMSISDN = "";
        int position = 0;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;

            tarifList.Add(new Tariff("basic", 6, 0.05, 7));
            tarifList.Add(new Tariff("pro", 6, 0.04, 9));
            tarifList.Add(new Tariff("smart", 6, 0.04, 5));

            subscriberList.Add(new Subscriber("99450", 2, tarifList[0]));
            subscriberList.Add(new Subscriber("99455", 1, tarifList[1]));
            subscriberList.Add(new Subscriber("99470", 1, tarifList[2]));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage1;
            if (tarifList.Count == 0)
            {
                textBox1.Text = "Currently there is not any tariff";
            }
            else
            {
                ShowTariffs();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage2;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage3;
            if (subscriberList.Count == 0)
            {
                textBox6.Text = "Currently there is not any subs";
            }
            else
            {
                textBox6.Text = ShowSubs();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage4;

            panel1.Controls.Clear();
            RadioButton[] radioButtons = new RadioButton[tarifList.Count];

            if (tarifList.Count == 0)
            {
                MessageBox.Show("Currently there is not any tariff");
                this.tabControl1.SelectedTab = tabPage0;

            }
            else
            {
                for (int i = 0; i < tarifList.Count; ++i)
                {
                    radioButtons[i] = new RadioButton();
                    radioButtons[i].Text = tarifList[i].PackageName;
                    radioButtons[i].Location = new System.Drawing.Point(10, 10 + i * 25);
                    panel1.Controls.Add(radioButtons[i]);
                    panel1.Show();
                    radioButtons[i].CheckedChanged += RadioButton_CheckedChanged;
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            if (radiobutton.Checked)
            {

                if (String.IsNullOrEmpty(textBox10.Text))
                {
                    MessageBox.Show("Please enter MSISDN");
                }
                else if (subscriberList.Any(t => t.MSISDN == textBox10.Text))
                {
                    MessageBox.Show("ALERT!! MSISDN already exist");
                    radiobutton.Checked = false;
                }
                else
                {
                    if (textBox8.Text == "")
                    {
                        textBox8.Text = "0.00";
                    }
                    var result = tarifList.Find(x => x.PackageName == radiobutton.Text);

                    subscriberList.Add(new Subscriber(textBox10.Text, Convert.ToDecimal(textBox8.Text), result));
                    MessageBox.Show("Subscriber " + textBox10.Text + " created.");
                    radiobutton.Checked = false;
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();  //”this” refers to the form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage5;
            panel2.Controls.Clear();
            RadioButton[] radioButtons = new RadioButton[subscriberList.Count];

            if (subscriberList.Count == 0)
            {
                MessageBox.Show("Currently there is not any tariff");
                this.tabControl1.SelectedTab = tabPage0;
            }
            else
            {
                for (int i = 0; i < subscriberList.Count; ++i)
                {
                    radioButtons[i] = new RadioButton();
                    radioButtons[i].Text = subscriberList[i].MSISDN + "\r\n";
                    radioButtons[i].Location = new System.Drawing.Point(10, i * 30);
                    panel2.Controls.Add(radioButtons[i]);
                    panel2.Show();

                    radioButtons[i].CheckedChanged += RadioButton2_CheckedChanged;
                }
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            var textMsisdn = radiobutton.Text;

            if (radiobutton.Checked)
            {
                panel2.Controls.Clear();
                Button b = new Button();
                b.Text = "1. show balance";
                b.Width = 150;
                b.Location = new System.Drawing.Point(10, 5);
                Button b2 = new Button();
                b2.Text = "2. make a call";
                b2.Width = 150;
                b2.Location = new System.Drawing.Point(10, 50);
                label8.Text = "Select operation";
                panel2.Controls.Add(b);
                panel2.Controls.Add(b2);

                b.Click += new EventHandler(delegate (Object o, EventArgs a)
                {
                    Decimal balance = subscriberList.First(item => item.MSISDN + "\r\n" == textMsisdn).Balance;
                    sourceMSISDN = subscriberList.First(item => item.MSISDN + "\r\n" == textMsisdn).MSISDN;
                    MessageBox.Show("Subscriber balance is : " + balance.ToString() + " azn.");
                });

                b2.Click += new EventHandler(delegate (Object o, EventArgs a)
                {
                    RadioButton[] radioButtons = new RadioButton[subscriberList.Count];
                    panel2.Controls.Clear();
                    int interval = 5;
                    for (int i = 0; i < subscriberList.Count; ++i)
                    {
                        if (subscriberList[i].MSISDN + "\r\n" != textMsisdn)
                        {
                            radioButtons[i] = new RadioButton();
                            radioButtons[i].Text = subscriberList[i].MSISDN + "\r\n";
                            radioButtons[i].Location = new System.Drawing.Point(10, interval);
                            panel2.Controls.Add(radioButtons[i]);
                            panel2.Show();
                            interval += 25;
                            radioButtons[i].CheckedChanged += RadioButton3_CheckedChanged;
                        }
                    }
                    sourceMSISDN = subscriberList.First(item => item.MSISDN + "\r\n" == textMsisdn).MSISDN;
                });
            }
        }
        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            for (int i = 0; i < subscriberList.Count; i++)
            {
                if (subscriberList[i].MSISDN == sourceMSISDN)
                {
                    position = i;
                }
            }

            MessageBox.Show("Press OK to start call to " + radiobutton.Text);

            Button bb1 = new Button();
            bb1.Text = "Press to interrupt";
            bb1.Width = 150;
            bb1.Height = 30;
            bb1.Location = new System.Drawing.Point(10, 75);
            panel2.Controls.Add(bb1);

            preBalance = subscriberList[position].Balance;

            t = new Thread(func1);
            t.Start();

            bb1.Click += new EventHandler(delegate (Object o, EventArgs a)
            {
                Operation.checkpoint = true;

            });
        }

        private void func1()
        {
                oper.StartCall(subscriberList[position]);
              //string seconds = oper.Duration(Operation.startTime).ToString("hh':'mm':'ss");
              //  MessageBox.Show(oper.CallDurationDisplay(subscriberList[position]));
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage1;
            if (tarifList.Count == 0)
            {
                textBox1.Text = "Currently there is not any tariff";
            }
            else
            {
                ShowTariffs();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void tabPage0_Click(object sender, EventArgs e)
        {
        }

        public void ShowTariffs()
        {
            textBox1.Clear();
            foreach (Tariff tarif in tarifList)
            {
                textBox1.Text += " - " + tarif.PackageName.ToUpper() + "\r\n";
                textBox1.Text += "      Price perMin: " + tarif.PricePerMinute.ToString() + " azn." + "\r\n";
                textBox1.Text += "      Price perSMS: " + tarif.PricePerSms.ToString() + " azn." + "\r\n";
                textBox1.Text += "      Price perMb: " + tarif.PricePerMegabyte.ToString() + " azn." + "\r\n";
                textBox1.Text += "-----------------------" + "\r\n";
            }
        }

        public string ShowSubs()
        {
            textBox6.Clear();
            string text = "";
            foreach (Subscriber sub in subscriberList)
            {
                text += " - " + sub.MSISDN + " (" + sub.TarifPackage.PackageName.ToUpper() + ") balance: " + sub.Balance + " azn." + "\r\n" + "\r\n";
            }
            return text;
        }

        public string ShowSubsForOperation()
        {
            textBox6.Clear();
            string text = "";
            int count = 0;
            foreach (Subscriber sub in subscriberList)
            {
                count++;
                text += count + ". Subscriber: " + sub.MSISDN + "\r\n" + "\r\n";
            }
            return text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage1;
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.WordWrap = false;
            if (tarifList.Count == 0)
            {
                textBox1.Text = "Currently there is not any tariff";
            }
            else
            {
                ShowTariffs();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void button12_Click_2(object sender, EventArgs e)
        {
            List<string> tarifNameAlreadyExist = new List<string>();// to to identify existing tarif names to prevent dublicates
            foreach (Tariff t in tarifList)
            {
                tarifNameAlreadyExist.Add(t.PackageName);
            }
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter TarifName");
            }
            else if (tarifNameAlreadyExist.Any(text => text == textBox2.Text))
            {
                MessageBox.Show("ALERT!! Tarif already exist.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox3.Text) | string.IsNullOrWhiteSpace(textBox4.Text) | string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    textBox3.Text = "0.03";
                    textBox4.Text = "0.09";
                    textBox5.Text = "9";
                }

                tarifList.Add(new Tariff(textBox2.Text, Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text)));
                MessageBox.Show("Tariff " + textBox2.Text.ToUpper() + " created.");

            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage4;
            if (tarifList.Count == 0)
            {
                MessageBox.Show("Currently there is not any tariff");
                this.tabControl1.SelectedTab = tabPage0;

            }
            else
            {
                panel1.Controls.Clear();
                RadioButton[] radioButtons = new RadioButton[tarifList.Count];

                for (int i = 0; i < tarifList.Count; ++i)
                {
                    radioButtons[i] = new RadioButton();
                    radioButtons[i].Text = tarifList[i].PackageName;
                    radioButtons[i].Location = new System.Drawing.Point(10, 10 + i * 25);
                    panel1.Controls.Add(radioButtons[i]);
                    panel1.Show();
                    radioButtons[i].CheckedChanged += RadioButton_CheckedChanged;
                }
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage3;
            if (subscriberList.Count == 0)
            {
                textBox6.Text = "Currently there is not any subs";
            }
            else
            {
                textBox6.Text = ShowSubs();
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }


        private void button19_Click(object sender, EventArgs e)
        {
        }

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (subscriberList.Count == 0)
            {
                textBox6.Text = "Currently there is not any subs";
            }
            else
            {
                textBox6.Text = ShowSubs();
            }
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox8_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedTab = tabPage0;
            label8.Text = "Select subscriber for operation";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            Operation oper = new Operation();
            Subscriber sub = new Subscriber("994", 0, tarifList[0]);
            oper.StartCall(sub);
        }

        private void button9_Click_3(object sender, EventArgs e)
        {

        }



    }
}
