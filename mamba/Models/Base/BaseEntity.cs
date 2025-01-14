namespace mamba.Models.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool SoftDeleted { get; set; }
    }
}
