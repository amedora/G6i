using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace G6i
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private string ChooseFile(CommonFileDialogFilter filter)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            dialog.EnsureReadOnly = false;
            dialog.AllowNonFileSystemItems = false;
            dialog.Filters.Add(filter);

            var result = dialog.ShowDialog();

            return result == CommonFileDialogResult.Ok ? dialog.FileName : null;
        }

        private void btnSetupFile_Click(object sender, EventArgs e)
        {
            var file = this.ChooseFile(new CommonFileDialogFilter("Setup", ".htm"));
            if (file != null)
            {
                tbxSetupFile.Text = file;
                tbxSetupFile.Select(tbxSetupFile.Text.Length, 0);
            }
        }

        private void btnMotecFile_Click(object sender, EventArgs e)
        {
            var file = this.ChooseFile(new CommonFileDialogFilter("Motec", ".ldx"));
            if (file != null)
            {
                tbxMotecFile.Text = file;
                tbxMotecFile.Select(tbxMotecFile.Text.Length, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
