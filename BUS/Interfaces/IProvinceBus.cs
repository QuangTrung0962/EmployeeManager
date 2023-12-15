using DTO;
using System.Collections.Generic;


namespace BUS.Interfaces
{
    public interface IProvinceBus
    {
        List<ProvinceDto> GetProvincesData(string searchString);
        ProvinceDto GetProvineById(int? id);
        bool AddProvice(ProvinceDto provinceDTO);
        bool UpdateProvince(ProvinceDto provinceDTO);
        bool DeleteProvice(int id);
    }
}
