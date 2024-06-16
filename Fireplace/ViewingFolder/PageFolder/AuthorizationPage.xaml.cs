using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using Fireplace.ViewingFolder.WindowFolder;
using System;
using System.Data.Entity;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class AuthorizationPage : Page
    {
        string messageNullBox;
        private Timer getTimer;
        private double currentRotationAngle = 0;

        public AuthorizationPage()
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();
            getTimer = new Timer(UpdateAnimation, null, Timeout.Infinite, 100);
        }
        #region Event_
        private void UpdateAnimation(object state)
        {
            currentRotationAngle += 10;

            if (currentRotationAngle >= 360)
            {
                currentRotationAngle = 0;
            }

            Dispatcher.Invoke(() =>
            {
                RotateTransform loadingAnimation = new RotateTransform();
                loadingAnimation.Angle = currentRotationAngle;
                LoadingSpinnerTextBlock.RenderTransformOrigin = new Point(0.5, 0.5);
                LoadingSpinnerTextBlock.RenderTransform = loadingAnimation;
            });
        }
        private void Event_StartEnterUser(object sender = null, RoutedEventArgs e = null)
        {
            Event_EnterUser();
            Event_StartAnimation();
        }

        private void Event_StartAnimation()
        {
            Dispatcher.Invoke(() =>
            {
                MainTextButtonEnterTextBlock.Visibility = Visibility.Collapsed;
                LoadingTextTextBlock.Visibility = Visibility.Visible;
                LoadingSpinnerTextBlock.Visibility = Visibility.Visible;
                EnterUserButton.IsEnabled = false;
                PasswordResetButton.IsEnabled = false;
                PasswordUserPasswordBox.IsEnabled = false;
                WatchPasswordButton.IsEnabled = false;
                LoginUserTextBox.IsEnabled = false;
            });
            getTimer.Change(0, 20);
        }

        private void Event_StopAnimation()
        {
            Dispatcher.Invoke(() =>
            {
                MainTextButtonEnterTextBlock.Visibility = Visibility.Visible;
                LoadingTextTextBlock.Visibility = Visibility.Collapsed;
                LoadingSpinnerTextBlock.Visibility = Visibility.Collapsed;
                EnterUserButton.IsEnabled = true;
                PasswordResetButton.IsEnabled = true;
                PasswordUserPasswordBox.IsEnabled = true;
                WatchPasswordButton.IsEnabled = true;
                LoginUserTextBox.IsEnabled = true;
            });

            getTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private async void Event_EnterUser() // Авторизация пользователя
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

                    var logInUser = await AppConnectClass.connectDataBase_ACC.UserTable.FirstOrDefaultAsync(dataUser =>
                            dataUser.Email_User == receiveLogin && dataUser.Password_User == receivePasswordUserHash);


                    if (logInUser != null)
                    {
                        switch (logInUser.pnRole_User)
                        {
                            case 1:
                                // переход в приложение
                                AppConnectClass.receiveConnectUser_ACC = logInUser.PersonalNumber_User.ToString();

                                MainWindow mainWindow = new MainWindow();
                                mainWindow.Show();

                                AuthorizationRegistrationWindow authorizationRegistrationWindow = Window.GetWindow(this) as AuthorizationRegistrationWindow; // Получаем родительское 
                                FrameNavigationClass.bodyFrame_FNC.Navigate(new ProfilPage(null));
                                authorizationRegistrationWindow.Close();
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
            finally
            {
                Event_StopAnimation();
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
        #region _Click _KeyDown
        private void PasswordResetButton_Click(object sender, RoutedEventArgs e) // Переход на сброс пароля
        {
            FrameNavigationClass.mainFarme_FNC.Navigate(new PasswordResetPage());
        }

        private void PasswordUserPasswordBox_KeyDown(object sender, KeyEventArgs e) // Если пользователь, находясь в PasswordBox нажал на Enter
        {
            if (e.Key == Key.Enter) { Event_StartEnterUser(); }
        }
        #endregion
    }
}
