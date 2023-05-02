using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Api.ViewModels;
using Infrastructure.Data.Entities;
using Application.Services;
using Application.Services.Interfaces;
using Newtonsoft.Json;  
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 //   [Authorize]  //can be used on individual controllers or registered as a global filter in startup
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, LinkGenerator linkGenerator, IMapper mapper)
        {          
            _customerService = customerService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerViewModel>> Get(bool includeBoards = true)
        {
            try
            {
                var customersDTO = await _customerService.GetAllCustomersAsync(includeBoards);
                var customersViewModel = _mapper.Map<CustomerViewModel[]>(customersDTO);

                return Ok(customersViewModel);

            }
            catch (Exception ex)
            {
              //  return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }


        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<CustomerViewModel>> Get(int customerId, bool includeBoards = true) 
        {
            try
            {
                var customerDTO = await _customerService.GetSingleCustomerAsync(customerId);
                var customerViewModel = _mapper.Map<CustomerViewModel>(customerDTO.Value);
                
                return Ok(customerViewModel);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        //TODO  SEARCH**

        [HttpPost]
        public async Task<ActionResult<CreateCustomerViewModel>> Post(CreateCustomerViewModel createCustomerViewModel)
        {
            try
            {
                var createCustomerDTO = _mapper.Map<CreateCustomerDTO>(createCustomerViewModel);
                var newCustomerDTO = await _customerService.CreateCustomerAsync(createCustomerDTO);

                if (newCustomerDTO != null)
                {
                    var newCreateCustomerViewModel = _mapper.Map<CreateCustomerViewModel>(newCustomerDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId = newCreateCustomerViewModel.CustomerId });

                    return Created(url, newCreateCustomerViewModel);

                  
                }

                else
                {
                    return BadRequest("Failed to save new Customer");
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
  
        [HttpPut("{customerId:int}")]
        public async Task<ActionResult<CustomerViewModel>> Put([FromBody] CustomerViewModel customerViewModel, [FromRoute] int customerId)
        {
            try
            {
                //await _customerService.UpdateCustomerAsync(customerId, customer);
                //return Ok();
                var customerDTO = _mapper.Map<CustomerDTO>(customerViewModel);

                await _customerService.UpdateCustomerAsync(customerId, customerDTO);

                var updatedCustomerDTO = await _customerService.GetSingleCustomerAsync(customerId);
                var updatedCustomerViewModel = _mapper.Map<CustomerViewModel>(updatedCustomerDTO.Value);

                return updatedCustomerViewModel;

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }  


        [HttpDelete("{customerId:int}")]
        public async Task<IActionResult> Delete(int customerId)
        {
            try
            {
                var deleteCustomer = await _customerService.DeleteCustomerAsync(customerId);
                return Ok();

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
    }
}
