using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
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

        public List<DistrictViewModel> GetDistrictsData(string searchString)
        {
            if (int.TryParse(searchString, out int provinceId))
            {
                return GetDistrictsByProvinceId(provinceId);
            }

            var data = GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.DistrictName.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .ToList();
            return SetDistrictsViewModel(data);
        }

        public District GetDistrictById(int? id)
        {
            return GetData().FirstOrDefault(i => i.DistrictId == id);
        }

        public List<DistrictViewModel> GetDistrictsByProvinceId(int provinceId)
        {
            var data = GetData().Where(i => i.ProvinceId == provinceId).ToList();
            return SetDistrictsViewModel(data);
        }

        private List<DistrictViewModel> SetDistrictsViewModel(List<District> districts)
        {
            return districts.Select(i => new DistrictViewModel(i)).ToList();
        }
    }
}
