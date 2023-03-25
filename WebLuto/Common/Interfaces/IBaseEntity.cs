namespace WebLuto.Common.Interfaces
{
    public interface IBaseEntity
    {
        public long Id { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime? DeletionDate { get; set; }

        public long? CreateUserId { get; set; }

        public long? UpdateUserId { get; set; }
    }
}
