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
using System.IO;
using System.Windows.Media.Animation;

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

            // Set hidden controls opacity to 0.
            txtAddGroup.Opacity = 0;
            lstGroup.Opacity = 0;
            txtAddModule.Opacity = 0;
            lstModule.Opacity = 0;
            lstAllFiles.Opacity = 0;
            lstSelectedFiles.Opacity = 0;
            btnAddFiles.Opacity = 0;
            btnRemoveFiles.Opacity = 0;
            rboSelectAny.Opacity = 0;
            rboSelectOne.Opacity = 0;

            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                foldersItem.Items.Add(item);
            }
        }

        // Plays the show animation for the given controls.
        private void DoFadeInAnimation(params Control[] selectedControls)
        {
            // Fade in Animation.
            foreach (var control in selectedControls)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromSeconds(1.0));
                control.BeginAnimation(OpacityProperty, da);
            }
        }

        // Adds a group to the lstGroup listbox.
        private void txtAddGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtAddGroup.Text != "")
                {
                    e.Handled = true;
                    lstGroup.Items.Add(txtAddGroup.Text);
                    lstGroup.SelectedItem = txtAddGroup.Text;
                    txtAddGroup.Clear();

                    // Unhides module controls.
                    if (txtAddModule.Opacity == 0)
                    {
                        DoFadeInAnimation(txtAddModule);
                        DoFadeInAnimation(lstModule);
                    }
                }
            }
        }

        // Adds a module to the lstModule listbox.
        private void txtAddModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtAddModule.Text != "")
                {
                    e.Handled = true;
                    lstModule.Items.Add(txtAddModule.Text);
                    lstModule.SelectedItem = txtAddModule.Text;
                    txtAddModule.Clear();

                    // Unhides file controls.
                    if (lstAllFiles.Opacity == 0)
                    {
                        DoFadeInAnimation(lstAllFiles);
                        DoFadeInAnimation(lstSelectedFiles);
                        DoFadeInAnimation(btnAddFiles);
                        DoFadeInAnimation(btnRemoveFiles);
                    }
                }
            }
        }

        // Adds files to the Selected Files listbox and removes from all files.
        private void btnAddFiles_Click(object sender, RoutedEventArgs e)
        {
            if (lstAllFiles.SelectedIndex >= 0)
            {
                lstSelectedFiles.Items.Add(lstAllFiles.SelectedItem);
                lstAllFiles.Items.Remove(lstAllFiles.SelectedItem);
            }
        }

        // Removes files from selected and adds to All Files listbox.
        private void btnRemoveFiles_Click(object sender, RoutedEventArgs e)
        {
            if (lstSelectedFiles.SelectedIndex >= 0)
            {
                lstAllFiles.Items.Add(lstSelectedFiles.SelectedItem);
                lstSelectedFiles.Items.Remove(lstSelectedFiles.SelectedItem);
            }
        }

        // Opens the folder browser flyout when you click browse, unless it's already open in which case it closes it.
        private void btnFolderBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (this.FolderBrowserFlyout.IsOpen != true)
            {
                this.FolderBrowserFlyout.IsOpen = true;
            }
            else
            {
                this.FolderBrowserFlyout.IsOpen = false;
            }
            
        }
        
        // Sets the active folder textbox to whatever you select in the flyout treeview.
        private void foldersItem_SelectedItemChanged (object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Sets the folderbrowse textbox to the path.
            txtFolderBrowse.Text = ((TreeViewItem)e.NewValue).Tag.ToString();

            lstAllFiles.Items.Clear();
            // Runs a loop for each file in the selected directory, adding it to the AllFiles listbox.
            string file = "";
            foreach (var element in Directory.GetFiles(txtFolderBrowse.Text))
            {
                file = System.IO.Path.GetFileName(element);
                lstAllFiles.Items.Add(file);
            }

            // Unhides the group controls.
            if (txtAddGroup.Opacity == 0)
            {
                DoFadeInAnimation(txtAddGroup);
                DoFadeInAnimation(lstGroup);
                DoFadeInAnimation(rboSelectAny);
                DoFadeInAnimation(rboSelectOne);
            }
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        private object dummyNode = null;
    }
}
