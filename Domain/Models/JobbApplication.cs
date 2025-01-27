namespace Domain.Models
{
    public class JobbApplicationViewModel
    {
        public Guid Id { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public bool Status { get; set; }

        public Guid UserId { get; set; }

        public JobbApplicationViewModel() { }

        public JobbApplicationViewModel(string jobTitle, string companyName, DateTime applicationDate)
        {
            JobTitle = jobTitle;
            CompanyName = companyName;
            ApplicationDate = applicationDate;            
            UserId = Guid.NewGuid();
        }

    }
    
}
