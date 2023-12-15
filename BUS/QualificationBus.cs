using BUS.Interfaces;
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

        public List<QualificationDto> GetQualificationsData(string searchString)
        {
            return _qualificationDal.GetQualificationsData(searchString);
        }
    }

}
