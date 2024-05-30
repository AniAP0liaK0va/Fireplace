using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class ListRoomPage : Page
    {
        public ListRoomPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            ListRoomListView.ItemsSource = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.ToList();
            TotalRoomTextBlock.Text = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.ToList().Count().ToString();
        }

        #region _SelectionChanged
        private void ListRoomListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ClassRoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BusyRoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DiscountRoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion
        #region _KeyDown
        private void SearchRoomTextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion
        #region _Click
        private void SearchRoomButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BathroomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region Event_

        #endregion
    }
}
