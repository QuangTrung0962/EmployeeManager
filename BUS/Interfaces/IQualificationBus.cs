using DTO;
using System.Collections.Generic;


namespace BUS.Interfaces
{
    public interface IQualificationBus
    {
        List<QualificationDto> GetQualificationsData(string searchString);
    }
}
