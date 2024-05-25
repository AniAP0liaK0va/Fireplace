using System.Windows;

namespace Fireplace.AppDataFolder.ClassFolder
{
    public class MessageBoxClass
    {
        /// <summary>
        /// MessageBox для ошибок try{} catch (Exception ex){}
        /// </summary>
        /// <param name="textMessage"> это то, что будет выводиться (определённый текст)</param>
        /// <param name="topRow">это само название</param>
        public static void ExceptionMessageBox_MBC(
            string textMessage = "Разработчик (программист) не присвоил этому значению сообщение",
            string topRow = "Error Exception")
        {
            MessageBox.Show(
                textMessage, topRow,
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void GoodMessageBox_MBC( /// MessageBox вывода удачи для пользователя
            string textMessage = "Разработчик (программист) не присвоил этому значению сообщение",
            string topRow = "Fine")
        {
            MessageBox.Show(
                textMessage, topRow,
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void FailureMessageBox_MBC( /// MessageBox для ошибок допущенных пользователем
            string textMessage = "Разработчик (программист) не присвоил этому значению сообщение",
            string topRow = "Неудача")
        {
            MessageBox.Show(
                textMessage, topRow,
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static MessageBoxResult RemoveMessageBox_MBC( /// MessageBox для подтверждения удаления
            string textMessage = "Разработчик (программист) не присвоил этому значению сообщение",
            string topRow = "Удаление")
        {
            return MessageBox.Show(
                textMessage, topRow,
                MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
    }
}
