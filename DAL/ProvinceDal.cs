using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
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
            return GetData().FirstOrDefault(i => i.ProvinceId == id);
        }

        public List<ProvinceViewModel> GetProvincesData(string searchString)
        {
            var data = GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.ProvinceName.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .ToList();
            return SetProvincesViewModel(data);
        }

        private List<ProvinceViewModel> SetProvincesViewModel(List<Province> provinces)
        {
            return provinces.Select(i => new ProvinceViewModel(i)).ToList();
        }
    }
}
