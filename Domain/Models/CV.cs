namespace Domain.Models
{
    public class CV
    {
        public Guid Id { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }

        public Guid UserId { get; set; }
    }
}
