using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Text.Json;
using Infrastructure.Data.Entities;
using Application.Services;
using Application.Services.Interfaces;
using ProjectManagementApp.Api.ViewModels;
using Application.DTOs;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/customers/{customerId:int}/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly ICustomerService _customerService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public BoardsController(IBoardService boardService, ICustomerService customerService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _boardService = boardService;
            _customerService = customerService;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }


        [HttpGet]
        public async Task<ActionResult<BoardViewModel>> Get(int customerId, bool includeBlocks= true)
        {
            try
            {
                var boardsDTO = await _boardService.GetAllBoardsByCustomerIdAsync(customerId, includeBlocks);
                var boardsViewModel = _mapper.Map<BoardViewModel[]>(boardsDTO);

                return Ok(boardsViewModel);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpGet("{boardId:int}")]
        public async Task<ActionResult<BoardViewModel>> Get(int customerId, int boardId, bool includeBlocks = true)
        {
            try
            {
                var boardDTO = await _boardService.GetSingleBoardByCustomerIdAsync(boardId, customerId, includeBlocks); //includeBlocks
                var boardViewModel = _mapper.Map<BoardViewModel>(boardDTO.Value);


                return Ok(boardViewModel);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("search")]  //TODO
        public async Task<ActionResult<BoardViewModel>> SearchByDate(DateTime dueDate, bool includeBlocks = false)  
            // TODO  MAYBE SEARCH BY A DIFFERENT FIELD?
        {
            try
            {
                var results = await _boardService.GetAllBoardsByDueDate(dueDate, includeBlocks);
                var boardsViewModel = _mapper.Map<BoardViewModel[]>(results);

                if (!results.Any()) return NotFound();

                return Ok(results);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateBoardViewModel>> Post(int customerId, CreateBoardViewModel createBoardViewModel)
        {
            try
            {

                var createBoardDTO = _mapper.Map<CreateBoardDTO>(createBoardViewModel);
                var newBoardDTO = await _boardService.CreateBoardAsync(customerId, createBoardDTO);

                if (newBoardDTO != null)
                {
                    var newCreateBoardViewModel = _mapper.Map<CreateBoardViewModel>(newBoardDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId, id = newCreateBoardViewModel.BoardId });

                    return Created(url, newCreateBoardViewModel);
                }
                else
                {
                    return BadRequest("Failed to save new Board");
                }
                               
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{boardId:int}")]
        public async Task<ActionResult<BoardViewModel>> Put([FromBody] BoardViewModel boardViewModel, [FromRoute] int boardId, int customerId) 
        {
            try
            {
                var boardDTO = _mapper.Map<BoardDTO>(boardViewModel);

                await _boardService.UpdateBoardAsync(boardId, boardDTO);

                var updatedBoardDTO = await _boardService.GetSingleBoardByCustomerIdAsync(boardId, customerId);
                var updatedBoardViewModel = _mapper.Map<BoardViewModel>(updatedBoardDTO.Value);

                return updatedBoardViewModel;

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpDelete("{boardId:int}")]
        public async Task<IActionResult> Delete (int customerId, int boardId)
        {
            try
            {
                var deleteBoard = await _boardService.DeleteBoardAsync(customerId, boardId);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
    }
}
