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
            label1.Text = @"Welcome to 'File manager'.

What's good about our product?

We have everything you need for working with files and directories.

Simple interface.

Tree view allows you to easily find directories.

This version of File manager provides the opportunity to delete, create, edit, move, copy files and directories.

Requires minimal amount of computer resources.


A list of commands:

Delete - delete object.
Create - creates object. 
Move - moves the object at the specified path.
Copy - copies the object. 
Rename - renames the object to a random name.";
        }
    }
}
