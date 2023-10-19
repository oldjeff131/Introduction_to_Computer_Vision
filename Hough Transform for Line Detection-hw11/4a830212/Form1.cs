using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            try
            {
                int H = this.pictureBox1.Image.Height;
                int W = this.pictureBox1.Image.Width;
                Bitmap NBitmap = new Bitmap(W, H);
                int[,] sobelcache = new int[W, H];
                Bitmap OBitmap = (Bitmap)this.pictureBox1.Image;
                int[] pmask = new int[9];
                int Gt, Max = 0, Min = 10000; 

                int[] maskx = new int[] { -1, 0, 1, -2, 0, 2, -1, 0, 1 };//x軸陣列
                int[] masky = new int[] { -1, -2, -1, 0, 0, 0, 1, 2, 1 };//y軸陣列
                for (int x = 1; x < W - 1; x++)
                {
                    for (int y = 1; y < H - 1; y++)
                    {
                        int Gx = 0, Gy = 0;
                        pmask[0] = OBitmap.GetPixel(x - 1, y - 1).G;
                        pmask[1] = OBitmap.GetPixel(x, y - 1).G;
                        pmask[2] = OBitmap.GetPixel(x + 1, y - 1).G;
                        pmask[3] = OBitmap.GetPixel(x - 1, y).G;
                        pmask[4] = OBitmap.GetPixel(x, y).G;
                        pmask[5] = OBitmap.GetPixel(x + 1, y).G;
                        pmask[6] = OBitmap.GetPixel(x - 1, y + 1).G;
                        pmask[7] = OBitmap.GetPixel(x, y + 1).G;
                        pmask[8] = OBitmap.GetPixel(x + 1, y + 1).G;
                        for (int i = 0; i < 9; i++)
                        {
                            Gx += pmask[i] * maskx[i];
                            Gy += pmask[i] * masky[i];
                        }
                        Gt = (int)(Math.Abs(Gx) + Math.Abs(Gy));
                        if (Gt < Min) { Min = Gt; }
                        if (Gt > Max) { Max = Gt; }
                        sobelcache[x, y] = Gt;
                    }
                }
                for (int x = 1; x < W; x++)
                {
                    for (int y = 1; y < H; y++)
                    {
                        Gt = (sobelcache[x, y] - Min) * 255 / (Max - Min);//正規化
                        Gt = Gt <= 110 ? Gt = 0 : Gt = 255;
                        NBitmap.SetPixel(x, y, Color.FromArgb(Gt, Gt, Gt));
                    }
                }
                this.pictureBox2.Image = NBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤提示");
            }
        }

        struct XYPoint
        {
            public short X, Y;
        };
        struct LineParameters
        {
            public int Angle, Distance;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int H = this.pictureBox1.Image.Height;
                int W = this.pictureBox1.Image.Width;
                Bitmap oldBitmap = (Bitmap)this.pictureBox2.Image;
                int EdgeNum = 0;
                XYPoint[] EdgePoint = new XYPoint[W * H];
                LineParameters[] Line = new LineParameters[W * H];
                for (short x = 0; x < W; x++)
                {
                    for (short y = 0; y < H; y++)
                    {
                        if (oldBitmap.GetPixel(x, y).G == 255)
                        {
                            EdgePoint[EdgeNum].X = x;
                            EdgePoint[EdgeNum].Y = y;
                            EdgeNum++;
                        }
                    }
                }
                int AN = 360;
                int DN = (int)Math.Sqrt(W * W + H + H) * 2;
                int Threshold = (int)Math.Min(W, H) / 5;
                int HSMax = 0;
                Bitmap newBitamp = new Bitmap(AN, DN);
                int pixH = 0,LC;
                double DA, DD;
                double MaxDis, MinDis;
                double Angle, Dis;
                int[,] HS = new int[AN, DN];
                MaxDis = Math.Sqrt(W * W + H * H);
                MinDis = (double)(-W);
                DA = Math.PI / AN;
                DD = (MaxDis - MinDis) / DN;
                StreamWriter sw = new StreamWriter("result.txt", true, Encoding.ASCII);
                //Hought Space
                for (int i = 0; i < AN; i++)
                {
                    for (int j = 0; j < DN; j++)
                    {
                        HS[i, j] = 0;
                    }
                }
                for (int i = 0; i < EdgeNum; i++)
                {
                    for (int j = 0; j < AN; j++)
                    {
                        Angle = j * DA;
                        Dis = EdgePoint[i].X * Math.Cos(Angle) + EdgePoint[i].Y * Math.Sin(Angle);
                        HS[j, (int)((Dis - MinDis) / DD)]++;
                    }
                }
               LC = 0;
                for (int i = 0; i < AN; i++)
                {
                    for (int j = 0; j < DN; j++)
                    {
                        if (HS[i, j] > HSMax) HSMax = HS[i, j];
                        if (HS[i, j] >= Threshold)
                        {
                            Line[LC].Angle = i;
                            Line[LC].Distance = j;
                           LC++;
                        }
                    }             
                }
                int[] xfer = new int[256];
                double gamma = 0.4, pow255;
                pow255 = Math.Pow(255.0, gamma);
                for (int i = 0; i < 256; i++)
                    xfer[i] = (int)(Math.Pow((double)i, gamma) / pow255 * 255 + 0.5);
                for (int x = 0; x < AN; x++)
                {
                    for (int y = 0; y < DN; y++)
                    {
                        pixH = 255 - ((HSMax) - HS[x, y]) * 255 / HSMax;
                        newBitamp.SetPixel(x, y, Color.FromArgb(xfer[pixH], xfer[pixH], xfer[pixH]));

                    }
                }
                this.pictureBox3.Image = newBitamp;

                for (int i = 0; i <LC & i < W * H; i+=5)//畫線
                {
                    for (int x = 0; x < W; x++)
                    {
                        int y = (int)((Line[i].Distance * DD + MinDis - x * Math.Cos(Line[i].Angle * DA)) / Math.Sin(Line[i].Angle * DA));
                        if (y >= 0 & y < H)
                        {
                            sw.Write("y-" + Line[i].Angle + "x-" + Line[i].Distance + "\n");
                            pixH = oldBitmap.GetPixel(x, y).G;
                            oldBitmap.SetPixel(x, y, Color.FromArgb(pixH ^ 255, pixH, pixH));

                        }
                    }
                    for (int y = 0; y < H; y++)
                    {
                        int x = (int)((Line[i].Distance * DD + MinDis - y * Math.Sin(Line[i].Angle * DA)) / Math.Cos(Line[i].Angle * DA));
                        if (x >= 0 & x < W)
                        {
                            sw.Write("y-" + Line[i].Angle + "x-" + Line[i].Distance + "\n");
                            pixH = oldBitmap.GetPixel(x, y).G;
                            oldBitmap.SetPixel(x, y, Color.FromArgb(pixH ^ 255, pixH, pixH));
                        }
                    }
                }
                this.pictureBox2.Image = oldBitmap;
                MessageBox.Show("Hough transform完成");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog SFD1 = new SaveFileDialog();
                SFD1.Filter = "Bitmap Image|*.bmp";
                SFD1.Title = "儲存圖片";
                SFD1.ShowDialog();
                if (SFD1.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)SFD1.OpenFile();
                    switch (SFD1.FilterIndex)
                    {
                        case 1:
                            this.pictureBox3.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "訊息提示");
            }
        }
    }
}
