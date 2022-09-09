using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices
{
    public class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _conn;

        //custom OR parameterized constructor: allows us to create objects
        //set properties and values inside the constructor
        //we're setting the connection
        public DapperDepartmentRepository(IDbConnection conn) 
        {
            _conn = conn;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _conn.Query<Department>("SELECT * FROM Departments;"); 
            
            //.Query is Dapper; we're using .Query because we're not manipulating data, just reading

        }

        public void InsertDepartment(string name)
        {
            _conn.Execute("INSERT INTO Departments (Name) VALUES (@name)", new {name }); //new {name = name}
            
            //using @ symbol = anonymous function to prevent SQL injection
            //void is the return type. Using void because we're not expecting anything back, we're just inserting data

        }
    }
}
