using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IQualificationDal
    {
        List<QualificationDto> GetQualificationsData(string searchString);
        bool AddQualificatio(Qualification qualification);
        bool UpdateQualification(QualificationDto qualificationDto);
        bool DeleteQualification(int id);
        List<QualificationDto> GetQualificationsByEmployeeId(int id);
        QualificationDto GetQualificationById(int id);
    }
}
