using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using log4net;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class ProvinceBus : IProvinceBus
    {
        private readonly IProvinceDal _provinceDal;
        private readonly IBaseDal<Province> _baseDal;
        private readonly ILog _log;

        public ProvinceBus(IProvinceDal provinces, IBaseDal<Province> baseDal)
        {
            _provinceDal = provinces;
            _baseDal = baseDal;
            _log = LogManager.GetLogger(typeof(ProvinceBus));
        }

        public List<ProvinceDto> GetProvincesData(string searchString)
        {
            return _provinceDal.GetProvincesData(searchString);
        }

        public ProvinceDto GetProvinceById(int? id)
        {
            return _provinceDal.GetProvinceById(id);
        }

        public bool AddProvince(ProvinceDto provinceDto)
        {
            try
            {
                Province province = SetProvinceModel(provinceDto);
                _baseDal.InsertEntity(province);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool UpdateProvince(ProvinceDto provinceDto)
        {
            try
            {
                Province province = SetProvinceModel(provinceDto);
                _baseDal.UpdateEntity(province);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool DeleteProvince(int id)
        {
            try
            {
                ProvinceDto provinceDto = GetProvinceById(id);
                Province province = SetProvinceModel(provinceDto);
                _baseDal.DeleteEntity(province);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public Province SetProvinceModel(ProvinceDto provinceDto)
        {
            return new Province(provinceDto.Id, provinceDto.ProvinceName);
        }

        public ProvinceDto SetProvinceDtoModel(Province province)
        {
            return new ProvinceDto(province.ProvinceId, province.ProvinceName);
        }

      
    }
}
