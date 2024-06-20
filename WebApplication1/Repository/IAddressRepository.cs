using WebApplication1.Dto;

namespace WebApplication1.Repository
{
    public interface IAddressRepository
    {
        Task<List<AddressDto>> GetAllAddress();
        Task<AddressDto> CreateAddress(AddressDto addressDto);

    }
}
