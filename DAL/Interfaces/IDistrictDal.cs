using DTO;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface IDistrictDal
    {
        District GetDistrictById(int? id);
        List<District> GetDistrictsData(string searchString);
        List<District> GetDistrictsByProvinceId(int provinceId);
    }
}
