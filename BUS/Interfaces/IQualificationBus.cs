using DTO;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface IQualificationBus
    {
        List<QualificationDto> GetQualificationsData(string searchString);
        bool AddQualification(QualificationDto qualificationDto);
        bool UpdateQualification(QualificationDto qualificationDto);
        bool DeleteQualification(int id);
        List<QualificationDto> GetQualificationsByEmployeeId(int id);
        QualificationDto GetQualificationById(int id);
    }
}
