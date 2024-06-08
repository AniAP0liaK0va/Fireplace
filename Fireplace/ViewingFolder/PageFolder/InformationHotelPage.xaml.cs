using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System.Data.Entity;
using System.Windows.Controls;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class InformationHotelPage : Page
    {
        public InformationHotelPage(HotelTable hotelTable)
        {
            InitializeComponent();
            if (hotelTable != null)
            {
                AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
                AppConnectClass.connectDataBase_ACC.HotelTable.Include(dataRoom => dataRoom.RoomsHotelTable).Load();
                ListRoomListView.ItemsSource = AppConnectClass.connectDataBase_ACC.HotelTable.Find(hotelTable.PersonalNumber_Hotel).RoomsHotelTable;
            }
            else
            {
                FrameNavigationClass.bodyFrame_FNC.Navigate(new ListHotelPage());
            }
        }
    }
}
