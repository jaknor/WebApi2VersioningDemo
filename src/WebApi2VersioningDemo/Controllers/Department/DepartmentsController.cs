namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;
    using Versioning;

    [RoutePrefix("api/Departments")]
    public class DepartmentsController : ApiController
    {
        [HttpGet]
        [VersionedRoute("", 1)]
        public List<DepartmentModel> GetDepartments()
        {
            var departments = DepartmentRepository.Departments;

            return departments.Select(
                d => new DepartmentModel
                {
                    Name = d.Name,
                    Employees = d.Employees.Select(e => new DepartmentEmployee
                    {
                        EmployeeLink = Url.Link("EmployeeByNameV1", new { firstName = e.FirstName, lastName = e.LastName })
                    }).ToList()
                }).ToList();
        }

        [HttpGet]
        [VersionedRoute("{name}", 1, Name = "DepartNameByNameV1")]
        public DepartmentModel GetDepartment(string name)
        {
            var d = DepartmentRepository.GetDepartment(name);

            return new DepartmentModel
            {
                Name = d.Name,
                Employees = d.Employees.Select(e => new DepartmentEmployee
                {
                    EmployeeLink = Url.Link("EmployeeByNameV1", new { firstName = e.FirstName, lastName = e.LastName })
                }).ToList()
            };
        }

        [HttpGet]
        [VersionedRoute("{departmentName}/employees", 1)]
        public List<EmployeeModel> GetEmployees(string departmentName)
        {
            var employees = DepartmentRepository.GetEmployees(departmentName);

            return employees.Select(
                e => new EmployeeModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV1", new { name = e.DepartmentName })
                })
                    .ToList();
        }
    }
}
