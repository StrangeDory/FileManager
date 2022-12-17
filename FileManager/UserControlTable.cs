using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FileManager
{
    public partial class UserControlTable : UserControl
    {
        public delegate void Insetr(List<string> s, string n);

        public UserControlTable()
        {
            InitializeComponent();
        }

        #region peremen

        public string value_dgv { get; set; }
        int SelectedRowIndex = -1;
        Thread _lth;
        public event EventHandler loedMUC2;
        public event EventHandler createFile;
        public event EventHandler deleteFile;
        public event EventHandler updateForm;
        public event EventHandler chanName;
        public event Insetr insertFile;
        List<string> rowNames = new List<string>();

        #endregion

        #region Load

        private void mainUserControl_Load(object sender, EventArgs e)
        {

        }

        internal void loadForm(string name)
        {

            this.Text = name;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Rows.Clear();


            if ((_lth != null) && _lth.IsAlive)
            {
                try
                {
                    _lth.IsBackground = true;
                    _lth.Start(this.Text);
                }
                catch { }
            }
            else
            {
                try
                {
                    _lth = new Thread(_loadDir);
                    _lth.IsBackground = true;
                    _lth.Start(this.Text);
                }
                catch { }
            }


            if (chanName != null)
                chanName(name, null);
        }

        internal void _loadDir(object _directory)
        {
            SelectedRowIndex = -1;
            try
            {
                string dir = (string)_directory;
                string[] _dirs = Directory.GetDirectories(dir);
                string[] _files = Directory.GetFiles(dir);

                int _current = 0;
                int _count = 1;
                //определяется количество файлов и каталогов, который будут загруженны на форму.
                if (_dirs.Length + _files.Length != 0)
                    _count = _dirs.Length + _files.Length;

                try
                {
                    if (!dir.Equals(Directory.GetDirectoryRoot(this.Text)))
                    {
                        printToDataGrid("[ ... ]", null, null);
                    }
                    foreach (string _dir in _dirs)
                    {
                        printToDataGrid(Path.GetFileName(_dir), "Папка с файлами", Directory.GetCreationTime(_dir));
                    }
                    foreach (string _f in _files)
                    {
                        printToDataGrid(Path.GetFileNameWithoutExtension(_f), Path.GetExtension(_f), File.GetCreationTime(_f));
                    }
                }
                catch (Exception _exc)
                {
                    MessageBox.Show(_exc.Message);
                }
            }
            catch
            {

            }

            if ((createFile != null) && (deleteFile != null) && (updateForm != null))
            {
                EventArgs e = null;
                createFile(_directory, e);
            }

        }

        private delegate void printDelegate(object name, object extension, object date);

        private void printToDataGrid(object name, object extension, object date)
        {
            //если ничего не изменяется то обращается снова в этот метод
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((printDelegate)printToDataGrid, name, extension, date);
                return;
            }
            int row = dataGridView1.Rows.Add(name, extension, date);
            changeColor(row);
        }

        private void changeColor(int row)
        {
            Color col;
            try
            {
                if (dataGridView1.Rows[row].Cells[0].Value.Equals("[ ... ]"))
                {
                    col = Color.BurlyWood;
                }
                else if (dataGridView1.Rows[row].Cells[1].Value.Equals("Папка с файлами"))
                {
                    col = Color.LemonChiffon;
                }
                else if (dataGridView1.Rows[row].Cells[1].Value.Equals(".txt"))
                {
                    col = Color.LightCyan;
                }
                else
                {
                    col = Color.Linen;
                }
                dataGridView1.Rows[row].DefaultCellStyle.BackColor = col;
            }
            catch { }
        }

        #endregion

        public void createDirectory(string dir)
        {
            string name = Path.GetRandomFileName().Replace(".", "");
            try
            {
                var dirInfo = Directory.CreateDirectory(dir + "\\" + name);
                printToDataGrid(name, "Папка с файлами", dirInfo.CreationTime);
                MessageBox.Show("Created directory '" + dir + "\\" + name + "'.");
            }
            catch (Exception e)
            {
                MessageBox.Show("It is impossible to create the current file. Exception:" + e);
            }
        }

        public void create_file(string dir)
        {
            string name = Path.GetRandomFileName().Replace(".", "");
            try
            {
                File.Create(dir + "\\" + name + ".txt");
                FileInfo fileInfo = new FileInfo(dir + "\\" + name + ".txt");
                printToDataGrid(name, ".txt", fileInfo.CreationTime);
                MessageBox.Show("Created file '" + dir + "\\" + name + ".txt' .");
            }
            catch (Exception e)
            {
                MessageBox.Show("It is impossible to create the current file. Exception:" + e);
            }
        }

        private bool isFile(int rowIndex)
        {
            return (!dataGridView1[1, rowIndex].Value.Equals("Папка с файлами")) ?  true : false;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string path = this.Text + "\\" + dataGridView1[0, e.RowIndex].Value.ToString() + dataGridView1[1, e.RowIndex].Value.ToString();
                if (File.Exists(path) && isFile(e.RowIndex))
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        UseShellExecute = true,
                        Verb = "Open",
                        FileName = path
                    };
                    try
                    {
                        p.Start();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                path = this.Text + "\\" + dataGridView1[0, e.RowIndex].Value.ToString();
                if (Directory.Exists(path))
                {
                    this.Text = path;
                    chanName(path, null);
                    loadForm(path);
                }
            }
        }

        public void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedRowIndex != -1)
            {
                string name = this.Text + "\\" + dataGridView1[0, SelectedRowIndex].Value + dataGridView1[1, SelectedRowIndex].Value;
                if (File.Exists(name) && isFile(SelectedRowIndex))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + dataGridView1[0, SelectedRowIndex].Value + " file?", "Delete file", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                        try
                        {
                            File.Delete(name);
                            MessageBox.Show("File " + name + " successfully deleted!");
                            loadForm(this.Text);
                        }
                        catch { }

                }
                name = this.Text + "\\" + dataGridView1[0, SelectedRowIndex].Value.ToString();
                if (Directory.Exists(name))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + dataGridView1[0, SelectedRowIndex].Value + " directory?", "Delete directory", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                        try
                        {
                            Directory.Delete(name, true);
                            MessageBox.Show("Directory " + name + " successfully deleted!");
                            loadForm(this.Text);
                        }
                        catch { }
                }
            }
            else
                MessageBox.Show("Choose file or directory to delete!");
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(SelectedRowIndex != 01)
                changeColor(SelectedRowIndex);
            SelectedRowIndex = e.RowIndex;
            if (SelectedRowIndex != -1)
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aqua;
        }

        public void userControl_Resize(int deltaHeight, int deltaWidth)
        {
            this.Height += deltaWidth;
            this.Width += deltaWidth;
            dataGridView1.Height  += deltaHeight;
            dataGridView1.Width += deltaWidth;
            Column_Name.Width += deltaWidth - (2*deltaWidth / 3);
            Column_Extension.Width += deltaWidth / 3;
            Column_Date.Width += deltaWidth / 3;
        }
    }

}
