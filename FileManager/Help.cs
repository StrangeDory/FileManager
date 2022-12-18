using System;
using System.IO;
using System.Windows.Forms;

namespace FileManager
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("help.txt");
            string line;
            label1.Text = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                label1.Text = label1.Text + line + "\n";
            }
        }
    }
}
