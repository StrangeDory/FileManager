using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace FileManager
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer dateTimeTimer = new System.Timers.Timer();
        private int width;
        private int height;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            makeTreeView();
            fullPathLabel.Text = Button_Disk.DropDownItems[0].Text;
            userControlTable1.loadForm(fullPathLabel.Text);
            dateTimeTimer.Elapsed += new ElapsedEventHandler(Tm_Tick);
            dateTimeTimer.Interval = 1000;
            dateTimeTimer.Enabled = true;
            width = this.ClientSize.Width;
            height = this.ClientSize.Height;
        }

        void Tm_Tick(object sender, ElapsedEventArgs e)
        {
            dateTime.Text = DateTime.Now.ToString();
        }

        public void makeTreeView()
        {
            treeView1.Nodes.Clear();
            foreach (var myDrives in DriveInfo.GetDrives())
            {
                if (myDrives.IsReady)  
                {
                    Button_Disk.DropDownItems.Add(myDrives.Name);
                    LoadDirectory(myDrives.Name);
                }
            }
        }

        public void LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            TreeNode tds = treeView1.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadSubDirectories(0, Dir, tds);
        }

        private void LoadSubDirectories(int level, string dir, TreeNode td)
        {
            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            level++;
            foreach (string subdirectory in subdirectoryEntries)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(subdirectory);
                    TreeNode tds = td.Nodes.Add(di.Name);
                    tds.StateImageIndex = 0;
                    tds.Tag = di.FullName;
                    if (level < 4)
                        LoadSubDirectories(level, subdirectory, tds);
                }
                catch (Exception) { }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode CurrentNode = e.Node;
            string fullpath = CurrentNode.FullPath;
            fullPathLabel.Text = fullpath;
            int countDir = Directory.GetDirectories(fullpath).Length;
            int countFiles = Directory.GetFiles(fullpath).Length;
            userControlTable1.loadForm(fullPathLabel.Text);
            amountFoldersFiles.Text = "Показано каталогов: " + countDir + " и файлов: " + countFiles;
        }

        private void Button_Disk_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int countDir = Directory.GetDirectories(e.ClickedItem.Text).Length;
            int countFiles = Directory.GetFiles(e.ClickedItem.Text).Length;
            userControlTable1.loadForm(e.ClickedItem.Text);
            amountFoldersFiles.Text = "Показано каталогов: " + countDir + " и файлов: " + countFiles;
            fullPathLabel.Text = e.ClickedItem.Text;
        }

        private void Button_CreateDir_Click(object sender, EventArgs e)
        {
            userControlTable1.createDirectory(fullPathLabel.Text);
            makeTreeView();
        }

        private void Button_CreateFile_Click(object sender, EventArgs e)
        {
            userControlTable1.create_file(fullPathLabel.Text);
        }

        private void userControlTable1_chanName(object sender, EventArgs e)
        {
            try
            {
                fullPathLabel.Text = sender.ToString();
            }
            catch { }
            int countDir = Directory.GetDirectories(fullPathLabel.Text).Length;
            int countFiles = Directory.GetFiles(fullPathLabel.Text).Length;
            amountFoldersFiles.Text = "Показано каталогов: " + countDir + " и файлов: " + countFiles;
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            userControlTable1.deleteToolStripMenuItem_Click(sender, e);
            int countDir = Directory.GetDirectories(fullPathLabel.Text).Length;
            int countFiles = Directory.GetFiles(fullPathLabel.Text).Length;
            amountFoldersFiles.Text = "Показано каталогов: " + countDir + " и файлов: " + countFiles;
            makeTreeView();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!(this.WindowState == FormWindowState.Minimized))
            {
                int deltaHeight = this.ClientSize.Height - height;
                int deltaWidth = this.ClientSize.Width - width;
                treeView1.Height += deltaHeight;
                height = this.ClientSize.Height;
                width = this.ClientSize.Width;
                userControlTable1.userControl_Resize(deltaHeight, deltaWidth);
            }
        }

        public struct POINT
        {
            public int X;
            public int Y;
        }

        public POINT getCursorInWindow(POINT point)
        {
            point.X -= this.Left;
            point.Y -= this.Top;
            return point;
        }

        private void userControlTable1_deleteFile(object sender, EventArgs e)
        {
            makeTreeView();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure you want to leave the program?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
                e.Cancel = true;
        }
    }


}
