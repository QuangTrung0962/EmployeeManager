using BUS.Interfaces;
using DAL.Interfaces;
using DTO;
using System.Collections.Generic;


namespace BUS
{
    public class GeneralBus : IGeneralBus
    {
        private readonly IEmployeeDal _employee;
        private readonly IProvinceBus _province;

        public GeneralBus(IEmployeeDal employee, IProvinceBus province)
        {
            _employee = employee;
            _province = province;
        }

        public List<EthnicityDto> LoadEthnicityOptions()
        {
            return _employee.GetEthnicitiesData();
        }

        public List<JobDto> LoadJobOptions()
        {
            return _employee.GetJobsData();
        }

        public List<ProvinceDto> LoadProvinceOptions()
        {
            return _province.GetProvincesData(null);
        }
    }
}
