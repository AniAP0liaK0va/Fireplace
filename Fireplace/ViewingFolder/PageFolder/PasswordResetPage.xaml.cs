using Fireplace.AppDataFolder.ClassFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Fireplace.AppDataFolder.ClassFolder.EmailClass;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class PasswordResetPage : Page
    {
        public PasswordResetPage()
        {
            InitializeComponent();
        }
        #region _Click
        private void BackAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.mainFarme_FNC.Navigate(new AuthorizationPage());
        }
        #endregion
        #region Event_
        private void Event_SendingCode(object sender, RoutedEventArgs e)
        {
            string emailUser = EmailUserTextBox.Text;
            string personalNumberUser = PersonalNumberUserTextBox.Text;

            if (AppConnectClass.connectDataBase_ACC.UserTable.FirstOrDefault(dataUser =>
                            dataUser.Email_User == emailUser && dataUser.PersonalNumber_User == personalNumberUser) != null)
            {
                EnterRandomeCodeEmail_EC enterRandomeCodeEmailUser = new EnterRandomeCodeEmail_EC();
                enterRandomeCodeEmailUser.SendPasswordResetEmail(emailUser);

                MessageBoxClass.GoodMessageBox_MBC(textMessage: enterRandomeCodeEmailUser.ressetCode_ERCE);
            }
            else
            {
                MessageBoxClass.FailureMessageBox_MBC(textMessage: "Извините, но данного пользователя не найдено! Попробуйте обратиться к администратору");
            }
        }

        #endregion
    }
}
