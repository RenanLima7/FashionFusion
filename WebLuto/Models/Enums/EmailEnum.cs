using System.ComponentModel;

namespace WebLuto.Models.Enums
{
    public enum EmailTemplateType
    {
        [Description("Confirmação de Cadastro")]
        AccountCreation = 0,
        [Description("Exclusão de Conta")]
        AccountDeletion = 1,
        [Description("Alteração de Senha")]
        ChangedPassword = 2
    }
}