using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using Microsoft.Win32;
using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Fireplace.ViewingFolder.PageFolder
{
    public partial class AddEdditUserInformationPage : Page
    {
        string pathImage;
        string validationStringDate;
        UserTable dataUserTable;

        public AddEdditUserInformationPage(UserTable userTable)
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();

            RoleComboBox.ItemsSource = AppConnectClass.connectDataBase_ACC.RoleUserTable.ToList();
            PaulComboBox.ItemsSource = AppConnectClass.connectDataBase_ACC.PaulTable.ToList();

            if (userTable != null)
            {
                DataContext = userTable;
                dataUserTable = userTable;
                TextAddEditUserDataButtonTextBlock.Text = "Сохранить изменения";
                PasswordStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                TextAddEditUserDataButtonTextBlock.Text = "Добавить пользователя";
                VisibilityPasswordStackPanel.Visibility = Visibility.Collapsed;
            }
        }
        #region _Click
        private void VisibilityPasswordToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (VisibilityPasswordToggleButton.IsChecked == true)
            {
                PasswordStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordStackPanel.Visibility = Visibility.Collapsed;
            }
        }
        private void AddEditUserDataButton_Click(object sender, RoutedEventArgs e)
        {
            Event_AddUppdateInformationUser();
        }

        private void OpenNewImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (openFileDialog.ShowDialog() == true)
            {
                pathImage = openFileDialog.FileName;
                UserPhotoImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void DeliteImageButton_Click(object sender, RoutedEventArgs e)
        {
            pathImage = "";
            UserPhotoImage.Source = null;
        }
        #endregion
        #region Event_
        private void Event_AddUppdateInformationUser()
        {
            Event_ValidationData();

            if (validationStringDate != null)
            {
                MessageBoxClass.FailureMessageBox_MBC(textMessage: validationStringDate);
                validationStringDate = null;
            }
            else
            {
                // Создаем объекты таблиц
                var addOrUpdateUserTable = new UserTable();
                var addOrUpdatePassportDataUserTable = new PasspordDataUserTable();
                var addOrUpdateImageTable = new ImageTable();

                // Заполняем данные для таблицы паспорта
                addOrUpdatePassportDataUserTable.PassportSeries_PasspordDataUser = SeriesPasspordUserTextBox.Text;
                addOrUpdatePassportDataUserTable.PassportNumber_PasspordDataUser = NumberPasspordUserTextBox.Text;
                addOrUpdatePassportDataUserTable.Surname_PasspordDataUser = SurnameUserTextBox.Text;
                addOrUpdatePassportDataUserTable.Name_PasspordDataUser = NameUserTextBox.Text;
                addOrUpdatePassportDataUserTable.Middlename_PasspordDataUser = MiddlenameUserTextBox.Text;
                addOrUpdatePassportDataUserTable.PassportHasBeenIssued_PasspordDataUser = PassportHasBeenIssuedTextBox.Text;
                addOrUpdatePassportDataUserTable.DateIssue_PasspordDataUser = Convert.ToDateTime(DateIssueTextBox.Text);
                addOrUpdatePassportDataUserTable.UnitCode_PasspordDataUser = UnitCodeTextBox.Text;
                addOrUpdatePassportDataUserTable.pnPaul_PasspordDataUser = (PaulComboBox.SelectedItem as PaulTable).PersonalNumber_Paul;
                addOrUpdatePassportDataUserTable.DateBirth_PasspordDataUser = Convert.ToDateTime(DateBirthTextBox.Text);
                addOrUpdatePassportDataUserTable.PlaceBirth_PasspordDataUser = PlaceBirthTextBox.Text;
                AppConnectClass.connectDataBase_ACC.PasspordDataUserTable.AddOrUpdate(addOrUpdatePassportDataUserTable);


                if (dataUserTable == null && pathImage != null)
                {
                    addOrUpdateImageTable.PersonalNumber_Inage =
                        addOrUpdatePassportDataUserTable.PassportNumber_PasspordDataUser + addOrUpdatePassportDataUserTable.PassportSeries_PasspordDataUser;
                    addOrUpdateUserTable.pnImage_User = addOrUpdateImageTable.PersonalNumber_Inage;
                }
                if (dataUserTable == null && pathImage == null)
                {
                    addOrUpdateUserTable.pnImage_User = "0         ";
                }
                if (pathImage == null && dataUserTable != null && addOrUpdateUserTable.pnImage_User != "0         ")
                {
                    AppConnectClass.connectDataBase_ACC.ImageTable.Remove(
                            AppConnectClass.connectDataBase_ACC.ImageTable.Find(dataUserTable.pnImage_User));
                    addOrUpdateUserTable.pnImage_User = "0         ";
                }
                if (pathImage != null)
                {
                    byte[] imageData;

                    // Конвертация изображения в байты
                    using (FileStream fs = new FileStream(pathImage, FileMode.Open, FileAccess.Read))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                    }
                    addOrUpdateImageTable.BinaryCode_Image = imageData;
                }


                // Обновление данных для таблицы пользователя
                addOrUpdateUserTable.PersonalNumber_User =
                    $"{addOrUpdatePassportDataUserTable.PassportSeries_PasspordDataUser}{addOrUpdatePassportDataUserTable.PassportNumber_PasspordDataUser}";
                addOrUpdateUserTable.pnPassportSeries_User = addOrUpdatePassportDataUserTable.PassportSeries_PasspordDataUser;
                addOrUpdateUserTable.pnPassportNumber_User = addOrUpdatePassportDataUserTable.PassportNumber_PasspordDataUser;
                addOrUpdateUserTable.Email_User = EmailTextBox.Text;
                addOrUpdateUserTable.pnRole_User = (RoleComboBox.SelectedItem as RoleUserTable).PersonalNumber_Role;

                if (dataUserTable != null &&
                    (dataUserTable.pnPassportSeries_User != addOrUpdateUserTable.pnPassportSeries_User ||
                    dataUserTable.pnPassportNumber_User != addOrUpdateUserTable.pnPassportNumber_User))
                {
                    addOrUpdateImageTable.PersonalNumber_Inage =
                        addOrUpdatePassportDataUserTable.PassportNumber_PasspordDataUser + addOrUpdatePassportDataUserTable.PassportSeries_PasspordDataUser;
                    addOrUpdateUserTable.pnImage_User = addOrUpdateImageTable.PersonalNumber_Inage;
                }

                if (dataUserTable == null)
                {
                    addOrUpdateUserTable.PersonalNumber_User =
                        addOrUpdatePassportDataUserTable.PassportSeries_PasspordDataUser + addOrUpdatePassportDataUserTable.PassportNumber_PasspordDataUser;
                    addOrUpdateUserTable.DateRegistration_User = DateTime.Today;
                }

                if (dataUserTable == null || VisibilityPasswordToggleButton.IsChecked == true)
                {
                    addOrUpdateUserTable.Password_User = HashClass.GetHash(PasswordTextBox.Text);
                }

                AppConnectClass.connectDataBase_ACC.UserTable.AddOrUpdate(addOrUpdateUserTable);
                AppConnectClass.connectDataBase_ACC.SaveChanges();

                MessageBoxClass.GoodMessageBox_MBC(textMessage:
                    $"Пользователь {addOrUpdatePassportDataUserTable.Surname_PasspordDataUser} {addOrUpdatePassportDataUserTable.Name_PasspordDataUser} успешно добавлен");

            }
        }

        private void Event_ValidationData()
        {
            if (string.IsNullOrWhiteSpace(SeriesPasspordUserTextBox.Text)) validationStringDate += "Вы не указали 'Серию' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(NumberPasspordUserTextBox.Text)) validationStringDate += "Вы не указали 'Номер' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(SurnameUserTextBox.Text)) validationStringDate += "Вы не указали 'Фамилию' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(NameUserTextBox.Text)) validationStringDate += "Вы не указали 'Имя' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(PassportHasBeenIssuedTextBox.Text)) validationStringDate += "Вы не указали 'Кем выдан' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(DateIssueTextBox.Text)) validationStringDate += "Вы не указали 'Дату выдачи' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(UnitCodeTextBox.Text)) validationStringDate += "Вы не указали 'Номер подрзделения' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(PaulComboBox.Text)) validationStringDate += "Вы не указали 'Пол' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(DateBirthTextBox.Text)) validationStringDate += "Вы не указали 'Дату рождения' в 'Паспорт'\n";
            if (string.IsNullOrWhiteSpace(PlaceBirthTextBox.Text)) validationStringDate += "Вы не указали 'Место рождения' в 'Паспорт'\n";

            if (SeriesPasspordUserTextBox.Text.Length != 4) validationStringDate += "В 'Серии паспорта' должно быть 4 цифры\n";
            if (NumberPasspordUserTextBox.Text.Length != 6) validationStringDate += "В 'Номере паспорта' должно быть 6 цифры\n";
            if (SurnameUserTextBox.Text.Length <= 2) validationStringDate += "В 'Фамилии' должно быть минимум 3 Буквы\n";
            if (NameUserTextBox.Text.Length <= 1) validationStringDate += "В 'Имени' должно быть минимум 2 буквы\n";
            if (PassportHasBeenIssuedTextBox.Text.Length <= 7) validationStringDate += "'Кем выдан' должно быть минимум 10 букв\n";
            if (DateIssueTextBox.Text.Length != 10) validationStringDate += "В 'Дате выдачи' дата должны быть нормально указана (00.00.0000)\n";
            if (UnitCodeTextBox.Text.Length != 7) validationStringDate += "В 'Код подразделения' должно быть 6 цифр и 1 символ '-'\n";
            if (DateBirthTextBox.Text.Length != 10) validationStringDate += "В 'Дата рождения' дата должны быть нормально указана (00.00.0000)\n";
            if (PlaceBirthTextBox.Text.Length <= 2) validationStringDate += "'Место рождения' не может быть короче 3 букв\n";
        }
        #endregion
        #region ValidData
        private void DateValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DateRegex = new Regex("[^0-9/.]");
            e.Handled = DateRegex.IsMatch(e.Text);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex NumberRegex = new Regex("[^0-9]");
            e.Handled = NumberRegex.IsMatch(e.Text);
        }
        private void DivisionCodeValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex DivisionCodeRegex = new Regex("[^0-9-]");
            e.Handled = DivisionCodeRegex.IsMatch(e.Text);
        }
        #endregion
    }
}
