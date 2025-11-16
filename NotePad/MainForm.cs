using System;
using System.IO;
using System.Windows.Forms;

namespace NotepadClone
{
    public partial class MainForm : Form
    {
        private string currentFilePath = null;
        private bool unsavedChanges = false;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Untitled - Notepad Clone";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            unsavedChanges = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PromptToSaveChanges())
            {
                textBox1.Clear();
                currentFilePath = null;
                unsavedChanges = false;
                this.Text = "Untitled - Notepad Clone";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PromptToSaveChanges())
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        currentFilePath = openFileDialog.FileName;
                        textBox1.Text = File.ReadAllText(currentFilePath);
                        unsavedChanges = false;
                        this.Text = Path.GetFileName(currentFilePath) + " - Notepad Clone";
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                File.WriteAllText(currentFilePath, textBox1.Text);
                unsavedChanges = false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    File.WriteAllText(currentFilePath, textBox1.Text);
                    unsavedChanges = false;
                    this.Text = Path.GetFileName(currentFilePath) + " - Notepad Clone";
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Notepad Clone v1.0", "About Notepad Clone", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!PromptToSaveChanges())
            {
                e.Cancel = true;
            }
        }

        private bool PromptToSaveChanges()
        {
            if (unsavedChanges)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(this, new EventArgs());
                    return !unsavedChanges; // return false if save was cancelled
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
