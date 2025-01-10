namespace Domain.Models
{
    public class JobbApplication
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public bool Status { get; set; }

        public Guid UserId { get; set; }
    }
}
