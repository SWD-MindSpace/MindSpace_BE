using MindSpace.Application.Commons.Utilities;
using System.Text;

namespace MindSpace.Infrastructure.Seeders
{
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Reading File Asynchronously
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> ReadFileAsync(string filePath)
        {
            try
            {
                // Check if file exists before proceeding
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found.", filePath);
                }

                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var readerStream = new StreamReader(fileStream, Encoding.UTF8);

                return await readerStream.ReadToEndAsync();
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception specifically
                throw new Exception(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Handle permission-related issues
                throw new Exception($"Access to the file at {filePath} is denied.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while reading the file.", ex);
            }
        }
    }
}