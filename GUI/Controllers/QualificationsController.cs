using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class QualificationsController : Controller
    {
        private readonly IQualificationBus _qualificationBUS;
        private readonly IEmployeeBus _employeeBUS;

        public QualificationsController(IQualificationBus qualificationBus, IEmployeeBus employeeBUS)
        {
            _qualificationBUS = qualificationBus;
            _employeeBUS = employeeBUS;
        }

        // GET: Qualification
        public ActionResult Index(string searchString)
        {
            var qualifications =
                _qualificationBUS.GetQualificationsData(searchString);

            return View(qualifications);
        }

        public ActionResult QualificationsByEmployeeId(int id)
        {
            var item = _qualificationBUS.GetQualificationsByEmployeeId(id);
            ViewBag.id = id;
            return View(item);
        }

        public ActionResult Create()
        {
            ViewBag.provinces = _employeeBUS.GetProvinceDataForDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, QualificationDto qualificationDto)
        {
            if (_employeeBUS.GetEmployeeById(id).NumberDegree >= 3)
            {
                TempData["error"] = "Nhân viên đã đủ số bằng cấp";
                return RedirectToAction("Index");
            }

            qualificationDto.EmployeeId = id;
            if (!ModelState.IsValid || !_qualificationBUS.AddQualification(qualificationDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã thêm thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.provinces = _employeeBUS.GetProvinceDataForDropdown();
            var obj = _qualificationBUS.GetQualificationById(id);
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QualificationDto qualificationDto)
        {
            qualificationDto.EmployeeId = id;
            if (!ModelState.IsValid || !_qualificationBUS.UpdateQualification(qualificationDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã sửa thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var obj = _qualificationBUS.GetQualificationById(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            
            if (!ModelState.IsValid || !_qualificationBUS.DeleteQualification(id))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã xóa thành công";
            return RedirectToAction("Index");
        }
    }
}