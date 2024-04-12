using asp.net.Students.Models;
using System.Text.Json;

namespace asp.net.Students.Services
{
    public class MapInfoStorage
    {
        public List<MapInfo> Infos { get; set; }

        private readonly string _fileName = "MapInfo.json";

        public MapInfoStorage()
        {
            Infos = new List<MapInfo>();
            Load();
        }

        private void Load()
        {
            try
            {
                Infos = JsonSerializer.Deserialize<List<MapInfo>>(File.ReadAllText(_fileName));
            }
            catch { }
        }

        public async Task Save()
        {
            await File.WriteAllTextAsync(_fileName, JsonSerializer.Serialize(Infos));
        }
    }
}
