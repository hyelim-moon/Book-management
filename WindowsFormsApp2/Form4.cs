using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace WindowsFormsApp2
{
    public partial class Form4 : Form
    {
        MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=study_db;Uid=root;Pwd=123456");

        private MySqlConnection bb_Conn;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            bb_Conn = DB_Connection.DB_conn();
            bb_Conn.Open();


            DataSet ds = new DataSet();
            String customer = "";
            customer = "select * from mbi";
            MySqlCommand cmd = new MySqlCommand(customer, bb_Conn);


            MySqlDataAdapter table = new MySqlDataAdapter(cmd);
            table.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
            bb_Conn.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["CUST_ID"].Value.ToString();
                textBox3.Text = row.Cells["CUST_NAME"].Value.ToString();
                textBox4.Text = row.Cells["CUST_PHONE"].Value.ToString();
                textBox5.Text = row.Cells["CUST_ADDRESS"].Value.ToString();
                textBox6.Text = row.Cells["BOOK_COUNT"].Value.ToString();
                textBox7.Text = row.Cells["MEMO"].Value.ToString();
                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bb_Conn.Open();
            DataSet ds = new DataSet();
            String search_combi = comboBox1.Text;
            String search_text = textBox1.Text;
            String select_stu = "";

            if (search_combi == "회원번호")
            {
                select_stu = "SELECT * FROM mbi where CUST_ID like '%" + search_text + "%'";
            }
            else if (search_combi == "이름")
            {
                select_stu = "SELECT * FROM mbi where CUST_NAME like '%" + search_text + "%'";
            }

            MySqlCommand cmd = new MySqlCommand(select_stu, connection);
            MySqlDataAdapter table = new MySqlDataAdapter(cmd);
            table.Fill(ds);

            bb_Conn.Close();
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;

            DataSet ds = new DataSet();

            string re_cus = "select * from mbi";
            MySqlCommand up_cmd = new MySqlCommand(re_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (textBox2.Text == "")
            {
                MessageBox.Show("추가할 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }
            DialogResult da = MessageBox.Show("추가하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.None);

            bb_Conn.Open();
            string insertQuery = "INSERT INTO mbi(CUST_ID, CUST_NAME, CUST_PHONE, CUST_ADDRESS, BOOK_COUNT, MEMO) VALUES('" + textBox2.Text + "', '" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "', '" + textBox6.Text + "','" + textBox7.Text + "' )";

            MySqlCommand command = new MySqlCommand(insertQuery, bb_Conn);


            try
            {

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("추가되었습니다.");
                }
                else
                {
                    MessageBox.Show("재입력이 필요합니다.");
                }
            }
            finally { bb_Conn.Close(); }
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;


            string select_cus = "select * from mbi";
            MySqlCommand cus_cmd = new MySqlCommand(select_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(cus_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DataSet del = new DataSet();
            if (textBox2.Text == "")
            {
                MessageBox.Show("수정할 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }
            DialogResult dr = MessageBox.Show("수정하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.None);

            DataSet ds = new DataSet();
            string select_cus = "UPDATE mbi SET CUST_ID ='" + this.textBox2.Text + "', CUST_NAME ='" + this.textBox3.Text + "', CUST_PHONE = '" + this.textBox4.Text + "', CUST_ADDRESS ='" + this.textBox5.Text + "' , BOOK_COUNT ='" + this.textBox6.Text + "', MEMO ='" + this.textBox7.Text + "' WHERE CUST_NAME ='" + this.textBox3.Text + "';";
            MySqlCommand cus_cmd = new MySqlCommand(select_cus, bb_Conn);
            MySqlDataReader myreader;


            try
            {
                bb_Conn.Open();
                if (dr == DialogResult.OK)
                {
                    myreader = cus_cmd.ExecuteReader();
                    MessageBox.Show("수정되었습니다.");
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

            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;

            string update_cus = "select * from mbi";
            MySqlCommand up_cmd = new MySqlCommand(update_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(up_cmd);
            table.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataSet del = new DataSet();
            if (textBox2.Text == "")
            {
                MessageBox.Show("삭제할 정보가 없습니다.", "", MessageBoxButtons.OK);
                return;
            }

            DialogResult dr = MessageBox.Show("삭제하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            DataSet ds = new DataSet();
            string deleteQuery = "DELETE from mbi where CUST_ID = '" + textBox2.Text + "' ;";

            MySqlCommand sqlCommand = new MySqlCommand(deleteQuery, bb_Conn);

            MySqlDataReader myreader;

            bb_Conn.Open();

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
                bb_Conn.Close();
            }
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;

            string select_cus = "select * from mbi";
            MySqlCommand cus_cmd = new MySqlCommand(select_cus, bb_Conn);

            MySqlDataAdapter table = new MySqlDataAdapter(cus_cmd);
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
    }
}
