using WebLuto.Common;
using WebLuto.Models.Enums.ProductEnum;

namespace WebLuto.Models
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }

        

        #region Add Properties
        /*
            Os produtos terão ao menos:
            • nome;
            • preço;
            • marca;
            • quantidade em estoque;
            • dia e hora da venda de cada produto.
            Conforme o tipo de produto escolhido, deve-se acrescentar ao menos três outras informações ao
            produto como cor, peso, dimensões, classificação, etc.
         */
        #endregion
        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductType Type { get; set; }

        public bool? Solt { get; set; }
    }
}
