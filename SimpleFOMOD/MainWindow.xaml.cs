using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Media.Animation;
using SimpleFOMOD.Class_Files;
using System.Collections.ObjectModel;
using System.Net.Http;
using MahApps.Metro.Controls.Dialogs;

namespace SimpleFOMOD
{

    public partial class MainWindow : MetroWindow
    {

        public static string currentVersion = "1.2r";

        public MainWindow()
        {
            InitializeComponent();
            imgLogo.Opacity = 0.0;
            txtModName.Opacity = 0.0;
            txtAuthor.Opacity = 0.0;
            txtVersion.Opacity = 0.0;
            txtURL.Opacity = 0.0;
            cboCategory.Opacity = 0.0;
            btnNext.Opacity = 0.0;

            // Check for update
            UpdateCheck();

            //// TESTING VALUES //
            //txtAuthor.Text = "TestAuthor";
            //txtModName.Text = "Test Mod Name";
            //txtURL.Text = "nexusmods.com/mods/";
            //txtVersion.Text = "v1.07.5ab";
            //// TESTING VALUES //

            cboCategory.ItemsSource = list;

        }

        private async void UpdateCheck()
        {
            var http = new HttpClient();
            string tempLatestVersion = await http.GetStringAsync(new Uri("https://raw.githubusercontent.com/sirdoombox/SimpleFOMOD/master/latest.txt"));
            string latestVersion = tempLatestVersion;
            string msgTemplate = "({0}) An Update is available - Press OK to go to download page.";
            string msgData = latestVersion;
            string updateMessage = string.Format(msgTemplate, msgData);
            if (currentVersion != latestVersion)
            {
                MessageDialogResult result = await this.ShowMessageAsync("UPDATE", updateMessage, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Negative)
                {
                    //don't do anything
                }
                else
                {
                    System.Diagnostics.Process.Start("http://www.nexusmods.com/fallout4/mods/6441/");
                    Application.Current.Shutdown();
                }
            }
        }

        // Opens NexusMods page in browser.
        private void LaunchSimpleFOMODOnNexusMods(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.nexusmods.com/fallout4/mods/6441/");
        }

        // Plays the show animation for the given controls.
        private void DoFadeInAnimation(Control control)
        {
            // Fade in Animation.
            if (control.Opacity == 0)
            {
                DoubleAnimation da = new DoubleAnimation();
                da.From = 0;
                da.To = 1;
                da.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                da.BeginTime = TimeSpan.FromMilliseconds(100);
                control.BeginAnimation(OpacityProperty, da);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // List of all controls on the page.
            List<Control> controlList = new List<Control>();
            controlList.Add(txtModName); controlList.Add(txtAuthor); controlList.Add(txtVersion);
            controlList.Add(txtURL); controlList.Add(cboCategory); controlList.Add(btnNext);

            // Variables
            double AnimationLength = 0.5;
            int AnimationStart = 500;
            int AnimationGap = 185;

            // Fade in Animation.
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(AnimationLength));
            da.BeginTime = TimeSpan.FromMilliseconds(AnimationStart);
            imgLogo.BeginAnimation(OpacityProperty, da);

            foreach (var control in controlList)
            {
                AnimationStart = AnimationStart + AnimationGap;
                da.BeginTime = TimeSpan.FromMilliseconds(AnimationStart);
                control.BeginAnimation(OpacityProperty, da);
            }
        }

        private void txtModName_LostFocus(object sender, RoutedEventArgs e)
        {
            // Checks if modname is blank.
            if (MainWindowChecker.ModNameCheck(txtModName.Text)) { DoInputOK(lblNameError); all_ok = true; }
            else { DoInputNotOK(txtModName, lblNameError); all_ok = false; }
        }

        private void txtAuthor_LostFocus(object sender, RoutedEventArgs e)
        {
            // Checks if author name is blank.
            if (MainWindowChecker.AuthorNameCheck(txtAuthor.Text)) { DoInputOK(lblAuthorError); all_ok = true; }
            else { DoInputNotOK(txtAuthor, lblAuthorError); all_ok = false; }
        }

        private void txtVersion_LostFocus(object sender, RoutedEventArgs e)
        {
            // Checks if the version number is blank or isn't a number.
            if (MainWindowChecker.VerNumberCheck(txtVersion.Text)) { DoInputOK(lblVerError); all_ok = true; }
            else { DoInputNotOK(txtVersion, lblVerError); all_ok = false; }
        }

        private void txtURL_LostFocus(object sender, RoutedEventArgs e)
        {
            // checks if the URL is blank or invalid.
            if (MainWindowChecker.URLCheck(txtURL.Text)) { DoInputOK(lblURLError); all_ok = true; }
            else { DoInputNotOK(txtURL, lblURLError); all_ok = false; }
        }

        // Used to check if any of the inputs are okay.
        bool all_ok = false;

        private void btnNext_Click(object sender, RoutedEventArgs e) // if all the inputs are bueno, this should run.
        {
            if (all_ok) // Checks that there aren't any errors on the page.
            {
                // Casts the input over to the "Mod" object.
                ModuleConfigWindow.mod = new Mod(txtModName.Text, txtAuthor.Text, txtVersion.Text, txtURL.Text, cboCategory.SelectedItem.ToString(), new ObservableCollection<Mod.Group>());

                // Close this window and open the Module Config Window
                ModuleConfigWindow newWin = new ModuleConfigWindow();
                newWin.Owner = this;
                newWin.Show();
                newWin.Owner = null;
                this.Close();
            }
            else // If there are any errors at all with the input, this will fire.
            {
                // Something goes here to tell the user to make sure all the inputs are bueno.
            }
        }

        // Do this if the input is correct.
        public void DoInputOK(Control inputLabel)
        {
            inputLabel.Visibility = Visibility.Hidden; inputLabel.Opacity = 0;
        }

        // Do this if the input is incorrect.
        public void DoInputNotOK(TextBox inputControl, Control inputLabel)
        {
            DoFadeInAnimation(inputLabel); inputLabel.Visibility = Visibility.Visible;
        }

        // List of Categories available on NexusMods
        public List<string> list = new List<string>()
        {
            "Ammo",
            "Animation",
            "Armour",
            "Audio - Misc",
            "Audio - Music",
            "Audio - SFX",
            "Audio - Voice",
            "Bug Fixes",
            "Buildings",
            "Cheats and God Items",
            "Clothing",
            "Collectibles, Treasure Hunts, and Puzzles",
            "Companions",
            "Crafting - Equipment",
            "Crafting - Home",
            "Creatures",
            "ENB Presets",
            "Environment",
            "Factions",
            "Gameplay Effects and Changes",
            "Hair and Face Models",
            "Items - Food/Drinks/Chems/etc",
            "Locations - New",
            "Locations - Vanilla",
            "Miscellaneous",
            "Modders Resources and Tutorials",
            "Models and Textures",
            "New Lands",
            "NPC",
            "NPC - Vendors",
            "Overhauls",
            "Patches",
            "Performance",
            "Perks",
            "Player Homes",
            "Player Settlement",
            "Poses",
            "Quests and Adventures",
            "Radio",
            "Saved Games/Characters",
            "Skills and Leveling",
            "User Interface",
            "Utilities",
            "Vehicles",
            "Visuals and Graphics",
            "Weapons",
            "Weapons and Armour"
        };
    }
}
