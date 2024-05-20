using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository.Interfaces;
using EmployeeManagementAPI.Service.Interface;

namespace EmployeeManagementAPI.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> CreateDepartment(Department department)
        {
            return await _departmentRepository.CreateDepartment(department);
        }
        public async Task<bool> UpdateDepartment(int Id, Department department)
        {
            return await _departmentRepository.UpdateDepartment(Id, department);
        }

        public async Task<bool> DeleteDepartment(int id)
        {
            return await _departmentRepository.DeleteDepartment(id);
        }
    }
}
