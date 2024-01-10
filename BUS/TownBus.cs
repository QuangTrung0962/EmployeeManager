using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using log4net;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class TownBus : ITownBus
    {
        private readonly ITownDal _townDal;
        private readonly IBaseDal<Town> _baseDal;
        private readonly ILog _log;

        public TownBus(ITownDal townDal, IBaseDal<Town> baseDal)
        {
            _townDal = townDal;
            _baseDal = baseDal;
            _log = LogManager.GetLogger(typeof(TownBus));
        }

        public bool AddTown(TownDto townDto)
        {
            try
            {
                Town town = SetTownModel(townDto);
                _baseDal.InsertEntity(town);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool UpdateTown(TownDto townDto)
        {
            try
            {
                Town town = SetTownModel(townDto);
                _baseDal.UpdateEntity(town);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool DeleteTown(int id)
        {
            try
            {
                TownDto townDto = GetTownById(id);
                Town town = SetTownModel(townDto);
                _baseDal.DeleteEntity(town);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
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

        public Town SetTownModel(TownDto townDto)
        {
            return new Town(townDto.Id, townDto.TownName, townDto.DistrictId);
        }

        public TownDto SetTownDtoModel(Town town)
        {
            return new TownDto(town.TownId, town.TownName, town.DistrictId);
        }
    }
}
