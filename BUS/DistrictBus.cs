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
            var districts = _district.GetDistrictsData(searchString);
            return SetDistrictsViewModel(districts);
        }

        public bool AddDistrict(DistrictDto districtDto)
        {
            try
            {
                var district = SetDistrictModel(districtDto);
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
                var district = SetDistrictModel(districtDto);
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
                var district = SetDistrictModel(districtDto);
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
            return SetDistrictDtoModel(district);
        }

        public DistrictViewModel GetDistrictViewModel(int id)
        {
            var district = _district.GetDistrictById(id);
            return SetDistrictViewModel(district);
        }

        public List<DistrictViewModel> GetDistrictsByProvinceId(int provinceId)
        {
            var districts = _district.GetDistrictsByProvinceId(provinceId);
            return SetDistrictsViewModel(districts);
        }

        private District SetDistrictModel(DistrictDto districtDto)
        {
            return new District(districtDto.Id, districtDto.DistrictName, districtDto.ProvinceId);
        }

        private DistrictDto SetDistrictDtoModel(District district)
        {
            return new DistrictDto(district.DistrictId, district.DistrictName, district.ProvinceId);
        }

        private DistrictViewModel SetDistrictViewModel(District district)
        {
            return new DistrictViewModel(district.DistrictId, district.DistrictName,
                district.Province.ProvinceName);
        }

        private List<DistrictViewModel> SetDistrictsViewModel(List<District> districts)
        {
            return districts.Select(i => SetDistrictViewModel(i)).ToList();
        }
    }
}
