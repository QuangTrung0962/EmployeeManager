using DTO;
using DTO.ViewModels;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface IDistrictDal
    {
        District GetDistrictById(int? id);
        List<DistrictViewModel> GetDistrictsData(string searchString);
        List<DistrictViewModel> GetDistrictsByProvinceId(int provinceId);
    }
}
