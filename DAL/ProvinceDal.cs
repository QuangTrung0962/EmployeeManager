using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ProvinceDal : IProvinceDal
    {
        private readonly EmployeesDBEntities _db;

        public ProvinceDal(EmployeesDBEntities db)
        {
            _db = db;
        }

        private IQueryable<Province> GetData()
        {
            return _db.Provinces;
        }

        public Province GetProvinceById(int? id)
        {
            return GetData().Where(i => i.ProvinceId == id).FirstOrDefault();
        }

        public List<Province> GetProvincesData(string searchString)
        {
            return GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.ProvinceName.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .ToList();
        }
    }
}
