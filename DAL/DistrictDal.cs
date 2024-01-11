﻿using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;


namespace DAL
{
    public class DistrictDal : IDistrictDal
    {
        private readonly EmployeesDBEntities _db;
        private readonly IBaseDal<District> _service;
        public DistrictDal(EmployeesDBEntities context, IBaseDal<District> service)
        {
            _db = context;
            _service = service;
        }

        public List<DistrictDto> GetDistrictsData(string searchString)
        {
            bool isNumeric = int.TryParse(searchString, out int provinceId);

            if (isNumeric)
            {
                return _db.Districts.Where(i => i.ProvinceId.ToString().Trim() == searchString)
               .Select(i => new DistrictDto
               {
                   Id = i.DistrictId,
                   DistrictName = i.DistrictName,
                   ProvinceId = i.ProvinceId,
               })
               .ToList();
            }

            return _db.Districts.Where(i => i.DistrictName.Trim().ToLower().Contains(searchString.Trim().ToLower()) || string.IsNullOrEmpty(searchString))
               .Select(i => new DistrictDto
               {
                   Id = i.DistrictId,
                   DistrictName = i.DistrictName,
                   ProvinceId = i.ProvinceId,
               }).ToList();
        }

        public DistrictDto GetDistrictById(int? id)
        {
            var district = _db.Districts.FirstOrDefault(x => x.DistrictId == id);

            if (district == null) return null;
            return new DistrictDto()
            {
                Id = district.DistrictId,
                DistrictName = district.DistrictName,
                ProvinceId = district.ProvinceId,
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

    }
}
