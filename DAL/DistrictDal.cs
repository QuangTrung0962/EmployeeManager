using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;


namespace DAL
{
    public class DistrictDal : IDistrictDal
    {
        private readonly EmployeesDBEntities _db;

        public DistrictDal(EmployeesDBEntities context)
        {
            _db = context;
        }

        public List<DistrictDto> GetDistrictsData(string searchString)
        {
            bool isNumeric = int.TryParse(searchString, out int provinceId);

            if(isNumeric)
            {
                return _db.Districts.Where(i => i.ProvinceId.ToString() == searchString)
               .Select(i => new DistrictDto
               {
                   Id = i.DistrictId,
                   DistrictName = i.DistrictName,
                   ProvinceId = i.ProvinceId,
               })
               .ToList();
            }

            return _db.Districts.Where(i => i.DistrictName.ToLower().Contains(searchString.ToLower()) || string.IsNullOrEmpty(searchString))
               .Select(i => new DistrictDto
               {
                   Id = i.DistrictId,
                   DistrictName = i.DistrictName,
                   ProvinceId = i.ProvinceId,
               }).ToList();
        }

        public bool AddDistrict(District district)
        {
            try
            {
                _db.Districts.Add(district);
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteDistrict(int id)
        {
            try
            {
                var towns = _db.Towns.Where(t => t.DistrictId == id).ToList();
                _db.Towns.RemoveRange(towns);

                var districts = _db.Districts.Where(i => i.DistrictId == id).ToList();
                _db.Districts.RemoveRange(districts);

                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public DistrictDto GetDistrictById(int? id)
        {
            var model = _db.Districts.First(x => x.DistrictId == id);

            return new DistrictDto()
            {
                Id = model.DistrictId,
                DistrictName = model.DistrictName,
                ProvinceId = model.ProvinceId,
            };
        }

        public List<DistrictDto> GetDistrictsByProvinceId(int pronviceId)
        {
            return _db.Districts.Where(i => i.ProvinceId == pronviceId).
                Select(j => new DistrictDto
                {
                    Id = j.DistrictId,
                    DistrictName = j.DistrictName,
                    ProvinceId = j.ProvinceId,
                }).ToList();
        }

        public bool UpdateDistrict(DistrictDto districtDto)
        {
            District district = _db.Districts.FirstOrDefault(i => i.DistrictId == districtDto.Id);

            if (district == null) return false;
            
            try
            {
                district.DistrictId = districtDto.Id;
                district.DistrictName = districtDto.DistrictName;
                district.ProvinceId = districtDto.ProvinceId;
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
