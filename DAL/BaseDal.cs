using DAL.Interfaces;
using System.Data.Entity;

namespace DAL
{
    public class BaseDal<T> : IBaseDal<T> where T : class
    {
        private readonly EmployeesDBEntities _db;

        public BaseDal(EmployeesDBEntities db)
        {
            _db = db;
        }

        public void DeleteEntity(T entity)
        {
            
            _db.Set<T>().Attach(entity);
            _db.Set<T>().Remove(entity);
            _db.SaveChanges();
        }

        public void InsertEntity(T entity)
        {
            _db.Set<T>().Add(entity);
            _db.SaveChanges();
        }

        public void UpdateEntity(T entity)
        {
            _db.Set<T>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();

        }
    }
}
