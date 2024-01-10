using DTO;
using System.Collections.Generic;


namespace DAL.Interfaces
{
    public interface IEmployeeDal
    {
        List<EmployeeDto> GetEmployeesData(string searchString, int pageIndex, int pageSize);
        List<JobDto> GetJobsData();
        List<EthnicityDto> GetEthnicitiesData();
        List<ProvinceDto> GetProvincesData();
        List<DistrictDto> GetDistrictsData();
        List<TownDto> GetTownsData();
        string GetJobNameById(string id);
        string GetEthnicityNameById(string id);
        EmployeeDto GetEmployeeById(int? id);
        List<EmployeeDto> GetDataForExcel();
        int GetNumberOfRecords(string searchString);
    }
}
