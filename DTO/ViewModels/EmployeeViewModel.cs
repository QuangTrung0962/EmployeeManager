using System;
using System.ComponentModel.DataAnnotations;

namespace DTO.ViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel() { }
        public EmployeeViewModel(Employee entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            DateOfBirth = entity.DateOfBirth;
            Age = entity.Age;
            EthnicityName = entity.Ethnicity.EthnicityName;
            JobName = entity.Job.JobName;
            ProvinceName = entity.Province.ProvinceName;
            DistrictName = entity.District.DistrictName;
            TownName = entity.Town.TownName;
            Details = entity.Details;
            NumberDegree = entity.Qualifications.Count;
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
