//using ProjectManagementApp.Api.Data.Entities;
using Application.DTOs;
using Core;
using Infrastructure.Data.Entities;


namespace Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync(bool includeBoards = true);
        Task<Response<CustomerDTO?>> GetSingleCustomerAsync(int customerId, bool includeBoards = true);
        Task<Response<CreateCustomerDTO?>> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO);
        Task UpdateCustomerAsync(int customerId, CustomerDTO customerDTO);
        Task<CustomerDTO?> DeleteCustomerAsync(int customerId);
    }
}