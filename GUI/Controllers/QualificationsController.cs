using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class QualificationsController : Controller
    {
        private readonly IQualificationBus _qualification;
        private readonly IEmployeeBus _employee;
        private readonly IGeneralBus _general;

        public QualificationsController(IQualificationBus qualification, IEmployeeBus employee,
            IGeneralBus general)
        {
            _qualification = qualification;
            _employee = employee;
            _general = general;
        }

        // GET: Qualification
        public ActionResult Index(string searchString)
        {
            var qualifications =
                _qualification.GetQualificationsData(searchString);

            return View(qualifications);
        }

        public ActionResult EmployeeQualifications(int id)
        {
            var qualifications = _qualification.GetQualificationsByEmployeeId(id);
            ViewBag.Id = id;
            return View(qualifications);
        }

        public ActionResult Create()
        {
            SetDropDownListData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, QualificationDto qualificationDto)
        {
            if (_employee.GetEmployeeById(id).NumberDegree >= 3)
            {
                TempData["error"] = "Nhân viên đã đủ số bằng cấp";
                return RedirectToAction("Index");
            }

            qualificationDto.EmployeeId = id;
            if (ModelState.IsValid && _qualification.AddQualification(qualificationDto))
                TempData["success"] = "Bạn đã thêm thành công";
            else TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            SetDropDownListData();
            var qualification = _qualification.GetQualificationById(id);
            return View(qualification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, QualificationDto qualificationDto)
        {
            qualificationDto.EmployeeId = id;
            if (ModelState.IsValid && _qualification.UpdateQualification(qualificationDto))
                TempData["success"] = "Bạn đã sửa thành công";
            else TempData["error"] = "Có lỗi xảy ra";

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var qualification = _qualification.GetQualificationById(id);
            return View(qualification);
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid && _qualification.DeleteQualification(id))
                TempData["success"] = "Bạn đã xóa thành công";
            else TempData["error"] = "Có lỗi xảy ra";

            return RedirectToAction("Index");
        }

        private void SetDropDownListData()
        {
            ViewBag.Provinces = _general.LoadProvinceOptions();
        }
    }
}