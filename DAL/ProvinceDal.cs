using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL
{
	public class ProvinceDal : IProvinceDal
    {
		private readonly EmployeesDBEntities _db;

		public ProvinceDal (EmployeesDBEntities context)
		{
			_db = context;
		}

        public ProvinceDto GetProvinceById(int? id)
		{
			var province = _db.Provinces.FirstOrDefault(x => x.ProvinceId == id);
            if (province == null) return null;

			return new ProvinceDto()
			{
				Id = province.ProvinceId, ProvinceName = province.ProvinceName,
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

		public bool AddProvince(Province province)
		{
			try
			{
				_db.Provinces.Add(province);
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

		public bool UpdateProvince(ProvinceDto provinceDto)
		{
            Province province = _db.Provinces.FirstOrDefault(i => i.ProvinceId == provinceDto.Id);

			if (province == null) return false;
 
			try
			{
                province.ProvinceId = provinceDto.Id;
                province.ProvinceName = provinceDto.ProvinceName;
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

		public bool DeleteProvince(int id)
        {
            try
			{
                var towns = _db.Towns.Where(t => t.District.ProvinceId == id);
                _db.Towns.RemoveRange(towns);

                var districts = _db.Districts.Where(i => i.ProvinceId == id);
                _db.Districts.RemoveRange(districts);

				var qualif = _db.Qualifications.Where(i => i.IssuancePlace == id);
				_db.Qualifications.RemoveRange(qualif);

                var province = _db.Provinces.FirstOrDefault(i => i.ProvinceId == id);
                _db.Provinces.Remove(province);

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
