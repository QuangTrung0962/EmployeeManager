using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL
{
	public class ProvinceDal : IProvinceDal
    {
		private readonly EmployeesDBEntities _db;
        private readonly IBaseDal<Province> _service;

		public ProvinceDal (EmployeesDBEntities db, IBaseDal<Province> service)
		{
			_db = db;
            _service = service;
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
				_service.InsertEntity(province);
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

		public bool UpdateProvince(Province province)
		{
            try
            {
                if (province == null) return false;
                _service.UpdateEntity(province);
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
                Province province = _db.Provinces.FirstOrDefault(i => i.ProvinceId == id);
                if (province == null) return false;
                _db.Entry(province).State = EntityState.Detached;
                _service.DeleteEntity(province);
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
