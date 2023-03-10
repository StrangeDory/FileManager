using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        public event EventHandler createFile;
        public event EventHandler deleteFile;
        public event EventHandler updateForm;
        public event EventHandler chanName;
        public event Insetr insertFile;

        #endregion

        #region Load

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
                GC.Collect();
                GC.WaitForPendingFinalizers();
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
                GC.Collect();
                GC.WaitForPendingFinalizers();
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
            if(dataGridView1[0, e.RowIndex].Value.Equals("[ ... ]"))
            {
                var text = this.Text.Split("\\");
                this.Text = string.Join("\\", text.SkipLast(1).ToArray());
                loadForm(this.Text);
                return;
            }
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
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            File.Delete(name);
                            MessageBox.Show("File " + name + " successfully deleted!");
                            loadForm(this.Text);
                        }
                        catch (Exception ex) { }

                }
                if (SelectedRowIndex != -1)
                {
                    name = this.Text + "\\" + dataGridView1[0, SelectedRowIndex].Value.ToString();
                    if (Directory.Exists(name))
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + dataGridView1[0, SelectedRowIndex].Value + " directory?", "Delete directory", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                            try
                            {
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                Directory.Delete(name, true);
                                MessageBox.Show("Directory " + name + " successfully deleted!");
                                loadForm(this.Text);
                            }
                            catch { }
                    }
                }
                SelectedRowIndex = -1;
            }
            else
                MessageBox.Show("Choose file or directory to delete!");
        }

        private void SelectRow(int index)
        {
            if (SelectedRowIndex != -1)
            {
                changeColor(SelectedRowIndex);
                if (isRename)
                {
                    dataGridView1.Rows[SelectedRowIndex].Cells[0].Value = OldName;
                    isRename = false;
                }
            }
            SelectedRowIndex = index;
            if (SelectedRowIndex != -1)
                dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Aqua;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectRow(e.RowIndex);
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

        private void createDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createDirectory(this.Text);
        }

        private void createFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_file(this.Text);
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(sender, e);
            loadForm(this.Text);
            deleteFile(sender, e);
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out Form1.POINT lpPoint);

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            Form1.POINT point;
            GetCursorPos(out point);
            var form = new Form1();
            point = form.getCursorInWindow(point);
            Point p = dataGridView1.PointToClient(new Point(point.X, point.Y));

            SelectedRowIndex = dataGridView1.HitTest(p.X, p.Y).RowIndex;
        }

        private string OldName = "";
        private bool isCopy = false;
        private bool isRename = false;

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[SelectedRowIndex].Cells[0].ReadOnly = false;
            dataGridView1.CurrentCell = dataGridView1.Rows[SelectedRowIndex].Cells[0];
            dataGridView1.Focus();
            dataGridView1.BeginEdit(true);
            OldName = dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString();
            dataGridView1.Rows[SelectedRowIndex].Cells[0].Value = "";
            isRename = true;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(isRename && (char.IsLetter(e.KeyChar) || char.IsDigit(e.KeyChar)))
                dataGridView1.Rows[SelectedRowIndex].Cells[0].Value += e.KeyChar.ToString();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isRename && (e.KeyCode == Keys.Enter))
            {
                if(dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.Equals(""))
                {
                    dataGridView1.Rows[SelectedRowIndex].Cells[0].Value = OldName;
                    return;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Directory.Exists(this.Text + "\\" + OldName))
                {
                    try
                    {
                        Directory.Move(this.Text + "\\" + OldName, this.Text + "\\" + dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString());
                    }
                    catch(Exception ex) { }
                } 
                else
                {
                    try
                    {
                        File.Move(this.Text + "\\" + OldName + dataGridView1.Rows[SelectedRowIndex].Cells[1].Value.ToString(), this.Text + "\\" + dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString() + dataGridView1.Rows[SelectedRowIndex].Cells[1].Value.ToString());
                    }
                    catch (Exception ex) { }
                }
                dataGridView1.Rows[SelectedRowIndex].Cells[0].ReadOnly = true;
                SelectedRowIndex = -1;
                loadForm(this.Text);
                isRename = false;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OldName = this.Text + "\\" + dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString();
            if (!Directory.Exists(OldName))
                OldName += dataGridView1.Rows[SelectedRowIndex].Cells[1].Value.ToString();
            insertToolStripMenuItem.Enabled = true;
            isCopy = true;
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            string fileName = OldName.Split("\\")[OldName.Split("\\").Length - 1];
            if (isCopy)
            {
                if (File.Exists(OldName))
                {
                    try
                    {
                        File.Copy(OldName, this.Text + "\\" + fileName);
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    Directory.CreateDirectory(this.Text + "\\" + fileName);
                    CopyFilesRecursively(OldName, this.Text + "\\" + fileName);
                }
            }
            else
            {
                
                if (Directory.Exists(OldName))
                {
                    try
                    {
                        Directory.Move(OldName, this.Text + "\\" + fileName);
                    }
                    catch (Exception ex) { }
                }
                else
                {
                    try
                    {
                        File.Move(OldName, this.Text + "\\" + fileName);
                    }
                    catch (Exception ex) { }
                }
            }
            insertToolStripMenuItem.Enabled = false;
            loadForm(this.Text);
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OldName = this.Text + "\\" + dataGridView1.Rows[SelectedRowIndex].Cells[0].Value.ToString();
            if (!Directory.Exists(OldName))
                OldName += dataGridView1.Rows[SelectedRowIndex].Cells[1].Value.ToString();
            insertToolStripMenuItem.Enabled = true;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help _hf = new Help();
            _hf.ShowDialog();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (SelectedRowIndex != -1)
            {
                changeColor(SelectedRowIndex);
                if (isRename)
                {
                    dataGridView1.Rows[SelectedRowIndex].Cells[0].Value = OldName;
                    isRename = false;
                }
            }
        }
    }

}
