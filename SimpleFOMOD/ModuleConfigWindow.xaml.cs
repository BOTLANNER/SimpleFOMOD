using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using WinForms = System.Windows.Forms;

namespace SimpleFOMOD
{
    /// <summary>
    /// Interaction logic for ModuleConfigWindow.xaml
    /// </summary>
    public partial class ModuleConfigWindow
    {
        public ModuleConfigWindow()
        {
            InitializeComponent();
        }

        private void txtAddGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                lstGroup.Items.Add(txtAddGroup.Text);
                txtAddGroup.Clear();
            }
        }

        // Removes the existing text from 
        public void FolderBrowse_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            txtFolderBrowse.Text = dialog.SelectedPath.ToString();
        }

        // Removes the existing text from 
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
    }
}
