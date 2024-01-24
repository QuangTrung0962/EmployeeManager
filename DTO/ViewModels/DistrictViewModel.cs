
namespace DTO.ViewModels
{
    public class DistrictViewModel
    {
        public DistrictViewModel() { }

        public DistrictViewModel(int id, string districtName, string provinceName)
        {
            Id = id;
            DistrictName = districtName;
            ProvinceName = provinceName;
        }

        public int Id { get; set; }
        public string DistrictName { get; set; }

        public string ProvinceName { get; set; }
    }
}
