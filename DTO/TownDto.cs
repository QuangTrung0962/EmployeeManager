using System.ComponentModel.DataAnnotations;

namespace DTO
{
	public class TownDto
	{
		public TownDto() { }

		public TownDto(Town town) 
		{
			Id = town.TownId;
			TownName = town.TownName;
			DistrictId = town.DistrictId;
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Tên phố không được để trống")]
		[StringLength(250)]
		public string TownName { get; set; }
		public int DistrictId { get; set; }
	}
}
