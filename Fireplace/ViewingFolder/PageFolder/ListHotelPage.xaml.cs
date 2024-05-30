using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Data.Entity;
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
            CityHotelComboBox.ItemsSource = AppConnectClass.connectDataBase_ACC.CityTable.ToList();
            TotalHotelTextBlock.Text = AppConnectClass.connectDataBase_ACC.HotelTable.ToList().Count().ToString();

        }
        #region _KeyDown
        private void SearchHotelTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { SearchHotelButton_Click(); }
        }
        #endregion
        #region _Click
        private void SearchHotelButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (string.IsNullOrWhiteSpace(SearchHotelTextBox.Text))
            {
                ListHotelListView.ItemsSource = AppConnectClass.connectDataBase_ACC.HotelTable.ToList();
            }
            else
            {
                var objects = AppConnectClass.connectDataBase_ACC.HotelTable.ToList();

                var SearchResults = objects.Where(hotelData =>
                    hotelData.Name_Hotel.IndexOf(SearchHotelTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    hotelData.StreetLocal_Hotel.IndexOf(SearchHotelTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    hotelData.HomeLocal_Hotel.ToString().IndexOf(SearchHotelTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    hotelData.NumberFloors_Hotel.ToString().IndexOf(SearchHotelTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);


                ListHotelListView.ItemsSource = SearchResults.ToList();
            }
        }

        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region _SelectionChanged
        private void ListHotelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CityHotelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityHotelComboBox.SelectedItem == null)
            {
                ListHotelListView.ItemsSource = AppConnectClass.connectDataBase_ACC.HotelTable.ToList();
            }
            else
            {
                ListHotelListView.ItemsSource =
                    AppConnectClass.connectDataBase_ACC.HotelTable.Where(cityHotelData =>
                    cityHotelData.CityTable.PersonalNumber_City == (int)CityHotelComboBox.SelectedValue).ToList();
            }
        }
        #endregion
    }
}
