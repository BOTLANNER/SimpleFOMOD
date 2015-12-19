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
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace SimpleFOMOD
{
    public partial class ModuleConfigWindow
    {
        public static Mod mod;     

        public ModuleConfigWindow()
        {
            InitializeComponent();

            // Set hidden controls opacity to 0.
            txtAddGroup.Opacity = 0; txtAddGroup.Visibility = Visibility.Hidden;
            lstGroup.Opacity = 0; lstGroup.Visibility = Visibility.Hidden;
            txtAddModule.Opacity = 0; txtAddModule.Visibility = Visibility.Hidden;
            lstModule.Opacity = 0; lstModule.Visibility = Visibility.Hidden;
            lstAllFiles.Opacity = 0; lstAllFiles.Visibility = Visibility.Hidden;
            lstSelectedFiles.Opacity = 0; lstSelectedFiles.Visibility = Visibility.Hidden;
            rboSelectAny.Opacity = 0; rboSelectAny.Visibility = Visibility.Hidden;
            rboSelectOne.Opacity = 0; rboSelectOne.Visibility = Visibility.Hidden;
            txtDestination.Opacity = 0; txtDestination.Visibility = Visibility.Hidden;
            lblImageBrowse.Opacity = 0; lblImageBrowse.Visibility = Visibility.Hidden;
            lblDestinationHelp.Opacity = 0; lblDestinationHelp.Visibility = Visibility.Hidden;
            lblFolderBrowse.Opacity = 0; lblFolderBrowse.Visibility = Visibility.Hidden;
            txtDescription.Opacity = 0; txtDescription.Visibility = Visibility.Hidden;
            btnCreate.Opacity = 0; btnCreate.Visibility = Visibility.Hidden;
            lblDestSaveConfirm.Opacity = 0; lblDestSaveConfirm.Visibility = Visibility.Hidden;
            lblDescSaveConfirm.Opacity = 0; lblDescSaveConfirm.Visibility = Visibility.Hidden;
            lblImageClear.Visibility = Visibility.Hidden;

            this.DataContext = mod;
            lstGroup.ItemsSource = mod.Groups;

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

        
        // ---------------------------------- G R O U P         S T U F F ---------------------------------- //

        // Adds a group to the lstGroup listbox.
        public void txtAddGroup_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (txtAddGroup.Text != "")
                {
                    e.Handled = true;
                    mod.Groups.Add(new Mod.Group(txtAddGroup.Text, (rboSelectAny.IsChecked ?? false) ? "SelectAny" : "SelectExactlyOne", new ObservableCollection<Mod.Group.Module>()));
                    lstGroup.SelectedIndex = 0;
                    if (mod.Groups[lstGroup.SelectedIndex].Modules == null)
                    {
                        lstModule.ItemsSource = null;
                        lstSelectedFiles.ItemsSource = null;
                    }
                    txtAddGroup.Clear();

                    // Unhides module controls.
                    if (txtAddModule.Opacity == 0)
                    {
                        DoFadeInAnimation(txtAddModule, lstModule);
                    }
                }
            }
        }

        // Updates all the "Group" properties when the selected item changes.
        private void lstGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Updates the lstModules listbox with the relevant values.
            if (lstGroup.SelectedIndex != -1)
            {
                lstModule.ItemsSource = mod.Groups[lstGroup.SelectedIndex].Modules;
                lstModule.SelectedIndex = 0;
            }
            else
            {
                lstModule.ItemsSource = null;
                lstSelectedFiles.ItemsSource = null;
            }

            // Sets the correct checkbox for the current group.
            if (mod.Groups[lstGroup.SelectedIndex].Type == "SelectAny")
            {
                if (lstGroup.SelectedIndex != -1)
                {
                    rboSelectAny.IsChecked = true;
                }
            }
            else
            {
                if (lstGroup.SelectedIndex != -1)
                {
                    rboSelectOne.IsChecked = true;
                }
            }                      
        }

        // Sets the group type based on the checkboxes being checked.
        private void SelectAny_Checked(object sender, RoutedEventArgs e)
        {
            if(lstGroup.SelectedIndex != -1)
            {
                mod.Groups[lstGroup.SelectedIndex].Type = "SelectAny";
            }  
        }
        private void SelectOne_Checked(object sender, RoutedEventArgs e)
        {
            if (lstGroup.SelectedIndex != -1)
            {
                mod.Groups[lstGroup.SelectedIndex].Type = "SelectExactlyOne";
            }
        }

        // Removes the selected group when you press delete.
        public void lstGroup_DeleteDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if(lstGroup.SelectedIndex != -1)
                {
                    foreach (var modules in mod.Groups[lstGroup.SelectedIndex].Modules)
                    {
                        foreach (var files in modules.Files)
                        {
                            lstAllFiles.Items.Add(files.FileName);
                        }
                        mod.Groups.RemoveAt(lstGroup.SelectedIndex);
                    }
                }
            }
        }


        // ---------------------------------- M O D U L E        S T U F F ---------------------------------- //

        // Adds a module to the lstModule listbox.
        private void txtAddModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtAddModule.Text != "" && lstGroup.SelectedIndex != -1)
                {
                    e.Handled = true;
                    if (mod.Groups[lstGroup.SelectedIndex].Modules == null)
                    {
                        lstSelectedFiles.ItemsSource = null;
                    }
                    mod.Groups[lstGroup.SelectedIndex].Modules.Add(new Mod.Group.Module(txtAddModule.Text, new ObservableCollection<Mod.Group.Module.mFile>()));
                    lstModule.SelectedIndex = 0;
                    txtAddModule.Clear();


                    // Unhides file controls.
                    if (lstAllFiles.Opacity == 0)
                    {
                        DoFadeInAnimation(lstAllFiles, lstSelectedFiles, txtDestination, lblDestinationHelp, lblImageBrowse, btnCreate, txtDescription);
                    }
                }
            }
        }

        // Adds the description to the selected module
        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtDescription.Text != "" && lstModule.SelectedIndex != -1)
                {
                    mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Description = txtDescription.Text;
                    DoConfirmationAnimation(lblDescSaveConfirm);
                    Keyboard.ClearFocus();
                }
                else
                {
                    mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Description = null;
                }
            }
        }


        // Updates the relevant properties when you change the selected module.
        private void lstModule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Changes the "Description" property of the selected module if there is a module selected.
            if (lstModule.SelectedIndex != -1 )
            {
                lstSelectedFiles.ItemsSource = mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files;
                if(mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Description != "")
                {
                    txtDescription.Text = mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Description;
                }
                else
                {
                    txtDescription.Text = "";
                }

                if(mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].LocalImagePath != null)
                {
                    lblImageBrowse.Content = mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].LocalImagePath;
                    lblImageClear.Visibility = Visibility.Visible;
                }
                else
                {
                    lblImageBrowse.Content = "[OPTIONAL] Click Here To Select An Image For This Module...";
                    lblImageClear.Visibility = Visibility.Hidden;
                }
            }
            // Sets the selectedfile index to 1 if it has any entries
            if (lstSelectedFiles.SelectedIndex != -1)
            {
                lstSelectedFiles.SelectedIndex = 0;
            }

        }

        // Removes the selected module when you press delete.e
        public void lstModule_DeleteDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (lstModule.SelectedIndex != -1)
                {
                    foreach (var files in mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files)
                    {
                        lstAllFiles.Items.Add(files.FileName);
                    }
                    mod.Groups[lstGroup.SelectedIndex].Modules.RemoveAt(lstModule.SelectedIndex);
                }
            }
        }


        // ---------------------------------- F I L E         S T U F F ---------------------------------- //

        // Updates the relevant properties when you change the selected file.
        private void lstSelectedFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Changes the "Destination" property of the file list.
            if (lstSelectedFiles.SelectedIndex != -1 && lstModule.SelectedIndex != -1 && lstGroup.SelectedIndex != -1)
            {
                if (mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files[lstSelectedFiles.SelectedIndex].Destination != "")
                {
                    txtDestination.Text = mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files[lstSelectedFiles.SelectedIndex].Destination;
                }
                else
                {
                    txtDestination.Text = null;
                }
            }
        }

        // Adds files to the Selected Files listbox and removes from all files.
        private void lstAllFiles_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (lstAllFiles.SelectedIndex != -1 && lstModule.SelectedIndex != -1 && lstGroup.SelectedIndex != -1)
            {
                mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files.Add(new Mod.Group.Module.mFile(lstAllFiles.SelectedItem.ToString()));
                lstSelectedFiles.SelectedIndex = 0;
                lstAllFiles.Items.Remove(lstAllFiles.SelectedItem);
            }
        }

        // Removes files from selected and adds to All Files listbox.
        private void lstSelectedFiles_DoubleClick(object sender, RoutedEventArgs e)
        {
            if (lstSelectedFiles.SelectedIndex != -1)
            {
                lstAllFiles.Items.Add(mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files[lstSelectedFiles.SelectedIndex].FileName);
                mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files.RemoveAt(lstSelectedFiles.SelectedIndex);
            }
        }

        // Adds the destination to the selected file
        private void txtDestination_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtDestination.Text != "" && lstSelectedFiles.SelectedIndex != -1)
                {
                    mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files[lstSelectedFiles.SelectedIndex].Destination = txtDestination.Text;
                    DoConfirmationAnimation(lblDestSaveConfirm);
                    Keyboard.ClearFocus();
                }
                if (lstSelectedFiles.SelectedIndex != -1)
                {
                    mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].Files[lstSelectedFiles.SelectedIndex].Destination = "";
                }
                else { }
            }
        }

        // Opens Image Browser of some kind.
        private void ImageBrowse_MouseUp(object sender, RoutedEventArgs e)
        {
            if (lstModule.SelectedIndex != -1)
            {
                // Create file dialog instance.
                OpenFileDialog openImageDialog = new OpenFileDialog();
                openImageDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
                openImageDialog.FilterIndex = 1;
                openImageDialog.Multiselect = false;

                // Store the selected image in the selectedImage textbox.
                if (openImageDialog.ShowDialog() == true)
                {
                    lblImageBrowse.Content = openImageDialog.FileName;
                    mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].LocalImagePath = openImageDialog.FileName;
                    mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].RelativeImagePath = @"fomod\images\" + System.IO.Path.GetFileName(openImageDialog.FileName);
                    lblImageClear.Visibility = Visibility.Visible;
                }
            }
        }

        private void ImageClear_MouseUp(object sender, RoutedEventArgs e)
        {
            if (mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].LocalImagePath != "")
            {
                mod.Groups[lstGroup.SelectedIndex].Modules[lstModule.SelectedIndex].LocalImagePath = "";
                lblImageBrowse.Content = "[OPTIONAL] Click Here To Select An Image For This Module...";
                lblImageClear.Visibility = Visibility.Hidden;
            }
            else { }
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


        // ---------------------------------- O T H E R         S T U F F ---------------------------------- //

        // Opens NexusMods page in browser.
        private void LaunchSimpleFOMODOnNexusMods(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sirdoombox/SimpleFOMOD");
        }

        // Opens a help window
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow newWin = new HelpWindow();
            newWin.Owner = this;
            newWin.Show();
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
                control.Visibility = System.Windows.Visibility.Visible;
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                da.BeginTime = TimeSpan.FromMilliseconds(200);
                control.BeginAnimation(OpacityProperty, da);
            }
        }

        // Plays a quick fadeIn/fadeOut for the given control.
        private void DoConfirmationAnimation(Control control)
        {
            control.Visibility = Visibility.Visible;
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 0.9;
            da.Duration = new Duration(TimeSpan.FromSeconds(0.5));
            da.Completed += (s, e) =>
            {
                DoubleAnimation db = new DoubleAnimation();
                db.From = 0.9;
                db.To = 0;
                db.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                db.BeginTime = TimeSpan.FromSeconds(0.2);
                db.Completed += (sender, eargs) =>
                {
                    control.Visibility = Visibility.Hidden;
                };
                control.BeginAnimation(OpacityProperty, db);
            };
            control.BeginAnimation(OpacityProperty, da);
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
                FileIO.fileManipulation(lblFolderBrowse.Content.ToString(), mod);
                XMLgenerator.GenerateInfoXML(lblFolderBrowse.Content.ToString(), mod);
                XMLgenerator.GenerateModuleConfigXML(lblFolderBrowse.Content.ToString(), mod);
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
                    DoFadeInAnimation(txtAddGroup, 
                        lstGroup, 
                        rboSelectAny, 
                        rboSelectOne);
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
