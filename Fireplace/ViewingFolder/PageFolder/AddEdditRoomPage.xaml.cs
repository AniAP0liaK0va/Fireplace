using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Controls;



namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class AddEdditRoomPage : Page
    {
        string validationStringDate;

        public AddEdditRoomPage(RoomsHotelTable roomsHotelTable)
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            Event_SettingComboBox();
            ListHotelListView.ItemsSource = AppConnectClass.connectDataBase_ACC.HotelTable;

            if (roomsHotelTable != null)
            {
                DataContext = roomsHotelTable;
                SaveButton.Content = "Сохранить изменения";

                AppConnectClass.connectDataBase_ACC.RoomsHotelTable.Include(dataRoom => dataRoom.HotelTable).Load();
                var includeDate = AppConnectClass.connectDataBase_ACC.RoomsHotelTable.Find(roomsHotelTable.PersonalNumber_RoomHotel).HotelTable;

                ListHotelListView.SelectedItem = includeDate;
            }
            else
            {
                SaveButton.Content = "Добавить";
            }
        }
        #region Event_
        private void Event_AddorUpdateRoomsHotel()
        {
            Event_ValidationData();
            if (validationStringDate == null)
            {
                RoomsHotelTable addorUpdateRoomsHotelTable = new RoomsHotelTable();
                addorUpdateRoomsHotelTable.NumberRooms_RoomHotel = Convert.ToInt32(NumberRoomsTextBox.Text);
                addorUpdateRoomsHotelTable.TotalArea_RoomHotel = Convert.ToInt32(TotalAreaTextBox.Text);
                addorUpdateRoomsHotelTable.Cost_RoomHotel = Convert.ToInt32(CostTextBox.Text);
                addorUpdateRoomsHotelTable.FloorHottel_RoomsHotel = Convert.ToInt32(FloorHottelTextBox.Text);
                addorUpdateRoomsHotelTable.NumberRoom_RoomsHottel = Convert.ToInt32(NumberRoomTextBox.Text);
                addorUpdateRoomsHotelTable.pnPresenceBathroom_RoomHotel = (PresenceBathroomComboBox.SelectedItem as PresenceBathroomRoomTable).PersonalNumber_PresenceBathroomRoom;
                addorUpdateRoomsHotelTable.pnType_RoomHotel = (TypeRoomComboBox.SelectedItem as TypeRoomTable).PersonalNumber_TypeRoom;
                addorUpdateRoomsHotelTable.pnBusy_RoomHotel = (BusyRoomComboBox.SelectedItem as BusyRoomTable).PersonalNumber_Busy;
                addorUpdateRoomsHotelTable.pnDiscount_RoomHotel = (DiscountComboBox.SelectedItem as DiscountRoomTable).PersonalNumber_Discount;
                addorUpdateRoomsHotelTable.pnClassRoomHottel_RoomHotel = (ClassRoomComboBox.SelectedItem as ClassRoomHotelTable).PersonalNumber_ClassRoomHotel;
                AppConnectClass.connectDataBase_ACC.RoomsHotelTable.AddOrUpdate(addorUpdateRoomsHotelTable);
            }
            else
            {
                MessageBoxClass.ExceptionMessageBox_MBC(textMessage: validationStringDate);
                validationStringDate = null;
            }
        }

        private void Event_ValidationData()
        {
            if (string.IsNullOrWhiteSpace(NumberRoomsTextBox.Text)) validationStringDate += "Вы не указали номер комнаты\n";
            if (string.IsNullOrWhiteSpace(TotalAreaTextBox.Text)) validationStringDate += "Вы не указали общую площадь\n";
            if (string.IsNullOrWhiteSpace(CostTextBox.Text)) validationStringDate += "Вы не указали цену\n";
            if (string.IsNullOrWhiteSpace(PresenceBathroomComboBox.Text)) validationStringDate += "Вы не указали тип туалетной комнаты\n";
            if (string.IsNullOrWhiteSpace(TypeRoomComboBox.Text)) validationStringDate += "Вы не указали тип комнаты\n";
            if (string.IsNullOrWhiteSpace(FloorHottelTextBox.Text)) validationStringDate += "Вы не указали на каком этаже находится\n";
            if (string.IsNullOrWhiteSpace(NumberRoomTextBox.Text)) validationStringDate += "Вы не указали количество помещений\n";
            if (string.IsNullOrWhiteSpace(DiscountComboBox.Text)) validationStringDate += "Вы не указали скидку\n";
            if (string.IsNullOrWhiteSpace(ClassRoomComboBox.Text)) validationStringDate += "Вы не указали класс\n";
            if (string.IsNullOrWhiteSpace(BusyRoomComboBox.Text)) validationStringDate += "Вы не указали занята ли комната\n";
        }

        private void Event_SettingComboBox()
        {
            var typeRoomComboBoxItems = new List<TypeRoomTable> { new TypeRoomTable { PersonalNumber_TypeRoom = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_TypeRoom = AppConnectClass.TitleNullDataComboBox } };
            var busyRoomComboBoxItems = new List<BusyRoomTable> { new BusyRoomTable { PersonalNumber_Busy = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_Busy = AppConnectClass.TitleNullDataComboBox } };
            var discountComboBoxItems = new List<DiscountRoomTable> { new DiscountRoomTable { PersonalNumber_Discount = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_Discount = 100 } };
            var classRoomComboBoxItems = new List<ClassRoomHotelTable> { new ClassRoomHotelTable { PersonalNumber_ClassRoomHotel = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_ClassRoomHotel = AppConnectClass.TitleNullDataComboBox } };
            var presenceBathroomComboBoxItems = new List<PresenceBathroomRoomTable> { new PresenceBathroomRoomTable { PersonalNumber_PresenceBathroomRoom = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_PresenceBathroomRoom = AppConnectClass.TitleNullDataComboBox } };

            typeRoomComboBoxItems.AddRange(AppConnectClass.connectDataBase_ACC.TypeRoomTable.ToList());
            busyRoomComboBoxItems.AddRange(AppConnectClass.connectDataBase_ACC.BusyRoomTable.ToList());
            discountComboBoxItems.AddRange(AppConnectClass.connectDataBase_ACC.DiscountRoomTable.ToList());
            classRoomComboBoxItems.AddRange(AppConnectClass.connectDataBase_ACC.ClassRoomHotelTable.ToList());
            presenceBathroomComboBoxItems.AddRange(AppConnectClass.connectDataBase_ACC.PresenceBathroomRoomTable.ToList());

            TypeRoomComboBox.ItemsSource = typeRoomComboBoxItems;
            BusyRoomComboBox.ItemsSource = busyRoomComboBoxItems;
            DiscountComboBox.ItemsSource = discountComboBoxItems;
            ClassRoomComboBox.ItemsSource = classRoomComboBoxItems;
            PresenceBathroomComboBox.ItemsSource = presenceBathroomComboBoxItems;
        }
        #endregion

        private void ListHotelListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
