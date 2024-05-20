using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Service.Interface
{
    public interface IDepartmentService
    {
        Task<bool> CreateDepartment(Department department);
        Task<bool> DeleteDepartment(int id);
        Task<bool> UpdateDepartment(int Id, Department department);
    }
}