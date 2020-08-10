namespace PizzaStore.Client.Repositories
{
    public interface IStoreRepo
    {
        void SeedDB();
        void ClearDatabase();
    }
}