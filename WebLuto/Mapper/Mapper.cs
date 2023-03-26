using WebLuto.Models;
using WebLuto.Models.DTO;

namespace WebLuto.Mapper
{
    public static class Mapper
    {
        public static User MapUserDTOToUser(UserDTO userDTO)
        {
            return new User();
        }
    }
}
