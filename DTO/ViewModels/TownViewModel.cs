namespace DTO.ViewModels
{
    public class TownViewModel
    {
        public int Id { get; set; }
        public string TownName { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }

        public TownViewModel() { }

        public TownViewModel(Town town)
        {
            Id = town.TownId;
            TownName = town.TownName;
            DistrictName = town.District.DistrictName;
            ProvinceName = town.District.Province.ProvinceName;
        }
    }
}
