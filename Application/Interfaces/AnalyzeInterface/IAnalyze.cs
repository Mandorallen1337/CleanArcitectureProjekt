namespace Application.Interfaces.AnalyzeInterface
{
    public interface IAnalyze
    {
        Task<string> AnalyzeCVAsync(string fileContent);
    }
}
