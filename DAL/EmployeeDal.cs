using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Data.Entity;
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

        private IQueryable<Employee> GetData()
        {

            return _db.Employees
                    .Include(i => i.Ethnicity)
                    .Include(i => i.Job)
                    .Include(i => i.District)
                    .Include(i => i.Province)
                    .Include(i => i.Town)
                    .Include(i => i.Qualifications);
        }

        public List<Employee> GetEmployeesData(string searchString, int pageIndex, int pageSize)
        {
            return GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.Name.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .OrderBy(i => i.Name)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        public List<Employee> GetDataForExcel(string searchString)
        {
            return GetData()
                .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.Name.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                .ToList();
        }

        //Lấy danh sách công việc
        public List<Job> GetJobsData()
        {
            return _db.Jobs.ToList();
        }

        //Lấy danh sách dân tộc
        public List<Ethnicity> GetEthnicitiesData()
        {
            return _db.Ethnicities.ToList();
        }

        public Employee GetEmployeeById(int? id)
        {
            return GetData().Where(i => i.Id == id).FirstOrDefault();
        }

        public int GetNumberOfRecords(string searchString)
        {
            return GetData()
                    .Where(i => string.IsNullOrEmpty(searchString) ||
                        i.Name.Trim().ToLower().Contains(searchString.Trim().ToLower()))
                    .Count();
        }
    }
}
