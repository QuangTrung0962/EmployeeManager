using System.ComponentModel.DataAnnotations;

namespace DTO
{
	public class TownDto
	{
		public TownDto() { }

		public TownDto(int id, string name, int districtId) 
		{
			Id = id;
			TownName = name;
			DistrictId = districtId;
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Tên phố không được để trống")]
		[StringLength(250)]
		public string TownName { get; set; }
		public int DistrictId { get; set; }
	}
}
