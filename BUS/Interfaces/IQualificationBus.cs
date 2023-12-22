using DTO;
using System.Collections.Generic;


namespace BUS.Interfaces
{
    public interface IQualificationBus
    {
        List<QualificationDto> GetQualificationsData(string searchString);
        bool AddQualificatio(QualificationDto qualificationDto);
        bool UpdateQualificatio(QualificationDto qualificationDto);
        bool DeleteQualificatio(int id);
        List<QualificationDto> GetQualificationsByEmployeeId(int id);
    }
}
