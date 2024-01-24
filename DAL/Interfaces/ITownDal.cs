using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITownDal
    {
        Town GetTownById(int? id);
        List<Town> GetTownsByDistrictId(int districtId);
        List<Town> GetTownsData(string searchString);
    }
}
