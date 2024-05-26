using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class AuthorizationPage : Page
    {
        string messageNullBox;

        public AuthorizationPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
        }
        #region Event_
        private void Event_EnterUser(object sender = null, RoutedEventArgs e = null) // Авторизация пользователя
        {
            try
            {
                messageNullBox = null;
                Event_ErrorNullBox();

                if (messageNullBox != null)
                {
                    MessageBoxClass.FailureMessageBox_MBC(messageNullBox);
                }
                else
                {
                    string receiveLogin = LoginUserTextBox.Text;
                    string receivePasswordUserHash = HashClass.GetHash(PasswordUserPasswordBox.Password);

                    var logInUser = AppConnectClass.connectDataBase_ACC.UserTable.FirstOrDefault(dataUser =>
                            dataUser.Email_User == receiveLogin && dataUser.Password_User == receivePasswordUserHash);

                    
                    if (logInUser != null)
                    {
                        switch (logInUser.pnRole_User)
                        {
                            case 1:
                                // переход в приложение
                                AppConnectClass.receiveConnectUser_ACC = Convert.ToDouble(logInUser.PersonalNumber_User);
                                MessageBox.Show("Welcome");
                                break;
                            default:
                                string messageDefault =
                                    $"Извините {logInUser.PasspordDataUserTable.Surname_PasspordDataUser + " " + logInUser.PasspordDataUserTable.Name_PasspordDataUser} " +
                                    "но для вас доступ в АИС закрыт!";
                                MessageBoxClass.FailureMessageBox_MBC(textMessage: $"{messageDefault}");
                                break;
                        }
                    }
                    else
                    {
                        // Если данные которые ввел пользователь, не существуют в базе данных
                        string messageError = $"Извините, но пользователя с:\n\n" +
                            $"логином: {LoginUserTextBox.Text}\n" +
                            $"и паролем: {PasswordUserPasswordBox.Password}\n\n" +
                            $"не нашлось в нашей базе данных," +
                            $"возможно вы ввели данные не правильно или не зарегистрированы у нас!";

                        MessageBoxClass.FailureMessageBox_MBC(textMessage: $"{messageError}");
                    }
                }
            }
            catch (Exception exEvent_EnterUser)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие Event_AuthorizationUser в AuthorizationWindow:\n\n " +
                    $"{exEvent_EnterUser.Message}");
            }
        }
        private void Event_ErrorNullBox() // проверки текстовых полей на пустоту
        {
            if (string.IsNullOrWhiteSpace(LoginUserTextBox.Text)) messageNullBox += "Поле логин пустое\n";
            if (string.IsNullOrWhiteSpace(PasswordUserPasswordBox.Password)) messageNullBox += "Поле пароль пустое";
        }
        #endregion
        #region Посмотреть пароль
        private void WatchPasswordButton_PreviewMouseDown(object sender, MouseButtonEventArgs e) /// При нажатии мышкой
        {
            PasswordTextBox.Text = PasswordUserPasswordBox.Password;

            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordUserPasswordBox.Visibility = Visibility.Collapsed;
        }

        private void WatchPasswordButton_PreviewMouseUp(object sender, MouseButtonEventArgs e) /// При отпуске мышки
        {
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordUserPasswordBox.Visibility = Visibility.Visible;
        }
        #endregion
        #region _Click
        private void PasswordResetButton_Click(object sender, RoutedEventArgs e) // Переход на сброс пароля
        {
            FrameNavigationClass.mainFarme_FNC.Navigate(new PasswordResetPage());
        }
        #endregion
        #region _KeyDown
        private void PasswordUserPasswordBox_KeyDown(object sender, KeyEventArgs e) // Если пользователь, находясь в PasswordBox нажал на Enter
        {
            try
            {
                if (e.Key == Key.Enter) { Event_EnterUser(); }
            }
            catch (Exception exPasswordUserPasswordBox_KeyDown)
            {
                MessageBoxClass.ExceptionMessageBox_MBC(
                    textMessage: $"Событие PasswordUserPasswordBox_KeyDown в AuthorizationWindow:\n\n " +
                    $"{exPasswordUserPasswordBox_KeyDown.Message}");
            }
        }
        #endregion
    }
}
