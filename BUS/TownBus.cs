using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BUS
{
    public class TownBus : ITownBus
    {
        private readonly ITownDal _town;
        private readonly IProvinceDal _province;
        private readonly IBaseDal<Town> _base;
        private readonly ILog _log;

        public TownBus(ITownDal town, IBaseDal<Town> baseDal, IProvinceDal province)
        {
            _town = town;
            _base = baseDal;
            _log = LogManager.GetLogger(typeof(TownBus));
            _province = province;
        }

        public List<TownViewModel> GetTownsData(string searchString)
        {
            var towns = _town.GetTownsData(searchString);
            return SetTownsViewModel(towns);
        }

        public bool AddTown(TownDto townDto)
        {
            try
            {
                var town = SetTownModel(townDto);
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
                var town = SetTownModel(townDto);
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
                var town = SetTownModel(townDto);
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
            return SetTownDtoModel(town);
        }

        public TownViewModel GetTownViewModel(int id)
        {
            var town = _town.GetTownById(id);
            return SetTownViewModel(town);
        }

        public List<TownViewModel> GetTownsByDistrictId(int districtId)
        {
            var towns = _town.GetTownsByDistrictId(districtId);
            return SetTownsViewModel(towns);
        }

        private Town SetTownModel(TownDto townDto)
        {
            return new Town(townDto.Id, townDto.TownName, townDto.DistrictId);
        }

        private TownDto SetTownDtoModel(Town town)
        {
            return new TownDto(town.TownId, town.TownName, town.DistrictId);
        }

        private TownViewModel SetTownViewModel(Town town)
        {
            var id = town.District.ProvinceId;
            var provinceName = _province.GetProvinceById(id).ProvinceName;
            return new TownViewModel(town.TownId, town.TownName, town.District.DistrictName,
                provinceName);
        }

        private List<TownViewModel> SetTownsViewModel(List<Town> towns)
        {
            return towns.Select(i => SetTownViewModel(i)).ToList();
        }


    }
}
