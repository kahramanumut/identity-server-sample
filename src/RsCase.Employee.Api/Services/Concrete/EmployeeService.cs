using System;
using System.Collections.Generic;
using System.Linq;
using MapsterMapper;

public class EmployeeService : IEmployeeService
{
    private readonly EmployeeDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmployeeService(EmployeeDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Result<List<EmployeeDto>> GetAllEmployee()
    {
        List<Employee> employees = _dbContext.Employees.ToList();
        if(employees.Count == 0)
            return new Result<List<EmployeeDto>>(null, false, "Employees infomation could not found");

        var data = _mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
        return new Result<List<EmployeeDto>>(data, true, "Employees information has got successfully");
    }

    public Result<EmployeeDto> GetEmployeeById(string employeeId)
    {
        Employee existEmployee = _dbContext.Employees.Where(x => x.Id == employeeId.ToUpper()).FirstOrDefault();
        if(existEmployee == null)
            return new Result<EmployeeDto>(null, false, "Employee information could not found");
        
        EmployeeDto data = _mapper.Map<Employee, EmployeeDto>(existEmployee);
        return new Result<EmployeeDto>(data, true, "Employee information has got successfully");
    }

    public Result AddEmployee(EmployeeDto dto)
    {
        //TODO: It would better using FluentValidation, so we can check all properties
        if(dto == null)
            return new Result(false, "Employee information must fill");

        Employee newEmployee = _mapper.Map<EmployeeDto, Employee>(dto);
        newEmployee.Id = Guid.NewGuid().ToString().ToUpper();

        _dbContext.Employees.Add(newEmployee);
        _dbContext.SaveChanges();

        return new Result(true, "New employee added successfully");
    }

    public Result DeleteEmployee(string employeeId)
    {
        Employee existEmployee = _dbContext.Employees.Where(x => x.Id == employeeId.ToUpper()).FirstOrDefault();
        if(existEmployee == null)
            return new Result(false, "Employee information could not found");
        
        _dbContext.Employees.Remove(existEmployee);
        _dbContext.SaveChanges();

        return new Result(true, "Employee has deleted successfully");
    }

    public Result UpdateEmployee(string employeeId, EmployeeDto dto)
    {
        Employee existEmployee = _dbContext.Employees.Where(x => x.Id == employeeId.ToUpper()).FirstOrDefault();
        if(existEmployee == null)
            return new Result(false, "Employee information could not found");
        
        _mapper.Map<EmployeeDto, Employee>(dto,existEmployee);
        _dbContext.Update(existEmployee);
        _dbContext.SaveChanges();

        return new Result(true, "Employee has updated successfully");
    }

}