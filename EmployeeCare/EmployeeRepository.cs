using EmployeeCare.Model;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;

namespace EmployeeCare
{
    public class EmployeeRepository
    {
        private readonly string _configuration;
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration.GetConnectionString("DefaultConnectionString");

        }

        //get all employee
        public List<Employee> GetAllEmployee()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(" select *from Employee", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = (int)reader["Id"],
                        name = reader["Name"].ToString(),
                        position = reader["position"].ToString(),
                        salary = (decimal)reader["salary"]
                    });
                }
            }
            return employees;
        }
        // get all the interns 
        public List<Intern> GetAllIntern()
        {
            List<Intern> interns = new List<Intern>();
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select *from employee", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    interns.Add(new Intern {
                        Id = (int)reader["Id"],
                        Name = reader["name"].ToString(),
                        period = reader["period"].ToString(),
                        Description = reader["descrpition"].ToString(),
                    });
                }
                return interns;
            }
        }

        //Get all the senior manager and releted employee under them 
        public List<SeniorManagers> GetAllSeniorManagers()
        {

            List<SeniorManagers> seniorManagers = new List<SeniorManagers>();
            List<Employee> employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                conn.Open();
                SqlCommand sql = new SqlCommand("select *from seniormanagers", conn);
                SqlDataReader reader = sql.ExecuteReader();
                while (reader.Read())
                {
                    seniorManagers.Add(new SeniorManagers {

                        id = (int)reader["Id"],
                        name = reader["name"].ToString(),
                        age = (int)reader["Age"],
                        TotalExperience = reader["totalexperience"].ToString(),
                    });
                    employees.Add(new Employee
                    {
                        Id = (int)reader["Id"],
                        name = reader["name"].ToString(),
                        position = reader["position"].ToString(),
                        salary = (decimal)reader["salary"]
                    });
                    employees.Add(new Employee
                    {
                        Id = (int)reader["Id"],
                        name = reader["Name"].ToString(),
                        position = reader["position"].ToString(),
                        salary = (decimal)reader["salary"]
                    });
                }
                return seniorManagers;
            }
        }



        //To add Employee 

        public void createEmployee(Employee employee)
        {

            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into employee (name,position ,salary)values(@name,@position,@salary)", conn);

                cmd.Parameters.AddWithValue("@name", employee.name);
                cmd.Parameters.AddWithValue("@position", employee.position);
                cmd.Parameters.AddWithValue("@salary", employee.salary);
                cmd.ExecuteNonQuery();
                object result = cmd.ExecuteScalar();

            }

        }



        // update a particaular employee 
        public void updateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_configuration))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Employee SET Name = @Name, Position = @Position, Salary = @Salary WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@id", employee.Id);
                cmd.Parameters.AddWithValue("@name", employee.name);
                cmd.Parameters.AddWithValue("@position", employee.position);
                cmd.Parameters.AddWithValue("@salary", employee.salary);

                cmd.ExecuteNonQuery();
            }
        }

        // delete an employee 
        public void DeleteEmployee(int id)
        {
            using (SqlConnection con = new SqlConnection(_configuration))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE Id = @Id ", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
