using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;

namespace BUS
{
    public class DistrictBus : IDistrictBus
    {
        private readonly IDistrictDal _districtDal;

        public DistrictBus(IDistrictDal district)
        {
            _districtDal = district;
        }

        public bool AddDistrict(DistrictDto districtDto)
        {
            District district = new District()
            {
                DistrictId = districtDto.Id,
                DistrictName = districtDto.DistrictName,
                ProvinceId = districtDto.ProvinceId
            };

            if (_districtDal.AddDistrict(district)) return true;
            else return false;
        }

        public bool DeleteDistrict(int id)
        {
            if (_districtDal.DeleteDistrict(id)) return true;
            else return false;
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

        public bool UpdateDistrict(DistrictDto districtDto)
        {
            if (_districtDal.UpdateDistrict(districtDto)) return true;
            else return false;
        }
    }
}
