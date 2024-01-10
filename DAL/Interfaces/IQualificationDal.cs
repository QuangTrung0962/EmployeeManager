using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IQualificationDal
    {
        List<QualificationDto> GetQualificationsData(string searchString);
        List<QualificationDto> GetQualificationsByEmployeeId(int id);
        QualificationDto GetQualificationById(int id);
    }
}
