//using ProjectManagementApp.Api.Data.Entities;
using Application.DTOs;
using Infrastructure.Data.Entities;
using Core;


namespace Application.Services.Interfaces
{
    public interface ICardService
    {
      //  void Add<T>(T entity) where T : class;
      //  void Delete<T>(T entity) where T : class;
        //Task<IEnumerable<Card>> GetAllCardsByBlockIdAsync(int blockId, bool includeChecklist = false);
        Task<Response<CardDTO?>> GetSingleCardByBlockIdAsync(int cardId, int blockId, bool includeChecklist = false);
     //   Task<CardDTO?> GetSingleCardByBlockIdAsync(int cardId, int blockId, bool includeChecklist = false);

      //  Task<bool> SaveChangesAsync();
        Task UpdateCardAsync(int cardId, CardDTO cardDTO);
        // Task<Card?> CreateCardAsync(int boardId, int blockId, Card newCard);
        Task<Response<CreateCardDTO?>> CreateCardAsync(int boardId, int blockId, CreateCardDTO createCardDTO);

        Task<CardDTO?> DeleteCardAsync(int blockId, int cardId);
        Task<IEnumerable<CardDTO>> GetAllCardsByBlockIdAsync(int blockId, bool includeChecklist = false);





    }
}