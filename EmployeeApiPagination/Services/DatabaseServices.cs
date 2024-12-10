using EmployeeApiPagination.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; // Make sure to use Microsoft.Data.SqlClient, not System.Data.SqlClient
using Dapper; // Add Dapper namespace to use Query extension
using System.Data;


namespace EmployeeApiPagination.Services
{
    public class DatabaseServices
    {
        private readonly string _connectionString;

        public DatabaseServices(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to fetch all employees
        public IEnumerable<Employees> GetAllEmployees()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Employees";  // Replace with your actual table name
                var employees = connection.Query<Employees>(query).ToList();  // Assuming Dapper or similar ORM for database query
                return employees;
            }
        }

        public IEnumerable<Employees> GetEmployees(int pageNumber, int pageSize, string searchString)
        {
            // Use SqlConnection directly, not IDbConnection
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Employees>(
                    "GetEmployees", // Stored procedure name
                    new
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        SearchString = searchString
                    },
                    commandType: CommandType.StoredProcedure
                );
            }
        }
    }
}
