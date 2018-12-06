using CsvHelper.Configuration;
using System;

namespace FileProcessor.Core.Models
{
    public class TouRecord
    {
        public const string Prefix = "TOU";
        public string MeterPointCode { get; set; }
        public string SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime TouDateTime { get; set; }
        public string DataType { get; set; }
        public decimal Energy { get; set; }
        public decimal MaximumDemand { get; set; }
        public DateTime TimeOfMaxDemand { get; set; }
        public string Units { get; set; }
        public string Status { get; set; }
        public string Period { get; set; }
        public bool DlsActive { get; set; }
        public string BillingResetCount { get; set; }
        public DateTime BillingResetDateTime { get; set; }
        public string Rate { get; set; }
    }

    public sealed class TouRecordMap : ClassMap<TouRecord>
    {
        public TouRecordMap()
        {
            Map(x => x.MeterPointCode).Name("MeterPoint Code");
            Map(x => x.SerialNumber).Name("Serial Number");
            Map(x => x.PlantCode).Name("Plant Code");
            Map(x => x.TouDateTime).Name("Date/Time");
            Map(x => x.DataType).Name("Data Type");
            Map(x => x.Energy).Name("Energy");
            Map(x => x.MaximumDemand).Name("Maximum Demand");
            Map(x => x.TimeOfMaxDemand).Name("Time of Max Demand");
            Map(x => x.Units).Name("Units");
            Map(x => x.Status).Name("Status");
            Map(x => x.Period).Name("Period");
            Map(x => x.DlsActive).Name("DLS Active");
            Map(x => x.BillingResetCount).Name("Billing Reset Count");
            Map(x => x.BillingResetDateTime).Name("Billing Reset Date/Time");
            Map(x => x.Rate).Name("Rate");
        }
    }
}
