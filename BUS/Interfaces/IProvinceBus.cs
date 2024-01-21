using DTO;
using System.Collections.Generic;


namespace BUS.Interfaces
{
    public interface IProvinceBus
    {
        List<ProvinceDto> GetProvincesData(string searchString);
        List<ProvinceDto> ProvincesDataForDropdown();
        ProvinceDto GetProvinceById(int? id);
        bool AddProvince(ProvinceDto provinceDto);
        bool UpdateProvince(ProvinceDto provinceDto);
        bool DeleteProvince(int id);
        
    }
}
