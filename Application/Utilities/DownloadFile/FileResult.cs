namespace Application.Utilities.DownloadFile
{
    //Represents the object of the file operation, contains the file as a stream and the file URL
    public class FileResult
    {
        public required Stream Content { get; set; }
        public string FileUrl { get; set; } = string.Empty;
    }
}
