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

namespace Mayın_Tarlası
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        Random rnd = new Random();
        int oyun;
        int sure;
        int puan;
        List<int> mayın = new List<int>();
        int dtiklanan=0;
        int mayinsayisi = 0;
        int kutu = 0;
        private void ClickYakala()
        {
            foreach (Control ctl in this.tableLayoutPanel1.Controls)
            {
                ctl.MouseClick += new MouseEventHandler(Form1_MouseClick);
            }
        }

        void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Event(sender, "MouseClick");
        }


        private void HepsiniAc()
        {
            int i = 0;
            for (i=0; i <= (tableLayoutPanel1.ColumnCount * tableLayoutPanel1.RowCount) - 1; i++)
            {
                var item = tableLayoutPanel1.Controls[i];
                if (mayın.IndexOf(Convert.ToInt32(item.Name)) != -1)
                {
                    item.BackColor = Color.Red;
                }
                else
                {
                    item.BackColor = Color.Green;
                }
            }
        }

        private void Event(object sender, string msg)
        {
            
            if (sender.GetType().Name == "PictureBox")
            {
                var tiklanan = (PictureBox)sender;
                if(tiklanan.BackColor!= Color.Green && tiklanan.BackColor != Color.Red)
                {
                    var isim = tiklanan.Name;
                    if (mayın.IndexOf(Convert.ToInt32(isim)) != -1)
                    { // Yandın
                        timer1.Stop();
                        
                        label2.Text = "Süre: 0";
                        label1.Text = "Puan: 0";
                        HepsiniAc();
                        MessageBox.Show("Kaybettin. Oyun Bitti ! \nPuan: " + puan + "\nSüre: " + sure);
                    }
                    else
                    {
                        tiklanan.BackColor = Color.Green;
                        puan++;
                        label1.Text = "Puan: " + puan.ToString();
                        if (dtiklanan == ((tableLayoutPanel1.ColumnCount * tableLayoutPanel1.RowCount) - 1)-mayinsayisi)
                        {
                            puan = 100;
                            
                            label2.Text = "Süre: 0";
                            label1.Text = "Puan: 0";
                            HepsiniAc();
                            MessageBox.Show("Kazandın. Oyun Bitti ! \nPuan: " + puan + "\nSüre: " + sure);

                        }
                        else
                        {
                            dtiklanan++;
                        }
                    }
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
       
        public void basla(string mod)
        {
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Clear();
            mayın.Clear();
            dtiklanan = 0;
            mayinsayisi = 0;
            kutu = 0;
            if (mod == "kolay")
            {
                mayinsayisi = 1;
                this.Size = new Size(250, 300);

                tableLayoutPanel1.ColumnCount = 3;
                tableLayoutPanel1.RowCount = 3;
               
            }else if (mod == "orta")
            {
                mayinsayisi = 6;
                this.Size = new Size(350, 400);
                tableLayoutPanel1.ColumnCount = 5;
                tableLayoutPanel1.RowCount = 5;
            }else if (mod == "zor")
            {
                mayinsayisi = 15;
                this.Size = new Size(450, 500);
                tableLayoutPanel1.ColumnCount = 7;
                tableLayoutPanel1.RowCount = 7;
            }else if(mod== "imkansiz")
            {
                mayinsayisi = 99;
                this.Size = new Size(550, 600);
                tableLayoutPanel1.ColumnCount = 10;
                tableLayoutPanel1.RowCount = 10;
            }
            
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                for (int x = 0; x < tableLayoutPanel1.RowCount; x++)
                {
                    if (i == 0)
                    {
                        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                    }
                    PictureBox cmd = new PictureBox();
                    cmd.BackColor = Color.Gray;
                    cmd.Dock = DockStyle.Fill;
                    cmd.Name = kutu.ToString();
                    tableLayoutPanel1.Controls.Add(cmd, i, x);
                    kutu++;
                }
            }
            int randomsayi;
            for (int i = 0; i < mayinsayisi; i++)
            {
               
                do
                {
                  randomsayi = rnd.Next(0, (tableLayoutPanel1.ColumnCount * tableLayoutPanel1.RowCount) - 1);

                } while (mayın.Contains(randomsayi));

                mayın.Add(randomsayi);
            }

            sure = 0;
            puan = 0;

            ClickYakala();
            timer1.Start();
        }

        private void kolayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basla("kolay");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure++;
            label2.Text = "Süre: " + sure;
        }

        private void ortaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basla("orta");
        }

        private void zorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basla("zor");
        }

        private void imkansızToolStripMenuItem_Click(object sender, EventArgs e)
        {
            basla("imkansiz");
        }
    }
}
