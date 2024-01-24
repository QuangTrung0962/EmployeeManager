namespace DTO.ViewModels
{
    public class ProvinceViewModel
    {
        public ProvinceViewModel() { }

        public ProvinceViewModel(int id, string name)
        {
            Id = id;
            ProvinceName = name;
        }

        public int Id { get; set; }
        public string ProvinceName { get; set; }
    }
}
