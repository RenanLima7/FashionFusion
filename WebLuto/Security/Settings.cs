namespace WebLuto.Security
{
    public class Settings
    {
        private readonly IConfiguration _configuration;

        private readonly IConfigurationBuilder _configurationBuilder;

        private readonly IConfigurationRoot _configurationRoot;

        public Settings()
        {
            _configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configurationRoot = _configurationBuilder.Build();
            _configuration = _configurationRoot;
        }

        public string DataBase
        {
            get
            {
                return _configuration.GetConnectionString("DATABASE_URL");
            }
        }

        public string SecretKey
        {
            get
            {
                return _configuration.GetValue<string>("SecretKey");
            }
        }

        public string EmailContact
        {
            get
            {
                return _configuration.GetValue<string>("EmailContact");
            }
        }

        public string EmailPassword
        {
            get
            {
                return _configuration.GetValue<string>("EmailPassword");
            }
        }

        public string DefaultUrlApi
        {
            get
            {
                return _configuration.GetValue<string>("DefaultUrlApi");
            }
        }
    }
}
