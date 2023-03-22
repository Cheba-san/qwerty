using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_salon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string querry = "SELECT id_user FROM users WHERE login ='" + log.Text + "'and pass='" + pass.Text + "';";
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlCommand cmDB = new MySqlCommand(querry, conn);
            try
            {
                conn.Open();
                int result = 0;
                result = Convert.ToInt32(cmDB.ExecuteScalar());
                if (result > 0)
                {
                    Form2 Win = new Form2();
                    Win.Owner = this;
                    this.Hide();
                    Win.Show();
                    log.Clear();
                    pass.Clear();
                }
                else
                    MessageBox.Show("Возникла ошибка при входе.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла непредвиденная ошибка." + Environment.NewLine + ex.Message);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
