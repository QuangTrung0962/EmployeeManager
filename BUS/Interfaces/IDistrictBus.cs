using DTO;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface IDistrictBus
    {
        DistrictDto GetDistrictById(int? id);
        List<DistrictDto> GetDistrictsData(string searchString);
        List<DistrictDto> GetDistrictsByProvinceId(int provinceId);
        bool AddDistrict(DistrictDto districtDto);
        bool UpdateDistrict(DistrictDto districtDto);
        bool DeleteDistrict(int id);
    }
}
