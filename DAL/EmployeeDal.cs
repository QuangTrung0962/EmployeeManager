using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL
{
    public class EmployeeDal : IEmployeeDal
    {
        private readonly EmployeesDBEntities _db;
        private readonly IProvinceDal _provinceDal;
        private readonly IDistrictDal _districtDal;
        private readonly ITownDal _townDal;

        public EmployeeDal(EmployeesDBEntities context,IProvinceDal province, IDistrictDal district, ITownDal town)
        {
            _db = context;
            _provinceDal =  province;
            _districtDal = district;
            _townDal = town;
        }

        //Lấy danh sách nhân viên
        public List<EmployeeDto> GetEmployeesData(string searchString, int pageIndex, int pageSize)
        {
            var qualifications = _db.Qualifications.Where(i => i.ExpirationDate >= DateTime.Now);

            return (from e in _db.Employees
                    join d in _db.Ethnicities on e.Ethnicity equals d.Id
                    join j in _db.Jobs on e.Job equals j.Id
                    where string.IsNullOrEmpty(searchString) || e.Name.ToLower().Contains(searchString.ToLower())
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
                        NumberDegree = qualifications.Count(q => q.EmployeeId == e.Id),
                    }).OrderBy(i => i.Id)
                    .Skip((pageIndex - 1) * pageSize).
                    Take(pageSize).ToList();
        }

        //Thêm mới 1 nhân viên
        public bool AddEmployee(Employee employee)
        {
            try
            {
                _db.Employees.Add(employee);
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


        //Sửa thông tin nhân viên
        public bool UppdateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = _db.Employees.SingleOrDefault(i => i.Id == employeeDto.Id);
            try
            {
                if (employee != null)
                {
                    employee.Id = employeeDto.Id;
                    employee.Name = employeeDto.Name;
                    employee.Age = employeeDto.Age;
                    employee.DateOfBirth = employeeDto.DateOfBirth;
                    employee.Job = employeeDto.JobName;
                    employee.Ethnicity = employeeDto.EthnicityName;
                    employee.PhoneNumber = employeeDto.PhoneNumber;
                    employee.IdCard = employeeDto.IdCard;
                    employee.Details = employeeDto.Details;
                    employee.ProvinceId = employeeDto.Province.Id;
                    employee.DistrictId = employeeDto.District.Id;
                    employee.TownId = employeeDto.Town.Id;
                }
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

        public bool DeleteEmployeeData(int? id)
        {
            try
            {
                var employee = _db.Employees.SingleOrDefault(i => i.Id == id);
                _db.Employees.Remove(employee);
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

        //Lấy danh sách tỉnh/thành phố
        public List<ProvinceDto> GetProvincesData()
        {
            return (from i in _db.Provinces
                    select new ProvinceDto
                    {
                        Id = i.ProvinceId,
                        ProvinceName = i.ProvinceName,
                    }).ToList();
        }

        //Lấy danh sách quận/huyện
        public List<DistrictDto> GetDistrictsData()
        {
            return (from j in _db.Districts
                    select new DistrictDto
                    {
                        Id = j.DistrictId,
                        DistrictName = j.DistrictName,
                    }).ToList();
        }

        //Lấy danh sách xã
        public List<TownDto> GetTownsData()
        {
            return (from j in _db.Towns
                    select new TownDto
                    {
                        Id = j.TownId,
                        TownName = j.TownName,
                    }).ToList();
        }

        //Lấy tên công việc bằng id
        public string GetJobNameById(string id)
        {
            return _db.Jobs.First(x => x.Id == id).JobName;
        }

        //Lấy tên dân tộc bằng id
        public string GetEthnicityNameById(string id)
        {
            return _db.Ethnicities.First(x => x.Id == id).EthnicityName;
        }

        public EmployeeDto GetEmployeeById(int? id)
        {
            Employee employee = _db.Employees.First(x => x.Id == id);
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
                Province = _provinceDal.GetProvineById(employee.ProvinceId),
                District = _districtDal.GetDistrictById(employee.DistrictId),
                Town = _townDal.GetTownById(employee.TownId),
                Details = employee.Details,
            };
        }

        public List<EmployeeDto> GetDataForExcel()
        {
            var qualifications = _db.Qualifications.Where(i => i.ExpirationDate >= DateTime.Now);

            return (from e in _db.Employees
                    join d in _db.Ethnicities on e.Ethnicity equals d.Id
                    join j in _db.Jobs on e.Job equals j.Id
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
                        NumberDegree = qualifications.Count(q => q.EmployeeId == e.Id),
                    }).ToList();

        }

        public int GetNumberOfRecords(string searchString)
        {
            var qualifications = _db.Qualifications.Where(i => i.ExpirationDate >= DateTime.Now);

            return (from e in _db.Employees
                    join d in _db.Ethnicities on e.Ethnicity equals d.Id
                    join j in _db.Jobs on e.Job equals j.Id
                    where string.IsNullOrEmpty(searchString) || e.Name.ToLower().Contains(searchString.ToLower())
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
                        NumberDegree = qualifications.Count(q => q.EmployeeId == e.Id),
                    }).Count();
        }

    }
}
