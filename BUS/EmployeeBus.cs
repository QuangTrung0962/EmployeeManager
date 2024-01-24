using ClosedXML.Excel;
using DTO;
using System.Collections.Generic;
using System.Linq;
using DTO.Utils;
using BUS.Interfaces;
using DAL.Interfaces;
using System;
using System.IO;
using log4net;
using DTO.ViewModels;

namespace BUS
{
    public class EmployeeBus : IEmployeeBus
    {
        private readonly IEmployeeDal _employee;
        private readonly IBaseDal<Employee> _base;
        private readonly ILog _log;

        public EmployeeBus(IEmployeeDal employee, IBaseDal<Employee> baseDal)
        {
            _employee = employee;
            _base = baseDal;
            _log = LogManager.GetLogger(typeof(EmployeeBus));
        }

        private int GetNumberOfRecords(string searchString)
        {
            return _employee.GetNumberOfRecords(searchString);
        }

        public PageList<EmployeeViewModel> GetEmployeesData(string searchString, int? pageIndex, int? pageSize)
        {
            try
            {
                var page = new Paging(pageSize, pageIndex);

                var employees =
                    _employee.GetEmployeesData(searchString, page.PageIndex, page.PageSize);

                var employeesViewModel = SetEmployeesViewModel(employees);
                var numberRecords = GetNumberOfRecords(searchString);

                return new PageList<EmployeeViewModel>(employeesViewModel, numberRecords, page.PageIndex,
                    page.PageSize, searchString);
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return new PageList<EmployeeViewModel>();
            }
        }

        public bool AddEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var employee = SetEmployeeModel(employeeDto);
                _base.InsertEntity(employee);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool UpdateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var employee = SetEmployeeModel(employeeDto);
                _base.UpdateEntity(employee);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool DeleteEmployee(int? id)
        {
            try
            {
                var employeeDto = GetEmployeeById(id);
                var employee = SetEmployeeModel(employeeDto);
                _base.DeleteEntity(employee);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public EmployeeDto GetEmployeeById(int? id)
        {
            if (id == null) return null;
            var employee = _employee.GetEmployeeById(id);
            return SetEmployeeDtoModel(employee);
        }

        public EmployeeViewModel GetEmployeeViewModel(int id)
        {
            var employee = _employee.GetEmployeeById(id);
            return SetEmployeeViewModel(employee);
        }

        private XLWorkbook CreateExcelList(List<EmployeeViewModel> employees)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Employees");

            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.Gray;
            headerRow.Style.Font.FontColor = XLColor.Black;

            worksheet.Cell("A1").Value = "ID";
            worksheet.Cell("B1").Value = "Tên Employees";
            worksheet.Cell("C1").Value = "Ngày sinh";
            worksheet.Cell("D1").Value = "Tuổi";
            worksheet.Cell("E1").Value = "Dân tộc";
            worksheet.Cell("F1").Value = "Công việc";
            worksheet.Cell("G1").Value = "CCCD";
            worksheet.Cell("H1").Value = "Số điện thoại";
            worksheet.Cell("I1").Value = "Văn bằng";

            var row = 2;
            foreach (var employee in employees)
            {
                worksheet.Cell(row, 1).Value = employee.Id;
                worksheet.Cell(row, 2).Value = employee.Name;
                worksheet.Cell(row, 3).Value = employee.DateOfBirth;
                worksheet.Cell(row, 4).Value = employee.Age;
                worksheet.Cell(row, 5).Value = employee.EthnicityName;
                worksheet.Cell(row, 6).Value = employee.JobName;
                worksheet.Cell(row, 7).Value = employee.IdCard;
                worksheet.Cell(row, 8).Value = employee.PhoneNumber;
                worksheet.Cell(row, 9).Value = employee.NumberDegree;
                row++;
            }

            return workbook;
        }

        private List<EmployeeViewModel> GetDataForExcel()
        {
            var employees = _employee.GetDataForExcel();

            return SetEmployeesViewModel(employees);
        }

        public bool ExportExcel(string pathFile)
        {
            try
            {
                var employees = GetDataForExcel();
                var workbook = CreateExcelList(employees);
                workbook.SaveAs(pathFile);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool ImportExcel(Stream excelFileStream, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (var workbook = new XLWorkbook(excelFileStream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var currentRow = 2;

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        var i = 0;
                        var name = row.Cell(++i).Value.ToString();
                        var dateOfBirth = row.Cell(++i).Value.ToString();
                        var ethnicityId = row.Cell(++i).Value.ToString();
                        var jobId = row.Cell(++i).Value.ToString();
                        var idCard = row.Cell(++i).Value.ToString();
                        var phoneNumber = row.Cell(++i).Value.ToString();
                        var provinceId = row.Cell(++i).Value.ToString();
                        var districtId = row.Cell(++i).Value.ToString();
                        var townId = row.Cell(++i).Value.ToString();
                        var addressDescription = row.Cell(i).Value.ToString();

                        if (!ExcelValidators.IsNonEmptyString(name) ||
                            !ExcelValidators.IsNonEmptyString(phoneNumber) ||
                            !ExcelValidators.IsNonEmptyString(idCard) ||
                            !ExcelValidators.IsNonEmptyString(addressDescription) ||
                            !ExcelValidators.IsNonEmptyString
                            (ethnicityId) ||
                            !ExcelValidators.IsNonEmptyString(jobId))
                        {
                            errorMessage = $"Lỗi ở dòng {currentRow} trong tệp excel";
                            return false;
                        }

                        if (!ExcelValidators.IsNumber(provinceId) ||
                            !ExcelValidators.IsNumber(districtId) ||
                            !ExcelValidators.IsNumber(townId))
                        {
                            errorMessage = $"Lỗi ở dòng {currentRow} trong tệp excel";
                            return false;
                        }

                        if (!ExcelValidators.IsDateTime(dateOfBirth))
                        {
                            errorMessage = $"Lỗi ở dòng {currentRow} trong tệp excel";
                            return false;
                        }

                        if (!ExcelValidators.IsIdCard(idCard))
                        {
                            errorMessage = $"Lỗi ở dòng {currentRow} trong tệp excel";
                            return false;
                        }

                        if (!ExcelValidators.IsPhoneNumber(phoneNumber))
                        {
                            errorMessage = $"Lỗi ở dòng {currentRow} trong tệp excel";
                            return false;
                        }

                        currentRow++;
                    }

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        var i = 0;
                        var name = row.Cell(++i).Value.ToString();
                        var dateOfBirth = row.Cell(++i).GetDateTime();
                        var age = DateTime.Now.Year - dateOfBirth.Year;
                        var ethnicityId = int.Parse(row.Cell(++i).Value.ToString());
                        var jobId = int.Parse(row.Cell(++i).Value.ToString());
                        var idCard = row.Cell(++i).Value.ToString();
                        var phoneNumber = row.Cell(++i).Value.ToString();
                        var provinceId = int.Parse(row.Cell(++i).Value.ToString());
                        var districtId = int.Parse(row.Cell(++i).Value.ToString());
                        var townId = int.Parse(row.Cell(++i).Value.ToString());
                        var details = row.Cell(i).Value.ToString();

                        var employee = new Employee
                        {
                            Name = name,
                            DateOfBirth = dateOfBirth,
                            Age = age,
                            EthnicityId = ethnicityId,
                            JobId = jobId,
                            IdCard = idCard,
                            PhoneNumber = phoneNumber,
                            ProvinceId = provinceId,
                            DistrictId = districtId,
                            TownId = townId,
                            Details = details,
                        };

                        _base.InsertEntity(employee);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex.Message);
                return false;
            }

        }

        private EmployeeViewModel SetEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel(employee.Id, employee.Name, employee.DateOfBirth,
                employee.Age, employee.Ethnicity.EthnicityName, employee.Job.JobName,
                employee.Province.ProvinceName, employee.District.DistrictName,
                employee.Town.TownName, employee.Details, employee.Qualifications.Count);
        }

        private EmployeeDto SetEmployeeDtoModel(Employee employee)
        {
            return new EmployeeDto(employee.Id, employee.Name, employee.DateOfBirth,
                employee.Age, employee.Ethnicity.Id, employee.Job.Id,
                employee.Province.ProvinceId, employee.District.DistrictId,
                employee.Town.TownId, employee.Details, employee.Qualifications.Count);
        }

        private Employee SetEmployeeModel(EmployeeDto employeeDto)
        {
            return new Employee(employeeDto.Id, employeeDto.Name, employeeDto.DateOfBirth,
                employeeDto.Age, employeeDto.EthnicityId, employeeDto.JobId, employeeDto.IdCard,
                employeeDto.PhoneNumber, employeeDto.ProvinceId, employeeDto.DistrictId,
                employeeDto.TownId, employeeDto.Details, employeeDto.NumberDegree);
        }

        private List<EmployeeViewModel> SetEmployeesViewModel(List<Employee> employees)
        {
            return employees.Select(employee => SetEmployeeViewModel(employee)).ToList();
        }

    }
}
