using System.ComponentModel.DataAnnotations;

namespace WebLuto.Models
{
    public abstract class BaseEntity
    {
        [Key()]
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeletionDate { get; set; }
    }
}
