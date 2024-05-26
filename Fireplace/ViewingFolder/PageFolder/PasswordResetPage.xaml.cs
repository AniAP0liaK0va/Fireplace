using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static Fireplace.AppDataFolder.ClassFolder.EmailClass;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class PasswordResetPage : Page
    {
        private string ItsTimeToSleep;

        public PasswordResetPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
        }
        #region _Click
        private void BackAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigationClass.mainFarme_FNC.Navigate(new AuthorizationPage());
        }
        #endregion
        #region Event_
        private void Event_SendingCode(object sender = null, RoutedEventArgs e = null)
        {
            string emailUser = EmailUserTextBox.Text;
            string personalNumberUser = PersonalNumberUserTextBox.Text;

            if (AppConnectClass.connectDataBase_ACC.UserTable.FirstOrDefault(dataUser =>
                            dataUser.Email_User == emailUser && dataUser.PersonalNumber_User == personalNumberUser) != null)
            {
                EnterRandomeCodeEmail_EC enterRandomeCodeEmailUser = new EnterRandomeCodeEmail_EC();
                enterRandomeCodeEmailUser.SendPasswordResetEmail(emailUser);

                EnterEmailCodButton.Visibility = Visibility.Collapsed;
                CheckCodeUserButton.Visibility = Visibility.Visible;
                CheckCodeUserTextBox.IsReadOnly = false;

                ItsTimeToSleep = enterRandomeCodeEmailUser.ressetCode_ERCE;
            }
            else
            {
                MessageBoxClass.FailureMessageBox_MBC(textMessage: "Извините, но данного пользователя не найдено! Попробуйте обратиться к администратору");
            }
        }

        private void Event_NextNewPassword(object sender = null, RoutedEventArgs e = null)
        {
            if (CheckCodeUserTextBox.Text == ItsTimeToSleep)
            {
                AppConnectClass.userWithChangeablePassword_ACC = PersonalNumberUserTextBox.Text;
                FrameNavigationClass.mainFarme_FNC.Navigate(new NewPasswordPage());
            }
            else
            {
                MessageBoxClass.FailureMessageBox_MBC(textMessage: "Вы ввели не правильный код, попробуйте ещё раз!");
                FrameNavigationClass.mainFarme_FNC.Navigate(new PasswordResetPage());
            }
        }

        #endregion
    }
}
