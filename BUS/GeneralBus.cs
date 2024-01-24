using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;
using System.Linq;


namespace BUS
{
    public class GeneralBus : IGeneralBus
    {
        private readonly IEmployeeDal _employee;
        private readonly IProvinceDal _province;

        public GeneralBus(IEmployeeDal employee, IProvinceDal province)
        {
            _employee = employee;
            _province = province;
        }

        public List<EthnicityDto> LoadEthnicityOptions()
        {
            return _employee.GetEthnicitiesData().Select(i => new EthnicityDto
            {
                Id = i.Id, EthnicityName = i.EthnicityName
            }).ToList();
        }

        public List<JobDto> LoadJobOptions()
        {
            return _employee.GetJobsData().Select(i => new JobDto
            {
                Id = i.Id, JobName = i.JobName
            }).ToList();
        }

        public List<ProvinceDto> LoadProvinceOptions()
        {
            return _province.GetProvincesData(null).Select(i => new ProvinceDto
            { 
                Id = i.ProvinceId, ProvinceName = i.ProvinceName
            }).ToList();
        }
    }
}
