namespace DTO.ViewModels
{
    public class ProvinceViewModel
    {
        public int Id { get; set; }
        public string ProvinceName { get; set; }

        public ProvinceViewModel() { }

        public ProvinceViewModel(Province province)
        {
            Id = province.ProvinceId;
            ProvinceName = province.ProvinceName;
        }
    }
}
