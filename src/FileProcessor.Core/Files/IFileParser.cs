using System.Collections.Generic;
using System.IO;

namespace FileProcessor.Core.Files
{
    public interface IFileParser
    {
        Stream ReadFile(string filePath);
        IEnumerable<T> Read<T>(Stream fileStream);
    }
}