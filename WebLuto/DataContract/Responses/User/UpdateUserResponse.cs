using WebLuto.Models.Enums;

namespace WebLuto.DataContract.Responses
{
    public sealed class UpdateUserResponse
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public UserType Type { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
