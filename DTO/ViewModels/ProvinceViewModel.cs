namespace DTO.ViewModels
{
    public class ProvinceViewModel
    {
        public ProvinceViewModel() { }

        public ProvinceViewModel(Province province)
        {
            Id = province.ProvinceId;
            ProvinceName = province.ProvinceName;
        }

        public int Id { get; set; }
        public string ProvinceName { get; set; }
    }
}
