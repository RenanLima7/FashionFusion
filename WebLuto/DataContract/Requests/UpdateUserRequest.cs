using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebLuto.Models.Enums.UserEnum;

namespace WebLuto.DataContract.Requests
{
    public sealed class UpdateUserRequest
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public UserType? Type { get; set; }
    }
}
