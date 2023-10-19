using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4a830212
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image image;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.Image = image;
            }
            try
            {
                int Height = this.pictureBox1.Image.Height;
                int width = this.pictureBox1.Image.Width;
                Bitmap NBitmap = new Bitmap(width, Height);
                Bitmap OBitmap = (Bitmap)this.pictureBox1.Image;
                Color pixel;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        pixel = OBitmap.GetPixel(x, y);
                        int r, g, b, YIQ;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        YIQ = (int)(0.299 * r + 0.587 * g + 0.114 * b);
                        NBitmap.SetPixel(x, y, Color.FromArgb(YIQ, YIQ, YIQ));
                    }
                }
                this.pictureBox1.Image = NBitmap;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Height = this.pictureBox1.Image.Height;
            int Width = this.pictureBox1.Image.Width;
            Bitmap Histogrambitmap = new Bitmap(Width, Height);
            Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
            double[] numa = new double[256];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Color pixelRGB = oldbitmap.GetPixel(j, i);
                    if (pixelRGB.R == pixelRGB.G && pixelRGB.R == pixelRGB.B)
                    {
                        int grayNumb = pixelRGB.R;
                        numa[grayNumb]++;
                    }
                }
            }
            for (int k = 0; k < 256; k++)
            {
                double value = numa[k];
                double rate = value / (Height * Width * 1.0);
                numa[k] = rate;
            }
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    double DSSum = 0;
                    Color value = oldbitmap.GetPixel(i, j);
                    for (int k = 0; k <= value.R; k++)
                    {    
                        DSSum += numa[k];
                    }
                    byte s = (byte)Math.Round(255 * DSSum);
                    Color newValue = Color.FromArgb(s, s, s);
                    Histogrambitmap.SetPixel(i, j, newValue);
                }
            }
            this.pictureBox2.Image = Histogrambitmap;
        }
    }
}
