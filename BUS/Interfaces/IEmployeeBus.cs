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
        IEnumerable<SelectListItem> GetProvinceDataForDropdown();
        IEnumerable<SelectListItem> GetDistrcitDataForDropdown();
        IEnumerable<SelectListItem> GetTownDataForDropdown();
        bool AddEmployee(EmployeeDto employeeDTO);
        EmployeeDto GetEmployeeById(int? id);
        bool UpdateEmployee(EmployeeDto employeeDTO);
        bool DeleteEmployee(int? id);
        XLWorkbook ExportEmployeesData(List<EmployeeDto> employees);
        List<EmployeeDto> GetDataForExcel();
        int GetNumberOfRecords(string searchString);
        int GetPageIndex(int? page);
        int GetPageSize(int? page);
        bool SaveExcelFile(XLWorkbook workbook, string pathFile);
        bool ImportExcel(Stream excelFileStream, out string errorMessage);
    }
}
