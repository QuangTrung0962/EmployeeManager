using System;
using System.ComponentModel.DataAnnotations;


namespace DTO.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel() { }
        public EmployeeViewModel(int id, string name, DateTime dateOfBirth, int age,
            string ethnicityName, string jobName, string provinceName, string districtName,
            string townName, string details, int? numberDegree)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Age = age;
            EthnicityName = ethnicityName;
            JobName = jobName;
            ProvinceName = provinceName;
            DistrictName = districtName;
            TownName = townName;
            Details = details;
            NumberDegree = numberDegree;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string EthnicityName { get; set; }
        public string JobName { get; set; }
        public string IdCard { get; set; }
        public string PhoneNumber { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string TownName { get; set; }
        public string Details { get; set; }
        public int? NumberDegree { get; set; }
    }
}
