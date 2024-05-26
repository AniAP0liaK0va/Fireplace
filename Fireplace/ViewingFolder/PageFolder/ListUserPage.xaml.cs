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
        private void ClearSearchButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            SearchUserTextBox.Clear();
            PaulUserComboBox.Items.Clear();
            RoleUserComboBox.Items.Clear();

            ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
        }
        private void SearchUserButton_Click(object sender = null, RoutedEventArgs e = null)
        {
            try
            {
                if (SearchUserTextBox.Text == "") // Если SearchUserTextBox пустой
                {
                    ListUserListView.ItemsSource = AppConnectClass.connectDataBase_ACC.UserTable.ToList();
                }
                else // Если же в SearchTextBox есть что-то то:
                {
                    var objects = AppConnectClass.connectDataBase_ACC.UserTable.Include(userPasportData => userPasportData.PasspordDataUserTable).ToList();

                    var SearchResults = objects.Where(userdata =>
                        userdata.PasspordDataUserTable.Surname_PasspordDataUser.ToString().Contains(SearchUserTextBox.Text.ToString()) ||
                        userdata.PasspordDataUserTable.Name_PasspordDataUser.ToString().Contains(SearchUserTextBox.Text.ToString()) ||
                        userdata.PasspordDataUserTable.Middlename_PasspordDataUser.ToString().Contains(SearchUserTextBox.Text.ToString()));

                    ListUserListView.ItemsSource = SearchResults.ToList();
                }
            }
            catch (Exception exSearchUserButton_Click)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                        textMessage: $"Событие SearchSearchUserButton_Click в ListUserPage:\n\n " +
                        $"{exSearchUserButton_Click.Message}");
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
            ListUserListView.ItemsSource = 
                AppConnectClass.connectDataBase_ACC.UserTable.Where(roleUser =>
                roleUser.pnRole_User == (int)RoleUserComboBox.SelectedValue).ToList();
        }

        private void PaulUserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListUserListView.ItemsSource =
                AppConnectClass.connectDataBase_ACC.UserTable.Where(paulUser =>
                paulUser.PasspordDataUserTable.pnPaul_PasspordDataUser == (int)PaulUserComboBox.SelectedValue).ToList();
        }
        #endregion

        #region _KeyDown
        private void SearchUserTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { SearchUserButton_Click(); }
        }
        #endregion
    }
}
