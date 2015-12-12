using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Runtime.InteropServices;
using System.IO;

namespace alt_viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //
        DialogResult dlg;
        string folder = "";
        List<string> directory_files = new List<string>();
        int pos = 0;
        
        //

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlg = openfile_window.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                this.Text = openfile_window.SafeFileName;
                picture_box.ImageLocation = openfile_window.FileName;
                //
                string tmp_folder = openfile_window.FileName.Substring(0,openfile_window.FileName.LastIndexOf(@"\")); //without last "\"
                if (tmp_folder != folder)
                {
                    folder = tmp_folder;
                    directory_files.Clear();
                    foreach (string file in Directory.EnumerateFiles(folder + @"\", "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".jpg", true, null)
                        || s.EndsWith(".jpeg", true, null) || s.EndsWith(".bmp", true, null) || s.EndsWith(".png", true, null) || s.EndsWith(".gif", true, null)
                        || s.EndsWith(".wmf", true, null)))
                    {
                        directory_files.Add(file);
                    }
                }
                pos = directory_files.IndexOf(openfile_window.FileName);
                this.Text = pos + 1 + "/" + directory_files.Count + " " + picture_box.ImageLocation.Substring(openfile_window.FileName.LastIndexOf(@"\")).Substring(1);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openfile_window.FileName = "";
            openfile_window.Filter = "Image Files(*.jpg;*.jpeg;*.bmp;*.png;*.gif;*.wmf)|*.jpg;*.jpeg;*.bmp;*.png;*.gif;*.wmf|All files (*.*)|*.*";
            openfile_window.Multiselect = false;
            openfile_window.ReadOnlyChecked = true;
            openfile_window.Title = "Open file";
            //
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((folder != "") & (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)) navigate_img(e.KeyCode);
        }

        private void navigate_img(Keys e)
        {
            if (e == Keys.Right) //следующее изображение
            {
                pos++;
            }

            if (e == Keys.Left) //предыдущее изображение
            {
                pos--;
            }
            if (pos < 0) pos = directory_files.Count - 1;
            if (pos == directory_files.Count) pos = 0;
            picture_box.ImageLocation = directory_files.ElementAt(pos);
            this.Text = pos+1 + "/" + directory_files.Count + " " + picture_box.ImageLocation.Substring(openfile_window.FileName.LastIndexOf(@"\")).Substring(1);
        }

        private void picture_box_MouseClick(object sender, MouseEventArgs e)
        {
            if (folder != "")
            {
                if (e.Button == MouseButtons.XButton1) navigate_img(Keys.Right);
                if (e.Button == MouseButtons.XButton2) navigate_img(Keys.Left);
            }
            
        }



    }
}
