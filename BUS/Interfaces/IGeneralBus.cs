using DTO;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface IGeneralBus
    {
        List<JobDto> LoadJobOptions();
        List<EthnicityDto> LoadEthnicityOptions();
        List<ProvinceDto> LoadProvinceOptions();
    }
}
