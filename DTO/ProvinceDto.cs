﻿using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ProvinceDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên tỉnh/thành phố không được để trống")]
        [StringLength(250)]
        public string ProvinceName { get; set; }

        public ProvinceDto() { }

        public ProvinceDto(Province province)
        {
            Id = province.ProvinceId;
            ProvinceName = province.ProvinceName;
        }
    }
}
