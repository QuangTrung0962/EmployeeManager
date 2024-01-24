using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IQualificationDal
    {
        List<Qualification> GetQualificationsData(string searchString);
        List<Qualification> GetQualificationsByEmployeeId(int id);
        Qualification GetQualificationById(int id);
    }
}
