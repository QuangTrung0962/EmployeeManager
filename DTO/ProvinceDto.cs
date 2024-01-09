using System.ComponentModel.DataAnnotations;

namespace DTO
{
	public class ProvinceDto
	{
		public ProvinceDto() { }

		public ProvinceDto(int id, string provinceName) 
		{
			Id = id;
			ProvinceName = provinceName;
		}

		public int Id { get; set; }

		[Required(ErrorMessage = "Tên tỉnh/thành phố không được để trống")]
		[StringLength(250)]
		public string ProvinceName { get; set; }
	}
}
