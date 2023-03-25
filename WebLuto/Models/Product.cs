using FashionFusion.Common;

namespace FashionFusion.Models
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

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
    }
}
