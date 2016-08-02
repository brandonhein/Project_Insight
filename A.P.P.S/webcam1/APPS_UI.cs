using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Net;

namespace webcam1
{
    public partial class APPS_UI : Form
    {
        public APPS_UI()
        {
            InitializeComponent();
        }
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            label3.Text = DateTime.Now.ToString();
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in webcam)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;

        }

        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bit;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cam.Stop();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string saveFile = "Cam_" + comboBox1.SelectedIndex + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
            string mostRecent = "Cam_" + comboBox1.SelectedIndex + ".jpg";
            //label4.Text = saveFile;
            pictureBox1.Image.Save(saveFile);
            pictureBox1.Image.Save(mostRecent);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedIndex == 0)
            {
                if (label1.Text == "2")
                {
                    cam.Stop();
                    cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
                    cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                    cam.Start();
                }
                else
                {
                    cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
                    cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                    cam.Start();
                    label1.Text = "2";
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                cam.Stop();
                cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                cam.Stop();
                cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                cam.Stop();
                cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer3.Enabled = false;
            label4.Visible = true;
            label4.Text = "Cam " + comboBox1.SelectedIndex + " Taking pic now";
            button2_Click(sender, e);
            try
            {
                comboBox1.SelectedIndex = 1;
                timer4.Enabled = true;
                timer2.Enabled = false;
            }
            catch
            {
                button3_Click(sender, e);
            }
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Cam " + comboBox1.SelectedIndex + " Taking pic now";
            button2_Click(sender, e);
            try
            {
                comboBox1.SelectedIndex = 2;
                timer5.Enabled = true;
                timer4.Enabled = false;
            }
            catch
            {
                button3_Click(sender, e);
            }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Cam " + comboBox1.SelectedIndex + " Taking pic now";
            button2_Click(sender, e);
            try
            {
                comboBox1.SelectedIndex = 3;
                //button3_Click(sender, e);
                timer6.Enabled = true;
                timer5.Enabled = false;
            }
            catch
            {
                button3_Click(sender, e);
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            label4.Visible = true;
            label4.Text = "Cam " + comboBox1.SelectedIndex + " Taking pic now";
            button2_Click(sender, e);
            try
            {
                comboBox1.SelectedIndex = 4;
            }
            catch
            {
                button3_Click(sender, e);
            }
        }
    }
}
