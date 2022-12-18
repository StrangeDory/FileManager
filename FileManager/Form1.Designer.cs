
namespace FileManager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.userControlTable1 = new FileManager.UserControlTable();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Button_Disk = new System.Windows.Forms.ToolStripSplitButton();
            this.Button_Delete = new System.Windows.Forms.ToolStripButton();
            this.Button_CreateDir = new System.Windows.Forms.ToolStripButton();
            this.Button_CreateFile = new System.Windows.Forms.ToolStripButton();
            this.fullPathLabel = new System.Windows.Forms.ToolStripLabel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.amountFoldersFiles = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripEmpty = new System.Windows.Forms.ToolStripStatusLabel();
            this.dateTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // userControlTable1
            // 
            this.userControlTable1.Location = new System.Drawing.Point(211, 48);
            this.userControlTable1.Name = "userControlTable1";
            this.userControlTable1.Size = new System.Drawing.Size(586, 430);
            this.userControlTable1.TabIndex = 0;
            this.userControlTable1.value_dgv = null;
            this.userControlTable1.deleteFile += new System.EventHandler(this.userControlTable1_deleteFile);
            this.userControlTable1.chanName += new System.EventHandler(this.userControlTable1_chanName);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Button_Disk,
            this.Button_Delete,
            this.Button_CreateDir,
            this.Button_CreateFile,
            this.fullPathLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(798, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Button_Disk
            // 
            this.Button_Disk.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Button_Disk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button_Disk.Image = ((System.Drawing.Image)(resources.GetObject("Button_Disk.Image")));
            this.Button_Disk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Disk.Margin = new System.Windows.Forms.Padding(0, 10, 15, 2);
            this.Button_Disk.Name = "Button_Disk";
            this.Button_Disk.Size = new System.Drawing.Size(45, 20);
            this.Button_Disk.Text = "Disk";
            this.Button_Disk.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Button_Disk_DropDownItemClicked);
            // 
            // Button_Delete
            // 
            this.Button_Delete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Button_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_Delete.Image = global::FileManager.Properties.Resources.delete_icon;
            this.Button_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_Delete.Margin = new System.Windows.Forms.Padding(0, 10, 0, 2);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(23, 20);
            this.Button_Delete.Text = "toolStripButton1";
            this.Button_Delete.Click += new System.EventHandler(this.Button_Delete_Click);
            // 
            // Button_CreateDir
            // 
            this.Button_CreateDir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Button_CreateDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_CreateDir.Image = global::FileManager.Properties.Resources.create_icon;
            this.Button_CreateDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_CreateDir.Margin = new System.Windows.Forms.Padding(0, 10, 0, 2);
            this.Button_CreateDir.Name = "Button_CreateDir";
            this.Button_CreateDir.Size = new System.Drawing.Size(23, 20);
            this.Button_CreateDir.Text = "Create Directory";
            this.Button_CreateDir.Click += new System.EventHandler(this.Button_CreateDir_Click);
            // 
            // Button_CreateFile
            // 
            this.Button_CreateFile.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Button_CreateFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Button_CreateFile.Image = global::FileManager.Properties.Resources.createfile_icon;
            this.Button_CreateFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button_CreateFile.Margin = new System.Windows.Forms.Padding(0, 10, 0, 2);
            this.Button_CreateFile.Name = "Button_CreateFile";
            this.Button_CreateFile.Size = new System.Drawing.Size(23, 20);
            this.Button_CreateFile.Text = "toolStripButton3";
            this.Button_CreateFile.Click += new System.EventHandler(this.Button_CreateFile_Click);
            // 
            // fullPathLabel
            // 
            this.fullPathLabel.Name = "fullPathLabel";
            this.fullPathLabel.Size = new System.Drawing.Size(13, 29);
            this.fullPathLabel.Text = "0";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 48);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(193, 430);
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "FullFolder.png");
            this.imageList1.Images.SetKeyName(1, "EmptyFolder.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amountFoldersFiles,
            this.statusStripEmpty,
            this.dateTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 482);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(798, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // amountFoldersFiles
            // 
            this.amountFoldersFiles.Name = "amountFoldersFiles";
            this.amountFoldersFiles.Size = new System.Drawing.Size(121, 17);
            this.amountFoldersFiles.Text = "Показано каталогов:";
            // 
            // statusStripEmpty
            // 
            this.statusStripEmpty.Name = "statusStripEmpty";
            this.statusStripEmpty.Size = new System.Drawing.Size(649, 17);
            this.statusStripEmpty.Spring = true;
            // 
            // dateTime
            // 
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(13, 17);
            this.dateTime.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 504);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.userControlTable1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(814, 543);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControlTable userControlTable1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripSplitButton Button_Disk;
        private System.Windows.Forms.ToolStripButton Button_Delete;
        private System.Windows.Forms.ToolStripButton Button_CreateDir;
        private System.Windows.Forms.ToolStripButton Button_CreateFile;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel amountFoldersFiles;
        private System.Windows.Forms.ToolStripStatusLabel statusStripEmpty;
        private System.Windows.Forms.ToolStripStatusLabel dateTime;
        private System.Windows.Forms.ToolStripLabel fullPathLabel;
    }
}

