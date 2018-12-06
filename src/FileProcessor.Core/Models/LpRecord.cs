using CsvHelper.Configuration;
using System;

namespace FileProcessor.Core.Models
{
    public class LpRecord
    {
        public const string Prefix = "LP";
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime LpDateTime { get; set; }
        public string DataType { get; set; }
        public decimal DataValue { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
    }

    public sealed class LpRecordMap : ClassMap<LpRecord>
    {
        public LpRecordMap()
        {
            Map(x => x.MeterPointCode).Name("MeterPoint Code");
            Map(x => x.SerialNumber).Name("Serial Number");
            Map(x => x.PlantCode).Name("Plant Code");
            Map(x => x.LpDateTime).Name("Date/Time");
            Map(x => x.DataType).Name("Data Type");
            Map(x => x.DataValue).Name("Data Value");
            Map(x => x.Units).Name("Units");
            Map(x => x.Status).Name("Status");
        }
    }
}
