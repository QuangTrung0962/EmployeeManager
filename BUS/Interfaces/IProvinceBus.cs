using DTO;
using DTO.ViewModels;
using System.Collections.Generic;


namespace BUS.Interfaces
{
    public interface IProvinceBus
    {
        List<ProvinceViewModel> GetProvincesData(string searchString);
        ProvinceDto GetProvinceById(int? id);
        bool AddProvince(ProvinceDto provinceDto);
        bool UpdateProvince(ProvinceDto provinceDto);
        bool DeleteProvince(int id);
        
    }
}
