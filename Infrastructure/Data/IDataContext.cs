namespace Infrastructure.Data
{
    public interface IDataContext
    {
        DataState CurrentState { get; set; }

        void SaveState();
    }
}