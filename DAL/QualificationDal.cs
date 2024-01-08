using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

        public bool AddQualification(Qualification qualification)
        {
            try
            {
                _db.Qualifications.Add(qualification);
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteQualification(int id)
        {
            try
            {
                var obj = _db.Qualifications.FirstOrDefault(i => i.Id == id);
                _db.Qualifications.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
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

        public bool UpdateQualification(QualificationDto qualificationDto)
        {
            Qualification qualif = _db.Qualifications.FirstOrDefault(i => i.Id == qualificationDto.Id);

            if (qualif == null) return false;

            try
            {
                qualif.Id = qualificationDto.Id;
                qualif.Name = qualificationDto.Name;
                qualif.ReleaseDate = qualificationDto.ReleaseDate;
                qualif.IssuancePlace = qualificationDto.IssuancePlace;
                qualif.ExpirationDate = qualificationDto.ExpirationDate;
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
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
