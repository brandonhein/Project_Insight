using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;

namespace ApplicationScheduler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HHmmss");

            //Run A.P.P.S.
            if (label1.Text == "121830" || label1.Text == "123700") //time of day 24hour
            {
                Process programStart = new Process();
                programStart.StartInfo.FileName = @"Program location";
                programStart.Start();
            }
            
            //
            if (button2.Enabled == true && (label1.Text == "133656" || label1.Text == "225440"))
            {
                try
                {
                    Ping pingClass = new Ping();
                    PingReply pingReply = pingClass.Send("8.8.8.8");
                    File.AppendAllText(@"log.txt", DateTime.Now + " - Internet - " + pingReply.RoundtripTime.ToString() + "ms" + Environment.NewLine);
                }
                
                catch (System.Net.NetworkInformation.PingException)
                {
                    try
                    {
                        Ping pingClass = new Ping();
                        PingReply pingReply = pingClass.Send("www.routerlogin.com");
                        File.AppendAllText(@"log.txt", DateTime.Now + " - Router Only - " + pingReply.RoundtripTime.ToString() + "ms" + Environment.NewLine);
                        button3_Click(sender, e);
                    }
                    catch (System.Net.NetworkInformation.PingException)
                    {
                        File.AppendAllText(@"log.txt", DateTime.Now + " - No Router - " + Environment.NewLine);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HHmmss");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Router's Port Forwarding is 'Enabled' then saved for a reboot
            if (label2.Text == "1")
            {
                Process programStart = new Process();
                programStart.StartInfo.FileName = @"C:\Users\Brandon.Brandon-PC\Documents\iMacros\Macros\#Current.iim";
                programStart.Start();
                label2.Text = "2";
            }
            //Router's Port Forwarding is 'Disabled' then saved for a 'reboot'
            else if (label2.Text == "2")
            {
                Process programStart = new Process();
                programStart.StartInfo.FileName = @"C:\Users\Brandon.Brandon-PC\Documents\iMacros\Macros\#Current.iim";
                programStart.Start();
                label2.Text = "1";
            }
        }
    }
}
