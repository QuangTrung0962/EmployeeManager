using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace DAL
{
    public class DistrictDal : IDistrictDal
    {
        private readonly EmployeesDBEntities _db;
        public DistrictDal(EmployeesDBEntities db)
        {
            _db = db;
        }

        private IQueryable<District> GetData()
        {
            return _db.Districts
                    .Include(i => i.Province)
                    .Include(i => i.Towns);
        }

        public List<District> GetDistrictsData(string searchString)
        {
            if (int.TryParse(searchString, out int provinceId))
            {
                return GetDistrictsByProvinceId(provinceId);
            }

            return GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.DistrictName.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .ToList();
        }

        public District GetDistrictById(int? id)
        {
            return GetData().Where(i => i.DistrictId == id).FirstOrDefault();
        }

        public List<District> GetDistrictsByProvinceId(int provinceId)
        {
            return GetData().Where(i => i.ProvinceId == provinceId).ToList();
                
        }

    }
}
