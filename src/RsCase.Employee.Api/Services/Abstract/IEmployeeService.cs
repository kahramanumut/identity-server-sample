using System.Collections.Generic;

public interface IEmployeeService
{
    Result<EmployeeDto> GetEmployeeById(string employeeId);
    Result<List<EmployeeDto>> GetAllEmployee();
    Result AddEmployee(EmployeeDto dto);
    Result DeleteEmployee(string employeeId);
    Result UpdateEmployee(string employeeId, EmployeeDto dto);
}