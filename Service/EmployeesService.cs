using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository.Interfaces;
using EmployeeManagementAPI.Service.Interface;

namespace EmployeeManagementAPI.Service
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            return await _employeesRepository.CreateEmployee(employee);
        }
        public async Task<bool> UpdateEmployee(int Id,Employee employee)
        {
            return await _employeesRepository.UpdateEmployee(Id,employee);
        }

        public async Task<bool> DeleleEmployee(int id)
        {
            return await _employeesRepository.DeleteEmployee(id);
        }

    }
}
