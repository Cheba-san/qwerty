using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace auto_salon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Form3 Win = new Form3();
            Win.TopLevel= false;
            Win.Dock= DockStyle.Fill;
            Win.FormBorderStyle= FormBorderStyle.None;  
            panel2.Controls.Add(Win);
            Win.BringToFront();
            Win.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Form4 Win = new Form4();
            Win.TopLevel = false;
            Win.Dock = DockStyle.Fill;
            Win.FormBorderStyle = FormBorderStyle.None;
            panel2.Controls.Add(Win);
            Win.BringToFront();
            Win.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Form5 Win = new Form5();
            Win.TopLevel = false;
            Win.Dock = DockStyle.Fill;
            Win.FormBorderStyle = FormBorderStyle.None;
            panel2.Controls.Add(Win);
            Win.BringToFront();
            Win.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit(); 
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WindowState= FormWindowState.Minimized;
        }
    }
}
