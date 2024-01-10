using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;


namespace BUS
{
    public class ProvinceBus : IProvinceBus
    {
        private readonly IProvinceDal _provinceDal;
    
        public ProvinceBus(IProvinceDal provinces)
        {
            _provinceDal = provinces;
        }

        public List<ProvinceDto> GetProvincesData(string searchString)
        {
            return _provinceDal.GetProvincesData(searchString);
        }

        public ProvinceDto GetProvinceById(int? id)
        {
            return _provinceDal.GetProvinceById(id);
        }

        public bool AddProvince(ProvinceDto provinceDto)
        {
            Province province = ProvinceDtoToProvince(provinceDto);

            if (_provinceDal.AddProvince(province)) return true;
            else return false;
        }

        public bool UpdateProvince(ProvinceDto provinceDto)
        {
            Province province = ProvinceDtoToProvince(provinceDto);
            if (_provinceDal.UpdateProvince(province)) return true;
            else return false;
        }

        public bool DeleteProvince(int id)
        {
            if (_provinceDal.DeleteProvince(id)) return true;
            else return false;
        }

        public Province ProvinceDtoToProvince(ProvinceDto provinceDto)
        {
            return new Province
            {
                ProvinceId = provinceDto.Id,
                ProvinceName = provinceDto.ProvinceName
            };
        }

        public ProvinceDto ProvinceToProvinceDto(Province province)
        {
            return new ProvinceDto
            {
                Id = province.ProvinceId,
                ProvinceName = province.ProvinceName,
            };
        }
    }
}
