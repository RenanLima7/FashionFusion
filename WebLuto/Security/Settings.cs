namespace WebLuto.Security
{
    public class Settings
    {
        private readonly IConfiguration _configuration;

        private readonly IConfigurationBuilder _configurationBuilder;

        private readonly IConfigurationRoot _configurationRoot;

        public Settings() {
            _configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configurationRoot = _configurationBuilder.Build();
            _configuration = _configurationRoot;
        }

        public string SecretKey
        {
            get
            {
                return _configuration.GetValue<string>("SecretKey"); 
            }
        }
    }
}
