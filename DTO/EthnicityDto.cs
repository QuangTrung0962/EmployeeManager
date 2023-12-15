using System.ComponentModel.DataAnnotations;


namespace DTO
{
	public class EthnicityDto
	{
		public string Id { get; set; }

		[Required(ErrorMessage = "Tên dân tộc không được để trống")]
		public string EthnicityName { get; set; }
	}
}
