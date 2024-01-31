using DTO;
using DTO.ViewModels;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface IEmployeeDal
    {
        List<EmployeeViewModel> GetEmployeesData(string searchString, int pageIndex, int pageSize);
        List<Job> GetJobsData();
        List<Ethnicity> GetEthnicitiesData();
        Employee GetEmployeeById(int? id);
        List<EmployeeViewModel> GetDataForExcel(string searchString);
        int GetNumberOfRecords(string searchString);
    }
}
