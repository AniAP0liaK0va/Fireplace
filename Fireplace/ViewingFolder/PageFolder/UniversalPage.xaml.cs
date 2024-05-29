using Fireplace.AppDataFolder.ClassFolder;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class UniversalPage : Page
    {
        private int randomeTime;
        private DateTime startTime;
        private DispatcherTimer dispatcherTimer;

        public UniversalPage()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimer.Tick += Event_Timer_Tick;
        }


        #region _Click
        private void CheckUppdateButton_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            randomeTime = random.Next(4, 16); // Генерация случайного времени
            startTime = DateTime.Now;

            dispatcherTimer.Start();
            CheckUppdateButton.IsEnabled = false;
        }
        #endregion
        #region Event_
        private void Event_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
                int percentage = (int)(elapsedSeconds / (float)randomeTime * 100);

                CheckUppdateProgressBar.Value = percentage;

                if (percentage >= 100)
                {
                    dispatcherTimer.Stop();
                    CheckUppdateButton.IsEnabled = true;
                    MessageBoxClass.GoodMessageBox_MBC(textMessage: "У вас стоит последняя версия V123.333");
                    CheckUppdateProgressBar.Value = 0;
                }
            }
            catch (Exception exEvent_Timer_Tick)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                   textMessage: $"Событие Event_Timer_Tick в UpdateApplicationPage:\n\n " +
                   $"{exEvent_Timer_Tick.Message}");
            }
        }
        #endregion
        #region _Click
        private void TransitionGitHubButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/AniAP0liaK0va/Fireplace");
        }
        #endregion
    }
}
