using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
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

        public TownDto GetTownById(int? id)
        {
            var model = _db.Towns.FirstOrDefault(x => x.TownId == id);
            if (model == null) return null;
            return new TownDto(model.TownId, model.TownName, model.DistrictId);
        }

        public List<TownDto> GetTownsByDistrictId(int districtId)
        {
            return _db.Towns.Where(i => i.DistrictId == districtId)
                .Select(j => new TownDto
                {
                    Id = j.TownId,
                    TownName = j.TownName,
                    DistrictId = j.DistrictId,
                }).ToList();
        }

        public List<TownDto> GetTownsData(string searchString)
        {
            bool isNumeric = int.TryParse(searchString, out int districtId);

            if (isNumeric)
            {
                return _db.Towns.Where(i => i.DistrictId.ToString().Trim() == searchString)
                .Select(j => new TownDto
                {
                    Id = j.TownId,
                    TownName = j.TownName,
                    DistrictId = j.DistrictId,
                }).ToList();
            }

            return _db.Towns
                .Where(i => string.IsNullOrEmpty(searchString) || i.TownName.Trim().ToLower().Contains(searchString.Trim().ToLower())
                )
                .Select(j => new TownDto
                {
                    Id = j.TownId,
                    TownName = j.TownName,
                    DistrictId = j.DistrictId,
                }).ToList();
        }
    }
}
