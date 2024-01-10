using ClosedXML.Excel;
using DAL;
using DTO;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DTO.Utils;
using BUS.Interfaces;
using DAL.Interfaces;
using System;
using System.IO;
using log4net;

namespace BUS
{
    public class EmployeeBus : IEmployeeBus
    {
        private readonly IEmployeeDal _dataLayer;
        private readonly IBaseDal<Employee> _baseDal;
        private readonly ILog _log;

        public EmployeeBus(IEmployeeDal employee, IBaseDal<Employee> baseDal)
        {
            _dataLayer = employee;
            _baseDal = baseDal;
            _log = LogManager.GetLogger(typeof(EmployeeBus));
        }

        public PageList<EmployeeDto> GetEmployeesData(string searchString, int? pageIndex, int? pageSize)
        {
            pageSize = pageSize ?? PagingConstants.DefaultPageSize;
            pageIndex = pageIndex ?? PagingConstants.DefaultFirstPage;

            var employees = _dataLayer.GetEmployeesData(searchString, (int)pageIndex, (int)pageSize);

            int numberRecords = GetNumberOfRecords(searchString);

            return new PageList<EmployeeDto>(employees, numberRecords, pageIndex, pageSize);
        }

        public IEnumerable<SelectListItem> GetJobsDataForDropdown()
        {
            var list = _dataLayer.GetJobsData();
            return list.Select(x => new SelectListItem()
            {
                Text = x.JobName,
                Value = x.Id
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetEthnicityDataForDropdown()
        {
            var list = _dataLayer.GetEthnicitiesData();
            return list.Select(x => new SelectListItem()
            {
                Text = x.EthnicityName,
                Value = x.Id
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetProvinceDataForDropdown()
        {
            var list = _dataLayer.GetProvincesData();
            return list.Select(x => new SelectListItem()
            {
                Text = x.ProvinceName,
                Value = x.Id.ToString()
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetDistrcitDataForDropdown()
        {
            var list = _dataLayer.GetDistrictsData();
            return list.Select(x => new SelectListItem()
            {
                Text = x.DistrictName,
                Value = x.Id.ToString()
            }).ToList();
        }

        public IEnumerable<SelectListItem> GetTownDataForDropdown()
        {
            var list = _dataLayer.GetTownsData();
            return list.Select(x => new SelectListItem()
            {
                Text = x.TownName,
                Value = x.Id.ToString()
            }).ToList();
        }

        public bool AddEmployee(EmployeeDto employeeDTO)
        {
            try
            {
                Employee employee = SetEmployeeModel(employeeDTO);
                _baseDal.InsertEntity(employee);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool UpdateEmployee(EmployeeDto employeeDTO)
        {
            try
            {
                Employee employee = SetEmployeeModel(employeeDTO);
                _baseDal.UpdateEntity(employee);
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
                var employeeDTO = GetEmployeeById(id);
                Employee employee = SetEmployeeModel(employeeDTO);
                _baseDal.DeleteEntity(employee);
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
            return _dataLayer.GetEmployeeById(id);
        }

        public XLWorkbook ExportEmployeesData(List<EmployeeDto> employees)
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

        public List<EmployeeDto> GetDataForExcel()
        {
            return _dataLayer.GetDataForExcel();
        }

        public int GetNumberOfRecords(string searchString)
        {
            return _dataLayer.GetNumberOfRecords(searchString);
        }

        public int GetPageIndex(int? page)
        {
            int pageIndex = page ?? PagingConstants.DefaultFirstPage;

            return pageIndex;
        }

        public int GetPageSize(int? page)
        {
            int pageSize = page ?? PagingConstants.DefaultPageSize;

            return pageSize;
        }

        public bool SaveExcelFile(XLWorkbook workbook, string pathFile)
        {
            try
            {
                workbook.SaveAs(pathFile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving Excel file: {ex.Message}");
                return false;
            }
        }

        public bool ImportExcel(Stream excelFileStream, out string errorMessage)
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
                    var villageId = row.Cell(++i).Value.ToString();
                    var addressDescription = row.Cell(++i).Value.ToString();

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
                        !ExcelValidators.IsNumber(villageId))
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
                    var ethnicityId = row.Cell(++i).Value.ToString();
                    var jobId = row.Cell(++i).Value.ToString();
                    var idCard = row.Cell(++i).Value.ToString();
                    var phoneNumber = row.Cell(++i).Value.ToString();
                    var provinceId = int.Parse(row.Cell(++i).Value.ToString());
                    var districtId = int.Parse(row.Cell(++i).Value.ToString());
                    var townId = int.Parse(row.Cell(++i).Value.ToString());
                    var details = row.Cell(++i).Value.ToString();

                    var employee = new Employee
                    {
                        Name = name,
                        DateOfBirth = dateOfBirth,
                        Age = age,
                        Ethnicity = ethnicityId,
                        Job = jobId,
                        IdCard = idCard,
                        PhoneNumber = phoneNumber,
                        ProvinceId = provinceId,
                        DistrictId = districtId,
                        TownId = townId,
                        Details = details,
                    };

                    _baseDal.InsertEntity(employee);
                }

                errorMessage = null;
                return true;
            }
        }

        public EmployeeDto SetEmployeeDtoModel(Employee employee)
        {
            return new EmployeeDto(employee.Id, employee.Name, employee.Age, employee.DateOfBirth, employee.Job, employee.Ethnicity, employee.PhoneNumber, employee.IdCard, employee.Details);
        }

        public Employee SetEmployeeModel(EmployeeDto employeeDto)
        {
            return new Employee(employeeDto.Id, employeeDto.Name, employeeDto.Age, employeeDto.DateOfBirth, employeeDto.JobName, employeeDto.EthnicityName, employeeDto.PhoneNumber, employeeDto.IdCard, employeeDto.Details, employeeDto.ProvinceId, employeeDto.DistrictId, employeeDto.TownId);
        }
    }
}
