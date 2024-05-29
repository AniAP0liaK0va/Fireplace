using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using Fireplace.ViewingFolder.PageFolder;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Fireplace.ViewingFolder.WindowFolder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Event_ConnectPage();

            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            var informationOutputUser = AppConnectClass.connectDataBase_ACC.UserTable.FirstOrDefault(
                    dataUser => dataUser.PersonalNumber_User == AppConnectClass.receiveConnectUser_ACC.ToString());

            DataContext = informationOutputUser;
            ConclusionSNMTextBlock.Text = 
                $"{informationOutputUser.PasspordDataUserTable.Surname_PasspordDataUser} " +
                $"{informationOutputUser.PasspordDataUserTable.Name_PasspordDataUser[0]}. " +
                $"{informationOutputUser.PasspordDataUserTable.Middlename_PasspordDataUser[0]}.";
        }
        #region _Click
        private void MainProfilButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ProfilPage(AppConnectClass.connectDataBase_ACC.UserTable.FirstOrDefault(dataUser => 
            dataUser.PersonalNumber_User == AppConnectClass.userWithChangeablePassword_ACC)));
        }
        #endregion
        #region Управление окном
        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ControlPanelGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }

        private void ToСloseButton_Click(object sender, RoutedEventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
        }
        #endregion
        #region Event_
        private void Event_ConnectPage()
        {
            FrameNavigationClass.menuFrame_FNC = MenuFrame;
            FrameNavigationClass.bodyFrame_FNC = BodyFrame;
            FrameNavigationClass.menuFrame_FNC.Navigate(new MenuPage());
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ListUserPage());
        }
        #endregion
        #region Анимация
        private void MenuAnimationBorderr_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.To = 74;
            heightAnimation.Duration = TimeSpan.FromSeconds(0.2);
            MenuAnimationBorder.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void MenuAnimationBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.To = 10;
            heightAnimation.Duration = TimeSpan.FromSeconds(0.2);
            MenuAnimationBorder.BeginAnimation(HeightProperty, heightAnimation);
        }
        #endregion
    }
}
