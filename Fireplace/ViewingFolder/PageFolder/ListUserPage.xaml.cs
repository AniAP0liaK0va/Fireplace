using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
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
        private void Event_FilterUsers()
        {
            var filteredUser = AppConnectClass.connectDataBase_ACC.UserTable.AsQueryable();
            var selectedRoleUser = RoleUserComboBox.SelectedItem as RoleUserTable;
            var selectedPaulUser = PaulUserComboBox.SelectedItem as PaulTable;
            var searchText = SearchUserTextBox.Text.ToLower();

            if (RoleUserComboBox.SelectedIndex > 0 && selectedRoleUser != null)
            {
                filteredUser = filteredUser.Where(roleUser => 
                roleUser.RoleUserTable.PersonalNumber_Role == selectedRoleUser.PersonalNumber_Role);
            }

            if (PaulUserComboBox.SelectedIndex > 0 && selectedPaulUser != null)
            {
                filteredUser = filteredUser.Where(paulUser => 
                paulUser.PasspordDataUserTable.PaulTable.PersonalNumber_Paul == selectedPaulUser.PersonalNumber_Paul);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredUser = filteredUser.Where(dataUser =>
                    dataUser.PasspordDataUserTable.Surname_PasspordDataUser.ToString().ToLower().Contains(searchText) ||
                    dataUser.PasspordDataUserTable.Name_PasspordDataUser.ToString().ToLower().Contains(searchText) ||
                    dataUser.PasspordDataUserTable.Middlename_PasspordDataUser.ToString().ToLower().Contains(searchText));
            }

            ListUserListView.ItemsSource = filteredUser.ToList();
        }
        private void Event_SmallCodeUpgrade()
        {
            var userInformation = AppConnectClass.connectDataBase_ACC.UserTable.ToList();

            var roleUserItems = new List<RoleUserTable> { new RoleUserTable { PersonalNumber_Role = AppConnectClass.PersonalNumberTitleNullDataComboBox, Name_Role = AppConnectClass.TitleNullDataComboBox } };
            var paulUserItems = new List<PaulTable> { new PaulTable { PersonalNumber_Paul = AppConnectClass.PersonalNumberTitleNullDataComboBox, Title_Paul = AppConnectClass.TitleNullDataComboBox } };

            roleUserItems.AddRange(AppConnectClass.connectDataBase_ACC.RoleUserTable.ToList());
            paulUserItems.AddRange(AppConnectClass.connectDataBase_ACC.PaulTable.ToList());

            RoleUserComboBox.ItemsSource = roleUserItems;
            PaulUserComboBox.ItemsSource = paulUserItems;

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
            PaulUserComboBox.SelectedIndex = 0;
            RoleUserComboBox.SelectedIndex = 0;

            ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
        }
        #endregion
        #region _SelectionChanged _MouseDoubleClick
        private void ListUserListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataContextUser = (UserTable)ListUserListView.SelectedItem;
            FrameNavigationClass.miniInformationUserFrame_FNC.Navigate(new MiniInformationUserPage(dataContextUser));
        }

        private void RoleUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { Event_FilterUsers(); }

        private void PaulUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { Event_FilterUsers(); }

        private void SearchUserTextBox_SelectionChanged(object sender, RoutedEventArgs e) { Event_FilterUsers(); }

        private void ListUserListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FrameNavigationClass.bodyFrame_FNC.Navigate(new AddEdditUserInformationPage((UserTable)ListUserListView.SelectedItem));
        }
        #endregion
    }
}
