using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dto;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class AddressController :ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;

        public AddressController(IMapper mapper,IAddressRepository addressRepository)
        {
            _mapper = mapper;
            _addressRepository = addressRepository;
        }
        [Authorize]
        [HttpGet("GetByAllAddress")]
        public async Task<object> Get()
        {
            try
            {
                var Address = await _addressRepository.GetAllAddress();
                return Ok(Address);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [Authorize]
        [HttpPost("CreateAddress")]
        public async Task<ActionResult<AddressDto>> Create([FromBody] AddressDto addressDto)
        {
            try
            {
                var address = await _addressRepository.CreateAddress(addressDto);
                return Ok(address);

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
