using DroneController.Indicators;
using DroneController.Properties;
using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace DroneController
{
    public partial class Form1 : Form
    {

        double x = 10;
        double y = 10;
        long Altitude = 10;
        long AirSpeed = 0;
        long Heading = 10;
        long AttitudeIndicatorRollAngle = 10;
        long AttitudeIndicatorPitchAngle = 10;
        bool control = true;

        public Form1()
        {
            InitializeComponent();
            
        }
       
        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            label4.Text = x.ToString();
            label2.Text = y.ToString();
            gMapControl1.Position = new GMap.NET.PointLatLng(x, y);
            
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap bmp = new Bitmap(DroneController.Properties.Resources.tricopter);
            bmp.MakeTransparent(Color.White);
            Point point = new Point(150, 150);
            e.Graphics.DrawImage(bmp, bmp.Width, 0, bmp.Width, bmp.Height);

        }
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        void setPFD(long Altitude, long AirSpeed, long Heading, long AttitudeIndicatorRollAngle, long AttitudeIndicatorPitchAngle)
        {
            IPrimaryFlightDisplay pfd = pfdControl1 as IPrimaryFlightDisplay;
            pfd.Altitude.Value = Altitude;
            pfd.AirSpeed.Value = AirSpeed;
            pfd.Heading.Value = Heading;
            pfd.AttitudeIndicator.RollAngle = AttitudeIndicatorRollAngle;
            pfd.AttitudeIndicator.PitchAngle = AttitudeIndicatorPitchAngle;

            pfdControl1.Redraw();
        }
        void setGmap(double x, double y)
        {
            label4.Text = x.ToString();
            label2.Text = y.ToString();
            gMapControl1.Position = new GMap.NET.PointLatLng(x, y);
            gMapControl1.Refresh();
        }
        public void getData()
        {
            while (control)
            {
                x = x + 0.005;
                y = y + 0.005;
                Altitude = Altitude + 1;
                AirSpeed = AirSpeed + 1;
                Heading = Heading + 20;
                AttitudeIndicatorRollAngle = AttitudeIndicatorRollAngle + 1;
                AttitudeIndicatorPitchAngle = AttitudeIndicatorPitchAngle + 1;
                setPFD(Altitude, AirSpeed, Heading, AttitudeIndicatorRollAngle, AttitudeIndicatorPitchAngle);
                setGmap(x, y); 
                wait(100);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }
        private void pfdControl1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            control = true;
            getData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stop();
        }
        public void stop()
        {
            control = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
