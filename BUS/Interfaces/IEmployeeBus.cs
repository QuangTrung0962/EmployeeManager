using DTO;
using DTO.ViewModels;
using System.Collections.Generic;
using System.IO;

namespace BUS.Interfaces
{
    public interface IEmployeeBus
    {
        PageList<EmployeeViewModel> GetEmployeesData(string searchString, int? pageIndex,
            int? pageSize);
        EmployeeDto GetEmployeeById(int? id);
        EmployeeViewModel GetEmployeeViewModel(int id);
        bool AddEmployee(EmployeeDto employeeDto);
        bool UpdateEmployee(EmployeeDto employeeDto);
        bool DeleteEmployee(int? id);
        bool ExportExcel(string pathFile, string searchString);
        bool ImportExcel(Stream excelFileStream, out string errorMessage);
    }
}
