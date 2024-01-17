using BUS.Interfaces;
using DTO;
using log4net;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeBus _employeeBUS;
        private readonly ILog _log;

        public EmployeesController(IEmployeeBus employeeBUS)
        {
            _employeeBUS = employeeBUS;
            _log = LogManager.GetLogger(typeof(EmployeesController));
        }

        // GET: Employee
        public ActionResult Index(int? pageIndex, int? pageSize, string searchString)
        {
            PageList<EmployeeDto> employees = _employeeBUS.GetEmployeesData(searchString, pageIndex, pageSize);

            return View(employees);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            EmployeeDto employeeDTO = new EmployeeDto()
            {
                Jobs = _employeeBUS.GetJobsDataForDropdown(),
                Ethnicities = _employeeBUS.GetEthnicityDataForDropdown(),
                Provinces = _employeeBUS.GetProvinceDataForDropdown(),
            };
            return View(employeeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDto employee)
        {
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
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeDto employee)
        {

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

                var nameFile = "Export_" + DateTime.Now.Ticks + ".xlsx";
                var excelFilePath = ConfigurationManager.AppSettings["ExcelFilePath"];
                var pathFile = Server.MapPath(excelFilePath + nameFile);

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

        [HttpPost]
        public ActionResult ImportExcel(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                TempData["error"] = "Lỗi đọc file";
                return RedirectToAction("Index");
            }

            try
            {
                TempData["error"] = !_employeeBUS.ImportExcel(file.InputStream, out var message) ? "message" : null;
                TempData["success"] = "Import file Thành công";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}