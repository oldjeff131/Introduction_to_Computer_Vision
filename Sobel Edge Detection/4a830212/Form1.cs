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
            try
            {
                int H = this.pictureBox1.Image.Height;
                int W = this.pictureBox1.Image.Width;
                Bitmap NBitmap = new Bitmap(W, H);
                int[,] sobelcache = new int[W, H];
                Bitmap OBitmap = (Bitmap)this.pictureBox1.Image;
                int[] pmask = new int[9];
                int Gt,Max = 0, Min = 10000; ;
                int[] maskx = new int[] {-1,0,1,-2,0,2,-1,0,1};//x軸陣列
                int[] masky = new int[] {-1,-2,-1,0,0,0,1,2,1};//y軸陣列
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
                        sobelcache[x, y] = Gt;//放入每個點的Gt                    
                        
                    }
                }
                for (int x = 1; x < W; x++)
                {
                    for (int y = 1; y < H; y++)
                    {
                        Gt = (sobelcache[x, y] - Min) * 255 / (Max - Min);//正規化
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int H = this.pictureBox1.Image.Height;
                int W = this.pictureBox1.Image.Width;
                Bitmap NBitmap = new Bitmap(W, H);
                Bitmap OBitmap = (Bitmap)this.pictureBox1.Image;
                int[] pmask = new int[9];
                int threshold = Convert.ToInt32(textBox1.Text);
                int Gt,Max=0,Min=10000;
                int[,] sobelcache = new int[W, H];
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
                        sobelcache[x, y] = Gt;//放入每個點的Gt  
                    }
                }
                for (int x = 1; x < W ; x++)
                {
                    for (int y = 1; y < H ; y++)
                    {
                        Gt = (sobelcache[x,y] - Min) * 255 / (Max - Min);//正規化
                        if (Gt >= threshold)//判斷是否有超過門檻
                        {
                            NBitmap.SetPixel(x, y, Color.FromArgb(Gt, Gt, Gt));
                        }
                        else 
                        {
                            NBitmap.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                        }
                        
                    }
                }

                this.pictureBox3.Image = NBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤提示");
            }
        }
    }
   }
