using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System.Windows.Controls;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class MiniInformationUserPage : Page
    {
        public UserTable dataContextUserInformationScan;

        public MiniInformationUserPage(UserTable userTable)
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            DataContext = userTable;
            dataContextUserInformationScan = userTable;
        }

        private void FullInformationUserButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new ProfilPage(dataContextUserInformationScan));
        }
    }
}
