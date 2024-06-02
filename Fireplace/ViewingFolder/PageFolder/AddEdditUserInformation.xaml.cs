using Fireplace.AppDataFolder.ClassFolder;
using Fireplace.AppDataFolder.ModelFolder;
using Microsoft.Win32;
using System;
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

        public AddEdditUserInformationPage(UserTable userTable)
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();

            if (userTable != null)
            {
                DataContext = userTable;
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

            //addUppdateUserTable.PersonalNumber_User
            addUppdateUserTable.pnPassportSeries_User = addUppdatePasspordDataUserTable.PassportSeries_PasspordDataUser;
            addUppdateUserTable.pnPassportNumber_User = addUppdatePasspordDataUserTable.PassportNumber_PasspordDataUser;
            addUppdateUserTable.DateRegistration_User = DateTime.Today;
            addUppdateUserTable.Email_User = EmailTextBox.Text;
            //addUppdateUserTable.PasspordDataUserTable =
            addUppdateUserTable.pnImage_User = addUppdateImageTable.PersonalNumber_Inage;
            addUppdateUserTable.pnRole_User = (RoleComboBox.SelectedItem as RoleUserTable).PersonalNumber_Role;
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
