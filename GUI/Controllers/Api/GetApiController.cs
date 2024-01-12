using BUS.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Web.Http;

namespace GUI.Controllers.Api
{
    public class GetApiController : ApiController
    {
        private readonly IDistrictBus _districtBUS;
        private readonly ITownBus _townBUS;
        private readonly IEmployeeBus _employeeBUS;

        public GetApiController(IDistrictBus districtBUS, ITownBus townBUS, IEmployeeBus employeeBUS)
        {
            _districtBUS = districtBUS;
            _townBUS = townBUS;
            _employeeBUS = employeeBUS;
        }

        [HttpGet]
        public IHttpActionResult GetDistrictsByProvinceId(int id)
        {
            List<DistrictDto> districts = _districtBUS.GetDistrictsByProvinceId(id);

            if (districts.Count == 0)
                return NotFound();

            return Ok(districts);
        }

        [HttpGet]
        public IHttpActionResult GetTownsByDistrictId(int id)
        {
            List<TownDto> towns = _townBUS.GetTownsByDistrictId(id);

            if (towns.Count == 0)
                return NotFound();

            return Ok(towns);
        }

        [HttpGet]
        [Route("api/GetApi/{searchString}")]
        public IHttpActionResult ExportEmployeesBySearch()
        {
            var employees = _employeeBUS.GetDataForExcel();

            if (employees != null)
                return Ok(employees);

            return Ok("Fales");
        }
    }
}
