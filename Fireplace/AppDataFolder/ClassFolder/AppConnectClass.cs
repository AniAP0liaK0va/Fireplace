using Fireplace.AppDataFolder.ModelFolder;

namespace Fireplace.AppDataFolder.ClassFolder
{
    public class AppConnectClass
    {
        public static readonly int PersonalNumberTitleNullDataComboBox = -1;
        public static readonly string TitleNullDataComboBox = "-= Не выбранно =-";

        public static FireplaceEntities connectDataBase_ACC; // Подключаем БД

        public static string receiveConnectUser_ACC; // Сохраняем информацию об авторизированном пользователе
        public static string userWithChangeablePassword_ACC;
    }
}
