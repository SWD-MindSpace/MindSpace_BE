namespace MindSpace.Application.Commons.Utilities
{
    public interface IFileReader
    {
        Task<string> ReadFileAsync(string filePath);
    }
}
