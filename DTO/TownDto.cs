using System.ComponentModel.DataAnnotations;

namespace DTO
{
	public class TownDto
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Tên phố không được để trống")]
		[StringLength(250)]
		public string TownName { get; set; }
		public int DistrictId { get; set; }
	}
}
