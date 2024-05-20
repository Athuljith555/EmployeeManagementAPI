using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Service.Interface
{
    public interface IEmployeesService
    {
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> DeleleEmployee(int Id);
        Task<bool> UpdateEmployee(int Id, Employee employee);
    }
}