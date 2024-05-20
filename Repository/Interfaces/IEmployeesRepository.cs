using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repository.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int Id);
        Task<bool> UpdateEmployee(int Id, Employee employee);
    }
}