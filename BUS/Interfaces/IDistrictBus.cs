using DTO;
using DTO.ViewModels;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface IDistrictBus
    {
        DistrictDto GetDistrictById(int? id);
        DistrictViewModel GetDistrictViewModel(int id);
        List<DistrictViewModel> GetDistrictsData(string searchString);
        List<DistrictViewModel> GetDistrictsByProvinceId(int provinceId);
        bool AddDistrict(DistrictDto districtDto);
        bool UpdateDistrict(DistrictDto districtDto);
        bool DeleteDistrict(int id);
    }
}
