namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;

    [RoutePrefix("api/v3")]
    public class EmployeeV3Controller : ApiController
    {
        [HttpGet]
        [Route("Employees")]
        public List<EmployeeModel> GetEmployees()
        {
            var employees = EmployeeRepository.GetEmployees();

            return employees.Select(
                e => new EmployeeModel {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV2", new { name = e.DepartmentName }) })
                    .OrderBy(e => e.LastName)
                    .ToList();
        }
    }
}
