using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Resources;
using WinForms = System.Windows.Forms;
using System.IO;
using System.Windows.Media.Animation;
using MahApps.Metro.Controls.Dialogs;
using System.Reflection;
using System.Threading;
using SimpleFOMOD.Class_Files;

namespace SimpleFOMOD
{
    public partial class ModuleConfigWindow
    {
        public static Mod mod;

        public ModuleConfigWindow()
        {
            InitializeComponent();

            lstGroup.ItemsSource = mod.Groups;

            // Set hidden controls opacity to 0.
             txtAddGroup.Opacity = 0;
            lstGroup.Opacity = 0;
            txtAddModule.Opacity = 0;
            lstModule.Opacity = 0;
            lstAllFiles.Opacity = 0;
            lstSelectedFiles.Opacity = 0;
            rboSelectAny.Opacity = 0;
            rboSelectOne.Opacity = 0;
            txtDestination.Opacity = 0;
            lblImageBrowse.Opacity = 0;
            lblDestinationHelp.Opacity = 0;
            lblFolderBrowse.Opacity = 0;
            txtDescription.Opacity = 0;
            btnCreate.Opacity = 0;

            // Shows the folder controls.
            DoFadeInAnimation(lblFolderBrowse);

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

        // Opens NexusMods page in browser.
        private void LaunchSimpleFOMODOnNexusMods(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sirdoombox/SimpleFOMOD");
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var dialog = (BaseMetroDialog)this.Resources["CustomDialogTest"];

            //this.ShowMetroDialogAsync(dialog);
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
                da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                da.BeginTime = TimeSpan.FromMilliseconds(200);
                control.BeginAnimation(OpacityProperty, da);
            }
        }

        // Adds a group to the lstGroup listbox.
        public void txtAddGroup_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (txtAddGroup.Text != "")
                {
                    e.Handled = true;
                    //lstGroup.Items.Add(txtAddGroup.Text);
                    Group tempGroup = new Group(txtAddGroup.Text, (rboSelectAny.IsChecked ?? false) ? "SelectAny" : "SelectExactlyOne");
                    mod.Groups = new List<Group>();
                    mod.Groups.Add(tempGroup);
                    lstGroup.SelectedItem = txtAddGroup.Text;
                    txtAddGroup.Clear();

                    // Unhides module controls.
                    if (txtAddModule.Opacity == 0)
                    {
                        DoFadeInAnimation(txtAddModule, txtDescription, lstModule);
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
                        DoFadeInAnimation(lstAllFiles, lstSelectedFiles, txtDestination, lblDestinationHelp, lblImageBrowse, btnCreate);
                    }
                }
            }
        }

        // Adds files to the Selected Files listbox and removes from all files.
        private void lstAllFiles_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (lstAllFiles.SelectedIndex >= 0)
            {
                lstSelectedFiles.Items.Add(lstAllFiles.SelectedItem);
                lstAllFiles.Items.Remove(lstAllFiles.SelectedItem);
            }
        }

        // Removes files from selected and adds to All Files listbox.
        private void lstSelectedFiles_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (lstSelectedFiles.SelectedIndex >= 0)
            {
                lstAllFiles.Items.Add(lstSelectedFiles.SelectedItem);
                lstSelectedFiles.Items.Remove(lstSelectedFiles.SelectedItem);
            }
        }

        // This button creates the things.
        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            MessageDialogResult result = await this.ShowMessageAsync("WARNING", "ARE YOU ABSOLUTELY SURE? BE SURE, BECAUSE THIS MIGHT BREAK EVERYTHING.", MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Negative)
            {
                //don't do anything
            }
            else
            {
                //do the creation stuff
            }
            
        }

        // Opens Image Browser of some kind.
        private void ImageBrowse_MouseUp(object sender, RoutedEventArgs e)
        {
            // Open image browser here.
        }

        // Opens the folder Browser
        private void FolderBrowse_MouseUp(object sender, RoutedEventArgs e)
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
            lblFolderBrowse.Content = ((TreeViewItem)e.NewValue).Tag.ToString();

            lstAllFiles.Items.Clear();
            // Runs a loop for each file in the selected directory, adding it to the AllFiles listbox.
            string file;
            try
            {
                foreach (var element in Directory.GetFiles(lblFolderBrowse.Content.ToString()))
                {
                    file = System.IO.Path.GetFileName(element);
                    lstAllFiles.Items.Add(file);
                }

                // Unhides the group controls.
                if (txtAddGroup.Opacity == 0)
                {
                    DoFadeInAnimation(txtAddGroup, lstGroup, rboSelectAny, rboSelectOne);
                }
            }
            catch (Exception) { }

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
