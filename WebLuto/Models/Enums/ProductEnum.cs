using System.ComponentModel;

namespace WebLuto.Models.Enums.ProductEnum
{
    public enum ProductType
    {
        [Description("Urna Funerária")]
        FuneralUrn = 0,
        [Description("Urna De Cremação")]
        CremationUrn = 1,
        [Description("Coroa De Flores")]
        FlowerWreath = 2,
        [Description("Livro De Memórias")]
        BookMemories = 3
    }
}
