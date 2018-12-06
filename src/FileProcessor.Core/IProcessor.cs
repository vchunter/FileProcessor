using FileProcessor.Core.Models;
using System.Collections.Generic;

namespace FileProcessor.Core
{
    public interface IProcessor
    {
        void ProcessAllFiles();
        IEnumerable<RecordToDisplay> ProcessFile(string filePath);
        IEnumerable<RecordToDisplay> ProcessLpFile(string filePath);
        IEnumerable<RecordToDisplay> ProcessTouFile(string filePath);
        decimal GetMedian(IEnumerable<decimal> input);
        string GetFilePrefix(string filePath);
        string[] GetFiles(string filePath);
    }
}