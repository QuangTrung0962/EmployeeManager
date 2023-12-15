using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;

namespace BUS
{
	public class TownBus : ITownBus
    {
		private readonly ITownDal _townDal;
        public TownBus(ITownDal town)
        {
            _townDal = town;
        }

        public List<TownDto> GetDistrictsByProvinceId(int districtId)
		{
			return _townDal.GetTownsByDistrictId(districtId);
		}
	}
}
