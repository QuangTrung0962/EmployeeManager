using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
using log4net;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class ProvinceBus : IProvinceBus
    {
        private readonly IProvinceDal _province;
        private readonly IBaseDal<Province> _base;
        private readonly ILog _log;

        public ProvinceBus(IProvinceDal province, IBaseDal<Province> baseDal)
        {
            _province = province;
            _base = baseDal;
            _log = LogManager.GetLogger(typeof(ProvinceBus));
        }

        public List<ProvinceViewModel> GetProvincesData(string searchString)
        {
            return _province.GetProvincesData(searchString);
        }

        public ProvinceDto GetProvinceById(int? id)
        {
            var province = _province.GetProvinceById(id);
            return new ProvinceDto(province);
        }

        public bool AddProvince(ProvinceDto provinceDto)
        {
            try
            {
                var province = new Province(provinceDto);
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
                var province = new Province(provinceDto);
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
                var province = new Province(provinceDto);
                _base.DeleteEntity(province);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

    }
}
