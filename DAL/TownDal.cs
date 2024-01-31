using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
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

        public List<TownViewModel> GetTownsData(string searchString)
        {
            if(int.TryParse(searchString, out int districtId))
            {
                return GetTownsByDistrictId(districtId);
            }

            var data = GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.TownName.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .ToList();
            return SetTownsViewModel(data);
        }

        public Town GetTownById(int? id)
        {
            return GetData().FirstOrDefault(i => i.TownId == id);
        }

        public List<TownViewModel> GetTownsByDistrictId(int districtId)
        {
            var data =  GetData().Where(i => i.DistrictId == districtId).ToList();
            return SetTownsViewModel(data);
        }

        private List<TownViewModel> SetTownsViewModel(List<Town> towns)
        {
            return towns.Select(i => new TownViewModel(i)).ToList();
        }
    }
}
