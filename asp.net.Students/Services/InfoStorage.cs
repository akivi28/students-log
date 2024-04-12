using asp.net.Students.Models;
using System.Text.Json;

namespace asp.net.Students.Services
{
    public class InfoStorage
    {
        public List<Info> Infos { get; set; }

        private readonly string _fileName = "infos.json";

        public InfoStorage()
        {
            Infos = new List<Info>();
            Load();
        }

        private void Load()
        {
            try
            {
                Infos = JsonSerializer.Deserialize<List<Info>>(File.ReadAllText(_fileName));
            }
            catch { }
        }

        public async Task Save()
        {
            await File.WriteAllTextAsync(_fileName,JsonSerializer.Serialize(Infos));
        }
    }
}
