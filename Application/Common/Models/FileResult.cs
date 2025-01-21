namespace Application.Common.Models
{
    public class FileResult
    {
        public required byte[] Content { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
