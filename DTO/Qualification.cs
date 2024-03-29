//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DTO
{
    using System;

    public partial class Qualification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public int IssuancePlace { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Province Province { get; set; }

        public Qualification() { }

        public Qualification(QualificationDto enity)
        {
            Id = enity.Id;
            Name = enity.Name;
            ReleaseDate = enity.ReleaseDate;
            ExpirationDate = enity.ExpirationDate;
            IssuancePlace = enity.IssuancePlace;
            EmployeeId = enity.EmployeeId;
        }
    }
}
