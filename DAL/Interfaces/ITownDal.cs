using DTO;
using DTO.ViewModels;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITownDal
    {
        Town GetTownById(int? id);
        List<TownViewModel> GetTownsByDistrictId(int districtId);
        List<TownViewModel> GetTownsData(string searchString);
    }
}
