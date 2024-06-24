using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IMapper _mapper;
        private readonly EmployeeDbContext _employeeDbContext;

        public AddressRepository(IMapper mapper,EmployeeDbContext employeeDbContext)
        {
            _mapper = mapper;
            _employeeDbContext = employeeDbContext;
        }

        public async Task<AddressDto> CreateAddress(AddressDto addressDto)
        {
            if (addressDto == null) { 
                throw new ArgumentNullException(nameof(addressDto));
            } 
            var address = _mapper.Map<Address>(addressDto);
            _employeeDbContext.AddAsync(addressDto);
            await _employeeDbContext.SaveChangesAsync();
            return _mapper.Map<AddressDto>(address);
        }
        public async Task<List<AddressDto>> GetAllAddress()
        {
            var address = await _employeeDbContext.Addresses.ToListAsync();
            return _mapper.Map<List<AddressDto>>(address);
        }
    }
}
