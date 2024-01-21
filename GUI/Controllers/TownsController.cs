using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class TownsController : Controller
    {
        private readonly IDistrictBus _district;
        private readonly ITownBus _town;
        private readonly IProvinceBus _province;
        private readonly IGeneralBus _general;

        public TownsController(IDistrictBus district, ITownBus town, 
            IProvinceBus province, IGeneralBus general)
        {
            _district = district;
            _town = town;
            _province = province;
            _general = general;
        }

        // GET: Towns
        public ActionResult Index(string searchString)
        {
            var towns = _town.GetTownsData(searchString);
            return View(towns);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetDropDownListData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TownDto townDto)
        {
            if (ModelState.IsValid && _town.AddTown(townDto))
                TempData["success"] = "Bạn đã thêm thành công";
            else TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            var town = _town.GetTownById(id);
            if (town == null) return RedirectToAction("Index");

            var district = _district.GetDistrictById(town.DistrictId);
            ViewBag.DistrictName = district.DistrictName;
            ViewBag.ProvinceName = _province.GetProvinceById(district.ProvinceId).ProvinceName;
            return View(town);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            TownDto townDto = _town.GetTownById(id);
            if (townDto == null) return RedirectToAction("Index");
            SetDropDownListData();

            return View(townDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TownDto town)
        {
            if (ModelState.IsValid && _town.UpdateTown(town))
                TempData["success"] = "Bạn đã sửa thành công";
            else TempData["error"] = "Có lỗi xảy ra";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? townId)
        {
            var town = _town.GetTownById(townId);
            if (town == null) return RedirectToAction("Index");

            ViewBag.district = _district.GetDistrictById(town.DistrictId).DistrictName;
            return View(town);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid && _town.DeleteTown(id))
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