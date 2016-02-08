namespace WebApi2VersioningDemo.Repository
{
    using Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class EmployeeRepository
    {
        public static List<Employee> Employees = new List<Employee>();

        public static List<Employee> GetEmployees()
        {
            return Employees;
        }

        public static Employee GetEmployee(string firstName, string lastName)
        {
            return Employees.FirstOrDefault(e => e.FirstName == firstName && e.LastName == lastName);
        }

        public static void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            DepartmentRepository.AddEmployee(employee);
        }

        public static void RemoveEmployee(Employee employee)
        {
            Employees.Remove(employee);
        }
    }
}
