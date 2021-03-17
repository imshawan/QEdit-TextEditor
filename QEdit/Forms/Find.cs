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
    public partial class Find : Form
    {
        public RichTextBox richTextBox1 = Application.OpenForms["QEdit"].Controls["richTextBox1"] as RichTextBox;
        public Find()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Split a string to arrays
            string[] words = textBox4.Text.Split(',');
            foreach (string word in words)
            {
                int startIndex = 0;
                while (startIndex < richTextBox1.TextLength)
                {
                    //Find word & return index
                    int wordStartIndex = richTextBox1.Find(word, startIndex, RichTextBoxFinds.None);
                    if (wordStartIndex != -1)
                    {
                        //Highlight text in a richtextbox
                        richTextBox1.SelectionStart = wordStartIndex;
                        richTextBox1.SelectionLength = word.Length;
                        richTextBox1.SelectionBackColor = Color.Yellow;
                    }
                    else
                        break;
                    startIndex += wordStartIndex + word.Length;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }
    }
}
