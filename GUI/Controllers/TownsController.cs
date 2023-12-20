using BUS.Interfaces;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class TownsController : Controller
    {
        private readonly IDistrictBus _districtBus;
        private readonly ITownBus _townBus;

        public TownsController(IDistrictBus districtBus, ITownBus townBus)
        {
            _districtBus = districtBus;
            _townBus = townBus;
        }

        // GET: Towns
        public ActionResult Index(string searchString)
        {
            var towns = _townBus.GetTownsData(searchString);
            return View(towns);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.districts = _districtBus.GetDistrictsData("");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TownDto townDto)
        {
            if (!ModelState.IsValid || !_townBus.AddTown(townDto))
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
            TownDto townDto = _townBus.GetTownById(id);
            if (townDto == null) return RedirectToAction("Index");

            ViewBag.district = _districtBus.GetDistrictById(townDto.DistrictId).DistrictName;
            return View(townDto);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            TownDto townDto = _townBus.GetTownById(id);
            if (townDto == null) return RedirectToAction("Index");

            ViewBag.districts = _districtBus.GetDistrictsData("");
            return View(townDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TownDto townDto)
        {
            if (!ModelState.IsValid || !_townBus.UpdateTown(townDto))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");

            }
            TempData["success"] = "Bạn đã sửa thành công";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? townId)
        {
            TownDto townDto = _townBus.GetTownById(townId);
            if (townDto == null) return RedirectToAction("Index");

            ViewBag.district = _districtBus.GetDistrictById(townDto.DistrictId).DistrictName;
            return View(townDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!_townBus.DeleteTown(id))
            {
                TempData["error"] = "Có lỗi xảy ra";
                return RedirectToAction("Index");
            }

            TempData["success"] = "Bạn đã xóa thành công";
            return RedirectToAction("Index");
        }
    }
}