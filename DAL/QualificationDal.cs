using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class QualificationDal : IQualificationDal
    {
        private readonly EmployeesDBEntities _db;
        public QualificationDal(EmployeesDBEntities context)
        {
            _db = context;
        }

        public List<QualificationDto> GetQualificationsData(string searchString)
        {
            return (from q in _db.Qualifications
                    join e in _db.Employees on q.EmployeeId equals e.Id
                    join p in _db.Provinces on q.IssuancePlace equals p.ProvinceId
                    where q.Name.Trim().ToLower().Contains(searchString.Trim().ToLower()) || 
                    string.IsNullOrEmpty(searchString) 
                    select new QualificationDto
                    {
                        Id = q.Id,
                        Name = q.Name,
                        ReleaseDate = q.ReleaseDate,
                        IssuancePlace = q.IssuancePlace,
                        ExpirationDate = q.ExpirationDate,
                        EmployeeId = e.Id,
                        IssuancePlaceName = p.ProvinceName
                    }).ToList();
        }

        public List<QualificationDto> GetQualificationsByEmployeeId(int id)
        {
            return (from p in _db.Provinces join q in _db.Qualifications 
                    on p.ProvinceId equals q.IssuancePlace 
                    where  q.EmployeeId == id 
                    select new QualificationDto 
                    { 
                        Id = q.Id,
                        Name = q.Name,
                        ReleaseDate = q.ReleaseDate,
                        IssuancePlaceName = p.ProvinceName,
                        ExpirationDate = q.ExpirationDate,
                        EmployeeId = q.EmployeeId,
                    }).ToList();
        }

        public QualificationDto GetQualificationById(int id)
        {
            var obj = _db.Qualifications.FirstOrDefault(i => i.Id == id);

            if (obj == null) return null;

            return new QualificationDto
            {
                Id = obj.Id,
                Name = obj.Name,
                ReleaseDate = obj.ReleaseDate,
                ExpirationDate = obj.ExpirationDate,
                IssuancePlace = obj.IssuancePlace,
                EmployeeId = obj.EmployeeId
            };
        }
    }
}
