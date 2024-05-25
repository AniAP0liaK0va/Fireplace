using Fireplace.AppDataFolder.ClassFolder;
using System;
using System.Windows;
using System.Windows.Controls;

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

        private void EnterEmailCodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра и вызов метода отправки email
                EmailClass.EnterEmail_EC emailSender = new EmailClass.EnterEmail_EC();
                emailSender.SendEmail();
                MessageBox.Show("Email sent successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email. Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
