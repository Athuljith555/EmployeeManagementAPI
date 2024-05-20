using System.Data;
using Dapper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly MasterDbConnection _masterDbConnection;

        public EmployeesRepository(MasterDbConnection masterDbConnection)
        {
            _masterDbConnection = masterDbConnection;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection conn = _masterDbConnection.NewConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        DynamicParameters param1 = new DynamicParameters();
                        param1.Add("@EmployeeID", employee.EmployeeID);
                        param1.Add("@FirstName", employee.FirstName);
                        param1.Add("@LastName", employee.LastName);
                        param1.Add("@JobTitle", employee.JobTitle);
                        param1.Add("@HireDate", employee.HireDate);
                        param1.Add("@Salary", employee.Salary);

                        await conn.ExecuteAsync("InsertIntoEmployeesTable", param1, commandType: CommandType.StoredProcedure, transaction: transaction);

                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEmployee(int Id,Employee employee)
        {
            try
            {
                using (SqlConnection conn = _masterDbConnection.NewConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        DynamicParameters param = new();
                        param.Add("@EmployeeID", Id);
                        param.Add("@FirstName", employee.FirstName);
                        param.Add("@LastName", employee.LastName);
                        param.Add("@JobTitle", employee.JobTitle);
                        param.Add("@HireDate", employee.HireDate);
                        param.Add("@Salary",employee.Salary);
                        employee.EmployeeID = await conn.ExecuteScalarAsync<int>("UpdateEmployeesTable", param, commandType: CommandType.StoredProcedure, transaction: transaction);
                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(int Id)
        {
            try
            {
                using (SqlConnection conn = _masterDbConnection.NewConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        DynamicParameters param = new();
                        param.Add("@EmployeeID", Id);

                        await conn.ExecuteAsync("DeleteFromEmployeesTable", param, commandType: CommandType.StoredProcedure, transaction: transaction);
                        transaction.Commit();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
