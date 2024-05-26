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

        public AddEdditUserInformationPage(UserTable userTable)
        {
            InitializeComponent();
            AppConnectClass.connectDataBase_ACC = new FireplaceEntities();

            if (userTable != null)
            {
                DataContext = userTable;
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
            //addUppdatePasspordDataUserTable.pnPaul_PasspordDataUser = PaulComboBox.Text;
            addUppdatePasspordDataUserTable.DateBirth_PasspordDataUser = Convert.ToDateTime(DateBirthTextBox.Text);
            addUppdatePasspordDataUserTable.PlaceBirth_PasspordDataUser = PlaceBirthTextBox.Text;
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
