using System;

public class Employee
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public Department Department { get; set; }
}

public enum Department
{
    IT = 1,
    Business = 2,
    Finance = 3
}