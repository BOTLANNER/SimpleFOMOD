﻿using System;
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
            // CAST //
            // INPUT //
            // TO MOD //
            // OBJECT //
            // HERE //

            // Close this window and open the Module Config Window
            // Would be nice if we could animate a window size change, and then populate it with all the new controls.
            // Switching directly to a new form looks pretty damn hideous with all that flash fade in animation and stuff.
            ModuleConfigWindow newWin = new ModuleConfigWindow();
            newWin.Show();
            this.Close();
        }
    }
}