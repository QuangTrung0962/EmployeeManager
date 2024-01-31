
namespace DTO.ViewModels
{
    public class DistrictViewModel
    {
        public DistrictViewModel() { }

        public DistrictViewModel(District district)
        {
            Id = district.DistrictId;
            DistrictName = district.DistrictName;
            ProvinceName = district.Province.ProvinceName;
        }

        public int Id { get; set; }
        public string DistrictName { get; set; }

        public string ProvinceName { get; set; }
    }
}
