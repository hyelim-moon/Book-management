using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Org.BouncyCastle.Crypto;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Form5 : Form
    {

        MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=book;Uid=root;Pwd=123456");
        private MySqlConnection bb_conn;
        public Form5()
        {
            InitializeComponent();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            bb_conn = DB_Connection.DB_conn();
            bb_conn.Open();

            DataSet ds = new DataSet();
            string book = "";
            book = "select * from book";
            MySqlCommand cmd = new MySqlCommand(book, bb_conn);

            MySqlDataAdapter table = new MySqlDataAdapter(cmd);
            table.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            bb_conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["BOOK_NAME"].Value.ToString();
                textBox3.Text = row.Cells["BOOK_ID"].Value.ToString();
                textBox4.Text = row.Cells["BOOK_AUTHOR"].Value.ToString();
                textBox5.Text = row.Cells["BOOK_YOP"].Value.ToString();
                textBox6.Text = row.Cells["BOOK_PUB"].Value.ToString();
                textBox7.Text = row.Cells["BOOK_PRICE"].Value.ToString();
                textBox8.Text = row.Cells["BOOK_APM"].Value.ToString();
                textBox9.Text = row.Cells["BOOK_SIGN"].Value.ToString();
                textBox10.Text = row.Cells["BOOK_CLASS"].Value.ToString();
                textBox11.Text = row.Cells["BOOK_COPY"].Value.ToString();
                textBox12.Text = row.Cells["BOOK_NUM"].Value.ToString();

                comboBox2.SelectedItem = row.Cells["BOOK_LOCAT"].Value.ToString();
                comboBox3.SelectedItem = row.Cells["BOOK_COC"].Value.ToString();
                comboBox4.SelectedItem = row.Cells["BOOK_STATE"].Value.ToString();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bb_conn.Open();
            DataSet ds = new DataSet();
            String search_combi = comboBox1.Text;
            String search_text = textBox1.Text;
            String select_stu = "";

            if (search_combi == "제목")
            {
                select_stu = "SELECT * FROM study_db.book where BOOK_NAME like '%" + search_text + "%'";
            }
            else if (search_combi == "저자")
            {
                select_stu = "SELECT * FROM study_db.book where BOOK_AUTHOR like '%" + search_text + "%'";
            }
            else if (search_combi == "출판사")
            {
                select_stu = "SELECT * FROM study_db.book where BOOK_PUB like '%" + search_text + "%'";
            }

            MySqlCommand cmd = new MySqlCommand(select_stu, bb_conn);
            MySqlDataAdapter table = new MySqlDataAdapter(cmd);
            table.Fill(ds);

            bb_conn.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                if (textBox3.Text == "")
                {
                    MessageBox.Show("도서정보를 입력해주세요.", "도서 정보");
                    return;
                }

                bb_conn.Open();
                string insertQuery = "INSERT INTO book(BOOK_ID, BOOK_NAME, BOOK_AUTHOR, BOOK_YOP, BOOK_PUB, BOOK_PRICE, BOOK_APM, BOOK_LOCAT, BOOK_COC, BOOK_STATE, BOOK_SIGN, BOOK_CLASS, BOOK_COPY, BOOK_NUM) VALUES ('" + textBox3.Text + "', '" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "' , '" + textBox8.Text + "' , '" + comboBox2.SelectedItem + "' , '" + comboBox3.SelectedItem + "' , '" + comboBox4.SelectedItem + "' , '" + textBox9.Text + "' , '" + textBox10.Text + "' , '" + textBox11.Text + "' ,  '" + textBox12.Text + "' ) ";
                MySqlCommand command = new MySqlCommand(insertQuery, bb_conn);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("추가되었습니다.");
                }
                else
                {
                    MessageBox.Show("재입력이 필요합니다.");
                }

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                comboBox2.Text = "상태입력";
                comboBox3.Text = "상태입력";
                comboBox4.Text = "상태입력";

                string select_book = "SELECT * FROM study_db.book";
                MySqlCommand book_cmd = new MySqlCommand(select_book, bb_conn);

                MySqlDataAdapter table = new MySqlDataAdapter(book_cmd);
                table.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch
            {
                MessageBox.Show("도서정보를 입력해주세요", "도서 정보", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            finally
            {
                bb_conn.Close();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataSet del = new DataSet();
            if (textBox3.Text == "")
            {
                MessageBox.Show("수정할 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }
            DialogResult dr = MessageBox.Show("수정하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.None);

            DataSet ds = new DataSet();
            string select_book = "UPDATE book SET BOOK_ID ='" + this.textBox3.Text + "', BOOK_NAME ='" + this.textBox2.Text + "', BOOK_AUTHOR = '" + this.textBox4.Text + "', BOOK_YOP ='" + this.textBox5.Text + "' ,BOOK_PUB ='" + this.textBox6.Text + "',BOOK_PRICE='" + this.textBox7.Text + "',BOOK_APM='" + this.textBox8.Text + "',BOOK_LOCAT='" + this.comboBox2.SelectedItem + "',BOOK_COC='" + this.comboBox3.SelectedItem + "',BOOK_STATE='" + this.comboBox4.SelectedItem + "',BOOK_SIGN='" + this.textBox9.Text + "',BOOK_CLASS='" + this.textBox10.Text + "',BOOK_COPY='" + this.textBox11.Text + "',BOOK_NUM='" + this.textBox12.Text + "' WHERE BOOK_ID ='" + this.textBox3.Text + "';";
            MySqlCommand book_cmd = new MySqlCommand(select_book, bb_conn);
            MySqlDataReader myreader;


            try
            {
                bb_conn.Open();
                if (dr == DialogResult.OK)
                {
                    myreader = book_cmd.ExecuteReader();
                    MessageBox.Show("수정되었습니다.");
                }
                else
                {
                    MessageBox.Show("취소되었습니다.");
                }

            }
            finally
            {
                bb_conn.Close();
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            comboBox2.Text = "상태입력";
            comboBox3.Text = "상태입력";
            comboBox4.Text = "상태입력";

            string update_book = "select * from book";
            MySqlCommand up_cmd = new MySqlCommand(update_book, bb_conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataSet del = new DataSet();
            if (textBox3.Text == "")
            {
                MessageBox.Show("삭제할 도서의 일련번호를 입력해주세요.", "", MessageBoxButtons.OK);
                return;
            }

            DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            DataSet ds = new DataSet();
            string deleteQuery = "DELETE from book where BOOK_ID = '" + textBox3.Text + "' ;";

            MySqlCommand sqlCommand = new MySqlCommand(deleteQuery, bb_conn);

            MySqlDataReader myreader;

            bb_conn.Open();

            try
            {
                if (dr == DialogResult.OK)
                {

                    myreader = sqlCommand.ExecuteReader();
                    MessageBox.Show("삭제되었습니다.");
                }
                else
                {
                    MessageBox.Show("취소되었습니다.");
                }
            }
            finally
            {
                bb_conn.Close();
            }

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            comboBox2.Text = "상태입력";
            comboBox3.Text = "상태입력";
            comboBox4.Text = "상태입력";

            string select_cus = "select * from book";
            MySqlCommand cus_cmd = new MySqlCommand(select_cus, bb_conn);

            MySqlDataAdapter table = new MySqlDataAdapter(cus_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

            comboBox2.Text = "상태입력";
            comboBox3.Text = "상태입력";
            comboBox4.Text = "상태입력";

            DataSet ds = new DataSet();

            string re_cus = "select * from book";
            MySqlCommand up_cmd = new MySqlCommand(re_cus, bb_conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";

            DataSet ds = new DataSet();

            string re_cus = "select * from book";
            MySqlCommand up_cmd = new MySqlCommand(re_cus, bb_conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7(this);
            form7.Show();
        }
    }
}
