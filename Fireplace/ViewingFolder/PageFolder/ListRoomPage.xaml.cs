using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class ListRoomPage : Page
    {
        public RoomsHotelTable dataContextRoomsHotel;

        public ListRoomPage()
        {
            InitializeComponent();
            Event_SmallCodeUpgrade();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            ListRoomListView.ItemsSource = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.ToList();
            TotalRoomTextBlock.Text = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.ToList().Count().ToString();
        }

        #region _SelectionChanged _MouseDoubleClick
        private void ListRoomListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new AddEdditRoomPage(dataContextRoomsHotel));
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event_FilterUsers();
        }

        private void SearchRoomTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Event_FilterUsers();
        }

        private void ListRoomListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataContextRoomsHotel = (RoomsHotelTable)ListRoomListView.SelectedItem;
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
            FrameNavigationClass.bodyFrame_FNC.Navigate(new AddEdditRoomPage(null));
        }
        #endregion
        #region Event_
        private void Event_SmallCodeUpgrade()
        {
            var bathroomRoomItems = new List<PresenceBathroomRoomTable> { new PresenceBathroomRoomTable { PersonalNumber_PresenceBathroomRoom = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_PresenceBathroomRoom = AppConnectClass.TitleNullDataComboBox } };
            var classRoomItems = new List<ClassRoomHotelTable> { new ClassRoomHotelTable { PersonalNumber_ClassRoomHotel = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_ClassRoomHotel = AppConnectClass.TitleNullDataComboBox } };
            var busyRoomItems = new List<BusyRoomTable> { new BusyRoomTable { PersonalNumber_Busy = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_Busy = AppConnectClass.TitleNullDataComboBox } };
            var discountbathroomRoomItems = new List<DiscountRoomTable> { new DiscountRoomTable { PersonalNumber_Discount = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_Discount = 100 } };

            bathroomRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.PresenceBathroomRoomTable.ToList());
            classRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.ClassRoomHotelTable.ToList());
            busyRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.BusyRoomTable.ToList());
            discountbathroomRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.DiscountRoomTable.ToList());

            BathroomComboBox.ItemsSource = bathroomRoomItems;
            ClassRoomComboBox.ItemsSource = classRoomItems;
            BusyRoomComboBox.ItemsSource = busyRoomItems;
            DiscountRoomComboBox.ItemsSource = discountbathroomRoomItems;

        }

        private void Event_FilterUsers()
        {
            var filteredRoom = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.AsQueryable();

            var selectedBathroomRoom = BathroomComboBox.SelectedItem as PresenceBathroomRoomTable;
            var selectedClassRoom = ClassRoomComboBox.SelectedItem as ClassRoomHotelTable;
            var selectedBusyRoom = BusyRoomComboBox.SelectedItem as BusyRoomTable;
            var selectedDiscountRoom = DiscountRoomComboBox.SelectedItem as DiscountRoomTable;

            var searchText = SearchRoomTextBox.Text.ToLower();

            if (BathroomComboBox.SelectedIndex > 0 && selectedBathroomRoom != null)
            {
                filteredRoom = filteredRoom.Where(bathroomRoomData =>
                bathroomRoomData.pnPresenceBathroom_RoomHotel == selectedBathroomRoom.PersonalNumber_PresenceBathroomRoom);
            }

            if (ClassRoomComboBox.SelectedIndex > 0 && selectedClassRoom != null)
            {
                filteredRoom = filteredRoom.Where(classRoomData =>
                classRoomData.pnClassRoomHottel_RoomHotel == selectedClassRoom.PersonalNumber_ClassRoomHotel);
            }

            if (BusyRoomComboBox.SelectedIndex > 0 && selectedBusyRoom != null)
            {
                filteredRoom = filteredRoom.Where(busyRoomData =>
                busyRoomData.pnBusy_RoomHotel == selectedBusyRoom.PersonalNumber_Busy);
            }

            if (DiscountRoomComboBox.SelectedIndex > 0 && selectedDiscountRoom != null)
            {
                filteredRoom = filteredRoom.Where(discountRoomData =>
                discountRoomData.pnDiscount_RoomHotel == selectedDiscountRoom.PersonalNumber_Discount);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredRoom = filteredRoom.Where(dataRoom =>
                    dataRoom.Cost_RoomHotel.ToString().ToLower().Contains(searchText) ||
                    dataRoom.FloorHottel_RoomsHotel.ToString().ToLower().Contains(searchText) ||
                    dataRoom.NumberRoom_RoomsHottel.ToString().ToLower().Contains(searchText));
            }

            ListRoomListView.ItemsSource = filteredRoom.ToList();
        }
        #endregion
    }
}
