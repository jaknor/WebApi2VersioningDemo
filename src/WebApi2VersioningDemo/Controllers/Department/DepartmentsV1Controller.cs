namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;
    using Versioning;

    [RoutePrefix("api")]
    public class DepartmentsV1Controller : ApiController
    {
        const int Version = 1;
        const int MinDate = 0;
        const int MaxDate = 20160201;

        [HttpGet]
        [Route("v1/Departments")]
        [VersionedRoute("Departments", Version, MinDate, MaxDate)]
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
        [Route("v1/Departments/{name}")]
        [VersionedRoute("Departments/{name}", Version, MinDate, MaxDate, Name = "DepartNameByNameV1")]
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
        [Route("v1/Departments/{departmentName}/employees")]
        [VersionedRoute("Departments/{departmentName}/employees", Version, MinDate, MaxDate)]
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
