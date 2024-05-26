using Fireplace.AppDataFolder.ModelFolder;

namespace Fireplace.AppDataFolder.ClassFolder
{
    public class AppConnectClass
    {
        public static FireplaceEntities connectDataBase_ACC; // Подключаем БД

        public static double receiveConnectUser_ACC; // Сохраняем информацию об авторизированном пользователе
        public static string userWithChangeablePassword_ACC;
    }
}
