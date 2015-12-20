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
using System.Windows.Media.Animation;

namespace SimpleFOMOD
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow
    {
        // List of all controls on the page.
        public List<Control> aboutControlList = new List<Control>();
        public List<Control> helpControlList = new List<Control>();
        
        public HelpWindow()
        {            
            InitializeComponent();

            aboutControlList.Add(lblAuthors); aboutControlList.Add(lblVersion);
            helpControlList.Add(lblKeys);

            lblVersion.Content = "Version: " + MainWindow.currentVersion;
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TabHelp != null && TabHelp.IsSelected)
            {
                if (lblFeedback.Opacity != 1)
                {
                    // Variables
                    double AnimationLength = 0.5;
                    int AnimationStart = 100;
                    int AnimationGap = 185;

                    // Fade in Animation.
                    DoubleAnimation db = new DoubleAnimation();
                    db.From = 0;
                    db.To = 1;
                    db.Duration = new Duration(TimeSpan.FromSeconds(AnimationLength));
                    db.BeginTime = TimeSpan.FromMilliseconds(AnimationStart);
                    lblFeedback.BeginAnimation(OpacityProperty, db);

                    foreach (var control in helpControlList)
                    {
                        AnimationStart = AnimationStart + AnimationGap;
                        db.BeginTime = TimeSpan.FromMilliseconds(AnimationStart);
                        control.BeginAnimation(OpacityProperty, db);
                    }
                }
            }

            if(TabAbout != null && TabAbout.IsSelected)
            {

                if (imgLogo.Opacity != 1)
                {
                    // Variables
                    double AnimationLength = 0.5;
                    int AnimationStart = 100;
                    int AnimationGap = 185;

                    // Fade in Animation.
                    DoubleAnimation da = new DoubleAnimation();
                    da.From = 0;
                    da.To = 1;
                    da.Duration = new Duration(TimeSpan.FromSeconds(AnimationLength));
                    da.BeginTime = TimeSpan.FromMilliseconds(AnimationStart);
                    imgLogo.BeginAnimation(OpacityProperty, da);

                    foreach (var control in aboutControlList)
                    {
                        AnimationStart = AnimationStart + AnimationGap;
                        da.BeginTime = TimeSpan.FromMilliseconds(AnimationStart);
                        control.BeginAnimation(OpacityProperty, da);
                    }
                }
            }

        }
    }
}
