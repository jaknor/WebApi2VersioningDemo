namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;
    using Versioning;

    [RoutePrefix("api/Departments")]
    public class DepartmentsV2Controller : ApiController
    {
        const int Version = 2;
        const int MinDate = 20160201;

        [HttpGet]
        [VersionedRoute("", Version, MinDate)]
        public List<DepartmentModel> GetDepartments()
        {
            var departments = DepartmentRepository.Departments;

            return departments.Select(
                d => new DepartmentModel
                {
                    Name = d.Name,
                    Employees = d.Employees.Select(e => new DepartmentEmployee
                    {
                        EmployeeLink = UrlHelperExtension.Link(Request, "EmployeeByName", Version, new { firstName = e.FirstName, lastName = e.LastName })
                    }).ToList()
                }).ToList();
        }

        [HttpGet]
        [VersionedRoute("{name}", Version, MinDate, Name = "DepartNameByNameV2")]
        public DepartmentModel GetDepartment(string name)
        {
            var d = DepartmentRepository.GetDepartment(name);

            return new DepartmentModel
            {
                Name = d.Name,
                Employees = d.Employees.Select(e => new DepartmentEmployee
                {
                    EmployeeLink = UrlHelperExtension.Link(Request, "EmployeeByName", Version, new { firstName = e.FirstName, lastName = e.LastName })
                }).ToList()
            };
        }

        [HttpGet]
        [VersionedRoute("{departmentName}/employees", Version, MinDate)]
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
                    DepartmentLink = UrlHelperExtension.Link(Request, "DepartNameByName", Version, new { name = e.DepartmentName })
                })
                    .ToList();
        }
    }
}
