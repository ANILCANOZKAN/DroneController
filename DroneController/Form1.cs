using DroneController.Indicators;
using DroneController.Properties;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
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
using System.Drawing.Drawing2D;

namespace DroneController
{
    public partial class Form1 : Form
    {
        int vSpeed = 0;
        double x = 10;
        double y = 10;
        long Altitude = 10;
        long AirSpeed = 0;
        long Heading = 0;
        long AttitudeIndicatorRollAngle = 0;
        long AttitudeIndicatorPitchAngle = 0;
        bool control = true;


        public Form1()
        {
            InitializeComponent();
            panel2.BackColor = Color.Black;
        }



        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            label4.Text = x.ToString();
            label2.Text = y.ToString();
            PointLatLng point = new GMap.NET.PointLatLng(x, y);
            gMapControl1.Position = point;
            
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
                vSpeed = vSpeed + 1;
                x = x + 0.005;
                y = y + 0.005;
                Altitude = Altitude + 1;
                AirSpeed = AirSpeed + 1;
                Heading = Heading + 1;
                if(Heading == 36)
                {
                    Heading = 0;
                }
                AttitudeIndicatorRollAngle = AttitudeIndicatorRollAngle + 1;
                AttitudeIndicatorPitchAngle = AttitudeIndicatorPitchAngle + 1;
                label8.Text = AttitudeIndicatorRollAngle.ToString()+ "°";
                label5.Text = AttitudeIndicatorPitchAngle.ToString()+ "°";
                setPFD(Altitude, AirSpeed, Heading, AttitudeIndicatorRollAngle, AttitudeIndicatorPitchAngle);
                setGmap(x, y);
                panel1.Invalidate();
                panel2.Invalidate();
                wait(10);
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            Pen pencil;
            Point[] points;
            Brush brush;
            string text = "";
            Font font;
            SizeF textSize;

            //Background çizer
            pencil = new Pen(Color.Black);
            points = new Point[] {
                new Point(0, 0),
                new Point(60, 0),
                new Point(60, 299),
                new Point(0, 299),
            };
            g.DrawPolygon(pencil, points);
            brush = new SolidBrush(Color.Black);
            g.FillPolygon(brush, points);

            pencil = new Pen(Color.MidnightBlue);
            points = new Point[] {
                new Point(4, 4),
                new Point(55, 4),
                new Point(55, 294),
                new Point(4, 294),
            };
            g.DrawPolygon(pencil, points);
            brush = new SolidBrush(Color.FromArgb(120,5,39,101));
            g.FillPolygon(brush, points);

            int number = 10;
            for(int i = 0; i<21; i++)
            {
                text = ""+number;
                font = new Font("Arial", 8);
                textSize = g.MeasureString(text, font);
                g.DrawString(text, font, Brushes.White, 5, 14+(i*13));
                number--;
            }

            // İbreyi çizer
            int ibre = vSpeed * 13;
            pencil = new Pen(Color.Red);
            points = new Point[] {
                new Point(25, 150-ibre),
                new Point(40, 162-ibre),
                new Point(69, 162-ibre),
                new Point(69, 138-ibre),
                new Point(40, 138 - ibre)
            };
            g.DrawPolygon(pencil, points);
            brush = new SolidBrush(Color.Red);
            g.FillPolygon(brush, points);

            points = new Point[] {
                new Point(28, 150 - ibre),
                new Point(40, 160 - ibre),
                new Point(67, 161 - ibre),
                new Point(67, 140 - ibre),
                new Point(40, 140 - ibre)
            };
            brush = new SolidBrush(Color.Black);
            g.FillPolygon(brush, points);

            text = ""+vSpeed;
            font = new Font("Arial Black", 12);
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.White, 40, 138-ibre);

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pencil;
            Point[] points;
            Brush brush;
            string text = "";

            pencil = new Pen(Color.DarkGray);
            points = new Point[] {
                new Point(31, 45),
                new Point(31, 5),
                new Point(391, 5),
                new Point(391, 45)
        };
            brush = new SolidBrush(Color.DimGray);
            g.FillPolygon(brush, points);
            g.DrawPolygon(pencil, points);

            pencil = new Pen(Color.Red, 3);
            points = new Point[] {
                new Point(211, 15),
                new Point(221, 0),
                new Point(201, 0)
        };
            brush = new SolidBrush(Color.Red);
            g.FillPolygon(brush, points);
            g.DrawPolygon(pencil, points);
            label11.Text = (Heading * 10).ToString();
            float radyan = (Heading * 20);

            text = "N";
            Font font = new Font("Arial", 8);
            SizeF textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.White, 207-(radyan), 17);

            for(int i = 15; i <90; i += 15) {
                font = new Font("Arial", 8);
                text = "|";
                g.DrawString(text, font, Brushes.White, 209 - (radyan) + i*2, 17);
                font = new Font("Arial", 8);
                text = i+"°";
                g.DrawString(text, font, Brushes.White, 204 - (radyan) + i*2, 29);
            }
               
            font = new Font("Arial", 8);
            text = "E";
            g.DrawString(text, font, Brushes.White, 387-(radyan), 17);

            for (int i = 15; i < 90; i += 15)
            {
                font = new Font("Arial", 8);
                text = "|";
                g.DrawString(text, font, Brushes.White, 389 - (radyan) + i * 2, 17);
                font = new Font("Arial", 8);
                text = i + "°";
                g.DrawString(text, font, Brushes.White, 384 - (radyan) + i * 2, 29);
            }

            font = new Font("Arial", 8);
            text = "W";
            g.DrawString(text, font, Brushes.White, 25-(radyan), 17);

            for (int i = 15; i < 90; i += 15)
            {
                font = new Font("Arial", 8);
                text = "|";
                g.DrawString(text, font, Brushes.White, 27 - (radyan) + i * 2, 17);
                font = new Font("Arial", 8);
                text = i + "°";
                g.DrawString(text, font, Brushes.White, 22 - (radyan) + i * 2, 29);
            }

            font = new Font("Arial", 8);
            text = "S";
            g.DrawString(text, font, Brushes.White, 567-radyan, 17);

            for (int i = 15; i < 90; i += 15)
            {
                font = new Font("Arial", 8);
                text = "|";
                g.DrawString(text, font, Brushes.White, 569 - (radyan) + i * 2, 17);
                font = new Font("Arial", 8);
                text = i + "°";
                g.DrawString(text, font, Brushes.White, 564 - (radyan) + i * 2, 29);
            }

            font = new Font("Arial", 8);
            text = "W";
            g.DrawString(text, font, Brushes.White, 746 - (radyan), 17);

            for (int i = 15; i < 90; i += 15)
            {
                font = new Font("Arial", 8);
                text = "|";
                g.DrawString(text, font, Brushes.White, 748 - (radyan) + i * 2, 17);
                font = new Font("Arial", 8);
                text = i + "°";
                g.DrawString(text, font, Brushes.White, 743 - (radyan) + i * 2, 29);
            }

            text = "N";
            font = new Font("Arial", 8);
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.White, 927 - (radyan), 17);

            for (int i = 15; i < 90; i += 15)
            {
                font = new Font("Arial", 8);
                text = "|";
                g.DrawString(text, font, Brushes.White, 929 - (radyan) + i * 2, 17);
                font = new Font("Arial", 8);
                text = i + "°";
                g.DrawString(text, font, Brushes.White, 924 - (radyan) + i * 2, 29);
            }

            text = "E";
            font = new Font("Arial", 8);
            textSize = g.MeasureString(text, font);
            g.DrawString(text, font, Brushes.White, 1108 - (radyan), 17);

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
    }

