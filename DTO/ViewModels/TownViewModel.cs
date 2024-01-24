namespace DTO.ViewModels
{
    public class TownViewModel
    {
        public TownViewModel() { }

        public TownViewModel(int id, string name, string districtName, string provinceName)
        {
            Id = id;
            TownName = name;
            DistrictName = districtName;
            ProvinceName = provinceName;
        }

        public int Id { get; set; }
        public string TownName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName {  get; set; }
    }
}
