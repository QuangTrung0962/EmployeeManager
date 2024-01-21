using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly IDistrictBus _district;
        private readonly IProvinceBus _province;
        private readonly IGeneralBus _general;

        public DistrictsController(IDistrictBus district, IProvinceBus province, IGeneralBus general)
        {
            _district = district;
            _province = province;
            _general = general;
        }

        public ActionResult Index(string searchString)
        {
            var districts = _district.GetDistrictsData(searchString);
            return View(districts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetDropDownListData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DistrictDto district)
        {
            if (ModelState.IsValid && _district.AddDistrict(district))
                TempData["success"] = "Bạn đã thêm thành công";
            else
                TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var district = _district.GetDistrictById(id);
            if (district == null) return RedirectToAction("Index");

            ViewBag.ProvinceName = _province.GetProvinceById(district.ProvinceId).ProvinceName;
            return View(district);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var district = _district.GetDistrictById(id);
            if (district == null) return RedirectToAction("Index");

            SetDropDownListData();
            return View(district);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DistrictDto district)
        {
            if (ModelState.IsValid && _district.UpdateDistrict(district))
                TempData["success"] = "Bạn đã sửa thành công";
            else
                TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int districtId)
        {
            var district = _district.GetDistrictById(districtId);
            if (district == null) return RedirectToAction("Index");

            ViewBag.ProvinceName = _province.GetProvinceById(district.ProvinceId).ProvinceName;
            return View(district);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid && _district.DeleteDistrict(id))
                TempData["success"] = "Bạn đã xóa thành công";
            else
                TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        private void SetDropDownListData()
        {
            ViewBag.Provinces = _general.LoadProvinceOptions();
        }
    }
}