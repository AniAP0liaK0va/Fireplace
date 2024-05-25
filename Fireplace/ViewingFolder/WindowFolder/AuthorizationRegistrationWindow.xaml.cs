using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.ViewingFolder.PageFolder;
using Fireplace.ViewingFolder.PageFolder.ManagementPageFolder;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Fireplace.ViewingFolder.WindowFolder
{
    public partial class AuthorizationRegistrationWindow : Window
    {
        public AuthorizationRegistrationWindow()
        {
            InitializeComponent();
            Event_ConnectPage();
        }

        #region Event_
        private void Event_ConnectPage()
        {
            FrameNavigationClass.mainFarme_FNC = MainFrame;
            FrameNavigationClass.mainFarme_FNC.Navigate(new AuthorizationPage());
        }
        #endregion

        #region _Click
        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ToСloseButton_Click(object sender, RoutedEventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            currentProcess.Kill();
        }
        #endregion

        private void ControlPanelGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }
    }
}
