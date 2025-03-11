namespace Cenitu.Security.Dtos
{
    public class TracedBaseDto
    {
        public string? CreatedByUserName { get; set; } = default!;
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedByUserName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}