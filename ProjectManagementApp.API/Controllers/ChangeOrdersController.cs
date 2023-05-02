using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using ProjectManagementApp.Api.Data.Entities;
//using ProjectManagementApp.Api.Services;
//using ProjectManagementApp.Api.Services.Interfaces;
using Infrastructure.Data.Entities;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using ProjectManagementApp.Api.ViewModels;
using Application.DTOs;

namespace ProjectManagementApp.Api.Controllers
{

    [Route("api/customers/{customerId:int}/[controller]")]
    [ApiController]
    public class ChangeOrdersController : ControllerBase
    {
        private readonly IChangeOrderService _changeOrderService;
        private readonly ICustomerService _customerService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public ChangeOrdersController(IChangeOrderService changeOrderService, ICustomerService customerService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _changeOrderService = changeOrderService;
            _customerService = customerService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ChangeOrderViewModel>> Get(int customerId)
        {
            try
            {
                var changeOrdersDTO = await _changeOrderService.GetAllChangeOrdersByCustomerIdAsync(customerId);
                var changeOrdersViewModel = _mapper.Map<ChangeOrderViewModel[]>(changeOrdersDTO);

                return Ok(changeOrdersViewModel);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{changeOrderId:int}")]
        public async Task<ActionResult<ChangeOrderViewModel>> Get(int customerId, int changeOrderId)
        {
            try
            {
                var changeOrderDTO = await _changeOrderService.GetSingleChangeOrderByCustomerIdAsync(changeOrderId, customerId); 
                var changeOrderViewModel = _mapper.Map<ChangeOrderViewModel>(changeOrderDTO.Value);

                return Ok(changeOrderViewModel);
                
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateChangeOrderViewModel>> Post(int customerId, CreateChangeOrderViewModel createChangeOrderViewModel)
        {
            try
            {
                var createChangeOrderDTO = _mapper.Map<CreateChangeOrderDTO>(createChangeOrderViewModel);

                var newChangeOrderDTO = await _changeOrderService.CreateChangeOrderAsync(customerId, createChangeOrderDTO);

                if (newChangeOrderDTO != null)
                {
                    var newCreateChangeOrderViewModel = _mapper.Map<CreateChangeOrderViewModel>(newChangeOrderDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId, id = newCreateChangeOrderViewModel.ChangeOrderId });

                    return Created(url, newCreateChangeOrderViewModel);
                }
                else
                {
                    return BadRequest("Failed to save new change order");
                }


            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{changeOrderId:int}")]
        public async Task<ActionResult<ChangeOrderViewModel>> Put([FromBody] ChangeOrderViewModel changeOrderViewModel, [FromRoute] int changeOrderId, int customerId)
        {
            try
            {               
                var changeOrderDTO = _mapper.Map<ChangeOrderDTO>(changeOrderViewModel);

                await _changeOrderService.UpdateChangeOrderAsync(changeOrderId, changeOrderDTO);

                var updatedChangeOrderDTO = await _changeOrderService.GetSingleChangeOrderByCustomerIdAsync(changeOrderId, customerId);
                var updatedChangeOrderViewModel = _mapper.Map<ChangeOrderViewModel>(updatedChangeOrderDTO.Value);

                return updatedChangeOrderViewModel;

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpDelete("{changeOrderId:int}")]
        public async Task<IActionResult> Delete(int customerId, int changeOrderId)
        {
            try
            {
                var deleteChangeOrder = await _changeOrderService.DeleteChangeOrderAsync(customerId, changeOrderId);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
    }
}