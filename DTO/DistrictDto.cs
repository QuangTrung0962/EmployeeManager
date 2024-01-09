using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class DistrictDto
    {
        public DistrictDto() { }

        public DistrictDto(int id,string districtName, int provinceId) 
        {
            Id = id;
            DistrictName = districtName;
            ProvinceId = provinceId;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tên quận không được để trống")]
        [StringLength(250)]
        public string DistrictName { get; set; }

        public int ProvinceId { get; set; }
    }
}
