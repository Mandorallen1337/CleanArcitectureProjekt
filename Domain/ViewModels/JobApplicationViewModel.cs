namespace Domain.ViewModels
{
    public class JobApplicationViewModel
    {
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public bool Status { get; set; }
    }
}
