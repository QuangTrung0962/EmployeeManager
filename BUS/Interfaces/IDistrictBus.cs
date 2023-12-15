using DTO;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface IDistrictBus
    {
        List<DistrictDto> GetDistrictsByProvinceId(int pronviceId);
    }
}
