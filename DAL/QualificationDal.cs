using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class QualificationDal : IQualificationDal
    {
        private readonly EmployeesDBEntities _db;
        public QualificationDal(EmployeesDBEntities db)
        {
            _db = db;
        }

        private IQueryable<Qualification> GetData()
        {
            return _db.Qualifications
                    .Include(i => i.Employee)
                    .Include(i => i.Province);
        }


        public List<Qualification> GetQualificationsData(string searchString)
        {
            return GetData()
                     .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.Name.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                     .ToList();


        }

        public List<Qualification> GetQualificationsByEmployeeId(int id)
        {
            return GetData().Where(i => i.EmployeeId == id).ToList();
        }

        public Qualification GetQualificationById(int id)
        {
            return GetData().FirstOrDefault(i => i.Id == id);
        }
    }
}
