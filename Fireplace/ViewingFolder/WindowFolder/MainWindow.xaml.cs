using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Fireplace.ViewingFolder.WindowFolder
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

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
            if
    }
}
