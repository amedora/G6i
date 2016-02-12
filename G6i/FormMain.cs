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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace G6i
{
    public partial class FormMain : Form
    {
        private FileSelector _selectorSetup = new FileSelector { Filter = new CommonFileDialogFilter("Setup", ".htm") };
        private FileSelector _selectorMotec = new FileSelector { Filter = new CommonFileDialogFilter("Motec", ".ldx") };

        public FormMain()
        {
            InitializeComponent();
            this.ActiveControl = btnSetupFile;
        }

        private string ChooseFile(CommonFileDialogFilter filter, string initialDir)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            dialog.EnsureReadOnly = false;
            dialog.AllowNonFileSystemItems = false;
            dialog.Filters.Add(filter);
            if (initialDir != null)
                dialog.InitialDirectory = initialDir;

            var result = dialog.ShowDialog();

            return result == CommonFileDialogResult.Ok ? dialog.FileName : null;
        }

        private void btnSetupFile_Click(object sender, EventArgs e)
        {
            var file = this.ChooseFile(_selectorSetup.Filter, _selectorSetup.LastDir);
            if (file != null)
            {
                _selectorSetup.LastDir = Path.GetDirectoryName(file);
                tbxSetupFile.Text = file;
                tbxSetupFile.Select(tbxSetupFile.Text.Length, 0);
            }
        }

        private void btnMotecFile_Click(object sender, EventArgs e)
        {
            var file = this.ChooseFile(_selectorMotec.Filter, _selectorMotec.LastDir);
            if (file != null)
            {
                _selectorMotec.LastDir = Path.GetDirectoryName(file);
                tbxMotecFile.Text = file;
                tbxMotecFile.Select(tbxMotecFile.Text.Length, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    class FileSelector
    {
        public CommonFileDialogFilter Filter { get; set; }
        public string LastDir { get; set; }
    }
}
