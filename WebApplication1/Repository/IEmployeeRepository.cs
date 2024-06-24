using WebApplication1.Dto;

namespace WebApplication1.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetAllEmployee();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<EmployeeDto> CreateEmployee(EmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployee(EmployeeDto employeeDto);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
