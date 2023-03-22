using GemBox.Spreadsheet.WinFormsUtilities;
using GemBox.Spreadsheet;
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
using System.Xml.Linq;


namespace auto_salon
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            getInfo();
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public void getInfo()
        {
            string query = ("SELECT id_sale as '№', id_auto as 'Авто', id_client 'Клиент' from sales;");
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlDataAdapter ada = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                ada.Fill(dt);
                dataGridView3.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            IDa.Text = dataGridView3[1, dataGridView3.CurrentRow.Index].Value.ToString();
            IDc.Text = dataGridView3[2, dataGridView3.CurrentRow.Index].Value.ToString();
        }

        private void but_ad_Click(object sender, EventArgs e)
        {
            string addwork = "insert into sales (id_auto, id_client) values('" + IDa.Text + "' , '" + IDc.Text + "');";
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlCommand cmDB = new MySqlCommand(addwork, conn);
            try
            {
                conn.Open();
                MySqlDataReader rd = cmDB.ExecuteReader();
                conn.Close();
                MessageBox.Show("Добавлено.");
                getInfo();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка.");
            }
        }

        private void but_upd_Click(object sender, EventArgs e)
        {
            string update = "update sales set id_auto = '" + IDa.Text + "', id_client = '" + IDc.Text + "' where id_sale = " + dataGridView3[0, dataGridView3.CurrentRow.Index].Value.ToString() + ";";
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlCommand cmDB = new MySqlCommand(update, conn);
            try
            {
                conn.Open();
                cmDB.ExecuteReader();
                conn.Close();
                MessageBox.Show("Данные обновлены");
                getInfo();
            }
            catch (Exception)
            {
                MessageBox.Show("Возникла непредвиденная ошибка.");
            }
        }

        private void but_del_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Уверены, что хотите удалить данные?", "Вы уверены?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                string del = "delete from sales where id_sale = " + dataGridView3[0, dataGridView3.CurrentRow.Index].Value.ToString() + ";";
                MySqlConnection conn = Class2.GetSqlConnection();
                MySqlCommand cmDB = new MySqlCommand(del, conn);
                try
                {
                    conn.Open();
                    cmDB.ExecuteReader();
                    conn.Close();
                    MessageBox.Show("Данные удалены");
                    getInfo();
                }
                catch (Exception)
                {
                    MessageBox.Show("Возникла непредвиденная ошибка.");
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog(); saveFileDialog.Filter =
            "XLS (*.xls)|*.xls|" + "XLT (*.xlt)|*.xlt|" +
            "XLSX (*.xlsx)|*.xlsx|" + "XLSM (*.xlsm)|*.xlsm|" +
            "XLTX (*.xltx)|*.xltx|" + "XLTM (*.xltm)|*.xltm|" +
            "ODS (*.ods)|*.ods|" + "OTS (*.ots)|*.ots|" +
            "CSV (*.csv)|*.csv|" + "TSV (*.tsv)|*.tsv|" +
            "HTML (*.html)|*.html|" + "MHTML (.mhtml)|*.mhtml|" +
            "PDF (*.pdf)|*.pdf|" + "XPS (*.xps)|*.xps|" +
            "BMP (*.bmp)|*.bmp|" + "GIF (*.gif)|*.gif|" +
            "JPEG (*.jpg)|*.jpg|" + "PNG (*.png)|*.png|" +
            "TIFF (*.tif)|*.tif|" + "WMP (*.wdp)|*.wdp";

            saveFileDialog.FilterIndex = 0;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var workbook = new ExcelFile(); var worksheet = workbook.Worksheets.Add("Sheet1");
                // From DataGridView to ExcelFile.
                DataGridViewConverter.ImportFromDataGridView(worksheet,
                this.dataGridView3, new ImportFromDataGridViewOptions() { ColumnHeaders = true });
                workbook.Save(saveFileDialog.FileName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
            {
                dataGridView3.CurrentCell = null;
                dataGridView3.Rows[i].Visible = false;

                dataGridView3.Rows[i].Visible = false;
                for (int c = 0; c < dataGridView3.Columns.Count; c++)
                {
                    if (dataGridView3[c, i].Value.ToString().Contains(search.Text))
                    {
                        dataGridView3.Rows[i].Visible = true;
                        break;
                    }
                }
            }
        }
    }
}
