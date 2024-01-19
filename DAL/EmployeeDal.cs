﻿using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class EmployeeDal : IEmployeeDal
    {
        private readonly EmployeesDBEntities _db;
        private readonly IProvinceDal _provinceDal;
        private readonly IDistrictDal _districtDal;
        private readonly ITownDal _townDal;

        public EmployeeDal(EmployeesDBEntities context, IProvinceDal province, IDistrictDal district, ITownDal town)
        {
            _db = context;
            _provinceDal = province;
            _districtDal = district;
            _townDal = town;
        }

        private IQueryable<EmployeeDto> GetData(string searchString)
        {
            return from e in _db.Employees
                   join d in _db.Ethnicities on e.Ethnicity equals d.Id
                   join j in _db.Jobs on e.Job equals j.Id
                   where string.IsNullOrEmpty(searchString) || e.Name.Trim().ToLower().Contains(searchString.ToLower())
                   select new EmployeeDto
                   {
                       Id = e.Id,
                       Name = e.Name,
                       Age = e.Age,
                       DateOfBirth = e.DateOfBirth,
                       JobName = j.JobName,
                       EthnicityName = d.EthnicityName,
                       IdCard = e.IdCard,
                       PhoneNumber = e.PhoneNumber,
                       Details = e.Details,
                       NumberDegree = _db.Qualifications.Count(i => i.EmployeeId == e.Id && i.ExpirationDate >= System.DateTime.Now),
                   };
        }


        public List<EmployeeDto> GetEmployeesData(string searchString, int pageIndex, int pageSize)
        {
            return GetData(searchString)
                      .OrderBy(i => i.Id)
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

        //Lấy tên công việc bằng id
        private string GetJobNameById(string id)
        {
            return _db.Jobs.First(x => x.Id == id).JobName;
        }

        //Lấy tên dân tộc bằng id
        private string GetEthnicityNameById(string id)
        {
            return _db.Ethnicities.First(x => x.Id == id).EthnicityName;
        }

        public EmployeeDto GetEmployeeById(int? id)
        {
            var employee = _db.Employees.FirstOrDefault(i => i.Id == id);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                DateOfBirth = employee.DateOfBirth,
                Age = employee.Age,
                IdCard = employee.IdCard,
                PhoneNumber = employee.PhoneNumber,
                JobName = GetJobNameById(employee.Job),
                EthnicityName = GetEthnicityNameById(employee.Ethnicity),
                Province = _provinceDal.GetProvinceById(employee.ProvinceId),
                District = _districtDal.GetDistrictById(employee.DistrictId),
                Town = _townDal.GetTownById(employee.TownId),
                Details = employee.Details,
                NumberDegree = _db.Qualifications.Count(i => i.EmployeeId == id),
            };
        }

        public int GetNumberOfRecords(string searchString)
        {
            return (from e in _db.Employees
                    join d in _db.Ethnicities on e.Ethnicity equals d.Id
                    join j in _db.Jobs on e.Job equals j.Id
                    where string.IsNullOrEmpty(searchString) || e.Name.Trim().ToLower().Contains(searchString.ToLower())
                    select e)
                    .Count();
        }
    }
}
