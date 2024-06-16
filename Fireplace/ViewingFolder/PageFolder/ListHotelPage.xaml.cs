using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class ListHotelPage : Page
    {
        private Timer getTimer;
        private double currentRotationAngle = 0;

        public ListHotelPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            getTimer = new Timer(Event_UpdateAnimation, null, Timeout.Infinite, 100);
            Event_LoadData();
        }
        #region _Click
        private void AddHotelButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Сделать страницу добавления отелей
        }

        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ListHotelPage());
            //***
            // ДА да, переоткрытие страницы - это не правильно, но это единственный вариант, так как нет времяни, и нет желания мучеться
            //***
        }
        #endregion
        #region _SelectionChanged _MouseDoubleClick
        private void ListHotelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new InformationHotelPage((HotelTable)ListHotelListView.SelectedItem));
        }

        private void ListHotelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: Сделать страницу редактирования отеля (страница добавления)
        }

        private void SearchHotelTextBox_SelectionChanged(object sender, RoutedEventArgs e) { Event_FilterHotels(); }

        private void CityHotelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { Event_FilterHotels(); }
        #endregion
        #region Event_
        private async void Event_FilterHotels()
        {
            try
            {
                Event_StartAnimation();

                var filteredHotels = AppConnectClass.connectDataBase_ACC.HotelTable.AsQueryable();

                if (CityHotelComboBox.SelectedIndex > 0)
                {
                    var selectedCityHotel = CityHotelComboBox.SelectedItem as CityTable;
                    if (selectedCityHotel != null)
                    {
                        filteredHotels = filteredHotels.Where(hotel =>
                            hotel.pnCityLocal_Hotel == selectedCityHotel.PersonalNumber_City);
                    }
                }

                var searchText = SearchHotelTextBox.Text?.ToLower();
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    filteredHotels = filteredHotels.Where(hotel =>
                        hotel.Name_Hotel.ToLower().Contains(searchText) ||
                        hotel.StreetLocal_Hotel.ToLower().Contains(searchText) ||
                        hotel.HomeLocal_Hotel.ToString().ToLower().Contains(searchText) ||
                        hotel.NumberFloors_Hotel.ToString().ToLower().Contains(searchText));
                }

                var filteredHotelsList = await Task.Run(() => filteredHotels.ToList());
                ListHotelListView.ItemsSource = filteredHotelsList;
            }
            catch (Exception exEvent_FilterHotels)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие exEvent_FilterHotels в ListHotelPage:\n\n " +
                    $"{exEvent_FilterHotels.Message}");
            }
            finally
            {
                Event_StopAnimation();
            }
        }

        private async void Event_LoadData()
        {
            try
            {
                Event_StartAnimation();

                // Асинхронная загрузка данных из базы данных
                var hotels = await AppConnectClass.connectDataBase_ACC.HotelTable.ToListAsync();
                var hotelCount = await AppConnectClass.connectDataBase_ACC.HotelTable.CountAsync();
                var cityHotelItems = new List<CityTable>
                {
                    new CityTable { PersonalNumber_City = AppConnectClass.PersonalNumberTitleNullDataComboBox, Name_City = AppConnectClass.TitleNullDataComboBox }
                };
                cityHotelItems.AddRange(await AppConnectClass.connectDataBase_ACC.CityTable.ToListAsync());

                // Обновление элементов управления после завершения асинхронных операций
                ListHotelListView.ItemsSource = hotels;
                TotalHotelTextBlock.Text = hotelCount.ToString();
                CityHotelComboBox.ItemsSource = cityHotelItems;
            }
            catch (Exception exEvent_LoadData)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                   textMessage: $"Событие exEvent_LoadData в ListHotelPage:\n\n " +
                   $"{exEvent_LoadData.Message}");
            }
            finally
            {
                Event_StopAnimation();
            }
        }

        private void Event_UpdateAnimation(object state)
        {
            currentRotationAngle += 10;

            if (currentRotationAngle >= 360)
            {
                currentRotationAngle = 0;
            }

            Dispatcher.Invoke(() =>
            {
                RotateTransform loadingAnimation = new RotateTransform();
                loadingAnimation.Angle = currentRotationAngle;
                LoadingSpinnerTextBlock.RenderTransformOrigin = new Point(0.5, 0.5);
                LoadingSpinnerTextBlock.RenderTransform = loadingAnimation;
            });
        }

        private void Event_StartAnimation()
        {
            Dispatcher.Invoke(() =>
            {
                LoadingSpinnerTextBlock.Visibility = Visibility.Visible;
            });
            getTimer.Change(0, 20);
        }

        private void Event_StopAnimation()
        {
            Dispatcher.Invoke(() =>
            {
                LoadingSpinnerTextBlock.Visibility = Visibility.Collapsed;
            });

            getTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        #endregion
    }
}
