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
        #region _Click
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ListUserPage());
        }

        private void UniversalButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new UniversalPage());
        }

        private void RoomsButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ListRoomPage());
        }

        private void HottelButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ListHotelPage());
        }
        #endregion
    }
}
