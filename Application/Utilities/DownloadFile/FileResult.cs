namespace Application.Utilities.DownloadFile
{
    public class FileResult
    {
        public required byte[] Content { get; set; }
        public string FileName { get; set; } = string.Empty;
    }
}
