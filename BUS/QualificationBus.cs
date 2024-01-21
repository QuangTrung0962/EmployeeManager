using BUS.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using log4net;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class QualificationBus : IQualificationBus
    {
        private readonly IQualificationDal _qualification;
        private readonly IBaseDal<Qualification> _base;
        private readonly ILog _log;

        public QualificationBus(IQualificationDal qualification, IBaseDal<Qualification> baseDal)
        {
            _qualification = qualification;
            _base = baseDal;
            _log = LogManager.GetLogger(typeof(DistrictBus));
        }

        public bool AddQualification(QualificationDto qualificationDto)
        {
            try
            {
                var qualification =
                    new Qualification(qualificationDto.Id, qualificationDto.Name,
                    qualificationDto.ReleaseDate, qualificationDto.ExpirationDate,
                    qualificationDto.IssuancePlace, qualificationDto.EmployeeId);
                _base.InsertEntity(qualification);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool UpdateQualification(QualificationDto qualificationDto)
        {
            try
            {
                var qualification =
                    new Qualification(qualificationDto.Id, qualificationDto.Name,
                    qualificationDto.ReleaseDate, qualificationDto.ExpirationDate,
                    qualificationDto.IssuancePlace, qualificationDto.EmployeeId);
                _base.UpdateEntity(qualification);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public bool DeleteQualification(int id)
        {
            try
            {
                var qualificationDto = GetQualificationById(id);
                var qualification =
                    new Qualification(qualificationDto.Id, qualificationDto.Name,
                    qualificationDto.ReleaseDate, qualificationDto.ExpirationDate,
                    qualificationDto.IssuancePlace, qualificationDto.EmployeeId);
                _base.DeleteEntity(qualification);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public List<QualificationDto> GetQualificationsData(string searchString)
        {
            return _qualification.GetQualificationsData(searchString);
        }

        public List<QualificationDto> GetQualificationsByEmployeeId(int id)
        {
            return _qualification.GetQualificationsByEmployeeId(id);
        }

        public QualificationDto GetQualificationById(int id)
        {
            return _qualification.GetQualificationById(id);
        }

    }

}
