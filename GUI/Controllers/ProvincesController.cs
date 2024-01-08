using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class ProvincesController : Controller
    {
        private readonly IProvinceBus _provinceBus;

        public ProvincesController(IProvinceBus provinceBus)
        {
            _provinceBus = provinceBus;
        }

        // GET: Provinces
        public ActionResult Index(string searchString)
        {
            var provinces = _provinceBus.GetProvincesData(searchString);
            return View(provinces);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProvinceDto provinceDto)
        {
            if (!ModelState.IsValid || !_provinceBus.AddProvince(provinceDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã thêm thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            ProvinceDto provinceDTO = _provinceBus.GetProvinceById(id);
            if (provinceDTO == null) return RedirectToAction("Index");
            return View(provinceDTO);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var province = _provinceBus.GetProvinceById(id);
            return View(province);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvinceDto provinceDto)
        {
            if (!ModelState.IsValid || !_provinceBus.UpdateProvince(provinceDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
                
            }
            TempData["success"] = "Bạn đã sửa thành công";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? provinceId)
        {
            ProvinceDto provinceDTO = _provinceBus.GetProvinceById(provinceId);

            if (provinceDTO == null) return RedirectToAction("Index");
            return View(provinceDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!_provinceBus.DeleteProvince(id))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã xóa thành công";
            return RedirectToAction("Index");
        }
    }
}