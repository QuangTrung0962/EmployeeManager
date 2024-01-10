using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProvinceDal
    {
        ProvinceDto GetProvinceById(int? id);
        List<ProvinceDto> GetProvincesData(string searchString);
    }
}
