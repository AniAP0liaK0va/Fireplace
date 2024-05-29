using Fireplace.AppDataFolder.ClassFolder;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class UniversalPage : Page
    {
        private int randomeTime;
        private DateTime startTime;
        private DispatcherTimer dispatcherTimer;
        Random random = new Random();

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
            randomeTime = random.Next(10, 31); // Генерация случайного времени от 10 до 30 секунд
            startTime = DateTime.Now;

            dispatcherTimer.Start();
        }
        #endregion
        #region Event_
        private void Event_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
                double percentage = (5000 / 10000) * 100;
                string data =
                            $"Время начала {startTime}\n" +
                            $"Рандомное время {randomeTime}\n" +
                            $"elapsedSeconds {elapsedSeconds}\n" +
                            $"percentage {percentage}";

                if (percentage >= 100)
                {
                    dispatcherTimer.Stop();
                }

                hsfdgkfjs.Text = data;

                // CheckUppdateProgressBar.Value = percentage;
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
