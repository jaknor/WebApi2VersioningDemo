namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;

    [RoutePrefix("api/Employees")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public List<EmployeeModel> GetEmployees()
        {
            var employees = EmployeeRepository.GetEmployees();

            return employees.Select(
                e => new EmployeeModel {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByName", new { name = e.DepartmentName }) })
                    .ToList();
        }

        [HttpGet]
        [Route("", Name = "EmployeeByName")]
        public EmployeeModel GetEmployee(string firstName, string lastName)
        {
            var e = EmployeeRepository.GetEmployee(firstName, lastName);

            return new EmployeeModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Salary = e.Salary,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByName", new { name = e.DepartmentName })
                };
        }
    }
}
