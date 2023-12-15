using BUS.Interfaces;
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

        public List<DistrictDto> GetDistrictsByProvinceId(int pronviceId)
        {
            return _districtDal.GetDistrictsByProvinceId(pronviceId);
        }
    }
}
