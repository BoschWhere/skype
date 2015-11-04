using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;
namespace skype
{
    public partial class Skype : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public Skype()
        {
            InitializeComponent();
        }

        private void Skype_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo device in videoDevices)
            {
                comboBox1.Items.Add(device.Name);
            }

            videoSource = new VideoCaptureDevice();
            comboBox1.SelectedIndex = 0;
        }

        private void Start_key_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
            else
            {
                videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                videoSource.NewFrame += videoSource_NewFrame;
                videoSource.Start();

            }
        }
       void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

       private void Skype_FormClosing(object sender, FormClosingEventArgs e)
       {
           if (videoSource.IsRunning)
               videoSource.Stop();

       }
 
    }
}
