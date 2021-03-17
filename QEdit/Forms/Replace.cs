using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QEdit.Forms
{
    public partial class Replace : Form
    {
        public Replace()
        {
            InitializeComponent();
        }

        private void FindAndReplace_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox1 = Application.OpenForms["QEdit"].Controls["richTextBox1"] as RichTextBox;
            richTextBox1.Text = richTextBox1.Text.Replace(textBox4.Text, textBox3.Text);
        }
    }
}
