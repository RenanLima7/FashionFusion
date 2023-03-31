using Microsoft.Extensions.Configuration;

namespace WebLuto.Security
{
    public class Settings
    {
        private readonly IConfiguration _configuration;

        public Settings() { }

        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetKeyValue(string key)
        {
            return _configuration.GetValue<string>(key);
        }
    }
}
