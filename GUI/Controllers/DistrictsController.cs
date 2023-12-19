using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly IDistrictBus _districtBus;
        private readonly IProvinceBus _provinceBus;

        public DistrictsController(IDistrictBus districtBus, IProvinceBus provinceBus)
        {
            _districtBus = districtBus;
            _provinceBus = provinceBus;
        }

        public ActionResult Index(string searchString)
        {
            var district = _districtBus.GetDistrictsData(searchString);
            return View(district);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.provinces = _provinceBus.GetProvincesData("");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistrictDto districtDto)
        {
            if (!ModelState.IsValid || !_districtBus.AddDistrict(districtDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã thêm thành công";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            DistrictDto districtDto = _districtBus.GetDistrictById(id);
            if (districtDto == null) return RedirectToAction("Index");

            ViewBag.province = _provinceBus.GetProvineById(districtDto.ProvinceId).ProvinceName;
            return View(districtDto);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            DistrictDto districtDto = _districtBus.GetDistrictById(id);
            if (districtDto == null) return RedirectToAction("Index");

            ViewBag.provinces = _provinceBus.GetProvincesData("");
            return View(districtDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DistrictDto districtDto)
        {
            if (!ModelState.IsValid || !_districtBus.UpdateDistrict(districtDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");

            }
            TempData["success"] = "Bạn đã sửa thành công";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? districtId)
        {
            DistrictDto districtDto = _districtBus.GetDistrictById(districtId);
            if (districtDto == null) return RedirectToAction("Index");

            ViewBag.province = _provinceBus.GetProvineById(districtDto.ProvinceId).ProvinceName;
            return View(districtDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!_districtBus.DeleteDistrict(id))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã xóa thành công";
            return RedirectToAction("Index");
        }
    }
}