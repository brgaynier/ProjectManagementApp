using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
//using ProjectManagementApp.Api.Data;
//using ProjectManagementApp.Api.Data.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.DTOs;
using AutoMapper.QueryableExtensions;
using Core;

namespace Application.Services
{
    public class ChangeOrderService : IChangeOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ChangeOrderService> _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;

        public ChangeOrderService(ApplicationDbContext dbContext, ILogger<ChangeOrderService> logger, IMapper mapper, ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ChangeOrderDTO>> GetAllChangeOrdersByCustomerIdAsync(int customerId)
        {
            _logger.LogInformation($"Getting all Change Orders for Customer {customerId}");

            IQueryable<ChangeOrder> query = _dbContext.ChangeOrders;

            // Order It
            query = query
                .Where(t => t.Customer.CustomerId == customerId)
                .OrderBy(c => c.ChangeOrderId);

            var changeOrderDTOs = await query.ProjectTo<ChangeOrderDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return changeOrderDTOs;

        }

        public async Task<Response<ChangeOrderDTO?>> GetSingleChangeOrderByCustomerIdAsync(int changeOrderId, int customerId)
        {
            _logger.LogInformation($"Getting Change Order with Id {changeOrderId}");

            IQueryable<ChangeOrder> query = _dbContext.ChangeOrders;
           
            if (query == null)
            {
                return new Response<ChangeOrderDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Change Order does not exist" } } };

            }
            // Add Query
            query = query
                .Where(t => t.ChangeOrderId == changeOrderId && t.Customer.CustomerId == customerId);


            var changeOrderDTO = await query.ProjectTo<ChangeOrderDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<ChangeOrderDTO?> { Value = changeOrderDTO, Status = ResponseStatus.Ok };

        }

        public async Task<Response<CreateChangeOrderDTO?>> CreateChangeOrderAsync(int customerId, CreateChangeOrderDTO createChangeOrderDTO)
        {   
            var customerDTO = await _customerService.GetSingleCustomerAsync(customerId);
            var customer = _mapper.Map<Customer>(customerDTO);

            if (customer == null)
            {
                return new Response<CreateChangeOrderDTO?> { Value = null, Status = ResponseStatus.NotFound, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "No Customer found" } } };
            }

            var newChangeOrder = _mapper.Map<ChangeOrder>(createChangeOrderDTO);
            newChangeOrder.CustomerId = customerId;

            await _dbContext.ChangeOrders.AddAsync(newChangeOrder);
            await _dbContext.SaveChangesAsync();

            var changeOrderDTO = _mapper.Map<CreateChangeOrderDTO>(newChangeOrder);

            return new Response<CreateChangeOrderDTO?> { Value = changeOrderDTO, Status = ResponseStatus.Ok };

        }

        public async Task UpdateChangeOrderAsync(int changeOrderId, ChangeOrderDTO changeOrderDTO)  
        {
            _logger.LogInformation($"Updating Card {changeOrderId}");

            var updateChangeOrder = await _dbContext.ChangeOrders.FindAsync(changeOrderId);
            if (updateChangeOrder != null)
            {
                updateChangeOrder.ChangeOrderNumber = changeOrderDTO.ChangeOrderNumber;
                updateChangeOrder.ChangeOrderName = changeOrderDTO.ChangeOrderName;
                updateChangeOrder.Description = changeOrderDTO.Description;
                updateChangeOrder.AmountOwed = changeOrderDTO.AmountOwed;

                _mapper.Map(updateChangeOrder, changeOrderDTO);
                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<ChangeOrderDTO?> DeleteChangeOrderAsync(int customerId, int changeOrderId)
        {
            var changeOrder = await _dbContext.ChangeOrders.FindAsync(changeOrderId);

            if (changeOrder != null)
            {
                _dbContext.ChangeOrders.Remove(changeOrder);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedChangeOrderDTO = _mapper.Map<ChangeOrderDTO>(changeOrder);
            return deletedChangeOrderDTO;

        }

    }
}
