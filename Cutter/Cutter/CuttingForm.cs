using System;
using System.Drawing;
using System.IO;
using MyCutters;
using System.Threading;
using System.Windows.Forms;

namespace Cutter
{
    public partial class CuttingForm : Form
    {
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new CuttingForm());
		}

		public CuttingForm()
        {
            InitializeComponent();
        }

        delegate void textboxkitoltesdelegate(string text, TextBox tbx, Button btn);

        Thread szal1 = null, szal2 = null, szal3 = null;
        CuttingData ctd1, ctd2, ctd3;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (szal1 != null)
            {
                if (szal1.ThreadState == ThreadState.Running)
                {
                }
                else
                {
                    szal1.Abort();
                    szal1 = null;
                    if (ctd1.cc1 != null)
                    {
                        ctd1.cc1.ende = false;
                        textBox2.Text += ctd1.cc1.ToString();
                    }
                    button1.Text = "Start";
                }
            }
            if (szal2 != null)
            {
                if (szal2.ThreadState == ThreadState.Running)
                {
                }
                else
                {
                    szal2.Abort();
                    szal2 = null;
                    if (ctd2.cc2 != null)
                    {
                        ctd2.cc2.ende = false;
                        textBox2.Text += ctd2.cc2.ToString();
                    }
                    button2.Text = "Start";
                }
            }
            if (szal3 != null)
            {
                if (szal3.ThreadState == ThreadState.Running)
                {
                }
                else
                {
                    szal3.Abort();
                    szal3 = null;
                    if (ctd3.cc3 != null)
                    {
                        ctd3.cc3.ende = false;
                        textBox2.Text += ctd3.cc3.ToString();
                    }
                    button3.Text = "Start";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (szal1 != null && szal1.ThreadState == ThreadState.Running)
            {
                ctd1.cc1.ende = true;
            }
            else
            {
                if (textBox1.Text == "") return;
                long probakszama;
                if (!long.TryParse(textBox4.Text, out probakszama)) return;
                string text = textBox3.Text + ", " + numericUpDown1.Value.ToString() + Environment.NewLine + textBox1.Text;
                szal1 = new Thread(new ParameterizedThreadStart(Cut_1));
                ctd1 = new CuttingData();
                ctd1.Text = text;
                ctd1.Probakszama = probakszama;
                szal1.Start(ctd1);
                button1.Text = "Stop";
            }
        }

        void Cut_1(object o)
        {
            ctd1 = (CuttingData)o;
            ctd1.cc1 = null;
            try
            {
                ctd1.cc1 = new MyCutters.Cutter_1(ctd1.Text);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            ctd1.cc1.Manipulal(ctd1.Probakszama);
        }

        void TextboxKitoltes(string text, TextBox tbx, Button btn)
        {
            if (text != "") tbx.Text += text + Environment.NewLine;
            btn.Text = "Start";
        }

        private void CuttingForm_SizeChanged(object sender, EventArgs e)
        {
            textBox1.Location = new Point(15, 15);
            textBox1.Size = new Size(110, this.Height - 60);
            textBox2.Location = new Point(textBox1.Right + 10, 15);
            label3.Location = new Point(this.Width - textBox3.Width - 20, 15);
            numericUpDown1.Location = new Point(label3.Left, label3.Bottom + 10);
            label1.Location = new Point(label3.Left, numericUpDown1.Bottom + 10);
            textBox3.Location = new Point(label3.Left, label1.Bottom + 10);
            label2.Location = new Point(label3.Left, textBox3.Bottom + 10);
            textBox4.Location = new Point(label3.Left, label2.Bottom + 10);
            button1.Location = new Point(label3.Left, textBox4.Bottom + 10);
            button2.Location = new Point(label3.Left, button1.Bottom + 10);
            button3.Location = new Point(label3.Left, button2.Bottom + 10);
            button4.Location = new Point(label3.Left, button3.Bottom + 10);
            button5.Location = new Point(label3.Left, button4.Bottom + 10);
            textBox2.Size = new Size(label3.Left - textBox1.Right - 20, textBox1.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (szal2 != null && szal2.ThreadState == ThreadState.Running)
            {
                ctd2.cc2.ende = true;
            }
            else
            {
                if (textBox1.Text == "") return;
                long probakszama;
                if (!long.TryParse(textBox4.Text, out probakszama)) return;
                string text = textBox3.Text + ", " + numericUpDown1.Value.ToString() + Environment.NewLine + textBox1.Text;
                szal2 = new Thread(new ParameterizedThreadStart(Cut_2));
                ctd2 = new CuttingData();
                ctd2.Text = text;
                ctd2.Probakszama = probakszama;
                szal2.Start(ctd2);
                button2.Text = "Stop";
            }
        }

        void Cut_2(object o)
        {
            CuttingData ctd2 = (CuttingData)o;
            ctd2.cc2 = null;
            try
            {
                ctd2.cc2 = new Cutter_2(ctd2.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            ctd2.cc2.Manipulal(ctd2.Probakszama);
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            if (szal3 != null && szal3.ThreadState == ThreadState.Running)
            {
                ctd3.cc3.ende = true;
            }
            else
            {
                if (textBox1.Text == "") return;
                long probakszama;
                if (!long.TryParse(textBox4.Text, out probakszama)) return;
                string text = textBox3.Text + ", " + numericUpDown1.Value.ToString() + Environment.NewLine + textBox1.Text;
                szal3 = new Thread(new ParameterizedThreadStart(Cut_3));
                ctd3 = new CuttingData();
                ctd3.Text = text;
                szal3.Start(ctd3);
                button3.Text = "Stop";
            }
        }


        void Cut_3(object o)
        {
            CuttingData ctd = (CuttingData)o;
            ctd3.cc3 = null;
            try
            {
                ctd3.cc3 = new Cutter_3(ctd.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            ctd3.cc3.Manipulal();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string text = File.ReadAllText(openFileDialog1.FileName);
                StringReader sr = new StringReader(text);
                string str = sr.ReadLine();
                string[] strt = str.Split(',');
                textBox3.Text = strt[0];
                if(strt.Length != 2)
                {
                    MessageBox.Show("A szálhosszt és a ráhagyást is meg kell adni vesszővel elválasztva!!!");
                    return;
                }
                int rh = 0;
                int.TryParse(strt[1], out rh);
                numericUpDown1.Value = rh;
                textBox1.Text = text.Substring(strt[0].Length + 1 + strt[1].Length + Environment.NewLine.Length);
                sr.Close();
            }
        }

        private void CuttingForm_Shown(object sender, EventArgs e)
        {
            button2.Focus();
            timer1.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") return;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) File.WriteAllText(saveFileDialog1.FileName, textBox2.Text);
        }

        private void CuttingForm_Load(object sender, EventArgs e)
        {
            CuttingForm_SizeChanged(this, null);
        }
    }

    class CuttingData
    {
        public string Text;
        public long Probakszama;
        public MyCutters.Cutter_1 cc1;
        public Cutter_2 cc2;
        public Cutter_3 cc3;
    }
}
