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

        public bool AddQualification(QualificationDto qualificationDto)
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

        public bool DeleteQualification(int id)
        {
            if (_qualificationDal.DeleteQualification(id)) return true;
            else return false;
        }

        public List<QualificationDto> GetQualificationsData(string searchString)
        {
            return _qualificationDal.GetQualificationsData(searchString);
        }

        public bool UpdateQualification(QualificationDto qualificationDto)
        {
            if (_qualificationDal.UpdateQualification(qualificationDto)) return true;
            else return false;
        }

        public List<QualificationDto> GetQualificationsByEmployeeId(int id)
        {
             return _qualificationDal.GetQualificationsByEmployeeId(id);
        }

        public QualificationDto GetQualificationById(int id)
        {
            return _qualificationDal.GetQualificationById(id);
        }
    }

}
