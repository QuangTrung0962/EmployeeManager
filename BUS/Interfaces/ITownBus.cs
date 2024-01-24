using DTO;
using DTO.ViewModels;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface ITownBus
    {
        List<TownViewModel> GetTownsByDistrictId(int districtId);
        TownDto GetTownById(int? id);
        TownViewModel GetTownViewModel(int id);
        List<TownViewModel> GetTownsData(string searchString);
        bool AddTown(TownDto townDto);
        bool UpdateTown(TownDto townDto);
        bool DeleteTown(int id);
    }
}
