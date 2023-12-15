using DTO;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface ITownBus
    {
        List<TownDto> GetDistrictsByProvinceId(int districtId);
    }
}
