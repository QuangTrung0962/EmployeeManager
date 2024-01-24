using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var provinces = _province.GetProvincesData(searchString);
            return SetProvincesViewModel(provinces);
        }

        public ProvinceDto GetProvinceById(int? id)
        {
            var province = _province.GetProvinceById(id);
            return SetProvinceDtoModel(province);
        }

        public bool AddProvince(ProvinceDto provinceDto)
        {
            try
            {
                var province = SetProvinceModel(provinceDto);
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
                var province = SetProvinceModel(provinceDto);
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
                var province = SetProvinceModel(provinceDto);
                _base.DeleteEntity(province);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        private Province SetProvinceModel(ProvinceDto provinceDto)
        {
            return new Province(provinceDto.Id, provinceDto.ProvinceName);
        }

        private ProvinceDto SetProvinceDtoModel(Province province)
        {
            return new ProvinceDto(province.ProvinceId, province.ProvinceName);
        }

        private ProvinceViewModel SetProvinceViewModel(Province province)
        {
            return new ProvinceViewModel(province.ProvinceId, province.ProvinceName);
        }

        private List<ProvinceViewModel> SetProvincesViewModel(List<Province> provinces)
        {
            return provinces.Select(i => SetProvinceViewModel(i)).ToList();
        }

    }
}
