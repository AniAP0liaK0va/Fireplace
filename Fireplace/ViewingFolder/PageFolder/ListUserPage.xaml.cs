using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fireplace.ViewingFolder.PageFolder
{
   public partial class ListUserPage : Page
    {
        public ListUserPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
        }
        #region _Click
        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchUserTextBox.Clear();
            PaulUserComboBox.Items.Clear();
            RoleUserComboBox.Items.Clear();
        }
        #endregion
    }
}
