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

namespace SimpleFOMOD
{

    public partial class MainWindow : MetroWindow
    {
        
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

            cboCategory.ItemsSource = list;


        }

        // Opens NexusMods page in browser.
        private void LaunchSimpleFOMODOnNexusMods(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/sirdoombox/SimpleFOMOD");
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

        private void btnNext_Click(object sender, RoutedEventArgs e) // Basic as fuck validation and error catching.
        {
            bool all_ok = true;

            // Checks if modname is blank.
            if (txtModName.Text != "") { DoInputOK(lblNameError); }
            else { DoInputNotOK(txtModName, lblNameError); all_ok = false; }
            
            // Checks if author name is blank.
            if (txtAuthor.Text != "") { DoInputOK(lblAuthorError); }
            else { DoInputNotOK(txtAuthor, lblAuthorError); all_ok = false; }
            
            // Checks if the version number is blank or isn't a number.
            if (Checker.VerNumberCheck(txtVersion.Text)){ DoInputOK(lblVerError); }
            else { DoInputNotOK(txtVersion, lblVerError); all_ok = false; }

            // checks if the URL is blank or invalid.
            if (Checker.URLCheck(txtURL.Text)){ DoInputOK(lblURLError); }
            else { DoInputNotOK(txtURL, lblURLError); all_ok = false; }

            // Checks if a category has been selected.
            if (cboCategory.SelectedIndex != -1) { DoInputOK(lblCatError); }
            else { cboCategory.Focus(); DoFadeInAnimation(lblCatError); all_ok = false; }

            if (all_ok) // Checks that there aren't any errors on the page.
            {
                // Casts the input over to the "Mod" object.
                Mod mod = new Mod(txtModName.Text, txtAuthor.Text, txtVersion.Text, txtURL.Text, cboCategory.SelectedItem.ToString(), null);

                // Close this window and open the Module Config Window
                ModuleConfigWindow newWin = new ModuleConfigWindow();
                newWin.Show();
                this.Close();
            }
        }

        // Do this if the input is correct.
        private void DoInputOK (Control inputLabel)
        {
            inputLabel.Visibility = Visibility.Hidden; inputLabel.Opacity = 0;
        }

        // Do this if the input is incorrect.
        private void DoInputNotOK (TextBox inputControl, Control inputLabel)
        {
            inputControl.Focus(); inputControl.Clear(); DoFadeInAnimation(inputLabel); inputLabel.Visibility = Visibility.Visible;
        }

        // List of Categories available on NexusMods - To be used as
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
