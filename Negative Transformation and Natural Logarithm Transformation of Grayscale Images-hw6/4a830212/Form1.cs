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
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
                this.pictureBox2.Image = NBitmap;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int Height = this.pictureBox2.Image.Height;
                int width = this.pictureBox2.Image.Width;
                Bitmap NBitmap = new Bitmap(width, Height);
                Bitmap NBitmap1 = new Bitmap(width, Height);
                Bitmap OBitmap = (Bitmap)this.pictureBox2.Image;
                int[] XPF = new int[256];
                int[] XNL = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    XPF[i] = (int)(255-i);
                    if (i < 1) i = 1;
                    XNL[i] = (int)(Math.Log(i)/Math.Log(255)*255);
                }
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        int ResPF = XPF[OBitmap.GetPixel(i, j).G];
                        int ResNL = XNL[OBitmap.GetPixel(i, j).G];
                        NBitmap.SetPixel(i, j, Color.FromArgb(ResPF, ResPF, ResPF));
                        NBitmap1.SetPixel(i, j, Color.FromArgb(ResNL, ResNL, ResNL));
                    }
                }

                this.pictureBox3.Image = NBitmap;
                this.pictureBox4.Image = NBitmap1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }

    }
}
