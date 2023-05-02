using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Api.ViewModels;
using Infrastructure.Data.Entities;
using Application.Services.Interfaces;
using Application.DTOs;
using Application.Services;

namespace ProjectManagementApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public MembersController(IMemberService memberService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _memberService = memberService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<MemberViewModel>> Get()
        {
            try
            {
                var membersDTO = await _memberService.GetAllMembersAsync(); // bool include Customers?
                var membersViewModel = _mapper.Map<MemberViewModel[]>(membersDTO);

                return Ok(membersViewModel);

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("{memberId:int}")]
        public async Task<ActionResult<MemberViewModel>> Get(int memberId)  //TODO includeChecklistItems**
        {
            try
            {
                var memberDTO = await _memberService.GetSingleMemberAsync(memberId);
                var memberViewModel = _mapper.Map<MemberViewModel>(memberDTO.Value);

                return Ok(memberViewModel);

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateMemberViewModel>> Post(CreateMemberViewModel createMemberViewModel)
        {
            try
            {
                var createMemberDTO = _mapper.Map<CreateMemberDTO>(createMemberViewModel);
                var newMemberDTO = await _memberService.CreateMemberAsync(createMemberDTO);

                if (newMemberDTO != null)
                {
                    var newCreateMemberViewModel = _mapper.Map<CreateMemberViewModel>(newMemberDTO.Value);

                    var url = _linkGenerator.GetPathByAction(HttpContext,
                        "Get",
                        values: new { customerId = newCreateMemberViewModel.MemberId });

                    return Created(url, newCreateMemberViewModel);


                }

                else
                {
                    return BadRequest("Failed to save new Member");
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpPut("{memberId:int}")]
        public async Task<ActionResult<MemberViewModel>> Put([FromBody] MemberViewModel memberViewModel, [FromRoute] int memberId)
        {
            try
            {
                var memberDTO = _mapper.Map<MemberDTO>(memberViewModel);

                await _memberService.UpdateMemberAsync(memberId, memberDTO);

                var updatedMemberDTO = await _memberService.GetSingleMemberAsync(memberId);
                var updatedMemberViewModel = _mapper.Map<MemberViewModel>(updatedMemberDTO.Value);

                return updatedMemberViewModel;


            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpDelete("{memberId:int}")]
        public async Task<IActionResult> Delete(int memberId)
        {
            try
            {
                var deleteCustomer = await _memberService.DeleteMemberAsync(memberId);
                return Ok();

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }
    }
}
