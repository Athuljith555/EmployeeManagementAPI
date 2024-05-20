using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repository.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<bool> CreateDepartment(Department department);
        Task<bool> DeleteDepartment(int Id);
        Task<bool> UpdateDepartment(int Id, Department department);
    }
}