using BUS.Interfaces;
using DTO;
using System.Web;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeBus _employee;
        private readonly IGeneralBus _general;

        public EmployeesController(IEmployeeBus employee, IGeneralBus general)
        {
            _employee = employee;
            _general = general;
        }

        public ActionResult Index(int? pageIndex, int? pageSize, string searchString)
        {
            var employees = _employee.GetEmployeesData(searchString, pageIndex, pageSize);

            return View(employees);
        }

        public ActionResult Create()
        {
            SetDropDownListData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeDto employee)
        {
            if (ModelState.IsValid && _employee.AddEmployee(employee))
                TempData["success"] = "Bạn đã thêm thành công";
            else TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var employee = _employee.GetEmployeeViewModel(id);

            if (employee == null)
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public ActionResult Edit(int id)
        {
            SetDropDownListData();
            var employee = _employee.GetEmployeeById(id);

            if (employee == null)
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeDto employee)
        {
            if (ModelState.IsValid && _employee.UpdateEmployee(employee))
                TempData["success"] = "Bạn đã sửa thành công";
            else TempData["error"] = "Có lỗi xảy ra";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int employeeId)
        {
            var employee = _employee.GetEmployeeViewModel(employeeId);
            if (employee == null)
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid && _employee.DeleteEmployee(id))
                TempData["success"] = "Bạn đã xóa thành công";
            else TempData["error"] = "Có lỗi xảy ra";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ImportExcelFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                TempData["error"] = "Lỗi đọc file";
                return RedirectToAction("Index");
            }

            if (_employee.ImportExcel(file.InputStream, out var message))
                TempData["success"] = "Lưu file excel Thành công";
            else TempData["error"] = message;

            return RedirectToAction("Index");
        }

        private void SetDropDownListData()
        {
            ViewBag.Jobs = _general.LoadJobOptions();
            ViewBag.Ethnicities = _general.LoadEthnicityOptions();
            ViewBag.Provinces = _general.LoadProvinceOptions();
        }

    }
}