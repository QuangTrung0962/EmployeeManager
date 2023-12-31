﻿using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;


namespace BUS
{
    public class ProvinceBus : IProvinceBus
    {
        private readonly IProvinceDal _provinceDal;

        public ProvinceBus(IProvinceDal provinces)
        {
            _provinceDal = provinces;
        }

        public List<ProvinceDto> GetProvincesData(string searchString)
        {
            return _provinceDal.GetProvincesData(searchString);
        }

        public ProvinceDto GetProvinceById(int? id)
        {
            return _provinceDal.GetProvinceById(id);
        }

        public bool AddProvince(ProvinceDto provinceDTO)
        {
            Province province = new Province()
            {
                ProvinceId = provinceDTO.Id,
                ProvinceName = provinceDTO.ProvinceName
            };

            if (_provinceDal.AddProvince(province)) return true;
            else return false;
        }

        public bool UpdateProvince(ProvinceDto provinceDTO)
        {
            if (_provinceDal.UpdateProvince(provinceDTO)) return true;
            else return false;
        }

        public bool DeleteProvince(int id)
        {
            if (_provinceDal.DeleteProvince(id)) return true;
            else return false;
        }
    }
}
