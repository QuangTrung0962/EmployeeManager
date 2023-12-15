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
			var model = _db.Towns.First(x => x.TownId == id);

			return new TownDto()
			{
				Id = model.TownId,
				TownName = model.TownName,
			};
		}

		public List<TownDto> GetTownsByDistrictId(int districtId)
		{
			return _db.Towns.Where(i => i.DistrictId == districtId).
				Select(j => new TownDto
				{
					Id = j.TownId,
					TownName = j.TownName,
				}).ToList();
		}
	}
}
