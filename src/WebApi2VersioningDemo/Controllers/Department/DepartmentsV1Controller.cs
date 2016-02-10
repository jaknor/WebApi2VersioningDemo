namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;

    [RoutePrefix("api")]
    public class DepartmentsV1Controller : ApiController
    {
        [HttpGet]
        [Route("Departments")]
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
        [Route("Departments/{name}", Name = "DepartNameByNameV1")]
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
        [Route("Departments/{departmentName}/employees")]
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
