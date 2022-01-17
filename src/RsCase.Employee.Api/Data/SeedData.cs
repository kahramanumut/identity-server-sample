using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class SeedData
{
    public static void InitializeSampleData(this IApplicationBuilder app)
    {
        using(var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

            if(!dbContext.Employees.Any())
            {
                List<Employee> employees = new List<Employee>
                {
                    new Employee { Id = "1C7E3544-3F8D-4CF0-85CB-1E6472AABC63", Department = Department.IT, Name = "David", Surname = "Fowler", Age = 30 },
                    new Employee { Id = "D4013BB6-2CC0-4305-A6E2-570FED4EE9D8", Department = Department.Business, Name = "Scott", Surname = "Hanselman", Age = 23 },
                    new Employee { Id = Guid.NewGuid().ToString().ToUpper(), Department = Department.Finance, Name = "Bora", Surname = "Kaşmer", Age = 31 },
                    new Employee { Id = Guid.NewGuid().ToString().ToUpper(), Department = Department.IT, Name = "Umut", Surname = "Kahraman", Age = 22 },
                };

                dbContext.Employees.AddRange(employees);
                dbContext.SaveChanges();
            }
        }
    }
}