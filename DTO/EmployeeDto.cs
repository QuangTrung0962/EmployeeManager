using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class EmployeeDto
    {
        public EmployeeDto() { }
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Tuổi không được để trống")]
        [Range(0, 100, ErrorMessage = "Tuổi từ 1 - 100!")]
        public int Age { get; set; }
        public int EthnicityId { get; set; }
        public string EthnicityName { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }

        [StringLength(12)]
        [RegularExpression(@"^[0-9]{12}$", ErrorMessage = "Không phải số căn cước công dân")]
        public string IdCard { get; set; }

        [StringLength(12)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Không phải số điện thoại")]
        public string PhoneNumber { get; set; }

        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int TownId { get; set; }

        [Required(ErrorMessage = "Thông tin cụ thể không được để trống")]
        [StringLength(250)]
        public string Details { get; set; }

        [Range(0, 3)]
        public int NumberDegree { get; set; }
    }
}
