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
    public partial class Form7 : Form
    {
        Form5 f5;

        public Form7(Form5 form)
        {
            InitializeComponent();
            f5 = form;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            label1.Text = treeView1.SelectedNode.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f5.textBox10.Text = this.label1.Text.ToString();
            this.Close();
        }


        private void Form7_Load(object sender, EventArgs e)
        {

        }
    }
}
