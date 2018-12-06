using System;
using FileProcessor.Core.Files;
using FileProcessor.Core.Models;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FileProcessor.Core
{
    public class Processor : IProcessor
    {
        private readonly IFileParser _fileParser;

        public Processor(IFileParser fileParser)
        {
            _fileParser = fileParser;
        }

        public void ProcessAllFiles()
        {
            var filePath = ConfigurationManager.AppSettings["FileLocation"];

            var fileList = GetFiles(filePath);

            foreach (var file in fileList)
            {
                var data = ProcessFile(file);
                
                var dataToPrint = data.Where(d => d.OutOfMedianRange());

                foreach (var record in dataToPrint)
                {
                    Console.WriteLine($"{record.FileName} {record.RecordDate} {record.RecordValue} {record.MedianValue}");
                }
            }
        }

        public IEnumerable<RecordToDisplay> ProcessFile(string filePath)
        {
            var filePrefix = GetFilePrefix(filePath);
            
            if (filePrefix == LpRecord.Prefix)
                return ProcessLpFile(filePath);

            if (filePrefix == TouRecord.Prefix)
                return ProcessTouFile(filePath);

            throw new InvalidDataException($"Unable to determine file type from FileName {Path.GetFileName(filePath)}");
        }

        public IEnumerable<RecordToDisplay> ProcessLpFile(string filePath)
        {
            IEnumerable<LpRecord> records;
            using (var fileStream = _fileParser.ReadFile(filePath))
                records = _fileParser.Read<LpRecord>(fileStream).ToList();

            var median = GetMedian(records.Select(r => r.DataValue));

            var displayRecords = records
                .Select(r => new RecordToDisplay(filePath, median)
                {
                    RecordDate = r.LpDateTime,
                    RecordValue = r.DataValue,
                })
                .ToList();

            return displayRecords;
        }

        public IEnumerable<RecordToDisplay> ProcessTouFile(string filePath)
        {
            IEnumerable<TouRecord> records;
            using (var fileStream = _fileParser.ReadFile(filePath))
                records = _fileParser.Read<TouRecord>(fileStream).ToList();

            var median = GetMedian(records.Select(r => r.Energy));

            var displayRecords = records
                .Select(r => new RecordToDisplay(filePath, median)
                {
                    RecordDate = r.TouDateTime,
                    RecordValue = r.Energy,
                })
                .ToList();

            return displayRecords;
        }

        public decimal GetMedian(IEnumerable<decimal> input)
        {
            var count = input.Count();

            if (count == 0)
                return count;

            input = input.OrderBy(i => i);

            int midpoint = count / 2;
            if (count % 2 == 0)
                return (input.ElementAt(midpoint - 1) + input.ElementAt(midpoint)) / 2.0m;
            else
                return input.ElementAt(midpoint);
        }

        public string GetFilePrefix(string filePath)
        {
            var fileName = Path.GetFileName(filePath);

            if (Regex.IsMatch(fileName, LpRecord.Prefix, RegexOptions.IgnoreCase))
                return LpRecord.Prefix;

            if (Regex.IsMatch(fileName, TouRecord.Prefix, RegexOptions.IgnoreCase))
                return TouRecord.Prefix;

            return "UNKNOWN";
        }

        public string[] GetFiles(string filePath)
        {
            return Directory
                .GetFiles(filePath);
        }
    }
}
