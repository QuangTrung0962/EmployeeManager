using DAL;
using DTO;
using System.Collections.Generic;


namespace BUS.Interfaces
{
    public interface IProvinceBus
    {
        List<ProvinceDto> GetProvincesData(string searchString);
        ProvinceDto GetProvinceById(int? id);
        bool AddProvince(ProvinceDto provinceDto);
        bool UpdateProvince(ProvinceDto provinceDto);
        bool DeleteProvince(int id);
        Province SetProvinceModel(ProvinceDto provinceDto);
        ProvinceDto SetProvinceDtoModel(Province province);
    }
}
