using DTO;
using System.IO;

namespace BUS.Interfaces
{
    public interface IEmployeeBus
    {
        PageList<EmployeeDto> GetEmployeesData(string searchString, int? pageIndex, int? pageSize);
        EmployeeDto GetEmployeeById(int? id);
        bool AddEmployee(EmployeeDto employeeDto);
        bool UpdateEmployee(EmployeeDto employeeDto);
        bool DeleteEmployee(int? id);
        bool ExportExcel(string pathFile);
        bool ImportExcel(Stream excelFileStream, out string errorMessage);
        EmployeeDto AddEmployeeInfo(EmployeeDto employeeDto);
    }
}
