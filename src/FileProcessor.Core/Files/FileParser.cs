using CsvHelper;
using CsvHelper.Configuration;
using FileProcessor.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileProcessor.Core.Files
{
    public class FileParser : IFileParser
    {
        public Stream ReadFile(string filePath)
        {
            return File.OpenRead(filePath);
        }

        public IEnumerable<T> Read<T>(Stream fileStream)
        {
            fileStream.Seek(0, SeekOrigin.Begin);

            using (var streamReader = new StreamReader(fileStream))
            using (var csvReader = new CsvReader(streamReader))
            {
                RegisterMaps(csvReader);

                csvReader.Configuration.HasHeaderRecord = true;
                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.TrimOptions = TrimOptions.Trim | TrimOptions.InsideQuotes;
                csvReader.Configuration.ShouldSkipRecord = record => record.All(string.IsNullOrEmpty);
                csvReader.Configuration.ReadingExceptionOccurred = (ex) =>
                {
                    throw ex;
                };

                return csvReader.GetRecords<T>().ToList();
            }
        }

        private void RegisterMaps(CsvReader reader)
        {
            reader.Configuration.RegisterClassMap<LpRecordMap>();
            reader.Configuration.RegisterClassMap<TouRecordMap>();
        }
    }
}
