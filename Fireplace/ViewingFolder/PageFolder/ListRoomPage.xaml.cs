using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
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
            if (e.Key == Key.Enter) { SearchRoomButton_Click(); }
        }
        #endregion
        #region _Click
        private void SearchRoomButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (string.IsNullOrWhiteSpace(SearchRoomTextBox.Text))
            {
                ListRoomListView.ItemsSource = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.ToList();
            }
            else
            {
                var objects = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.ToList();

                var SearchResults = objects.Where(roomData =>
                    roomData.DiscountRoomTable.Title_Discount.ToString().IndexOf(SearchRoomTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    roomData.TypeRoomTable.Title_TypeRoom.IndexOf(SearchRoomTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    roomData.BusyRoomTable.Title_Busy.IndexOf(SearchRoomTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    roomData.ClassRoomHotelTable.Title_ClassRoomHotel.IndexOf(SearchRoomTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    roomData.PresenceBathroomRoomTable.Title_PresenceBathroomRoom.IndexOf(SearchRoomTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    roomData.NumberRoom_RoomsHottel.ToString().IndexOf(SearchRoomTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    roomData.NumberRooms_RoomHotel.ToString().IndexOf(SearchRoomTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);


                ListRoomListView.ItemsSource = SearchResults.ToList();
            }
        }
        private void BathroomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            BathroomComboBox.Text = null;
            ClassRoomComboBox.Text = null;
            BusyRoomComboBox.Text = null;
            DiscountRoomComboBox.Text = null;
            SearchRoomTextBox.Text = null;
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region Event_

        #endregion
    }
}
