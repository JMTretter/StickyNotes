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

namespace StickyNotes
{
    public partial class StickyNote : Form
    {
        public StickyNote()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void StickyNote_Load(object sender, EventArgs e)
        {
            string[] tabs = Directory.GetFiles("C:\\StickyNotes")
                .Select(Path.GetFileName).ToArray();
            if (tabs.Length > 0)
            {
                foreach (string tab in tabs)
                {
                    TabPage tp = new TabPage(tab);
                    tabControl1.TabPages.Add(tp);

                    TextBox tb = new TextBox();
                    tb.Dock = DockStyle.Fill;
                    tb.Multiline = true;

                    tp.Controls.Add(tb);

                    string text = " ";
                    try
                    {
                        text = File.ReadAllText("C:\\StickyNotes\\" + tab);
                    }
                    catch
                    {

                    }
                    if (!string.IsNullOrEmpty(text))
                    {
                        tb.Text = text;
                    }
                }

            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            TabPage tp = new TabPage("New Tab");
            tabControl1.TabPages.Add(tp);

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.Multiline = true;

            tp.Controls.Add(tb);

            //TabRenameDialog dialog = new TabRenameDialog();
            //dialog.Show();

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] tabs = Directory.GetFiles("C:\\StickyNotes", "*.txt")
                .Select(Path.GetFileName).ToArray();

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                string tabName = tabControl1.TabPages[i].Text;

                TextWriter txt = new StreamWriter("C:\\StickyNotes\\" + tabName);
                foreach (Control c in tabControl1.TabPages[i].Controls)
                {
                    txt.Write(c.Text);
                    txt.Close();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var showDialog = this.ShowDialog("Tab Name", "Rename the selected tab");
            tabControl1.SelectedTab.Text = showDialog;
        }

        public string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            return textBox.Text;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
