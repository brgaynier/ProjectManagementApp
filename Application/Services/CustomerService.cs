using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Application.DTOs;
using AutoMapper.QueryableExtensions;
using Core;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext dbContext, ILogger<CustomerService> logger, IMapper mapper) //should this be taking ViewModel instead of dbcontext?
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync(bool includeBoards = true)
        {
            _logger.LogInformation($"Getting all Customers");

            IQueryable<Customer> query = _dbContext.Customers;
            // .Include(c => c.Location);


            if (includeBoards)
            {
                query = query
                    .Include(w => w.WorkFlows)
                    .Include(a => a.ChangeOrders)
                    .Include(c => c.Boards)
                    .ThenInclude(m => m.Blocks)
                    .ThenInclude(t => t.Cards);
            }

            // Order It
          //  query = query.OrderBy(c => c.DueDate);

            var customerDTOs = await query.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return customerDTOs;
        }

        public async Task<Response<CustomerDTO?>> GetSingleCustomerAsync(int customerId, bool includeBoards = true)
        {
            _logger.LogInformation($"Getting a Customer with Id number {customerId}");

            IQueryable<Customer> query = _dbContext.Customers;
            //.Include(c => c.Boards);

            if (query == null)
            {
                return new Response<CustomerDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Customer does not exist" } } };

            }

            if (includeBoards)
            {
                query = query
                .Include(w => w.WorkFlows)
                .Include(a => a.ChangeOrders)
                .Include(c => c.Boards)
                .ThenInclude(m => m.Blocks)
                .ThenInclude(t => t.Cards);
            }

            // Query It
            query = query.Where(c => c.CustomerId == customerId);

            var customerDTO = await query.ProjectTo<CustomerDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<CustomerDTO?> { Value = customerDTO, Status = ResponseStatus.Ok };
        }

        public async Task<Response<CreateCustomerDTO?>> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {   

            var newCustomer = _mapper.Map<Customer>(createCustomerDTO);
            //newCard.BlockId = blockId;

            await _dbContext.Customers.AddAsync(newCustomer);
            await _dbContext.SaveChangesAsync();

            var customerDTO = _mapper.Map<CreateCustomerDTO>(newCustomer);

            return new Response<CreateCustomerDTO?> { Value = customerDTO, Status = ResponseStatus.Ok };

        }

        public async Task UpdateCustomerAsync(int customerId, CustomerDTO customerDTO)
        {
            _logger.LogInformation($"Updating Customer {customerId}");

            var updateCustomer = await _dbContext.Customers.FindAsync(customerId);
            if (updateCustomer != null)
            {
                updateCustomer.CustomerName = customerDTO.CustomerName;
                updateCustomer.CustomerPhone = customerDTO.CustomerPhone;
                updateCustomer.Email = customerDTO.Email;
                updateCustomer.DueDate = customerDTO.DueDate;
                updateCustomer.StartDate = customerDTO.StartDate;
                updateCustomer.CompanyName = customerDTO.CompanyName;
                updateCustomer.Address = customerDTO.Address;
                updateCustomer.CityTown = customerDTO.CityTown;
                updateCustomer.StateProvince = customerDTO.StateProvince;
                updateCustomer.Country = customerDTO.Country;
                updateCustomer.PostalCode = customerDTO.PostalCode;

                _mapper.Map(updateCustomer, customerDTO);


                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<CustomerDTO?> DeleteCustomerAsync(int customerId)
        {
            var customer = await _dbContext.Customers.FindAsync(customerId);

            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedCustomerDTO = _mapper.Map<CustomerDTO>(customer);
            return deletedCustomerDTO;

        }     
       
    }
}
