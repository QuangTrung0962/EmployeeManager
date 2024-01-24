using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProvinceDal
    {
        Province GetProvinceById(int? id);
        List<Province> GetProvincesData(string searchString);
    }
}
