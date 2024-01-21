using BUS.Interfaces;
using System.Web.Http;

namespace GUI.Controllers.Api
{
    public class GetApiController : ApiController
    {
        private readonly IDistrictBus _district;
        private readonly ITownBus _town;

        public GetApiController(IDistrictBus district, ITownBus town)
        {
            _district = district;
            _town = town;
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
    }
}
