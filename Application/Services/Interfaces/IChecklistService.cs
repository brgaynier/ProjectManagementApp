//using ProjectManagementApp.Api.Data.Entities;
using Infrastructure.Data.Entities;


namespace Application.Services.Interfaces
{
    public interface IChecklistService
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Checklist>> GetAllChecklistByCardIdAsync(int cardId, bool includeMember = false, bool includeItem = false);
        Task<IEnumerable<ChecklistItem>> GetAllChecklistItemsByChecklistIdAsync(int checklistId, bool includeMember = false);
        Task<Checklist?> GetSingleChecklistByCardIdAsync(int checklistId, int cardId, bool includeMember = false, bool includeItem = false);
        Task<ChecklistItem?> GetSingleChecklistItemByChecklistIdAsync(int checklistItemId, int checklistId, bool includeMember = false);
        Task<bool> SaveChangesAsync();
        Task UpdateChecklistAsync(int checklistId, Checklist checklist);
        Task UpdateChecklistItemAsync(int checklistItemId, ChecklistItem checklistItem);
    }
}