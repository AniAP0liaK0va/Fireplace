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
    public partial class ListUserPage : Page
    {
        public UserTable dataContextUser;

        public ListUserPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            FrameNavigationClass.miniInformationUserFrame_FNC = MiniInformationUserFrame;
            Event_SmallCodeUpgrade();
        }
        #region Event_
        private void Event_SmallCodeUpgrade()
        {
            var userInformation = AppConnectClass.connectDataBase_ACC.UserTable.ToList();

            RoleUserComboBox.ItemsSource = AppConnectClass.connectDataBase_ACC.RoleUserTable.ToList();
            PaulUserComboBox.ItemsSource = AppConnectClass.connectDataBase_ACC.PaulTable.ToList();

            ListUserListView.ItemsSource = userInformation;
            TotalUserTextBlock.Text = userInformation.Count().ToString();
            TotalAdministratorTextBlock.Text = userInformation.Count(roleAdmin => roleAdmin.pnRole_User == 1).ToString();
            TotalGuestsTextBlock.Text = userInformation.Count(roleAdmin => roleAdmin.pnRole_User == 2).ToString();

            if (dataContextUser == null)
            {
                FrameNavigationClass.miniInformationUserFrame_FNC.Navigate(
                    new MiniInformationUserPage(userInformation.FirstOrDefault(dataUser => dataUser.PersonalNumber_User == AppConnectClass.receiveConnectUser_ACC)));
            }
        }
        #endregion
        #region _Click
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new AddEdditUserInformationPage(null));
        }
        private void ClearSearchButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            SearchUserTextBox.Text = null;
            PaulUserComboBox.Text = null;
            RoleUserComboBox.Text = null;

            ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
        }
        private void SearchUserButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            if (string.IsNullOrWhiteSpace(SearchUserTextBox.Text)) // Если SearchUserTextBox пустой
            {
                ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
            }
            else // Если же в SearchTextBox есть что-то, то:
            {
                var objects = AppConnectClass.connectDataBase_ACC.UserTable.Include(userPasportData => userPasportData.PasspordDataUserTable).ToList();

                var SearchResults = objects.Where(userdata =>
                    userdata.PasspordDataUserTable.Surname_PasspordDataUser.IndexOf(SearchUserTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    userdata.PasspordDataUserTable.Name_PasspordDataUser.IndexOf(SearchUserTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    userdata.PasspordDataUserTable.Middlename_PasspordDataUser.IndexOf(SearchUserTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);

                ListUserListView.ItemsSource = SearchResults.ToList();
            }
        }
        #endregion
        #region _SelectionChanged
        private void ListUserListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataContextUser = (UserTable)ListUserListView.SelectedItem;
            FrameNavigationClass.miniInformationUserFrame_FNC.Navigate(new MiniInformationUserPage(dataContextUser));
        }

        private void RoleUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleUserComboBox.SelectedItem == null)
            {
                ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
            }
            else
            {
                ListUserListView.ItemsSource =
                    AppConnectClass.connectDataBase_ACC.UserTable.Where(roleUser =>
                    roleUser.pnRole_User == (int)RoleUserComboBox.SelectedValue).ToList();
            }
        }

        private void PaulUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaulUserComboBox.SelectedItem == null)
            {
                ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
            }
            else
            {
                ListUserListView.ItemsSource =
                    AppConnectClass.connectDataBase_ACC.UserTable.Where(paulUser =>
                    paulUser.PasspordDataUserTable.pnPaul_PasspordDataUser == (int)PaulUserComboBox.SelectedValue).ToList();
            }
        }
        #endregion

        #region _KeyDown
        private void SearchUserTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { SearchUserButton_Click(); }
        }
        #endregion

        private void ListUserListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new AddEdditUserInformationPage((UserTable)ListUserListView.SelectedItem));
        }
    }
}
