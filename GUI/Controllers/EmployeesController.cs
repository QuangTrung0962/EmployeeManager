using BUS.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeBus _employeeBUS;

        public EmployeesController(IEmployeeBus employeeBUS)
        {
            _employeeBUS = employeeBUS;
        }

        // GET: Employee
        public ActionResult Index(int? pageIndex, int? pageSize, string searchString)
        {
            pageIndex = _employeeBUS.GetPageIndex(pageIndex);
            pageSize = _employeeBUS.GetPageSize(pageSize);
            ViewBag.PageSize = pageSize;

            List<EmployeeDto> employees = _employeeBUS.GetEmployeesData(searchString, pageIndex, pageSize);

            int numberRecords = 
                _employeeBUS.GetNumberOfRecords(searchString);

            PageList<EmployeeDto> pagedEmployees = new PageList<EmployeeDto>(employees, pageIndex, pageSize, numberRecords);

            return View(pagedEmployees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            EmployeeDto employeeDTO = new EmployeeDto()
            {
                Jobs = _employeeBUS.GetJobsDataForDropdown(),
                Ethnicities = _employeeBUS.GetEthnicityDataForDropdown(),
                Provinces = _employeeBUS.GetProvinceDataForDropdown(),
                Districts = _employeeBUS.GetDistrcitDataForDropdown(),
                Towns = _employeeBUS.GetTownDataForDropdown(),
            };
            return View(employeeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDto employee, int provinceId, int districtId, int townId)
        {
            employee.Province = new ProvinceDto() { Id = provinceId };
            employee.District = new DistrictDto() { Id = districtId };
            employee.Town = new TownDto() { Id = townId };

            if (!ModelState.IsValid || !_employeeBUS.AddEmployee(employee))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã thêm thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            EmployeeDto employee = _employeeBUS.GetEmployeeById(id);
            if (employee == null) return RedirectToAction("Index");
            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            var employee = _employeeBUS.GetEmployeeById(id);
            employee.Jobs = _employeeBUS.GetJobsDataForDropdown();
            employee.Ethnicities = _employeeBUS.GetEthnicityDataForDropdown();
            employee.Provinces = _employeeBUS.GetProvinceDataForDropdown();
            employee.Districts = _employeeBUS.GetDistrcitDataForDropdown();
            employee.Towns = _employeeBUS.GetTownDataForDropdown();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeDto employee, int provinceId, int districtId, int townId)
        {
            employee.Province = new ProvinceDto() { Id = provinceId };
            employee.District = new DistrictDto() { Id = districtId };
            employee.Town = new TownDto() { Id = townId };

            if (!ModelState.IsValid || !_employeeBUS.UpdateEmployee(employee))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã sửa thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? employeeId)
        {
            EmployeeDto employee = _employeeBUS.GetEmployeeById(employeeId);
            if (employee == null) return RedirectToAction("Index");
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (!_employeeBUS.DeleteEmployee(id) || id == null)
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã xóa thành công";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ExportToExcel()
        {
            try
            {
                var employees = _employeeBUS.GetDataForExcel();
                var workbook = _employeeBUS.ExportEmployeesData(employees);

                string nameFile = "Export_" + DateTime.Now.Ticks + ".xlsx";
                string pathFile = Server.MapPath("~/App_Data/Excel_File/" + nameFile);

                if (!_employeeBUS.SaveExcelFile(workbook, pathFile))
                {
                    TempData["error"] = "Có lỗi xảy ra";
                    return RedirectToAction("Index");
                }
                TempData["success"] = "Bạn đã lưu file Excel thành công";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Noti = "Lỗi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}