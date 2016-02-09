namespace WebApi2VersioningDemo.Domain
{
    using System.Collections.Generic;
    public class DepartmentModel
    {
        public DepartmentModel()
        {
            Employees = new List<DepartmentEmployee>();
        }

        public string Name { get; set; }

        public List<DepartmentEmployee> Employees { get; set; }
    }
}
