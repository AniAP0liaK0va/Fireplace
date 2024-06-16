using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Data.Entity;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class ProfilPage : Page
    {
        private Timer getTimer;
        private double currentRotationAngle = 0;
        UserTable dataUserTable;

        public ProfilPage(UserTable userTable)
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            getTimer = new Timer(Event_UpdateAnimation, null, Timeout.Infinite, 100);
            dataUserTable = userTable;
            Event_WhichProfileShouldIOpen();
        }
        #region Event_
        private void Event_UpdateAnimation(object state)
        {
            currentRotationAngle += 10;

            if (currentRotationAngle >= 360)
            {
                currentRotationAngle = 0;
            }

            Dispatcher.Invoke(() =>
            {
                RotateTransform loadingAnimation = new RotateTransform();
                loadingAnimation.Angle = currentRotationAngle;
                LoadingSpinnerTextBlock.RenderTransformOrigin = new Point(0.5, 0.5);
                LoadingSpinnerTextBlock.RenderTransform = loadingAnimation;
            });
        }

        private void Event_StartAnimation()
        {
            Dispatcher.Invoke(() =>
            {
                LoadintgStackPanel.Visibility = Visibility.Visible;
            });
            getTimer.Change(0, 20);
        }

        private void Event_StopAnimation()
        {
            Dispatcher.Invoke(() =>
            {
                LoadintgStackPanel.Visibility = Visibility.Collapsed;
            });

            getTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        private async void Event_WhichProfileShouldIOpen()
        {
            try
            {
                Event_StartAnimation();
                if (dataUserTable == null)
                {
                    DataContext = await AppConnectClass.connectDataBase_ACC.UserTable.FirstOrDefaultAsync(infoUser => infoUser.PersonalNumber_User == AppConnectClass.receiveConnectUser_ACC);
                }
                else
                {
                    DataContext = dataUserTable;
                }
            }
            catch (Exception exEvent_WhichProfileShouldIOpen)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие exEvent_WhichProfileShouldIOpen в ProfilPage:\n\n " +
                    $"{exEvent_WhichProfileShouldIOpen.Message}");
            }
            finally
            {
                Event_StopAnimation();
            }
        }
        #endregion
    }
}
