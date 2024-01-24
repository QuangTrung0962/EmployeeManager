using System;
using System.ComponentModel.DataAnnotations;


namespace DTO.ViewModels
{
    public class QualificationViewModel
    {
        public QualificationViewModel() { }

        public QualificationViewModel(int id, string name, DateTime releaseDate,
            DateTime expirationDate, string issuancePlaceName, string employeeName)
        {
            Id = id;
            Name = name;
            ReleaseDate = releaseDate;
            ExpirationDate = expirationDate;
            IssuancePlaceName = issuancePlaceName;
            EmployeeName = employeeName;
        }

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
    }
}
