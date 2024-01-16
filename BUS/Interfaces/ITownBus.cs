using DTO;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface ITownBus
    {
        List<TownDto> GetTownsByDistrictId(int districtId);
        TownDto GetTownById(int? id);
        List<TownDto> GetTownsData(string searchString);
        bool AddTown(TownDto townDto);
        bool UpdateTown(TownDto townDto);
        bool DeleteTown(int id);
    }
}
