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

        public bool AddTown(TownDto townDto)
        {
            Town town = new Town()
            {
                TownId = townDto.Id,
                TownName = townDto.TownName,
                DistrictId = townDto.DistrictId
            };

            if (_townDal.AddTown(town)) return true;
            else return false;
        }

        public bool DeleteTown(int id)
        {
            if (_townDal.DeleteTown(id)) return true;
            else return false;
        }

        public TownDto GetTownById(int? id)
        {
            return _townDal.GetTownById(id);
        }

        public List<TownDto> GetTownsByDistrictId(int districtId)
		{
			return _townDal.GetTownsByDistrictId(districtId);
		}

        public List<TownDto> GetTownsData(string searchString)
        {
            return _townDal.GetTownsData(searchString);
        }

        public bool UpdateTown(TownDto townDto)
        {
            if (_townDal.UpdateTown(townDto)) return true;
            else return false;
        }
    }
}
