using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Services.Interfaces;
using ProjectManagementApp.Api.ViewModels;
using AutoMapper;
using Application.DTOs;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/customers/{customerId:int}/boards/{boardId:int}/blocks/{blockId:int}/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IBlockService _blockService;
        private readonly ICardService _cardService;
    //    private readonly IMemberService _memberService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public CardsController(IBlockService blockService, ICardService cardService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _blockService = blockService;
            _cardService = cardService;
         //   _memberService = memberService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CardViewModel>> Get(int customerId, int boardId, int blockId, bool includeChecklist = true)
        {
            try
            {
                var cardsDTO = await _cardService.GetAllCardsByBlockIdAsync(blockId, includeChecklist);
                var cardsViewModel = _mapper.Map<CardViewModel[]>(cardsDTO);

                return Ok(cardsViewModel);
            }
            catch (Exception ex)
            {
               return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
             //   return this.StatusCode(StatusCodes.Status500InternalServerError, ex);

            }
        }

        [HttpGet("{cardId:int}")]
        public async Task<ActionResult<CardViewModel>> Get(int customerId, int boardId, int blockId, int cardId, bool includeChecklist = true)
        {
            try
            {
                var cardDTO = await _cardService.GetSingleCardByBlockIdAsync(cardId, blockId, includeChecklist);
                var cardViewModel = _mapper.Map<CardViewModel>(cardDTO.Value);

                return Ok(cardViewModel);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        [HttpPost]
        public async Task<ActionResult<CreateCardViewModel>> Post(int customerId, int boardId, int blockId, CreateCardViewModel createCardViewModel)
        {
            try
            {
                var createCardDTO = _mapper.Map<CreateCardDTO>(createCardViewModel);

                var newCardDTO = await _cardService.CreateCardAsync(boardId, blockId, createCardDTO);

                if (newCardDTO != null)
                {
                    var newCreateCardViewModel = _mapper.Map<CreateCardViewModel>(newCardDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId, boardId, blockId, id = newCreateCardViewModel.CardId });

                    return Created(url, newCreateCardViewModel);
                }
                else
                {
                    return BadRequest("Failed to save new Card");
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }




        [HttpPut("{cardId:int}")]
        public async Task<ActionResult<CardViewModel>> Put([FromBody] CardViewModel cardViewModel, [FromRoute] int cardId, int blockId) //int boardId, int blockId -- do i need this to update the Block in the card service?
        {
            try
            {
                var cardDTO = _mapper.Map<CardDTO>(cardViewModel);

                await _cardService.UpdateCardAsync(cardId, cardDTO);

                var updatedCardDTO = await _cardService.GetSingleCardByBlockIdAsync(cardId, blockId);
                //try  var updatedCardDTO = await _cardService.UpdateCardAsync(cardId, cardDTO);
                var updatedCardViewModel = _mapper.Map<CardViewModel>(updatedCardDTO.Value);

                return updatedCardViewModel;

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
              //  return this.StatusCode(StatusCodes.Status500InternalServerError, ex);

            }

        }

        [HttpDelete("{cardId:int}")]
        public async Task<IActionResult> Delete(int customerId, int boardId, int blockId, int cardId)
        {
            try
            {                
                var deleteCard = await _cardService.DeleteCardAsync(blockId, cardId);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
