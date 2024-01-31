using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class QualificationDto
    {
        public QualificationDto() { }

        public QualificationDto(Qualification qualification)
        {
            Id = qualification.Id;
            Name = qualification.Name;
            ReleaseDate = qualification.ReleaseDate;
            IssuancePlace = qualification.IssuancePlace;
            ExpirationDate = qualification.ExpirationDate;
            EmployeeId = qualification.EmployeeId;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tên văn bằng không được để trống")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ngày cấp không được để trống")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn không được để trống")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
        public int IssuancePlace { get; set; }
        public int EmployeeId { get; set; }

    }
}
