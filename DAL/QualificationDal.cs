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

        public bool AddQualificatio(Qualification qualification)
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

        public bool DeleteQualificatio(int id)
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

        public bool UpdateQualificatio(QualificationDto qualificationDto)
        {
            Qualification qualification = _db.Qualifications.FirstOrDefault(i => i.Id == qualificationDto.Id);

            if (qualification == null) return false;

            try
            {
                qualification.Id = qualificationDto.Id;
                qualification.Name = qualificationDto.Name;
                qualification.ReleaseDate = qualificationDto.ReleaseDate;
                qualification.IssuancePlace = qualificationDto.IssuancePlace;
                qualification.ExpirationDate = qualificationDto.ExpirationDate;
                qualification.EmployeeId = qualificationDto.EmployeeId;
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
            return _db.Qualifications.Where(i => i.EmployeeId == id)
                .AsEnumerable()
                .Select(i => new QualificationDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    ReleaseDate = i.ReleaseDate,
                    IssuancePlaceName = _db.Provinces.Find(i.IssuancePlace).ProvinceName,
                    ExpirationDate = i.ExpirationDate,
                    EmployeeId = i.EmployeeId
                }).ToList();


        }
    }
}
