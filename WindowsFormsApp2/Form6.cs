using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string userId = textBox1.Text;
            string userPassword = textBox2.Text;

            if (userId.Equals("km1") && userPassword.Equals("123456"))
            {
                MessageBox.Show("로그인에 성공했습니다.", "로그인");

            }
            else
            {
                MessageBox.Show("아이디나 비밀번호가 틀렸습니다. 다시 시도해주세요", "로그인");
            }

            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox1.Focus();

            if (userId.Equals("km1") && userPassword.Equals("123456"))
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.ShowDialog();
                this.Close();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Button1_Click(sender, e);
            }
        }

        private void CheckBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Button1_Click(sender, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = default(char);
            }
            else
            {
                textBox2.PasswordChar = '●';
            }
        }
    }
}