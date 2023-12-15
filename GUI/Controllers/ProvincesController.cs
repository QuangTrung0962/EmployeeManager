using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class ProvincesController : Controller
    {
        private readonly IProvinceBus _province;

        public ProvincesController(IProvinceBus province)
        {
            _province = province;
        }

        // GET: Provinces
        public ActionResult Index(string searchString)
        {
            var provinces = _province.GetProvincesData(searchString);
            return View(provinces);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvinceDto provinceDTO)
        {
            if (!ModelState.IsValid || !_province.AddProvice(provinceDTO))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã thêm thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            ProvinceDto provinceDTO = _province.GetProvineById(id);
            if (provinceDTO == null) return RedirectToAction("Index");
            return View(provinceDTO);
        }

        public ActionResult Edit(int? id)
        {
            var province = _province.GetProvineById(id);
            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvinceDto provinceDTO)
        {
            if (!ModelState.IsValid || !_province.UpdateProvince(provinceDTO))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
                
            }
            TempData["success"] = "Bạn đã sửa thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? employeeId)
        {
            ProvinceDto provinceDTO = _province.GetProvineById(employeeId);

            if (provinceDTO == null) return RedirectToAction("Index");
            return View(provinceDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!_province.DeleteProvice(id))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã xóa thành công";
            return RedirectToAction("Index");
        }
    }
}