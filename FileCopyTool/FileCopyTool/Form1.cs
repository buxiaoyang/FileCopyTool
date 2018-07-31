using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileCopyTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            //Check source folder and target folder
            if (!Directory.Exists(this.textBoxSourceFolder.Text.Trim()))
            {
                MessageBox.Show("\"Source Folder\" does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.textBoxSourceFolder.Focus();
                return;
            }
            if (!Directory.Exists(this.textBoxTargetFolder.Text.Trim()))
            {
                MessageBox.Show("\"Target Folder\" does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.textBoxTargetFolder.Focus();
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach(string line in this.textBoxFileList.Lines)
            {
                try
                {
                    sb.Append(line);
                    string sourceFilePath = Path.Combine(this.textBoxSourceFolder.Text.Trim(), line);
                    string targetFilePath = Path.Combine(this.textBoxTargetFolder.Text.Trim(), line);
                    string targetFolder = Path.GetDirectoryName(targetFilePath);
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }
                    File.Copy(sourceFilePath, targetFilePath, true);
                    sb.Append(String.Format("    Complete"));
                }
                catch (Exception ex)
                {
                    sb.Append(String.Format("    Error:{0}", ex.Message));
                }
                finally 
                {
                    sb.Append("\r\n");
                }
            }
            this.textBoxFileList.Text = sb.ToString();
            MessageBox.Show("Copy complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
