//using ProjectManagementApp.Api.Data.Entities;
using Application.DTOs;
using Core;
using Infrastructure.Data.Entities;


namespace Application.Services.Interfaces
{
    public interface IChangeOrderService
    {
        Task<IEnumerable<ChangeOrderDTO>> GetAllChangeOrdersByCustomerIdAsync(int customerId);
        Task<Response<ChangeOrderDTO?>> GetSingleChangeOrderByCustomerIdAsync(int changeOrderId, int customerId);
        Task<Response<CreateChangeOrderDTO?>> CreateChangeOrderAsync(int customerId, CreateChangeOrderDTO createChangeOrderDTO);
        Task UpdateChangeOrderAsync(int changeOrderId, ChangeOrderDTO changeOrderDTO);
        Task<ChangeOrderDTO?> DeleteChangeOrderAsync(int customerId, int changeOrderId);


    }
}