using BUS.Interfaces;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;

namespace GUI.Controllers.Api
{
    public class GetApiController : ApiController
    {
        private readonly IDistrictBus _district;
        private readonly ITownBus _town;
        private readonly IEmployeeBus _employee;

        public GetApiController(IDistrictBus district, ITownBus town, IEmployeeBus employee)
        {
            _district = district;
            _town = town;
            _employee = employee;
        }

        [HttpGet]
        public IHttpActionResult GetDistrictsByProvinceId(int id)
        {
            var districts = _district.GetDistrictsByProvinceId(id);

            if (districts.Count == 0)
                return NotFound();

            return Ok(districts);
        }

        [HttpGet]
        public IHttpActionResult GetTownsByDistrictId(int id)
        {
            var towns = _town.GetTownsByDistrictId(id);

            if (towns.Count == 0)
                return NotFound();

            return Ok(towns);
        }

        [Route("api/getapi/ExportEmployeesData")]
        [HttpGet]
        public IHttpActionResult ExportEmployeesData(string searchString)
        {
            var nameFile = "Export_" + DateTime.Now.Ticks + ".xlsx";
            var excelFilePath = ConfigurationManager.AppSettings["ExcelFilePath"];
            var pathFile = HttpContext.Current.Server.MapPath(excelFilePath + nameFile);

            var import = _employee.ExportExcel(pathFile, searchString);

            if (import) return Ok();
            return NotFound();
        }
    }
}
