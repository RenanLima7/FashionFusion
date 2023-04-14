using WebLuto.Models.Enums;

namespace WebLuto.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Salt { get; set; }
    }
}
