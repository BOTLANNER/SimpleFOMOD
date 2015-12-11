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

namespace SimpleFOMOD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Fade in Animation.
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(1.5));
            // Sets and begins Logo Fade In
            da.BeginTime = TimeSpan.FromMilliseconds(600);
            imgLogo.BeginAnimation(OpacityProperty, da);
            // Name Fade
            da.BeginTime = TimeSpan.FromMilliseconds(750);
            txtModName.BeginAnimation(OpacityProperty, da);
            // Author Fade
            da.BeginTime = TimeSpan.FromMilliseconds(900);
            txtAuthor.BeginAnimation(OpacityProperty, da);
            // Version Fade
            da.BeginTime = TimeSpan.FromMilliseconds(1050);
            txtVersion.BeginAnimation(OpacityProperty, da);
            // URL Fade
            da.BeginTime = TimeSpan.FromMilliseconds(1200);
            txtURL.BeginAnimation(OpacityProperty, da);
            // Category Fade
            da.BeginTime = TimeSpan.FromMilliseconds(1350);
            cboCategory.BeginAnimation(OpacityProperty, da);
            // Next Fade
            da.BeginTime = TimeSpan.FromMilliseconds(1500);
            btnNext.BeginAnimation(OpacityProperty, da);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            // Casts the input over to the "Mod" object.
            Mod mod = new Mod(txtModName.Text,txtAuthor.Text,txtVersion.Text,txtURL.Text,cboCategory.SelectedItem.ToString(), null);
            MessageBox.Show(mod.ModName + mod.Author + mod.Version + mod.URL + mod.Category);
            // Close this window and open the Module Config Window
            // Would be nice if we could animate a window size change, and then populate it with all the new controls.
            // Switching directly to a new form looks pretty damn hideous with all that flash fade in animation and stuff.
            //ModuleConfigWindow newWin = new ModuleConfigWindow();
            //newWin.Show();
            //this.Close();
        }


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
