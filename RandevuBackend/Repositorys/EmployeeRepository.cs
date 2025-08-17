using System.Collections;
using Microsoft.EntityFrameworkCore;
using RandevuBackend.Data;
using RandevuBackend.Interfaces;
using RandevuBackend.Models;

namespace RandevuBackend.Repositorys;

public interface IEmployeeRepository
{
    public Task<IEnumerable<Employee>> GetAllAsync();
    public Task AddEmployee(Employee employee);
}
public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employee.ToListAsync();
    }

    public async Task AddEmployee(Employee employee)
    {
       await _context.Employee.AddAsync(employee);
       await _context.SaveChangesAsync();
    }
    
}