using DTO;
using DTO.ViewModels;
using System.Collections.Generic;

namespace BUS.Interfaces
{
    public interface IQualificationBus
    {
        List<QualificationViewModel> GetQualificationsData(string searchString);
        bool AddQualification(QualificationDto qualificationDto);
        bool UpdateQualification(QualificationDto qualificationDto);
        bool DeleteQualification(int id);
        List<QualificationViewModel> GetQualificationsByEmployeeId(int id);
        QualificationDto GetQualificationById(int id);
    }
}
