using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class EmployeeController :ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IMapper mapper,IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        [Authorize]
        [HttpGet("GetAllEmployee")]
       
        public async Task<object> Get()
        {
            var employee = await _employeeRepository.GetAllEmployee();
            return Ok(employee);    
        }
        [Authorize]
        [HttpGet("GetEmployyeById")]

        public async Task<object> GetEmployeeById([FromQuery] int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(employeeId);
                if(employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPost("CreateEmployee")]
        
        public async Task<ActionResult<EmployeeDto>> Create([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = await _employeeRepository.CreateEmployee(employeeDto);
                return Ok(employee);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPut("UpdateEmployee")]
        
        public async Task<Object> Update ([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = await _employeeRepository.UpdateEmployee(employeeDto);
                if(employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("DeleteEmployee")]
       
        public async Task<object> Delete([FromQuery] int Id)
        {
            try
            {
                var employee = await _employeeRepository.DeleteEmployee(Id);
                return Ok(employee);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
