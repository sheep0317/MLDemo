using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
namespace MLAPPML.ConsoleApp
{
    public partial class Form2 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo filterInfo in filterInfoCollection)
            {
                comboBox1.Items.Add(filterInfo.Name);
            }
            try
            {
                comboBox1.SelectedIndex = 0;

            }
            catch {
                MessageBox.Show("Khong tim thay Webcam");
                this.Close();
            }
                            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
           
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.SignalToStop();

                    // wait ~ 3 seconds
                    for (int i = 0; i < 30; i++)
                    {
                        if (!videoCaptureDevice.IsRunning)
                            break;
                        System.Threading.Thread.Sleep(100);
                    }

                    if (videoCaptureDevice.IsRunning)
                    {
                        videoCaptureDevice.Stop();
                    }


                }
            }
            catch { }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if ( videoCaptureDevice.IsRunning)
                {
                     videoCaptureDevice.SignalToStop();

                    // wait ~ 3 seconds
                    for (int i = 0; i < 30; i++)
                    {
                        if (! videoCaptureDevice.IsRunning)
                            break;
                        System.Threading.Thread.Sleep(100);
                    }

                    if ( videoCaptureDevice.IsRunning)
                    {
                         videoCaptureDevice.Stop();
                    }

                     
                }
            }
            catch { }
            Bitmap varBmp = new Bitmap(pictureBox1.BackgroundImage);
            Bitmap newBitmap = new Bitmap(varBmp);
            varBmp.Save(@"a_check.jpg");
            //Now Dispose to free the memory
            varBmp.Dispose();
            varBmp = null;
        }
    }
}
