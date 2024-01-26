using DTO;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface IEmployeeDal
    {
        List<Employee> GetEmployeesData(string searchString, int pageIndex, int pageSize);
        List<Job> GetJobsData();
        List<Ethnicity> GetEthnicitiesData();
        Employee GetEmployeeById(int? id);
        List<Employee> GetDataForExcel(string searchString);
        int GetNumberOfRecords(string searchString);
    }
}
