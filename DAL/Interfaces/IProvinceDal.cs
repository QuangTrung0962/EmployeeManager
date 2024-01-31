using DTO;
using DTO.ViewModels;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProvinceDal
    {
        Province GetProvinceById(int? id);
        List<ProvinceViewModel> GetProvincesData(string searchString);
    }
}
