using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProvinceDal
    {
        ProvinceDto GetProvineById(int? id);
        List<ProvinceDto> GetProvincesData(string searchString);
        bool AddProvince(Province province);
        bool UpdateProvince(ProvinceDto provinceDto);
        bool DeleteProvince(int id);
    }
}
