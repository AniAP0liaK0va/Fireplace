using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class ListHotelPage : Page
    {
        public ListHotelPage()
        {
            InitializeComponent();

            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            ListHotelListView.ItemsSource = AppConnectClass.connectDataBase_ACC.HotelTable.ToList();
            TotalHotelTextBlock.Text = AppConnectClass.connectDataBase_ACC.HotelTable.Count().ToString();

            var cityHotelItems = new List<CityTable> { new CityTable { PersonalNumber_City = AppConnectClass.PersonalNumberTitleNullDataComboBox, Name_City = AppConnectClass.TitleNullDataComboBox } };
            cityHotelItems.AddRange(AppConnectClass.connectDataBase_ACC.CityTable.ToList());
            CityHotelComboBox.ItemsSource = cityHotelItems;
        }
        #region _Click
        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchHotelTextBox.Text = null;
            CityHotelComboBox.Text = null;
        }
        #endregion
        #region _SelectionChanged _MouseDoubleClick
        private void ListHotelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new InformationHotelPage((HotelTable)ListHotelListView.SelectedItem));
        }

        private void ListHotelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchHotelTextBox_SelectionChanged(object sender, RoutedEventArgs e) { FilterHotels(); }

        private void CityHotelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { FilterHotels(); }
        #endregion
        private void FilterHotels()
        {
            var filteredHotels = AppConnectClass.connectDataBase_ACC.HotelTable.AsQueryable();

            if (CityHotelComboBox.SelectedIndex > 0)
            {
                var selectedCityHotel = CityHotelComboBox.SelectedItem as CityTable;
                if (selectedCityHotel != null)
                {
                    filteredHotels = filteredHotels.Where(hotel => hotel.CityTable.PersonalNumber_City == selectedCityHotel.PersonalNumber_City);
                }
            }

            var searchText = SearchHotelTextBox.Text.ToLower();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredHotels = filteredHotels.Where(hotel =>
                    hotel.Name_Hotel.ToLower().Contains(searchText) ||
                    hotel.StreetLocal_Hotel.ToLower().Contains(searchText) ||
                    hotel.HomeLocal_Hotel.ToString().ToLower().Contains(searchText) ||
                    hotel.NumberFloors_Hotel.ToString().ToLower().Contains(searchText));
            }

            ListHotelListView.ItemsSource = filteredHotels.ToList();
        }
    }
}
