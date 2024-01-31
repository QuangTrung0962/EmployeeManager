using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
using log4net;
using System;
using System.Collections.Generic;


namespace BUS
{
    public class DistrictBus : IDistrictBus
    {
        private readonly IDistrictDal _district;
        private readonly IBaseDal<District> _base;
        private readonly ILog _log;

        public DistrictBus(IDistrictDal district, IBaseDal<District> baseDal)
        {
            _district = district;
            _base = baseDal;
            _log = LogManager.GetLogger(typeof(DistrictBus));
        }

        public List<DistrictViewModel> GetDistrictsData(string searchString)
        {
            return _district.GetDistrictsData(searchString);
        }

        public bool AddDistrict(DistrictDto districtDto)
        {
            try
            {
                var district = new District(districtDto);
                _base.InsertEntity(district);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool DeleteDistrict(int id)
        {
            try
            {
                var districtDto = GetDistrictById(id);
                var district = new District(districtDto);
                _base.DeleteEntity(district);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool UpdateDistrict(DistrictDto districtDto)
        {
            try
            {
                var district = new District(districtDto);
                _base.UpdateEntity(district);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public DistrictDto GetDistrictById(int? id)
        {
            var district = _district.GetDistrictById(id);
            return new DistrictDto(district);
        }

        public DistrictViewModel GetDistrictViewModel(int id)
        {
            var district = _district.GetDistrictById(id);
            return new DistrictViewModel(district);
        }

        public List<DistrictViewModel> GetDistrictsByProvinceId(int provinceId)
        {
            return _district.GetDistrictsByProvinceId(provinceId);
        }
    }
}
