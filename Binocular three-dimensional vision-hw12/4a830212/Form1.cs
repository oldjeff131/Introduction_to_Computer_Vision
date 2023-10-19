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
        public int x1, x2, y1, y2;
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
            Image image;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog1.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = image;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int B;
            double ans,dx = 0.0, F = 12.07;
            dx = (x1 - x2)*0.00334;
            B = Convert.ToInt32(textBox1.Text);
            ans = (F * B/dx);
            label5.Text = Math.Round(ans,2) + "公分";

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
