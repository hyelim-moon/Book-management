using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Org.BouncyCastle.Asn1.Cms;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=study_db;Uid=root;Pwd=123456");
        private MySqlConnection bb_Conn;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            bb_Conn = DB_Connection.DB_conn();
            bb_Conn.Open();

            DataSet ds1 = new DataSet();
            String customer1 = "";
            customer1 = "select * from book_rental";
            MySqlCommand cmd1 = new MySqlCommand(customer1, bb_Conn);

            MySqlDataAdapter table1 = new MySqlDataAdapter(cmd1);
            table1.Fill(ds1);

            dataGridView1.DataSource = ds1.Tables[0];
            bb_Conn.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["CUST_ID"].Value.ToString();
                textBox2.Text = row.Cells["CUST_NAME"].Value.ToString();
                textBox3.Text = row.Cells["BOOKRENTALNUM"].Value.ToString();
                textBox4.Text = row.Cells["BOOK_COUNT"].Value.ToString();
                textBox5.Text = row.Cells["BOOK_ID"].Value.ToString();
                textBox6.Text = row.Cells["BOOK_NAME"].Value.ToString();
                textBox7.Text = row.Cells["BOOK_LOCAT"].Value.ToString();
                textBox8.Text = row.Cells["BOOKRETURNDATE"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;

            DataSet ds = new DataSet();

            string re_cus = "select * from book_rental";
            MySqlCommand up_cmd = new MySqlCommand(re_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bb_Conn.Open();
            DataSet ds = new DataSet();
            String search_ID = textBox1.Text;
            String search_NAME = textBox2.Text;
            String select_stu = "";

            if (textBox1.Text != null || textBox2.Text != null)
            {
                select_stu = "SELECT * FROM mbi where CUST_ID like '%" + search_ID + "%'";
            }
            else
            {
                select_stu = "SELECT * FROM mbi where CUST_NAME like '%" + search_NAME + "%'";
            }



            MySqlCommand cmd = new MySqlCommand(select_stu, connection);
            MySqlDataAdapter table = new MySqlDataAdapter(cmd);
            table.Fill(ds);

            bb_Conn.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;

            DataSet ds = new DataSet();

            string re_cus = "select * from book_rental";
            MySqlCommand up_cmd = new MySqlCommand(re_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataSet deㅣ = new DataSet();
            if (textBox8.Text == "")
            {
                MessageBox.Show("연장할 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }
            DialogResult delmes = MessageBox.Show("연장하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            DataSet ds = new DataSet();

            DateTime dateTime = DateTime.Parse(textBox8.Text);
            dateTime = dateTime.AddDays(7);
            textBox8.Text = string.Empty;
            textBox8.AppendText(dateTime.ToString("yyyy-MM-dd"));

            string update_book = "UPDATE book_rental SET BOOKRETURNDATE = '" + this.textBox8.Text + "' WHERE BOOKRENTALNUM = '" + this.textBox3.Text + "';";
            MySqlCommand cus_cmd = new MySqlCommand(update_book, bb_Conn);
            MySqlDataReader myreader;

            try
            {
                bb_Conn.Open();
                myreader = cus_cmd.ExecuteReader();
                MessageBox.Show("반납 기간이 연장 되었습니다.");
            }
            finally
            {
                bb_Conn.Close();
            }

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;

            string update_cus = "SELECT * FROM book_rental";
            MySqlCommand up_cmd = new MySqlCommand(update_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataSet deㅣ = new DataSet();
            if (textBox5.Text == "")
            {
                MessageBox.Show("반납할 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }
            DialogResult delmes = MessageBox.Show("반납하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            DataSet ds = new DataSet();
            string deleteQuery = "DELETE FROM book_rental WHERE BOOKRENTALNUM = '" + this.textBox3.Text + "';";

            MySqlCommand sqlCommand = new MySqlCommand(deleteQuery, bb_Conn);

            MySqlDataReader myreader;

            bb_Conn.Open();
            try
            {
                

                if (delmes == DialogResult.OK)
                {
                    int bookcount = int.Parse(textBox4.Text);
                    bookcount -= 1;
                    string count = bookcount.ToString();
                    textBox4.Text = string.Empty;
                    textBox4.AppendText(count);

                    string book_count = "UPDATE mbi SET BOOK_COUNT = '" + this.textBox4.Text + "' WHERE CUST_ID = '" + this.textBox1.Text + "';";
                    MySqlCommand book_cmd = new MySqlCommand(book_count, bb_Conn);

                    MySqlDataAdapter table1 = new MySqlDataAdapter(book_cmd);
                    table1.Fill(ds);

                    string book_count1 = "UPDATE book_rental SET BOOK_COUNT = '" + this.textBox4.Text + "' WHERE CUST_ID = '" + this.textBox1.Text + "';";
                    MySqlCommand book_cmd1 = new MySqlCommand(book_count1, bb_Conn);

                    MySqlDataAdapter table2 = new MySqlDataAdapter(book_cmd1);
                    table2.Fill(ds);

                    myreader = sqlCommand.ExecuteReader();
                    MessageBox.Show("반납이 되었습니다.");  
                }
                else
                {
                    MessageBox.Show("취소되었습니다.");
                }
            }
            finally
            {
                bb_Conn.Close();
            }

            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            textBox7.Text = String.Empty;
            textBox8.Text = String.Empty;

            string select_cus = "SELECT * FROM book_rental ";
            MySqlCommand cus_cmd = new MySqlCommand(select_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(cus_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}