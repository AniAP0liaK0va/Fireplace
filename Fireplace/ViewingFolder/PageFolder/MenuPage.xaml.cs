using Fireplace.AppDataFolder.ClassFolder;
using System.Windows;
using System.Windows.Controls;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ListUserPage());
        }
    }
}
