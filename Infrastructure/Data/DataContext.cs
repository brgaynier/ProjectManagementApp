using Microsoft.Extensions.Options;
using Newtonsoft.Json;
//using ProjectManagementApp.Api.Configuration;

namespace Infrastructure.Data
{
    public class DataContext : IDataContext
    {
        private readonly IOptionsMonitor<Settings> _settings;
        public DataContext(IOptionsMonitor<Settings> settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            CurrentState = new DataState();
            InitDataContext();

        }
        public DataState CurrentState { get; set; }

        private void InitDataContext()
        {
            if (!File.Exists(_settings.CurrentValue.DataContextFileLocation))
            {
                File.Create(_settings.CurrentValue.DataContextFileLocation);
            }

            JsonSerializer? jsonSearlizer = new JsonSerializer();
            using StreamReader streamReader = File.OpenText(_settings.CurrentValue.DataContextFileLocation);
            using JsonReader jsonReader = new JsonTextReader(streamReader);

            CurrentState = jsonSearlizer.Deserialize<DataState>(jsonReader) ?? new DataState();
        }

        public void SaveState()
        {
            if (!File.Exists(_settings.CurrentValue.DataContextFileLocation))
            {
                File.Create(_settings.CurrentValue.DataContextFileLocation);
            }

            JsonSerializer? jsonSearlizer = new JsonSerializer();
            using StreamWriter streramWriter = new StreamWriter(_settings.CurrentValue.DataContextFileLocation);
            using JsonWriter jsonWriter = new JsonTextWriter(streramWriter);
            jsonSearlizer.Serialize(jsonWriter, CurrentState);

        }
    }
}
