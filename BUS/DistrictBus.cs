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
        private readonly IDistrictDal _districtDal;
        private readonly IBaseDal<District> _baseDal;
        private readonly ILog _log;

        public DistrictBus(IDistrictDal district, IBaseDal<District> baseDal)
        {
            _districtDal = district;
            _baseDal = baseDal;
            _log = LogManager.GetLogger(typeof(DistrictBus));
        }

        public bool AddDistrict(DistrictDto districtDto)
        {
            try
            {
                District district = SetDistrictModel(districtDto);
                _baseDal.InsertEntity(district);
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
                District district = SetDistrictModel(districtDto);
                _baseDal.DeleteEntity(district);
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
                District district = SetDistrictModel(districtDto);
                _baseDal.UpdateEntity(district);
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
            return _districtDal.GetDistrictById(id);
        }

        public List<DistrictDto> GetDistrictsByProvinceId(int pronviceId)
        {
            return _districtDal.GetDistrictsByProvinceId(pronviceId);
        }

        public List<DistrictDto> GetDistrictsData(string searchString)
        {
            return _districtDal.GetDistrictsData(searchString);
        }

        public District SetDistrictModel(DistrictDto districtDto)
        {
            return new District(districtDto.Id, districtDto.DistrictName, districtDto.ProvinceId);
        }

        public DistrictDto SetDistrictDtoModel(District district)
        {
            return new DistrictDto(district.DistrictId, district.DistrictName, district.ProvinceId);
        }
    }
}
