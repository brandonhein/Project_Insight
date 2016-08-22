using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;


namespace ReadGmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int count = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                System.Net.WebClient objClient = new System.Net.WebClient();
                string response;

                //Creating a new xml document
                XmlDocument doc = new XmlDocument();

                //Logging in Gmail server to get data
                objClient.Credentials = new System.Net.NetworkCredential("email", "password");

                //reading data and converting to string
                response = Encoding.UTF8.GetString(
                           objClient.DownloadData(@"https://mail.google.com/mail/feed/atom"));

                response = response.Replace(
                     @"<feed version=""0.3"" xmlns=""http://purl.org/atom/ns#"">", @"<feed>");

                //loading into an XML so we can get information easily
                doc.LoadXml(response);

                int newMessageCount = Convert.ToInt32(doc.SelectSingleNode(@"/feed/fullcount").InnerText);
                label6.Text = newMessageCount.ToString();

                if (newMessageCount == 0)
                {
                    label1.Text = "No new emails";
                    label2.Text = string.Empty;
                    label3.Text = string.Empty;
                    label4.Text = count.ToString();
                    //if (count >= 6)
                    //{
                    //    timer1.Interval = 100000;
                    //}
                    count++;
                }

                //
                else if (newMessageCount >= 1)
                {
                    label4.Text = count.ToString();
                    label1.Text = doc.SelectSingleNode(@"/feed/entry/summary").InnerText.ToLower();
                    label2.Text = doc.SelectSingleNode(@"/feed/entry/author/email").InnerText.ToLower();
                    if (label2.Text == "" || label2.Text == "" || label2.Text == "" || 
                        label2.Text == "")
                    {
                        label3.Text = "On whitelist!  DO THE THING! ༼つಠ益ಠ༽つ ─=≡ΣO))";
                        if (label1.Text == "get ping")
                        {
                            getPing();
                        }
                        else if (label1.Text == "get temp")
                        {
                            getTemp();
                        }
                        else if (label1.Text == "get cam1" || label1.Text == "get cam2" || label1.Text == "get cam3" || label1.Text == "get cam4")
                        {
                            getCamPic();
                        }
                        else
                        {
                            failSafeResponse();
                        }
                    }
                    else
                    {
                        label3.Text = "not on whitelist! Houston we have a problem!";
                        
                    }
                    count++;
                }
                
                /*
                //Reading the title and the summary for every email
                foreach (XmlNode node in doc.SelectNodes(@"/feed/entry"))
                {
                    title = node.SelectSingleNode("title").InnerText;
                    summary = node.SelectSingleNode("summary").InnerText;
                }*/
            }
            catch (Exception exe)
            {
                MessageBox.Show("Check your network connection");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        public void getPing()
        {
            label5.Text = "I would send a response to: " + label2.Text + " With the text PONG";
        }

        public void getTemp()
        {
            label5.Text = "I would send a response to: " + label2.Text + " with the most recent temp and last extract";
        }

        public void getCamPic()
        {
            label5.Text = "I would send a response to: " + label2.Text + " with an attachment of what camera they chose";
        }

        public void failSafeResponse()
        {
            label5.Text = "I would send a response to: " + label2.Text + " with the text 'invalid request' start with get and what you're trying to do Example: 'Get ping'";
        }

    }
}
