﻿namespace MindSpace.Application.Commons.Interfaces.Utilities
{
    public interface IFileReader
    {
        Task<string> ReadFileAsync(string filePath);
    }
}
