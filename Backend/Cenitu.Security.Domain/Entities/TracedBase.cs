namespace Cenitu.Security.Domain.Entities
{
    public class TracedBase
    {
        public ApplicationUser CreatedBy { get; set; } = default!;
        public string? CreatedById { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
        public ApplicationUser? LastModifiedBy { get; set; }
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}