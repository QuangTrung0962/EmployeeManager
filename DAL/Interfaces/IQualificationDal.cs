using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IQualificationDal
    {
        List<QualificationDto> GetQualificationsData(string searchString);
    }
}
