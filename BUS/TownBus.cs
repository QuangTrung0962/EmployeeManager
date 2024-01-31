using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
using log4net;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class TownBus : ITownBus
    {
        private readonly ITownDal _town;
        private readonly IBaseDal<Town> _base;
        private readonly ILog _log;

        public TownBus(ITownDal town, IBaseDal<Town> baseDal)
        {
            _town = town;
            _base = baseDal;
            _log = LogManager.GetLogger(typeof(TownBus));
        }

        public List<TownViewModel> GetTownsData(string searchString)
        {
            return _town.GetTownsData(searchString);
        }

        public bool AddTown(TownDto townDto)
        {
            try
            {
                var town = new Town(townDto);
                _base.InsertEntity(town);
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
                var town = new Town(townDto);
                _base.UpdateEntity(town);
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
                var townDto = GetTownById(id);
                var town = new Town(townDto);
                _base.DeleteEntity(town);
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
            var town = _town.GetTownById(id);
            return new TownDto(town);
        }

        public TownViewModel GetTownViewModel(int id)
        {
            var town = _town.GetTownById(id);
            return new TownViewModel(town);

        }

        public List<TownViewModel> GetTownsByDistrictId(int districtId)
        {
            return _town.GetTownsByDistrictId(districtId);
        }
    }
}
