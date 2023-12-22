using BUS;
using BUS.Interfaces;
using DAL;
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
            qualificationDto.EmployeeId = id;

            if (!ModelState.IsValid || !_qualificationBUS.AddQualificatio(qualificationDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã thêm thành công";
            return RedirectToAction("Index");
        }
    }
}