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
                Bitmap NBitmap0 = new Bitmap(width, Height);
                Bitmap NBitmap1 = new Bitmap(width, Height);
                Bitmap NBitmap2 = new Bitmap(width, Height);
                Bitmap NBitmap3 = new Bitmap(width, Height);
                Bitmap NBitmap4 = new Bitmap(width, Height);
                Bitmap NBitmap5 = new Bitmap(width, Height);
                Bitmap NBitmap6 = new Bitmap(width, Height);
                Bitmap NBitmap7 = new Bitmap(width, Height);
                Bitmap NBitmap8 = new Bitmap(width, Height);
                Bitmap oldBitmap = (Bitmap)this.pictureBox1.Image;
                Color piexl;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        piexl = oldBitmap.GetPixel(x, y);
                        int r, g, b, res = 0,tmp;
                        r = piexl.R;
                        g = piexl.G;
                        b = piexl.B;
                        res = (299 * r + 587 * g + 114 * b) / 1000;
                        NBitmap.SetPixel(x, y, Color.FromArgb(res, res, res));

                        tmp = res & 0x01;
                        if (tmp != 0) 
                            NBitmap0.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap0.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0x02;
                        if (tmp != 0) 
                            NBitmap1.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap1.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0x04;
                        if (tmp != 0) 
                            NBitmap2.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap2.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0x08;
                        if (tmp != 0) 
                            NBitmap3.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap3.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0x10;
                        if (tmp != 0) 
                            NBitmap4.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap4.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0x20;
                        if (tmp != 0) 
                            NBitmap5.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap5.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0x40;
                        if (tmp != 0) 
                            NBitmap6.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap6.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0x80;
                        if (tmp != 0) 
                            NBitmap7.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        else 
                            NBitmap7.SetPixel(x, y, Color.FromArgb(0, 0, 0));

                        tmp = res & 0xF0;
                        NBitmap8.SetPixel(x, y, Color.FromArgb(tmp, tmp, tmp));


                    }
                }

                this.pictureBox2.Image = NBitmap;
                this.pictureBox3.Image = NBitmap0;
                this.pictureBox4.Image = NBitmap1;
                this.pictureBox5.Image = NBitmap2;
                this.pictureBox6.Image = NBitmap3;
                this.pictureBox7.Image = NBitmap4;
                this.pictureBox8.Image = NBitmap5;
                this.pictureBox9.Image = NBitmap6;
                this.pictureBox10.Image = NBitmap7;
                this.pictureBox11.Image = NBitmap8;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }
    }
}
