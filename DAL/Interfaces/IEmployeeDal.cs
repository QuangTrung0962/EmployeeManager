using DTO;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface IEmployeeDal
    {
        List<EmployeeDto> GetEmployeesData(string searchString, int pageIndex, int pageSize);
        List<JobDto> GetJobsData();
        List<EthnicityDto> GetEthnicitiesData();
        EmployeeDto GetEmployeeById(int? id);
        List<EmployeeDto> GetDataForExcel();
        int GetNumberOfRecords(string searchString);
    }
}
