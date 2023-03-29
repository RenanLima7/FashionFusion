using System.ComponentModel;

namespace WebLuto.Models.Enums.UserEnum
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin = 0,
        [Description("Funcionário")]
        Employee = 1,
        [Description("Cliente")]
        Client = 2
    }
}
