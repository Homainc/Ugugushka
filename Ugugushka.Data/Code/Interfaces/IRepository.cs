namespace Ugugushka.Data.Code.Interfaces
{
    interface IRepository<in TItem> where TItem : class
    {
        void Create(TItem item);
        void Update(TItem item);
        void Delete(TItem item);
    }
}
