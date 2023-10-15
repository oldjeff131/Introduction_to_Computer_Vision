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
        int x1, x2, y1, y2;
        int claX1, claX2, claY1, claY2;
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
            Image image;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog1.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = image;
            }
            try
            {
                int Height = this.pictureBox2.Image.Height;
                int width = this.pictureBox2.Image.Width;
                Bitmap NBitmap = new Bitmap(width, Height);
                Bitmap OBitmap = (Bitmap)this.pictureBox2.Image;
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

        private void button4_Click(object sender, EventArgs e)
        {
            Image image;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog1.FileName);
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox3.Image = image;
            }
            try
            {
                int Height = this.pictureBox3.Image.Height;
                int width = this.pictureBox3.Image.Width;
                Bitmap NBitmap = new Bitmap(width, Height);
                Bitmap OBitmap = (Bitmap)this.pictureBox3.Image;
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
                this.pictureBox3.Image = NBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double MINSAD = 999999999;
            double SAD, Image, Red, SADSUM;
            int PosX = 0, PosY = 0;
            int S_W = pictureBox1.Image.Width, S_H = pictureBox1.Image.Height; //抓取picture2的寬跟高
            Bitmap oldbitmap = (Bitmap)pictureBox1.Image;   //讀取picture2的圖案
            Bitmap Tmap = (Bitmap)pictureBox3.Image;        //讀取紅點模板的圖案
            int T_W = pictureBox3.Image.Width, T_H = pictureBox3.Image.Height;//抓取picture2的寬跟高
            for (int x = 0; x <= S_W - T_W; x += 5)
            {
                for (int y = 0; y <= S_H - T_H; y += 5)
                {
                    SAD = 0.0;
                    for (int i = 0; i < T_W; i++)
                    {
                        for (int j = 0; j < T_H; j++)
                        {
                            Image = oldbitmap.GetPixel(x + i, y + j).R;
                            Red = Tmap.GetPixel(i, j).R;
                            SADSUM = Image - Red;
                            SAD += Math.Abs(SADSUM);
                        }
                    }
                    if (MINSAD > SAD)
                    {
                        MINSAD = SAD;
                        PosX = x + (pictureBox3.Image.Width / 2);
                        PosY = y + (pictureBox3.Image.Height / 2);
                    }
                }
            }
            label6.Text = "(" + PosX + "," + PosY + ")";
            claX1 = PosX;claY1 = PosY;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double MINSAD = 999999999;
            double SAD, Image, Red, SADSUM;
            int PosX = 0, PosY = 0;
            int S_W = pictureBox2.Image.Width, S_H = pictureBox2.Image.Height; //抓取picture2的寬跟高
            Bitmap oldbitmap = (Bitmap)pictureBox2.Image;   //讀取picture2的圖案
            Bitmap Tmap = (Bitmap)pictureBox3.Image;        //讀取紅點模板的圖案
            int T_W = pictureBox3.Image.Width, T_H = pictureBox3.Image.Height;//抓取picture2的寬跟高
            for (int x = 0; x <= S_W - T_W; x += 5)
            {
                for (int y = 0; y <= S_H - T_H; y += 5)
                {
                    SAD = 0.0;
                    for (int i = 0; i < T_W; i++)
                    {
                        for (int j = 0; j < T_H; j++)
                        {
                            Image = oldbitmap.GetPixel(x + i, y + j).R;
                            Red = Tmap.GetPixel(i, j).R;
                            SADSUM = Image - Red;
                            SAD += Math.Abs(SADSUM);
                        }
                    }
                    if (MINSAD > SAD)
                    {
                        MINSAD = SAD;
                        PosX = x + (pictureBox3.Image.Width / 2);
                        PosY = y + (pictureBox3.Image.Height / 2);
                    }
                }
            }
            label7.Text = "(" + PosX + "," + PosY + ")";
            claX2 = PosX; claY2 = PosY;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int B;
            double ans,dx = 0.0, F = 12.07,ans2;
            dx = (x1 - x2)*0.00334;
            B = Convert.ToInt32(textBox1.Text);
            ans = (F * B/dx);
            dx = (claX1 - claX2) * 0.00334;
            ans2 = F * B / dx;
            label5.Text = Math.Round(ans,2) + "公分，"+Math.Round(ans2, 2) + "公分";

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null) 
            {
                x1 = (e.X) * 4;
                y1 = (e.Y) * 4;
                label1.Text = "(" + x1 + "," + y1 + ")";
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                x2 = (e.X) * 4;
                y2 = (e.Y) * 4;
                label2.Text = "(" + x2 + "," + y2 + ")";
            }
        }
    }
}
