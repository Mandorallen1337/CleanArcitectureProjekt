namespace Domain.Models
{
    public class CV
    {
        public Guid Id { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }

        public Guid UserId { get; set; }

        public CV() { }

        public CV(string fileUrl, DateTime uploadDate, Guid userId)
        {
            FileUrl = fileUrl;
            UploadDate = uploadDate;
            UserId = userId;
        }
    }
}
