namespace WebLuto.Common
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeletionDate { get; set; }
    }
}
