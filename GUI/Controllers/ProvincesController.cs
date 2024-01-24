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
        public ActionResult Create(ProvinceDto province)
        {
            if (ModelState.IsValid && _province.AddProvince(province))
                TempData["success"] = "Bạn đã thêm thành công";
            else TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var province = _province.GetProvinceById(id);
            if (province == null) return RedirectToAction("Index");
            return View(province);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var province = _province.GetProvinceById(id);
            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvinceDto provinceDto)
        {
            if (ModelState.IsValid && _province.UpdateProvince(provinceDto))
                TempData["success"] = "Bạn đã sửa thành công";
            else TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? provinceId)
        {
            var province = _province.GetProvinceById(provinceId);

            if (province == null) return RedirectToAction("Index");
            return View(province);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid && _province.DeleteProvince(id))
                TempData["success"] = "Bạn đã xóa thành công";
            else TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }
    }
}