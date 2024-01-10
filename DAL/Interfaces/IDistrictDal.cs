using DTO;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface IDistrictDal
    {
        DistrictDto GetDistrictById(int? id);
        List<DistrictDto> GetDistrictsData(string searchString);
        List<DistrictDto> GetDistrictsByProvinceId(int pronviceId);
    }
}
