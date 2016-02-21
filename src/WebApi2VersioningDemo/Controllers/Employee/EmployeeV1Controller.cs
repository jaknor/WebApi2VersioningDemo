namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;
    using Versioning;

    [RoutePrefix("api")]
    public class EmployeeV1Controller : ApiController
    {
        const int Version = 1;
        const int MinDate = 0;
        const int MaxDate = 20160201;

        [HttpGet]
        [Route("v1/Employees")]
        [VersionedRoute("Employees", Version, MinDate, MaxDate)]
        public List<EmployeeModel> GetEmployees()
        {
            var employees = EmployeeRepository.GetEmployees();

            return employees.Select(
                e => new EmployeeModel {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV1", new { name = e.DepartmentName }) })
                    .ToList();
        }

        [HttpGet]
        [Route("v1/Employees")]
        [VersionedRoute("Employees", Version, MinDate, MaxDate, Name = "EmployeeByNameV1")]
        public EmployeeModel GetEmployee(string firstName, string lastName)
        {
            var e = EmployeeRepository.GetEmployee(firstName, lastName);

            return new EmployeeModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV1", new { name = e.DepartmentName })
                };
        }
    }
}
