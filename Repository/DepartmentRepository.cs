using System.Data;
using Dapper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MasterDbConnection _masterDbConnection;

        public DepartmentRepository(MasterDbConnection masterDbConnection)
        {
            _masterDbConnection = masterDbConnection;
        }

        public async Task<bool> CreateDepartment(Department department)
        {
            try
            {
                using (SqlConnection conn = _masterDbConnection.NewConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        DynamicParameters param1 = new DynamicParameters();
                        param1.Add("@DepartmentID", department.DepartmentID);
                        param1.Add("@DepartmentName", department.DepartmentName);

                        await conn.ExecuteAsync("InsertIntoDepartmentTable", param1, commandType: CommandType.StoredProcedure, transaction: transaction);

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

        public async Task<bool> UpdateDepartment(int Id, Department department)
        {
            try
            {
                using (SqlConnection conn = _masterDbConnection.NewConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        DynamicParameters param = new();
                        param.Add("@DepartmentID", Id);
                        param.Add("@DepartmentName", department.DepartmentName);

                        department.DepartmentID = await conn.ExecuteScalarAsync<int>("UpdateDepartmentTable", param, commandType: CommandType.StoredProcedure, transaction: transaction);
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

        public async Task<bool> DeleteDepartment(int Id)
        {
            try
            {
                using (SqlConnection conn = _masterDbConnection.NewConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        DynamicParameters param = new();
                        param.Add("@DepartmentID", Id);

                        await conn.ExecuteAsync("DeleteFromDepartmentTable", param, commandType: CommandType.StoredProcedure, transaction: transaction);
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
