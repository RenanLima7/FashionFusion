using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Responses
{
    public sealed class LoginResponse
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public UserType Type { get; set; }
    }
}
