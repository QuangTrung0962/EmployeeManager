using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;

namespace BUS
{
    public class QualificationBus : IQualificationBus
    {
        private readonly IQualificationDal _qualificationDal;

        public QualificationBus(IQualificationDal qualification)
        {
            _qualificationDal = qualification;
        }

        public bool AddQualificatio(QualificationDto qualificationDto)
        {
            Qualification obj = new Qualification()
            {
                Name = qualificationDto.Name,
                ReleaseDate = qualificationDto.ReleaseDate,
                IssuancePlace = qualificationDto.IssuancePlace,
                ExpirationDate = qualificationDto.ExpirationDate,
                EmployeeId = qualificationDto.EmployeeId
            };

            if (_qualificationDal.AddQualificatio(obj)) return true;
            else return false;
        }

        public bool DeleteQualificatio(int id)
        {
            if (_qualificationDal.DeleteQualificatio(id)) return true;
            else return false;
        }

        public List<QualificationDto> GetQualificationsData(string searchString)
        {
            return _qualificationDal.GetQualificationsData(searchString);
        }

        public bool UpdateQualificatio(QualificationDto qualificationDto)
        {
            if (_qualificationDal.UpdateQualificatio(qualificationDto)) return true;
            else return false;
        }

        public List<QualificationDto> GetQualificationsByEmployeeId(int id)
        {
             return _qualificationDal.GetQualificationsByEmployeeId(id);
        }
    }

}
