using System;
using System.ComponentModel.DataAnnotations;


namespace DTO.ViewModels
{
    public class QualificationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
        public string IssuancePlaceName { get; set; }
        public string EmployeeName { get; set; }

        public QualificationViewModel() { }

        public QualificationViewModel(Qualification qualification)
        {
            Id = qualification.Id;
            Name = qualification.Name;
            ReleaseDate = qualification.ReleaseDate;
            ExpirationDate = qualification.ExpirationDate;
            IssuancePlaceName = qualification.Province.ProvinceName;
            EmployeeName = qualification.Employee.Name;
        }
    }
}
