using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AnalysisResult
    {
        public string Summary { get; set; } = string.Empty;
        public List<string> KeySkills { get; set; } = new List<string>();

        public string ExperienceDetails { get; set; } = string.Empty;
    }
}
