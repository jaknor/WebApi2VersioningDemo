namespace WebApi2VersioningDemo.Domain
{
    using System.Collections.Generic;
    public class Department
    {
        public Department()
        {
            Employees = new List<Employee>();
        }

        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
