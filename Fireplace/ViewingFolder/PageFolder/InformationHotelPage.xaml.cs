using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class InformationHotelPage : Page
    {
        public InformationHotelPage(HotelTable hotelTable)
        {
            InitializeComponent();

            // Создаем список с элементом "Не выбрано" и добавляем данные
            var classRoomItems = new List<ClassRoomHotelTable> { new ClassRoomHotelTable { PersonalNumber_ClassRoomHotel = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_ClassRoomHotel = AppConnectClass.TitleNullDataComboBox } };
            classRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.ClassRoomHotelTable.ToList());
            ClassRoomFilterComboBox.ItemsSource = classRoomItems;

            var busyRoomItems = new List<BusyRoomTable> { new BusyRoomTable { PersonalNumber_Busy = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_Busy = AppConnectClass.TitleNullDataComboBox } };
            busyRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.BusyRoomTable.ToList());
            BusyRoomFilterComboBox.ItemsSource = busyRoomItems;

            var typeRoomItems = new List<TypeRoomTable> { new TypeRoomTable { PersonalNumber_TypeRoom = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_TypeRoom = AppConnectClass.TitleNullDataComboBox } };
            typeRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.TypeRoomTable.ToList());
            TypeRoomFilterComboBox.ItemsSource = typeRoomItems;

            var presenceBathroomRoomItems = new List<PresenceBathroomRoomTable> { new PresenceBathroomRoomTable { PersonalNumber_PresenceBathroomRoom = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_PresenceBathroomRoom = AppConnectClass.TitleNullDataComboBox } };
            presenceBathroomRoomItems.AddRange(AppConnectClass.connectDataBase_ACC.PresenceBathroomRoomTable.ToList());
            PresenceBathroomRoomFilterComboBox.ItemsSource = presenceBathroomRoomItems;

            var discountItems = new List<DiscountRoomTable> { new DiscountRoomTable { PersonalNumber_Discount = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_Discount = 100 } };
            discountItems.AddRange(AppConnectClass.connectDataBase_ACC.DiscountRoomTable.ToList());
            DiscountFilterComboBox.ItemsSource = discountItems;

            if (hotelTable != null)
            {
                DataContext = hotelTable;

                AppConnectClass.connectDataBase_ACC = new FireplaceEntities();

                AppConnectClass.connectDataBase_ACC.HotelTable.Include(dataRoom => dataRoom.RoomsHotelTable).Load();
                var includeDate = AppConnectClass.connectDataBase_ACC.HotelTable.Find(hotelTable.PersonalNumber_Hotel).RoomsHotelTable;

                ListRoomListView.ItemsSource = includeDate;
                NumberOfOccupiedRoomsTextBlock.Text = includeDate.Count(countData => countData.pnBusy_RoomHotel == 1).ToString();
                NumberOfAvailableRoomsTextBlock.Text = includeDate.Count(countData => countData.pnBusy_RoomHotel == 2).ToString();
            }
            else
            {
                FrameNavigationClass.bodyFrame_FNC.Navigate(new ListHotelPage());
            }
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var hotelTable = DataContext as HotelTable;
            if (hotelTable == null) return;

            var filteredRooms = AppConnectClass.connectDataBase_ACC.HotelTable.Find(hotelTable.PersonalNumber_Hotel).RoomsHotelTable.AsQueryable();

            if (ClassRoomFilterComboBox.SelectedIndex > 0)
            {
                var selectedClassRoom = ClassRoomFilterComboBox.SelectedItem as ClassRoomHotelTable;
                if (selectedClassRoom != null)
                {
                    filteredRooms = filteredRooms.Where(room => room.pnClassRoomHottel_RoomHotel == selectedClassRoom.PersonalNumber_ClassRoomHotel);
                }
            }

            if (BusyRoomFilterComboBox.SelectedIndex > 0)
            {
                var selectedBusyRoom = BusyRoomFilterComboBox.SelectedItem as BusyRoomTable;
                if (selectedBusyRoom != null)
                {
                    filteredRooms = filteredRooms.Where(room => room.pnBusy_RoomHotel == selectedBusyRoom.PersonalNumber_Busy);
                }
            }

            if (TypeRoomFilterComboBox.SelectedIndex > 0)
            {
                var selectedTypeRoom = TypeRoomFilterComboBox.SelectedItem as TypeRoomTable;
                if (selectedTypeRoom != null)
                {
                    filteredRooms = filteredRooms.Where(room => room.pnType_RoomHotel == selectedTypeRoom.PersonalNumber_TypeRoom);
                }
            }

            if (PresenceBathroomRoomFilterComboBox.SelectedIndex > 0)
            {
                var selectedPresenceBathroomRoom = PresenceBathroomRoomFilterComboBox.SelectedItem as PresenceBathroomRoomTable;
                if (selectedPresenceBathroomRoom != null)
                {
                    filteredRooms = filteredRooms.Where(room => room.pnPresenceBathroom_RoomHotel == selectedPresenceBathroomRoom.PersonalNumber_PresenceBathroomRoom);
                }
            }

            if (DiscountFilterComboBox.SelectedIndex > 0)
            {
                var selectedDiscountRoom = DiscountFilterComboBox.SelectedItem as DiscountRoomTable;
                if (selectedDiscountRoom != null)
                {
                    filteredRooms = filteredRooms.Where(room => room.pnDiscount_RoomHotel == selectedDiscountRoom.PersonalNumber_Discount);
                }
            }

            ListRoomListView.ItemsSource = filteredRooms.ToList();
        }
    }
}
