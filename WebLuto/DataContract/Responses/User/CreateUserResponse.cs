using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Responses
{
    public sealed class CreateUserResponse
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
