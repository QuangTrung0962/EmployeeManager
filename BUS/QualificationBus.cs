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
        private readonly IQualificationDal _qualificationDal;
        private readonly IBaseDal<Qualification> _baseDal;
        private readonly ILog _log;

        public QualificationBus(IQualificationDal qualification,IBaseDal<Qualification> baseDal)
        {
            _qualificationDal = qualification;
            _baseDal = baseDal;
            _log = LogManager.GetLogger(typeof(DistrictBus));
        }

        public bool AddQualification(QualificationDto qualificationDto)
        {
            try
            {
                Qualification qualification = SetQualificationModel(qualificationDto);
                _baseDal.InsertEntity(qualification);
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
                Qualification qualification = SetQualificationModel(qualificationDto);
                _baseDal.UpdateEntity(qualification);
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
                Qualification qualification = SetQualificationModel(qualificationDto);
                _baseDal.DeleteEntity(qualification);
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
            return _qualificationDal.GetQualificationsData(searchString);
        }

        public List<QualificationDto> GetQualificationsByEmployeeId(int id)
        {
             return _qualificationDal.GetQualificationsByEmployeeId(id);
        }

        public QualificationDto GetQualificationById(int id)
        {
            return _qualificationDal.GetQualificationById(id);
        }

        public QualificationDto SetQualificationDtoModel(Qualification qualifi)
        {
            return new QualificationDto(qualifi.Id, qualifi.Name, qualifi.ReleaseDate, qualifi.IssuancePlace,
                qualifi.ExpirationDate, qualifi.EmployeeId);
        }

        public Qualification SetQualificationModel(QualificationDto qualifi)
        {
            return new Qualification(qualifi.Id, qualifi.Name, qualifi.ReleaseDate, qualifi.IssuancePlace,
               qualifi.ExpirationDate, qualifi.EmployeeId);
        }
    }

}
