﻿using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface ITownDal
    {
        TownDto GetTownById(int? id);
        List<TownDto> GetTownsByDistrictId(int districtId);
        List<TownDto> GetTownsData(string searchString);
        bool AddTown(Town town);
        bool UpdateTown(TownDto townDto);
        bool DeleteTown(int id);
    }
}
