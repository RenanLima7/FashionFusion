using WebLuto.Common;

namespace WebLuto.Models.AbstractModel
{
    public abstract class Person : BaseEntity
    {     
        public string Name { get; set; }

        public string TaxNumber { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public Address? Address { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
