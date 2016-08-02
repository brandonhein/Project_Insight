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
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace webcam1
{
    public partial class BRADS_UI : Form
    {
        public BRADS_UI()
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

            try
            {
                WebRequest request;
                HttpWebResponse response;
                Stream dataStream = null;
                StreamReader reader = null;
                request = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=rogerscity,mi&APPID=750ea949f23d7f00ad728e07f648018e");
                response = (HttpWebResponse)request.GetResponse();
                label4.Text = "API Status: " + (response.StatusDescription);
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                string jsonText = reader.ReadToEnd();
                
                jsonText = jsonText.Replace("\"", "");
                //jsonText = jsonText.Replace(",", "");
                //jsonText = jsonText.Replace("}", "");
                string humidity = getBetween(jsonText, "humidity:", ",");
                string temp = getBetween(jsonText, "temp:", ",");
                string highTemp = getBetween(jsonText, "temp_max:", "},");
                string lowTemp = getBetween(jsonText, "temp_min:", ",");
                string pressure = getBetween(jsonText, "pressure:", ",");
                string weatherCondition = getBetween(jsonText, "main:", ",des");
                string description = getBetween(jsonText, "description:", ",");
                
                label5.Text = "Current: " + weatherCondition + "\n" +
                              "Descriptive: " + description + "\n\n" +
                              "Current Temp: " + Math.Round((Convert.ToDouble(temp) * 9 / 5 - 459.67), 2) + "°F\n" +
                              "High Temp: " + Math.Round((Convert.ToDouble(highTemp) * 9 / 5 - 459.67), 0) + "°F\n" +
                              "Low Temp: " + Math.Round((Convert.ToDouble(lowTemp) * 9 / 5 - 459.67), 0) + "°F\n\n" +
                              "Humidity: " + humidity + "% \n" +
                              "Pressure: " + pressure + " hpa \n";
                File.AppendAllText("C:\Users\Brandon.Brandon-PC\Desktop\Project_Insight-master\ApplicationScheduler\ApplicationScheduler\bin\Debug\log.txt", DateTime.Now + " - BRADS - WeatherAPI - Success" + Environment.NewLine);
            }
            catch
            {
                label4.Text = "API Status: ERROR";
                label5.Text = "API Error";
                File.AppendAllText(@"C:\Users\Brandon.Brandon-PC\Desktop\Project_Insight-master\ApplicationScheduler\ApplicationScheduler\bin\Debug\log.txt", DateTime.Now + " - BRADS - WeatherAPI - ERROR " + Environment.NewLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
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
            string saveFile = "Cam_" + comboBox1.SelectedIndex + "_Snapshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";
            //label4.Text = saveFile;
            pictureBox1.Image.Save(saveFile);
            label8.Visible = true;
            label8.Text = "Picture saved! FileName= " + saveFile;
            File.AppendAllText(@"C:\Users\Brandon.Brandon-PC\Desktop\Project_Insight-master\ApplicationScheduler\ApplicationScheduler\bin\Debug\log.txt", DateTime.Now + " - BRADS - PictureTaken - CAM:" + comboBox1.SelectedIndex + Environment.NewLine);
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
                //label1.Text = "2";
                //Thread.Sleep(3000);
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                cam.Stop();
                cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();
                //label1.Text = "2";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                cam.Stop();
                cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();
                //label1.Text = "2";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();
        }
    }
}
