using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using Microsoft.Win32;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.IO;
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

            if (userTable != null)
            {
                DataContext = userTable;
                dataUserTable = userTable;
                AddEditUserDataButton.Content = "Сохранить изменения";
            }
            else
            {
                AddEditUserDataButton.Content = "Добавить пользователя";
            }
        }
        #region _Click
        private void AddEditUserDataButton_Click(object sender, RoutedEventArgs e)
        {

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
                UserTable addUppdateUserTable = new UserTable();
                PasspordDataUserTable addUppdatePasspordDataUserTable = new PasspordDataUserTable();
                ImageTable addUppdateImageTable = new ImageTable();

                addUppdatePasspordDataUserTable.PassportSeries_PasspordDataUser = SeriesPasspordUserTextBox.Text;
                addUppdatePasspordDataUserTable.PassportNumber_PasspordDataUser = NumberPasspordUserTextBox.Text;
                addUppdatePasspordDataUserTable.Surname_PasspordDataUser = SurnameUserTextBox.Text;
                addUppdatePasspordDataUserTable.Name_PasspordDataUser = NameUserTextBox.Text;
                addUppdatePasspordDataUserTable.Middlename_PasspordDataUser = MiddlenameUserTextBox.Text;
                addUppdatePasspordDataUserTable.PassportHasBeenIssued_PasspordDataUser = PassportHasBeenIssuedTextBox.Text;
                addUppdatePasspordDataUserTable.DateIssue_PasspordDataUser = Convert.ToDateTime(DateIssueTextBox.Text);
                addUppdatePasspordDataUserTable.UnitCode_PasspordDataUser = UnitCodeTextBox.Text;
                addUppdatePasspordDataUserTable.pnPaul_PasspordDataUser = (PaulComboBox.SelectedItem as PaulTable).PersonalNumber_Paul;
                addUppdatePasspordDataUserTable.DateBirth_PasspordDataUser = Convert.ToDateTime(DateBirthTextBox.Text);
                addUppdatePasspordDataUserTable.PlaceBirth_PasspordDataUser = PlaceBirthTextBox.Text;

                addUppdateUserTable.pnPassportSeries_User = addUppdatePasspordDataUserTable.PassportSeries_PasspordDataUser;
                addUppdateUserTable.pnPassportNumber_User = addUppdatePasspordDataUserTable.PassportNumber_PasspordDataUser;
                addUppdateUserTable.DateRegistration_User = DateTime.Today;
                addUppdateUserTable.Email_User = EmailTextBox.Text;
                addUppdateUserTable.pnImage_User = addUppdateImageTable.PersonalNumber_Inage;
                addUppdateUserTable.pnRole_User = (RoleComboBox.SelectedItem as RoleUserTable).PersonalNumber_Role;

                // Если пользователь новый
                if (dataUserTable == null)
                {
                    addUppdateUserTable.PersonalNumber_User = 
                        addUppdatePasspordDataUserTable.PassportSeries_PasspordDataUser + addUppdatePasspordDataUserTable.PassportNumber_PasspordDataUser;
                }

                // Сравниваем пароь, и есл он не такой же, (поменяли пароль или новый пользователь), то сохраняем новый пароль
                if (HashClass.GetHash(PasswordTextBox.Text) != addUppdateUserTable.Password_User) 
                {
                    addUppdateUserTable.Password_User = HashClass.GetHash(PasswordTextBox.Text);
                }

                // Если фото было, но его убрали (удалили) то сохраняем пустую картинку и удаляем фото из базы
                if (UserPhotoImage.Source == null || addUppdateUserTable.pnImage_User != null)
                {
                    addUppdateUserTable.pnImage_User = "0";

                    AppConnectClass.connectDataBase_ACC.ImageTable.Remove(
                        AppConnectClass.connectDataBase_ACC.ImageTable.Find(dataUserTable.pnImage_User));
                }
                else
                {
                    if (dataUserTable == null || pathImage != "")
                    {
                        addUppdateImageTable.PersonalNumber_Inage =
                            addUppdatePasspordDataUserTable.PassportNumber_PasspordDataUser + addUppdatePasspordDataUserTable.PassportSeries_PasspordDataUser;
                    }
                    else
                    {
                        addUppdateUserTable.pnImage_User = "0";
                    }

                    if (pathImage != "")
                    {
                        byte[] imageData;

                        // Конвертация изображения в байты
                        using (FileStream fs = new FileStream(pathImage, FileMode.Open, FileAccess.Read))
                        {
                            imageData = new byte[fs.Length];
                            fs.Read(imageData, 0, imageData.Length);
                        }
                        addUppdateImageTable.BinaryCode_Image = imageData;
                    }
                    
                }
                AppConnectClass.connectDataBase_ACC.ImageTable.AddOrUpdate(addUppdateImageTable);
                AppConnectClass.connectDataBase_ACC.PasspordDataUserTable.AddOrUpdate(addUppdatePasspordDataUserTable);
                AppConnectClass.connectDataBase_ACC.UserTable.AddOrUpdate(addUppdateUserTable);
                AppConnectClass.connectDataBase_ACC.SaveChanges();
                MessageBoxClass.GoodMessageBox_MBC(textMessage: 
                    $"Пользователь {addUppdatePasspordDataUserTable.Surname_PasspordDataUser} {addUppdatePasspordDataUserTable.Name_PasspordDataUser} усешно добавлен");
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
            if (DateBirthTextBox.Text.Length !=10) validationStringDate += "В 'Дата рождения' дата должны быть нормально указана (00.00.0000)\n";
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
