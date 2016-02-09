namespace WebApi2VersioningDemo
{
    using Owin;
    using System.Web.Http;
    using Domain;
    using Repository;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);

            InitializeData();
        }

        private void InitializeData()
        {
            var sabr = new Department { Name = "SABR" };
            var ano = new Department { Name = "ANO" };
            DepartmentRepository.AddDepartment(sabr);
            DepartmentRepository.AddDepartment(ano);

            var j = new Employee { DepartmentName = "SABR", FirstName = "Jakob", LastName = "N", Salary = 10m, StartDate = "2014-11-10" };
            var d = new Employee { DepartmentName = "SABR", FirstName = "Dan", LastName = "G", Salary = 99m, StartDate = "1990-01-01" };
            var p = new Employee { DepartmentName = "ANO", FirstName = "Phil", LastName = "W", Salary = 10m, StartDate = "2013-11-10" };
            var n = new Employee { DepartmentName = "ANO", FirstName = "Nick", LastName = "S", Salary = 10m, StartDate = "2014-01-10" };

            EmployeeRepository.AddEmployee(j);
            EmployeeRepository.AddEmployee(d);
            EmployeeRepository.AddEmployee(p);
            EmployeeRepository.AddEmployee(n);
        }
    }
}
