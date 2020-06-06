namespace Ugugushka.WebUI.Code.Constants
{
    public static class PageNameDefaults
    {
        public const string Domain = "Ugugushka.by";
        public const string Login = "Авторизация";
        public const string Main = "Главная";
        public const string AdminToys = "Управление игрушками";
        public const string AddToy = "Добавление игрушек";
        public const string EditToy = "Изменение игрушек";
        public const string Cart = "Корзина";
    }

    public static class PageNameStringExtenstion
    {
        public static string WithDomain(this string str, string domain = null)
            => $"{str} - {domain ?? PageNameDefaults.Domain}";
    }
}
