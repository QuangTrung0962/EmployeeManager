using ClosedXML.Excel;
using DTO;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace BUS.Interfaces
{
    public interface IEmployeeBus
    {
        PageList<EmployeeDto> GetEmployeesData(string searchString, int? pageIndex, int? pageSize);
        IEnumerable<SelectListItem> GetJobsDataForDropdown();
        IEnumerable<SelectListItem> GetEthnicityDataForDropdown();
        List<ProvinceDto> GetProvinceDataForDropdown();
        bool AddEmployee(EmployeeDto employeeDto);
        EmployeeDto GetEmployeeById(int? id);
        bool UpdateEmployee(EmployeeDto employeeDto);
        bool DeleteEmployee(int? id);
        XLWorkbook ExportEmployeesData(List<EmployeeDto> employees);
        List<EmployeeDto> GetDataForExcel();
        int GetNumberOfRecords(string searchString);
        bool SaveExcelFile(XLWorkbook workbook, string pathFile);
        bool ImportExcel(Stream excelFileStream, out string errorMessage);
    }
}
