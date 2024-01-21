using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using log4net;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class ProvinceBus : Interfaces.IProvinceBus
    {
        private readonly DAL.Interfaces.IProvinceDal _province;
        private readonly IBaseDal<Province> _base;
        private readonly ILog _log;

        public ProvinceBus(DAL.Interfaces.IProvinceDal province, IBaseDal<Province> baseDal)
        {
            _province = province;
            _base = baseDal;
            _log = LogManager.GetLogger(typeof(ProvinceBus));
        }

        public List<ProvinceDto> GetProvincesData(string searchString)
        {
            return _province.GetProvincesData(searchString);
        }

        public ProvinceDto GetProvinceById(int? id)
        {
            return _province.GetProvinceById(id);
        }

        public bool AddProvince(ProvinceDto provinceDto)
        {
            try
            {
                var province = new Province(provinceDto.Id, provinceDto.ProvinceName);
                _base.InsertEntity(province);
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
                var province = new Province(provinceDto.Id, provinceDto.ProvinceName);
                _base.UpdateEntity(province);
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
                var provinceDto = GetProvinceById(id);
                var province = new Province(provinceDto.Id, provinceDto.ProvinceName);
                _base.DeleteEntity(province);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public List<ProvinceDto> ProvincesDataForDropdown()
        {
            return _province.GetProvincesData(null);
        }
    }
}
