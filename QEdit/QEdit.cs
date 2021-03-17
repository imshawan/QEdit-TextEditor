using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QEdit
{
    public partial class QEdit : Form
    {
        public string filePath = null;
        public string fileName = null;
        public string byteArray = null;
        public byte[] byteArrays;
        public string data = null;

        public QEdit()
        {
            InitializeComponent();
        }
        private void SaveAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (fileName == "" || fileName == null)
            {
                dialog.FileName = "Untitled Document.txt";
            }
            else
            {
                dialog.FileName = fileName;
            }

            dialog.Filter = "text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }

            var editorData = richTextBox1.Text;
            try
            {
                File.WriteAllText(filePath, editorData);
            }
            catch (Exception)
            {
                return;
            }
        }
        public void FindIn_MainTextBox(string text)
        {
            //Split a string to arrays
            string[] words = text.Split(',');
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
        public void exitButtonEvent(FormClosingEventArgs e)
        {
            string currentData = richTextBox1.Text;

            if (currentData != data)
            {
                DialogResult d = MessageBox.Show("You have unsaved document, do you want to save?", "Confirm",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (d == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    this.Activate(); 
                }
                else if (d == DialogResult.Yes)
                {
                    SaveAs();
                }
                else if (d == DialogResult.No) {
                    this.Close();
                }
            }
            else
            {
                DialogResult d = MessageBox.Show("Are you sure to exit QEdit?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)

                { this.Close(); }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                // Closed by calling Close()
                //exitButtonEvent(e);

            }

            else
            {
                // Closed by X or Alt+F4"
                exitButtonEvent(e);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void QEdit_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string currentData = richTextBox1.Text;

            if (currentData != data)
            {
                DialogResult d = MessageBox.Show("You have unsaved document, do you want to save?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.No)
                { this.Close(); }
                else if (d == DialogResult.Yes)
                {
                    SaveAs();
                }
            }
            else
            {
                DialogResult d = MessageBox.Show("Are you sure to exit QEdit?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (d == DialogResult.Yes)

                { this.Close(); }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ctrl + N New file function
            richTextBox1.Text = "";
            this.Text = "Untitled Document - QEdit Text Editor";
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            
            dialog.Filter = "text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = dialog.FileName;
            }
            if (filePath == "" || !File.Exists(filePath)) {
                return;
            }
            else
            {
                fileName = Path.GetFileName(filePath);
                this.Text = fileName + " - QEdit Text Editor";

                //byteArrays = File.ReadAllBytes(filePath);
                //data = Encoding.UTF8.GetString(byteArrays);
                data = File.ReadAllText(filePath);
                richTextBox1.Text = data;
            }
           
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePath == null || filePath == "")
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (fileName == "" || fileName == null)
                {
                    dialog.FileName = "Untitled Document.txt";
                }
                else
                {
                    dialog.FileName = fileName;
                }
                
                dialog.Filter = "text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = dialog.FileName;
                }
            }
            else
            {
                var editorData = richTextBox1.Text;
                File.WriteAllText(filePath, editorData);
            }
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QEdit newWin;
            newWin = new QEdit();
            newWin.Show();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e) => richTextBox1.Copy();

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e) => richTextBox1.Paste();

        private void inAmPMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string dateFormat = "dd-mm-yyyy";
            const string timeFormat = "HH:mm:ss tt";

            DateTime now = DateTime.Now;
            string dateTimeNow = now.ToString(dateFormat) + " " + now.ToString(timeFormat);
            richTextBox1.AppendText(dateTimeNow);
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e) => richTextBox1.Undo();

        private void redoToolStripMenuItem_Click(object sender, EventArgs e) => richTextBox1.Redo();

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e) => richTextBox1.Cut();

        private void copyToolStripMenuItem1_Click_1(object sender, EventArgs e) => richTextBox1.Copy();

        private void pasteToolStripMenuItem1_Click_1(object sender, EventArgs e) => richTextBox1.Paste();

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = richTextBox1.Text.Replace(richTextBox1.SelectedText, "");
            }
            catch (Exception)
            {
                MessageBox.Show("Null value to delete", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Replace R = new Forms.Replace();
            R.ShowDialog();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Find F = new Forms.Find();
            F.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = richTextBox1.Text.Replace(richTextBox1.SelectedText, "");
            }
            catch(Exception)
            {
                MessageBox.Show("Null value to delete", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            //fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            //fontDlg.ShowDialog();

            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.Font = fontDlg.Font;
                //richTextBox1.BackColor = fontDlg.Color;
                richTextBox1.ForeColor = fontDlg.Color;
            }
        }

        private void inHoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string dateFormat = "dd-mm-yyyy";
            const string timeFormat = "HH:mm:ss";

            DateTime now = DateTime.Now;
            string dateTimeNow = now.ToString(dateFormat) + " " + now.ToString(timeFormat);
            richTextBox1.AppendText(dateTimeNow);
        }

        private void QEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
           // e.Cancel = true;
        }

        private void aboutQEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.About about = new Forms.About();
            about.ShowDialog();
        }
    }
}
