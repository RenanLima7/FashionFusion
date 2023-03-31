using WebLuto.Models.Enums.UserEnum;

namespace WebLuto.Models
{
    public class User 
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Salt { get; set; }

        public UserType Type { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeletionDate { get; set; }
    }
}
