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
using System.Net;
using MySqlX.XDevAPI.Relational;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {   
        MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=study_db;Uid=root;Pwd=123456");
        private MySqlConnection bb_Conn;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            bb_Conn = DB_Connection.DB_conn();
            bb_Conn.Open();

            DataSet ds1 = new DataSet();
            String customer1 = "";
            customer1 = "select * from mbi";
            MySqlCommand cmd1 = new MySqlCommand(customer1, bb_Conn);

            MySqlDataAdapter table1 = new MySqlDataAdapter(cmd1);
            table1.Fill(ds1);

            dataGridView1.DataSource = ds1.Tables[0];
            bb_Conn.Close();

            bb_Conn = DB_Connection.DB_conn();
            bb_Conn.Open();

            DataSet ds2 = new DataSet();
            String customer2 = "";
            customer2 = "select * from book";
            MySqlCommand cmd2 = new MySqlCommand(customer2, bb_Conn);

            MySqlDataAdapter table2 = new MySqlDataAdapter(cmd2);
            table2.Fill(ds2);

            dataGridView2.DataSource = ds2.Tables[0];
            bb_Conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["CUST_ID"].Value.ToString();
                textBox3.Text = row.Cells["CUST_NAME"].Value.ToString();
                textBox4.Text = row.Cells["BOOK_COUNT"].Value.ToString();

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

                textBox5.Text = row.Cells["BOOK_NUM"].Value.ToString();
                textBox6.Text = row.Cells["BOOK_NAME"].Value.ToString();
                textBox7.Text = row.Cells["BOOK_AUTHOR"].Value.ToString();
                textBox8.Text = row.Cells["BOOK_PUB"].Value.ToString();
                textBox9.Text = row.Cells["BOOK_LOCAT"].Value.ToString();

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bb_Conn.Open();
            DataSet ds2 = new DataSet();
            String search_combi = ComboBox1.Text;
            String search_text = textBox1.Text;
            String select_stu = "";

            if (search_combi == "제목")
            {
                select_stu = "SELECT * FROM book where BOOK_NAME like '%" + search_text + "%'";
            }
            else if (search_combi == "저자")
            {
                select_stu = "SELECT * FROM book where BOOK_AUTHOR like '%" + search_text + "%'";
            }
            else if (search_combi == "출판사")
            {
                select_stu = "SELECT * FROM book where BOOK_PUB like '%" + search_text + "%'";
            }

            MySqlCommand cmd2 = new MySqlCommand(select_stu, connection);
            MySqlDataAdapter table2 = new MySqlDataAdapter(cmd2);
            table2.Fill(ds2);

            bb_Conn.Close();
            dataGridView2.DataSource = ds2.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;

            DataSet ds = new DataSet();

            string re_cus = "select * from mbi";
            MySqlCommand up_cmd = new MySqlCommand(re_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bb_Conn.Open();
            DataSet ds = new DataSet();
            String search_ID = textBox2.Text;
            String search_NAME = textBox3.Text;
            String select_stu = "";

            if (textBox2.Text != null || textBox3.Text != null)
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

        private void button5_Click(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;

            DataSet ds2 = new DataSet();

            string re_cus2 = "select * from book";
            MySqlCommand up_cmd2 = new MySqlCommand(re_cus2, bb_Conn);

            MySqlDataAdapter table2 = new MySqlDataAdapter(up_cmd2);
            table2.Fill(ds2);
            dataGridView2.DataSource = ds2.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();

            if (textBox2.Text == "")
            {
                MessageBox.Show("회원 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }
            if (textBox5.Text == "")
            {
                MessageBox.Show("대출할 책의 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }
            DialogResult dr = MessageBox.Show("대출하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.None);

            
            MySqlCommand count_cmd = new MySqlCommand("SELECT COUNT(*) FROM book_rental WHERE CUST_ID = '" + this.textBox2.Text + "';", bb_Conn);


            if (int.Parse(textBox4.Text) < 5)
            {
                bb_Conn.Open();
                textBox4.Text = string.Empty;
                int totalCount = Convert.ToInt32(count_cmd.ExecuteScalar());
                totalCount += 1;
                textBox4.AppendText(totalCount.ToString());
                bb_Conn.Close();
                
                DataSet ds2 = new DataSet();
                DateTime bookrental = DateTime.Now.Date;
                DateTime bookreturn = bookrental.AddDays(14);
                textBox10.AppendText(bookreturn.ToString("yyyy-MM-dd"));

                string insert_book = "INSERT INTO book_rental(CUST_ID, CUST_NAME, BOOK_COUNT, BOOK_ID, BOOK_NAME, BOOK_AUTHOR, BOOK_PUB, BOOK_LOCAT, BOOKRENTALDATE, BOOKRETURNDATE) VALUES('" + this.textBox2.Text + "', '" + this.textBox3.Text + "', '" + this.textBox4.Text + "', '" + this.textBox5.Text + "',  '" + this.textBox6.Text + "', '" + this.textBox7.Text + "', '" + this.textBox8.Text + "', '" + this.textBox9.Text + "', '" + this.dateTimePicker1.Text + "', '" + this.textBox10.Text + "')";

                MySqlCommand cus_cmd = new MySqlCommand(insert_book, bb_Conn);

                MySqlDataReader myreader;

                try
                {
                    bb_Conn.Open();

                    string booknum = "SELECT BOOK_ID FROM book_rental";
                    MySqlCommand num_cmd = new MySqlCommand(booknum, bb_Conn);
                    //num_cmd.ExecuteReader();

                    if (booknum == textBox5.Text)
                    {
                        MessageBox.Show("대여중인 책입니다");

                        textBox5.Text = string.Empty;
                        textBox6.Text = string.Empty;
                        textBox7.Text = string.Empty;
                        textBox8.Text = string.Empty;
                        textBox9.Text = string.Empty;
                        textBox10.Text = string.Empty;
                    }
                    else
                    { 
                        if (dr == DialogResult.OK)
                        {
                            string count = "UPDATE mbi SET BOOK_COUNT = '" + this.textBox4.Text + "' WHERE CUST_ID = '" + this.textBox2.Text + "';";
                            MySqlCommand book_cmd = new MySqlCommand(count, bb_Conn);
                            book_cmd.ExecuteNonQuery();

                            string count2 = "UPDATE book_rental SET BOOK_COUNT = '" + this.textBox4.Text + "' WHERE CUST_ID = '" + this.textBox2.Text + "';";
                            MySqlCommand book_cmd2 = new MySqlCommand(count2, bb_Conn);
                            book_cmd2.ExecuteNonQuery();

                            //string bookstate = "UPDATE book SET BOOK_STATE = '대출중' WHERE = BOOK_NUM'" + textBox5.Text + "';";
                            //MySqlCommand state_cmd = new MySqlCommand(bookstate, bb_Conn);
                            //state_cmd.ExecuteReader();

                            myreader = cus_cmd.ExecuteReader();
                            MessageBox.Show("대출되었습니다.");
                        }
                        else
                        {
                            MessageBox.Show("취소되었습니다.");
                        }
                        
                    }
                    

                }
                finally
                {
                    bb_Conn.Close();
                }

               

                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                textBox6.Text = string.Empty;
                textBox7.Text = string.Empty;
                textBox8.Text = string.Empty;
                textBox9.Text = string.Empty;
                textBox10.Text = string.Empty;

                string update_cus = "select * from mbi";
                MySqlCommand up_cmd = new MySqlCommand(update_cus, bb_Conn);

                MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
                table.Fill(ds2);
                dataGridView1.DataSource = ds2.Tables[0];

                
            }
            else
            {
                MessageBox.Show(" 더이상 대출이 불가합니다.");

                textBox2.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                textBox6.Text = string.Empty;
                textBox7.Text = string.Empty;
                textBox8.Text = string.Empty;
                textBox9.Text = string.Empty;
                textBox10.Text = string.Empty;
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Button1_Click(sender, e);
            }
        }

    }
}
