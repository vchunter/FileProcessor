using System;
using System.IO;

namespace FileProcessor.Core.Models
{
    public class RecordToDisplay
    {
        public RecordToDisplay(string filePath, decimal median)
        {
            FileName = Path.GetFileName(filePath);
            MedianValue = median;
        }

        public string FileName { get; set; }
        public DateTime RecordDate { get; set; }
        public decimal RecordValue { get; set; }
        public decimal MedianValue { get; set; }
        public bool OutOfMedianRange()
        {
            var medianPercentDiff = (0.2m * MedianValue);

            var medianMax = MedianValue + medianPercentDiff;
            var medianMin = MedianValue - medianPercentDiff;

            return RecordValue > medianMax || RecordValue < medianMin;
        }
    }
}
