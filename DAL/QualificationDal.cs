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
            return _db.Qualifications.Where(j => j.Name.ToLower()
            .Contains(searchString.ToLower()) || string.IsNullOrEmpty(searchString))
                .AsEnumerable()
                .Select(i => new QualificationDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ReleaseDate = i.ReleaseDate,
                    IssuancePlace = i.IssuancePlace,
                    ExpirationDate = i.ExpirationDate,
                    Employee = new EmployeeDto { Id = i.EmployeeId },
                    IssuancePlaceName = _db.Provinces.First(j => j.ProvinceId == i.IssuancePlace).ProvinceName
                }).ToList();
        }
    }
}
