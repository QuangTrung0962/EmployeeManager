namespace DAL.Interfaces
{
    public interface IBaseDal<in T> where T : class
    {
        void InsertEntity(T entity);
        void DeleteEntity(T entity);
        void UpdateEntity(T entity);
    }
}
