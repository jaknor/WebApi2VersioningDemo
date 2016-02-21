namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;
    using Versioning;

    [RoutePrefix("api")]
    public class EmployeeV2Controller : ApiController
    {
        const int Version = 2;
        const int MinDate = 20160201;

        [HttpGet]
        [Route("v2/Employees")]
        [VersionedRoute("Employees", Version, MinDate)]
        public List<EmployeeModel> GetEmployees()
        {
            var employees = EmployeeRepository.GetEmployees();

            return employees.Select(
                e => new EmployeeModel {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV2", new { name = e.DepartmentName }) })
                    .ToList();
        }

        [HttpGet]
        [Route("v2/Employees")]
        [VersionedRoute("Employees", Version, MinDate, Name = "EmployeeByNameV2")]
        public EmployeeModel GetEmployee(string firstName, string lastName)
        {
            var e = EmployeeRepository.GetEmployee(firstName, lastName);

            return new EmployeeModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV2", new { name = e.DepartmentName })
                };
        }
    }
}
