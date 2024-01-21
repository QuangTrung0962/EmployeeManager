using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class EmployeeDal : IEmployeeDal
    {
        private readonly EmployeesDBEntities _db;

        public EmployeeDal(EmployeesDBEntities context)
        {
            _db = context;
        }

        private IQueryable<EmployeeDto> GetData(string searchString)
        {
            return from e in _db.Employees
                   join eth in _db.Ethnicities on e.EthnicityId equals eth.Id
                   join j in _db.Jobs on e.JobId equals j.Id
                   join p in _db.Provinces on e.ProvinceId equals p.ProvinceId
                   join d in _db.Districts on e.DistrictId equals d.DistrictId
                   join t in _db.Towns on e.TownId equals t.TownId
                   where string.IsNullOrEmpty(searchString) || e.Name.Trim().ToLower().Contains(searchString.ToLower())
                   select new EmployeeDto
                   {
                       Id = e.Id,
                       Name = e.Name,
                       Age = e.Age,
                       DateOfBirth = e.DateOfBirth,
                       JobId = j.Id,
                       JobName = j.JobName,
                       EthnicityId = eth.Id,
                       EthnicityName = eth.EthnicityName,
                       IdCard = e.IdCard,
                       PhoneNumber = e.PhoneNumber,
                       ProvinceId = p.ProvinceId,
                       DistrictId = d.DistrictId,
                       TownId = t.TownId,
                       Details = e.Details,
                       NumberDegree = _db.Qualifications.Count(i => i.EmployeeId == e.Id && i.ExpirationDate >= System.DateTime.Now),
                   };
        }


        public List<EmployeeDto> GetEmployeesData(string searchString, int pageIndex, int pageSize)
        {
            return GetData(searchString)
                      .OrderBy(i => i.Name)
                      .Skip((pageIndex - 1) * pageSize)
                      .Take(pageSize)
                      .ToList();
        }

        public List<EmployeeDto> GetDataForExcel()
        {
            return GetData(null).ToList();
        }

        //Lấy danh sách công việc
        public List<JobDto> GetJobsData()
        {
            return (from j in _db.Jobs
                    select new JobDto
                    {
                        Id = j.Id,
                        JobName = j.JobName,
                    }).ToList();
        }

        //Lấy danh sách dân tộc
        public List<EthnicityDto> GetEthnicitiesData()
        {
            return (from e in _db.Ethnicities
                    select new EthnicityDto
                    {
                        Id = e.Id,
                        EthnicityName = e.EthnicityName,
                    }).ToList();
        }

        public EmployeeDto GetEmployeeById(int? id)
        {
            return (from e in _db.Employees
                    join eth in _db.Ethnicities on e.EthnicityId equals eth.Id
                    join j in _db.Jobs on e.JobId equals j.Id
                    join p in _db.Provinces on e.ProvinceId equals p.ProvinceId
                    join d in _db.Districts on e.DistrictId equals d.DistrictId
                    join t in _db.Towns on e.TownId equals t.TownId
                    where e.Id == id
                   select new EmployeeDto
                   {
                       Id = e.Id,
                       Name = e.Name,
                       Age = e.Age,
                       DateOfBirth = e.DateOfBirth,
                       JobId = j.Id,
                       JobName = j.JobName,
                       EthnicityId = eth.Id,
                       EthnicityName = eth.EthnicityName,
                       IdCard = e.IdCard,
                       PhoneNumber = e.PhoneNumber,
                       ProvinceId = p.ProvinceId,
                       DistrictId = d.DistrictId,
                       TownId = t.TownId,
                       Details = e.Details,
                       NumberDegree = _db.Qualifications.Count(i => i.EmployeeId == e.Id && i.ExpirationDate >= System.DateTime.Now),
                   }).FirstOrDefault();
        }

        public int GetNumberOfRecords(string searchString)
        {
            return (from e in _db.Employees
                    join eth in _db.Ethnicities on e.EthnicityId equals eth.Id
                    join j in _db.Jobs on e.JobId equals j.Id
                    join p in _db.Provinces on e.ProvinceId equals p.ProvinceId
                    join d in _db.Districts on e.DistrictId equals d.DistrictId
                    join t in _db.Towns on e.TownId equals t.TownId
                    where string.IsNullOrEmpty(searchString) || e.Name.Trim().ToLower().Contains(searchString.ToLower())
                    select e)
                    .Count();
        }
    }
}
