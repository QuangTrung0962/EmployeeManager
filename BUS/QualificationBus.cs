using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using DTO.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var qualification = new Qualification(qualificationDto);
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
                var qualification = new Qualification(qualificationDto);
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
                var qualification = new Qualification(qualificationDto);
                _base.DeleteEntity(qualification);
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Error: " + ex);
                return false;
            }
        }

        public List<QualificationViewModel> GetQualificationsData(string searchString)
        {
            var qualifications = _qualification.GetQualificationsData(searchString);
            return SetQualificationsViewModel(qualifications);
        }

        public List<QualificationViewModel> GetQualificationsByEmployeeId(int id)
        {
            var qualifications = _qualification.GetQualificationsByEmployeeId(id);
            return SetQualificationsViewModel(qualifications);
        }

        public QualificationDto GetQualificationById(int id)
        {
            var qualification = _qualification.GetQualificationById(id);
            return new QualificationDto(qualification);
        }

        private List<QualificationViewModel> SetQualificationsViewModel
            (List<Qualification> qualifications)
        {
            return qualifications.Select(i => new QualificationViewModel(i)).ToList();
        }
    }

}
