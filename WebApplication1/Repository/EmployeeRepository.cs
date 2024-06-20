using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMapper _mapper;
        private readonly EmployeeDbContext _dbContext;

        public EmployeeRepository(IMapper mapper,EmployeeDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<List<EmployeeDto>> GetAllEmployee()
        {
            var employee = await _dbContext.Employees.ToListAsync();
            return _mapper.Map<List<EmployeeDto>>(employee);
        }
        public async Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<bool> DeleteEmployee(int Id)
        {
            var employee = await _dbContext.Employees.FindAsync(Id);
            if(employee == null)
            {
                return false;
            }
            _dbContext.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<EmployeeDto> GetEmployeeById(int employeeId)
        {

            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return null;
            }
            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            if (employee == null)
            {
                throw new ArgumentNullException();
            }
            _dbContext.Update(employee);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<EmployeeDto>(employee);  
        }
        public async Task<object> Delete([FromQuery] int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if(employee == null)
            {
                return false;
            }
            _dbContext.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
