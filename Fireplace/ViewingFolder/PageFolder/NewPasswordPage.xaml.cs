using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class NewPasswordPage : Page
    {
        string messageNullBox;

        public NewPasswordPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
        }
        #region Event_
        private void Event_SaveNewPassword(object sender = null, RoutedEventArgs e = null)
        {
            messageNullBox = null;
            Event_ErrorNullBox();

            if (messageNullBox == null)
            {
                string receivePasswordUserHash = HashClass.GetHash(NewPasswordUserTextBox.Text);
                var newPasswordUser = new UserTable();
                
                newPasswordUser.PersonalNumber_User = AppConnectClass.userWithChangeablePassword_ACC;
                newPasswordUser.Password_User = receivePasswordUserHash;
                AppConnectClass.connectDataBase_ACC.UserTable.AddOrUpdate(newPasswordUser);
                AppConnectClass.connectDataBase_ACC.SaveChanges();

                MessageBoxClass.FailureMessageBox_MBC(textMessage:"Пароль успешно изменён, возвращайтесь на страницу авторизации и произведите вход");
                FrameNavigationClass.mainFarme_FNC.Navigate(new AuthorizationPage());
            }
            else
            {
                MessageBoxClass.FailureMessageBox_MBC(textMessage: messageNullBox);
            }
        }

        private void Event_ErrorNullBox() // проверки текстовых полей на пустоту
        {
            if (string.IsNullOrWhiteSpace(NewPasswordUserTextBox.Text)) messageNullBox += "Вы не придумали пароль\n";
            if (string.IsNullOrWhiteSpace(NewPasswordUserPasswordBox.Password)) messageNullBox += "Вы не повторили пароль\n";
            if (NewPasswordUserPasswordBox.Password != NewPasswordUserTextBox.Text) messageNullBox += "Вы ввели пароль не правильно\n";

            // Проверка на сложность пароля
            if (!Event_IsValidPassword(NewPasswordUserTextBox.Text))
                messageNullBox += "Пароль должен содержать хотя бы одну букву верхнего регистра, одну букву нижнего регистра, одну цифру и один специальный символ\n";
        }

        private bool Event_IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            bool hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, "[a-z]");
            bool hasDigit = Regex.IsMatch(password, "[0-9]");
            bool hasSpecialChar = Regex.IsMatch(password, "[!@#$%^&*(){}\\[\\]]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
        #endregion
        #region Посмотреть пароль
        private void NewWatchPasswordButton_PreviewMouseDown(object sender, MouseButtonEventArgs e) /// При нажатии мышкой
        {
            NewPasswordTextBox.Text = NewPasswordUserPasswordBox.Password;

            NewPasswordTextBox.Visibility = Visibility.Visible;
            NewPasswordUserPasswordBox.Visibility = Visibility.Collapsed;
        }

        private void NewWatchPasswordButton_PreviewMouseUp(object sender, MouseButtonEventArgs e) /// При отпуске мышки
        {
            NewPasswordTextBox.Visibility = Visibility.Collapsed;
            NewPasswordUserPasswordBox.Visibility = Visibility.Visible;
        }
        #endregion
        #region _KeyDown
        private void PasswordUserPasswordBox_KeyDown(object sender, KeyEventArgs e) // Если пользователь, находясь в PasswordBox нажал на Enter
        {
            try
            {
                if (e.Key == Key.Enter) { Event_SaveNewPassword(); }
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
