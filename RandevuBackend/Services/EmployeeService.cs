using RandevuBackend.Data;
using RandevuBackend.Models;
using RandevuBackend.Repositorys;

namespace RandevuBackend.Services;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployeeAsync();
    Task AddEmployee(Employee employee);
}

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
    {
        return await _employeeRepository.GetAllAsync();
    }

    public async Task AddEmployee(Employee employee)
    {
       await _employeeRepository.AddEmployee(employee);
    }
}