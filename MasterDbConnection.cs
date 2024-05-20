using Microsoft.Data.SqlClient;

namespace EmployeeManagementAPI
{
    public class MasterDbConnection
    {
        private readonly string _connectionString;

        public MasterDbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection NewConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
