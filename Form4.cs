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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            getInfo();
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public void getInfo()
        {
            string query = ("SELECT id_client as '№', fio as 'ФИО Клиента', number as 'Номер Телефона' from clients;");
            MySqlConnection conn = Class2.GetSqlConnection();
            MySqlDataAdapter ada = new MySqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                ada.Fill(dt);
                dataGridView2.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            fio.Text = dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString();
            number.Text = dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString();
        }

        private void but_ad_Click(object sender, EventArgs e)
        {
            string addwork = "insert into clients (fio, number) values('" + fio.Text + "' , '" + number.Text + "');";
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
            string update = "update clients set fio = '" + fio.Text + "', number = '" + number.Text + "' where id_client = " + dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + ";";
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
                string del = "delete from clients where id_client = " + dataGridView2[0, dataGridView2.CurrentRow.Index].Value.ToString() + ";";
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
                this.dataGridView2, new ImportFromDataGridViewOptions() { ColumnHeaders = true });
                workbook.Save(saveFileDialog.FileName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                dataGridView2.CurrentCell = null;
                dataGridView2.Rows[i].Visible = false;

                dataGridView2.Rows[i].Visible = false;
                for (int c = 0; c < dataGridView2.Columns.Count; c++)
                {
                    if (dataGridView2[c, i].Value.ToString().Contains(search.Text))
                    {
                        dataGridView2.Rows[i].Visible = true;
                        break;
                    }
                }
            }
        }
    }
}
