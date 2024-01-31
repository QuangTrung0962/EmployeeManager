namespace DTO.ViewModels
{
    public class DistrictViewModel
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }

        public string ProvinceName { get; set; }
        public DistrictViewModel() { }

        public DistrictViewModel(District district)
        {
            Id = district.DistrictId;
            DistrictName = district.DistrictName;
            ProvinceName = district.Province.ProvinceName;
        }
    }
}
