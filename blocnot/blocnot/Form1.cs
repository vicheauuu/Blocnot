using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace blocnot
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            MessageBoxManager.Abort = "Save";
            MessageBoxManager.Retry = "Don't Save";
            MessageBoxManager.Ignore = "Cancel";
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "New Text Document";
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|Bat (*.bat)|*.bat|All files (*.*)|*.*"; saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;


                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        using (TextWriter txt = new StreamWriter(filePath))
                        {
                            txt.Write(textBox1.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"A apărut o eroare la salvarea fișierului: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string ChosenFile = "";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = ".txt (*.txt)|*.txt|ALL Files(*.*)|*.*";
            ChosenFile = openFileDialog1.FileName;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form Form = new Form();
            if (textBox1.Text != "")
            {
                MessageBoxManager.Register();
                var selectedOption = MessageBox.Show("Do you want to save changes?", "Blocnot", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);
                if (selectedOption == DialogResult.Abort)
                {
                    saveToolStripMenuItem_Click(sender, e);
                }
                else if (selectedOption == DialogResult.Retry)
                {
                    Form.Close();
                }
                else if (selectedOption == DialogResult.Ignore)
                {
                    e.Cancel = true;
                }
            }
                else { Form.Close(); }
            MessageBoxManager.Unregister();
        }
    }
}
