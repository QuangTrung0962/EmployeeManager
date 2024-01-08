using DTO;
using System.Collections.Generic;


namespace BUS.Interfaces
{
    public interface IProvinceBus
    {
        List<ProvinceDto> GetProvincesData(string searchString);
        ProvinceDto GetProvinceById(int? id);
        bool AddProvince(ProvinceDto provinceDTO);
        bool UpdateProvince(ProvinceDto provinceDTO);
        bool DeleteProvince(int id);
    }
}
