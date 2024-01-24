using BUS.Interfaces;
using DocumentFormat.OpenXml.Office2010.Excel;
using DTO;
using System.Web.Mvc;

namespace GUI.Controllers
{
    public class TownsController : Controller
    {
        private readonly ITownBus _town;
        private readonly IGeneralBus _general;

        public TownsController(ITownBus town, IGeneralBus general)
        {
            _town = town;
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
        public ActionResult Details(int id)
        {
            var town = _town.GetTownViewModel(id);
            if (town == null) return RedirectToAction("Index");

            return View(town);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var townDto = _town.GetTownById(id);
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
        public ActionResult Delete(int townId)
        {
            var town = _town.GetTownViewModel(townId);
            if (town == null) return RedirectToAction("Index");
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