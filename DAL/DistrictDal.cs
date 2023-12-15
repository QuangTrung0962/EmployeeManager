using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;


namespace DAL
{
	public class DistrictDal : IDistrictDal
    {
        private readonly EmployeesDBEntities _db;

        public DistrictDal(EmployeesDBEntities context)
        {
            _db = context;
        }

        public DistrictDto GetDistrictById(int? id)
		{
			var model = _db.Districts.First(x => x.DistrictId == id);

			return new DistrictDto()
			{
				Id = model.DistrictId,
				DistrictName = model.DistrictName,
			};
		}

		public List<DistrictDto> GetDistrictsByProvinceId(int pronviceId)
		{
			return _db.Districts.Where(i => i.ProvinceId == pronviceId).
				Select(j => new DistrictDto
				{
					Id = j.DistrictId,
					DistrictName = j.DistrictName,
				}).ToList();
		}
	}
}
