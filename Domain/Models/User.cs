using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public List<CV> CVs { get; set; } = new List<CV>();
        public List<JobbApplication> JobbApplications { get; set; } = new List<JobbApplication>();
    }
}
