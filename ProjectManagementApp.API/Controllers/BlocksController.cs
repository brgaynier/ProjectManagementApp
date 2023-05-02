using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
//using ProjectManagementApp.Api.Data;
//using ProjectManagementApp.Api.Data.Entities;
//using ProjectManagementApp.Api.Services.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Application.Services.Interfaces;
using AutoMapper;
using ProjectManagementApp.Api.ViewModels;
using Application.DTOs;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/customers/{customerId:int}/boards/{boardId:int}/[controller]")]
    [ApiController]
    public class BlocksController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly IBlockService _blockService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public BlocksController(IBoardService boardService, IBlockService blockService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _boardService = boardService;
            _blockService = blockService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<BlockViewModel>> Get(int customerId, int boardId, bool includeCards = true)
        {
            try
            {

                var blocksDTO = await _blockService.GetAllBlocksByBoardIdAsync(boardId, includeCards);
                var blocksViewModel = _mapper.Map<BlockViewModel[]>(blocksDTO);

                return Ok(blocksViewModel);


                //var blocks = await _blockService.GetAllBlocksByBoardIdAsync(boardId, includeCards);
                //return Ok(blocks);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{blockId:int}")]
        public async Task<ActionResult<BlockViewModel>> Get(int customerId, int blockId, int boardId, bool includeCards = true)
        {
            try
            {
                var blockDTO = await _blockService.GetSingleBlockByBoardIdAsync(boardId, blockId, includeCards);
                var blockViewModel = _mapper.Map<BlockViewModel>(blockDTO.Value);

                return Ok(blockViewModel);


            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpPost]
        public async Task<ActionResult<CreateBlockViewModel>> Post(int customerId, int boardId, CreateBlockViewModel createBlockViewModel)
        {
            try
            {
                var createBlockDTO = _mapper.Map<CreateBlockDTO>(createBlockViewModel);

                var newBlockDTO = await _blockService.CreateBlockAsync(customerId, boardId, createBlockDTO);

                if (newBlockDTO != null)
                {
                    var newCreateBlockViewModel = _mapper.Map<CreateBlockViewModel>(newBlockDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId, boardId, id = newCreateBlockViewModel.BlockId });

                    return Created(url, newCreateBlockViewModel);
                }
                else
                {
                    return BadRequest("Failed to save new Block");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpPut("{blockId:int}")]
        public async Task<ActionResult<BlockViewModel>> Put([FromBody] BlockViewModel blockViewModel, [FromRoute] int blockId, int boardId) 
        {
            try
            {
                var blockDTO = _mapper.Map<BlockDTO>(blockViewModel);

                await _blockService.UpdateBlockAsync(blockId, blockDTO);

                var updatedBlockDTO = await _blockService.GetSingleBlockByBoardIdAsync(boardId, blockId);
                var updatedBlockViewModel = _mapper.Map<BlockViewModel>(updatedBlockDTO.Value);

                return updatedBlockViewModel;

            }
            catch (Exception ex)
            {
                //  return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);

            }

        }

        [HttpDelete("{blockId:int}")]
        public async Task<IActionResult> Delete(int customerId, int boardId, int blockId)
        {
            try
            {
                var deleteBlock = await _blockService.DeleteBlockAsync(boardId, blockId);
                return Ok();

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
