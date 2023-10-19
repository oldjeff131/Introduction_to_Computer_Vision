using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int[] numa = new int[256];
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
            int PN = Height * Width;
            double pr_rk ;
            double[] eqsk = new double[256];
            double[] eqrk = new double[256];
            double[] pr_sk = new double[256];
            int[] sa = new int[256];
            Bitmap Histogrambitmap = new Bitmap(Width, Height);
            Bitmap oldbitmap = (Bitmap)this.pictureBox1.Image;
            double[] numa = new double[256];
            chart1.ChartAreas[0].AxisX.Maximum = 255;  //chart設定
            chart1.ChartAreas[0].AxisY.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisX.Maximum = 255;
            chart2.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.Maximum = 255;
            chart3.ChartAreas[0].AxisX.Minimum = 0;
            chart3.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart3.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.Title = "轉換後灰階";
            chart1.ChartAreas[0].AxisX.Title = "原始灰階";
            chart1.Titles.Add("直方圖等化轉換函數");
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
            double sum = 0;
            for (int k = 0; k < 256; k++)
            {
                double value = numa[k];
                double rate = value / (Height * Width * 1.0);
                numa[k] = rate;
                sum = sum+numa[k];
                eqrk[k] = 255*sum; //計算函數
                chart3.Series[0].Points.AddXY(k, numa[k]);//原始直方圖
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
                    int s = (int)Math.Round(255 * DSSum);
                    pr_sk[s]++;
                    Color newValue = Color.FromArgb(s, s, s);
                    Histogrambitmap.SetPixel(i, j, newValue);
                }
            }
            this.pictureBox2.Image = Histogrambitmap;
            sum = 0;
            for (int i = 0; i < 256; i++)
            {
                pr_rk = 0.0;
                for (int j = 0; j < i; j++) 
                {
                    pr_rk += numa[j];
                }
                sa[i]= (int)Math.Round(255 * pr_rk);
                pr_sk[i] /= PN;
                sum = sum + pr_sk[i];//等化後的函數
                eqsk[i] = 255 * sum;
            }
            for (int i = 0; i < 256; i++)
            { 
                chart2.Series[0].Points.AddXY(i, pr_sk[i]);//等化的直方圖
                chart1.Series[0].Points.AddXY(eqsk[i], eqrk[i]);//直方圖等化轉換函
            }         
        }
    }
}
