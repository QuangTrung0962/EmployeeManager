using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IQualificationDal
    {
        List<QualificationDto> GetQualificationsData(string searchString);
        bool AddQualificatio(Qualification qualification);
        bool UpdateQualificatio(QualificationDto qualificationDto);
        bool DeleteQualificatio(int id);
        List<QualificationDto> GetQualificationsByEmployeeId(int id);
    }
}
