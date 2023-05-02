using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
//using ProjectManagementApp.Api.Data;
//using ProjectManagementApp.Api.Data.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ChecklistService : IChecklistService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ChecklistService> _logger;

        public ChecklistService(ApplicationDbContext dbContext, ILogger<ChecklistService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _dbContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _dbContext.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Checklist>> GetAllChecklistByCardIdAsync(int cardId, bool includeMember = false, bool includeItem = false)
        {
            _logger.LogInformation($"Getting all Checklists");

            IQueryable<Checklist> query = _dbContext.Checklists;

            if (includeMember)
            {
                query = query
                .Include(t => t.Member);
            }

            if (includeItem)
            {
                query = query
                    .Include(t => t.ChecklistItems);
            }

            // Add Query
            query = query
                .Where(t => t.Card.CardId == cardId);

            // Order It
            query = query.OrderBy(c => c.ChecklistId);

            return await query.ToArrayAsync();
            // return query;
        }

        public async Task<Checklist?> GetSingleChecklistByCardIdAsync(int checklistId, int cardId, bool includeMember = false, bool includeItem = false)
        {
            _logger.LogInformation($"Getting Checklist with Id {checklistId}");

            IQueryable<Checklist> query = _dbContext.Checklists;

            if (includeItem)
            {
                query = query
                    .Include(t => t.ChecklistItems);
            }

            query = query
               .Where(t => t.ChecklistId == checklistId && t.Card.CardId == cardId);

            // Query It
            query = query.Where(c => c.ChecklistId == checklistId);


            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateChecklistAsync(int checklistId, Checklist checklist)
        {
            _logger.LogInformation($"Updating checklist {checklistId}");

            var updateChecklist = await _dbContext.Checklists.FindAsync(checklistId);
            if (updateChecklist != null)
            {
                updateChecklist.Title = checklist.Title;
                updateChecklist.DateTime = checklist.DateTime;

                await SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<ChecklistItem>> GetAllChecklistItemsByChecklistIdAsync(int checklistId, bool includeMember = false)
        {
            _logger.LogInformation($"Getting all Checklist Items");

            IQueryable<ChecklistItem> query = _dbContext.ChecklistItems;

            if (includeMember)
            {
                query = query
                .Include(t => t.Member);
            }

            // Add Query
            query = query
                .Where(t => t.Checklist.ChecklistId == checklistId);

            // Order It
            query = query.OrderBy(c => c.Name);

            //   return query;
            return await query.ToArrayAsync();

        }

        public async Task<ChecklistItem?> GetSingleChecklistItemByChecklistIdAsync(int checklistItemId, int checklistId, bool includeMember = false)
        {
            _logger.LogInformation($"Getting a checklist item {checklistId}");

            IQueryable<ChecklistItem> query = _dbContext.ChecklistItems;

            query = query
               .Where(t => t.ChecklistItemId == checklistItemId && t.Checklist.ChecklistId == checklistId);

            // Query It
            query = query.Where(c => c.ChecklistItemId == checklistItemId);


            return await query.FirstOrDefaultAsync();
        }

        public async Task UpdateChecklistItemAsync(int checklistItemId, ChecklistItem checklistItem)
        {
            _logger.LogInformation($"Updating checklist item {checklistItemId}");

            var updateChecklistItem = await _dbContext.ChecklistItems.FindAsync(checklistItemId);
            if (updateChecklistItem != null)
            {
                updateChecklistItem.Name = checklistItem.Name;
                updateChecklistItem.Duration = checklistItem.Duration;
                updateChecklistItem.StartDate = checklistItem.StartDate;
                updateChecklistItem.DueDate = checklistItem.DueDate;

                await SaveChangesAsync();

            }
        }

    }
}
