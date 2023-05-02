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
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MemberService> _logger;
        private readonly IMapper _mapper;

        public MemberService(ApplicationDbContext dbContext, ILogger<MemberService> logger, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<MemberDTO>> GetAllMembersAsync() //include customers?
        {
            _logger.LogInformation($"Getting all Members");

            IQueryable<Member> query = _dbContext.Members;
            // .Include(c => c.Location);


            //if (includeCustomers)
            //{
            //    query = query
            //        .Include(w => w.Customers)
            //        .ThenInclude(a => a.ChangeOrders)
            //        .ThenInclude(c => c.Boards)
            //        .ThenInclude(m => m.WorkFlows)
            //}

            var memberDTOs = await query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToArrayAsync();
            return memberDTOs;
        }

        public async Task<Response<MemberDTO?>> GetSingleMemberAsync(int memberId)
        {
            _logger.LogInformation($"Getting a Member with Id number {memberId}");

            IQueryable<Member> query = _dbContext.Members;
            //.Include(c => c.Boards);

            if (query == null)
            {
                return new Response<MemberDTO?> { Value = null, Status = ResponseStatus.BadRequest, ErrorMessages = new List<ResponseMessage> { new ResponseMessage { DisplayText = "Member does not exist" } } };

            }

            //if (includeCustomers)
            //{
            //    query = query
            //        .Include(w => w.Customers)
            //        .ThenInclude(a => a.ChangeOrders)
            //        .ThenInclude(c => c.Boards)
            //        .ThenInclude(m => m.WorkFlows)
            //}

            var memberDTO = await query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return new Response<MemberDTO?> { Value = memberDTO, Status = ResponseStatus.Ok };
        }

        public async Task<Response<CreateMemberDTO?>> CreateMemberAsync(CreateMemberDTO createMemberDTO)
        {
            
            var newMember = _mapper.Map<Member>(createMemberDTO);
            //newCard.BlockId = blockId;

            await _dbContext.Members.AddAsync(newMember);
            await _dbContext.SaveChangesAsync();

            var memberDTO = _mapper.Map<CreateMemberDTO>(newMember);

            return new Response<CreateMemberDTO?> { Value = memberDTO, Status = ResponseStatus.Ok };

        }

        public async Task UpdateMemberAsync(int memberId, MemberDTO memberDTO)
        {
            _logger.LogInformation($"Updating Member {memberId}");

            var updateMember = await _dbContext.Members.FindAsync(memberId);
            if (updateMember != null)
            {
                updateMember.FirstName = memberDTO.FirstName;
                updateMember.LastName = memberDTO.LastName;
                updateMember.Email = memberDTO.Email;
                updateMember.UserName = memberDTO.UserName;
                updateMember.Bio = memberDTO.Bio;

                _mapper.Map(updateMember, memberDTO);

                await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<MemberDTO?> DeleteMemberAsync(int memberId)
        {
            var member = await _dbContext.Members.FindAsync(memberId);

            if (member != null)
            {
                _dbContext.Members.Remove(member);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            var deletedMemberDTO = _mapper.Map<MemberDTO>(member);
            return deletedMemberDTO;

        }
    }
}
