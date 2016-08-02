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

            //Run A.P.P.S. (7,8,9,11,1,3,5,7,8,9)
            if (label1.Text == "080000" || label1.Text == "090000" || label1.Text == "110000" || label1.Text == "130000"
                || label1.Text == "150000" || label1.Text == "170000" || label1.Text == "190000" || label1.Text == "200000"
                || label1.Text == "210000" || label1.Text == "070000") //time of day 24hour
            {
                Process programStart = new Process();
                programStart.StartInfo.FileName = @"Program location";
                programStart.Start();
            }
            
            //so many OR's tied to the button, want to be able to turn off this service when i want.
            if (button2.Enabled == true && (   label1.Text == "001500" || label1.Text == "011500" || label1.Text == "021500"
                                            || label1.Text == "031500" || label1.Text == "041500" || label1.Text == "051500"
                                            || label1.Text == "061500" || label1.Text == "071500" || label1.Text == "081500"
                                            || label1.Text == "091500" || label1.Text == "101500" || label1.Text == "111500"
                                            || label1.Text == "121500" || label1.Text == "131500" || label1.Text == "141500"
                                            || label1.Text == "151500" || label1.Text == "161500" || label1.Text == "171500"
                                            || label1.Text == "181500" || label1.Text == "191500" || label1.Text == "201500"
                                            || label1.Text == "211500" || label1.Text == "221500" || label1.Text == "231500"))
            {
                try
                {
                    Ping pingClass = new Ping();
                    PingReply pingReply = pingClass.Send("8.8.8.8");
                    File.AppendAllText(@"C:\Users\Brandon.Brandon-PC\Desktop\Project_Insight-master\ApplicationScheduler\ApplicationScheduler\bin\Debug\log.txt", DateTime.Now + " - PingService - Ping/Internet - " + pingReply.RoundtripTime.ToString() + "ms" + Environment.NewLine);
                }
                
                catch (System.Net.NetworkInformation.PingException)
                {
                    try
                    {
                        Ping pingClass = new Ping();
                        PingReply pingReply = pingClass.Send("www.routerlogin.com"); //att.elevate
                        File.AppendAllText(@"C:\Users\Brandon.Brandon-PC\Desktop\Project_Insight-master\ApplicationScheduler\ApplicationScheduler\bin\Debug\log.txt", DateTime.Now + " - PingService - Ping/Router - " + pingReply.RoundtripTime.ToString() + "ms" + Environment.NewLine);
                        button3_Click(sender, e);//run the Macro to reset router
                    }
                    catch (System.Net.NetworkInformation.PingException)
                    {
                        File.AppendAllText(@"C:\Users\Brandon.Brandon-PC\Desktop\Project_Insight-master\ApplicationScheduler\ApplicationScheduler\bin\Debug\log.txt", DateTime.Now + " - PingService - Ping/No Connection - null - " + Environment.NewLine);
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
