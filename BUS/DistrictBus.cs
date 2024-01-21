using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
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

        public bool AddDistrict(DistrictDto districtDto)
        {
            try
            {
                var district = 
                    new District(districtDto.Id, districtDto.DistrictName, districtDto.ProvinceId);
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
                District district = 
                    new District(districtDto.Id, districtDto.DistrictName, districtDto.ProvinceId);
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
                var district = 
                    new District(districtDto.Id, districtDto.DistrictName, districtDto.ProvinceId);
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
            return _district.GetDistrictById(id);
        }

        public List<DistrictDto> GetDistrictsByProvinceId(int provinceId)
        {
            return _district.GetDistrictsByProvinceId(provinceId);
        }

        public List<DistrictDto> GetDistrictsData(string searchString)
        {
            return _district.GetDistrictsData(searchString);
        }

    }
}
