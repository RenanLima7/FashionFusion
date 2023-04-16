using System.ComponentModel;

namespace WebLuto.Models.Enums
{
    public enum PaymentType
    {
        [Description("Dinheiro")]
        Money = 0,
        [Description("Boleto Bancário")]
        BankSlip = 1,
        [Description("PIX")]
        PIX = 2,
        [Description("Cartão De Crédito")]
        CreditCard = 3,
        [Description("Cartão De Débito")]
        DebitCard = 4
    }
}
