namespace WebApi2VersioningDemo.Repository
{
    using Domain;
    using System.Collections.Generic;
    using System.Linq;

    public class DepartmentRepository
    {
        public static List<Department> Departments = new List<Department>();

        public List<Department> GetDepartments()
        {
            return Departments;
        }

        public static Department GetDepartment(string name)
        {
            return Departments.FirstOrDefault(d => d.Name == name);
        }

        public static List<Employee> GetEmployees(string name)
        {
            return Departments.FirstOrDefault(d => d.Name == name).Employees;
        }

        public static void AddDepartment(Department department)
        {
            Departments.Add(department);
        }

        public static void AddEmployee(Employee employee)
        {
            var department = Departments.FirstOrDefault(d => d.Name == employee.DepartmentName);
            department.Employees.Add(employee);
        }

        public static void RemoveDepartment(Department department)
        {
            Departments.Remove(department);
        }
    }
}
