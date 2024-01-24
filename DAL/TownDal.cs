using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class TownDal : ITownDal
    {
        private readonly EmployeesDBEntities _db;

        public TownDal(EmployeesDBEntities db)
        {
            _db = db;
        }

        private IQueryable<Town> GetData()
        {
            return _db.Towns
                    .Include(x => x.District);
        }

        public List<Town> GetTownsData(string searchString)
        {
            if(int.TryParse(searchString, out int districtId))
            {
                return GetTownsByDistrictId(districtId);
            }

            return GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.TownName.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .ToList();
        }

        public Town GetTownById(int? id)
        {
            return GetData().Where(i => i.TownId == id).FirstOrDefault();
        }

        public List<Town> GetTownsByDistrictId(int districtId)
        {
            return GetData().Where(i => i.DistrictId == districtId).ToList();
        }
    }
}
