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

        public ProvinceDto GetProvinceById(int? id)
        {
            var province = _db.Provinces.FirstOrDefault(x => x.ProvinceId == id);
            if (province == null) return null;

            return new ProvinceDto()
            {
                Id = province.ProvinceId,
                ProvinceName = province.ProvinceName,
            };
        }

        public List<ProvinceDto> GetProvincesData(string searchString)
        {
            return _db.Provinces.Where(i => i.ProvinceName.Trim().ToLower().Contains(searchString.Trim().ToLower()) || string.IsNullOrEmpty(searchString))
                .Select(i => new ProvinceDto
                {
                    Id = i.ProvinceId,
                    ProvinceName = i.ProvinceName,
                }).ToList();
        }
    }
}
