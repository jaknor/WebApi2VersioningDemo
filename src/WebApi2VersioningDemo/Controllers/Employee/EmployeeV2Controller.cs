﻿namespace WebApi2VersioningDemo.Controllers
{
    using System.Web.Http;
    using Domain;
    using Repository;
    using System.Collections.Generic;
    using System.Linq;
    using Versioning;

    [RoutePrefix("api/Employees")]
    public class EmployeeV2Controller : ApiController
    {
        const int Version = 2;

        [HttpGet]
        [VersionedRoute("", Version)]
        public List<EmployeeModel> GetEmployees()
        {
            var employees = EmployeeRepository.GetEmployees();

            return employees.Select(
                e => new EmployeeModel {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV1", new { name = e.DepartmentName }) })
                    .ToList();
        }

        [HttpGet]
        [VersionedRoute("", Version, Name = "EmployeeByNameV2")]
        public EmployeeModel GetEmployee(string firstName, string lastName)
        {
            var e = EmployeeRepository.GetEmployee(firstName, lastName);

            return new EmployeeModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    StartDate = e.StartDate,
                    DepartmentLink = Url.Link("DepartNameByNameV1", new { name = e.DepartmentName })
                };
        }
    }
}
