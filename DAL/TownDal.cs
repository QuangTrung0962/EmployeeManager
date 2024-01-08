using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DAL
{
    public class TownDal : ITownDal
    {
        private readonly EmployeesDBEntities _db;

        public TownDal(EmployeesDBEntities context)
        {
            _db = context;
        }

        public bool AddTown(Town town)
        {
            try
            {
                _db.Towns.Add(town);
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteTown(int id)
        {
            try
            {
                Town town = _db.Towns.FirstOrDefault(t => t.TownId == id);
                _db.Towns.Remove(town);

                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public TownDto GetTownById(int? id)
        {
            var model = _db.Towns.FirstOrDefault(x => x.TownId == id);
            if (model == null) return null;
            return new TownDto()
            {
                Id = model.TownId,
                TownName = model.TownName,
                DistrictId = model.DistrictId
            };
        }

        public List<TownDto> GetTownsByDistrictId(int districtId)
        {
            return _db.Towns.Where(i => i.DistrictId == districtId).
                Select(j => new TownDto
                {
                    Id = j.TownId,
                    TownName = j.TownName,
                    DistrictId = j.DistrictId
                }).ToList();
        }

        public List<TownDto> GetTownsData(string searchString)
        {
            bool isNumeric = int.TryParse(searchString, out int districtId);

            if (isNumeric)
            {
                return _db.Towns.Where(i => i.DistrictId.ToString().Trim() == searchString)
                .Select(i => new TownDto
                {
                    Id = i.TownId,
                    TownName = i.TownName,
                    DistrictId = i.DistrictId
                })
                .ToList();
            }

            return _db.Towns
                .Where(i => string.IsNullOrEmpty(searchString) || i.TownName.Trim().ToLower().Contains(searchString.Trim().ToLower())
                )
                .Select(i => new TownDto
                {
                    Id = i.TownId,
                    TownName = i.TownName,
                    DistrictId = i.DistrictId
                })
                .ToList();
        }

        public bool UpdateTown(TownDto townDto)
        {
            Town town = _db.Towns.FirstOrDefault(i => i.TownId == townDto.Id);

            if (town == null) return false;

            try
            {
                town.TownId = townDto.Id;
                town.TownName = townDto.TownName;
                town.DistrictId = townDto.DistrictId;
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
