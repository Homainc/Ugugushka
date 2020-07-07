namespace Ugugushka.WebUI.Code.Constants
{
    public static class PageNameDefaults
    {
        public const string Domain = "Магазин Ugugushka";
        public const string Login = "Авторизация";
        public const string Main = "Каталог игрушек";
        public const string AdminToys = "Управление игрушками";
        public const string AddToy = "Добавление игрушек";
        public const string EditToy = "Изменение игрушек";
        public const string Cart = "Корзина";
        public const string Checkout = "Оформление заказа";
        public const string Orders = "Управление заказами";
        public const string OrderInfo = "Просмотр заказа";
        public const string Contacts = "Контакты";
        public const string Categories = "Управление категориями";
        public const string Partitions = "Управление разделами";
        public const string AddPartition = "Добавить раздел";
        public const string EditPartition = "Изменить раздел";
        public const string AddCategory = "Добавить категорию";
        public const string EditCategory = "Изменить категорию";
        public const string KeyWords = "ugugushka магазин игрушка покупка игрушки детские Минск доставка самовывоз";
    }

    public static class PageNameStringExtenstion
    {
        public static string WithDomain(this string str, string domain = null)
            => $"{str} - {domain ?? PageNameDefaults.Domain}";
    }
}
