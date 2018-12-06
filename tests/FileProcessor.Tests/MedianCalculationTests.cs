using System.Collections.Generic;
using System.Linq;
using FileProcessor.Core;
using FileProcessor.Core.Files;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace FileProcessor.Tests
{
    [TestFixture]
    public class MedianCalcuationTests
    {
        private IFileParser _fileParser;
        private Processor _processor;

        private List<decimal> _rangeOfValues;
        private decimal _medianValue;

        private List<decimal> _someOddValues;
        private List<decimal> _someEvenValues;
        private List<decimal> _noValues;
        private List<decimal> _allZeroValues;

        private decimal _someOddValueResults;
        private decimal _someEvenValueResults;
        private decimal _noValueResults;
        private decimal _allZeroValueResults;

        [SetUp]
        public void Init()
        {
            _fileParser = Substitute.For<IFileParser>();
            _processor = new Processor(_fileParser);

            _someOddValues = new [] { 0.1m, 0.2m, 0.3m }.ToList();
            _someEvenValues = new[] { 0.1m, 0.2m, 0.3m, 0.4m }.ToList();
            _noValues = new List<decimal>();
            _allZeroValues = new[] { 0.0m, 0.0m, 0.0m }.ToList();

            _someOddValueResults = 0.2m;
            _someEvenValueResults = 0.25m;
            _noValueResults = 0m;
            _allZeroValueResults = 0m;
        }

        [Test]
        public void WhenGivenAnOddRangeOfValuesToDetermineTheMedian()
        {
            var spec = this.Given(s => s.AnOddRangeOfValues())
                .When(s => s.CalculateTheMedianValue())
                .Then(s => s.MedianForOddRangeWasReturned());

            spec.BDDfy("Given a range of odd values return a median of 0.2");
        }

        [Test]
        public void WhenGivenAnEvenRangeOfValuesToDetermineTheMedian()
        {
            var spec = this.Given(s => s.AnEvenRangeOfValues())
                .When(s => s.CalculateTheMedianValue())
                .Then(s => s.MedianForEvenRangeWasReturned());

            spec.BDDfy("Given a range of even values return a median of 0.25");
        }

        [Test]
        public void WhenGivenNoValuesToDetermineTheMedian()
        {
            var spec = this.Given(s => s.AnEmptyRangeOfValues())
                .When(s => s.CalculateTheMedianValue())
                .Then(s => s.MedianForNoValueRangeWasReturned());

            spec.BDDfy("Given a range of no values return a median of 0");
        }

        [Test]
        public void WhenGivenARangeOfZeroValuesToDetermineTheMedian()
        {
            var spec = this.Given(s => s.ARangeOfZeroValues())
                .When(s => s.CalculateTheMedianValue())
                .Then(s => s.MedianForAllZeroRangeWasReturned());

            spec.BDDfy("Given a range of zero values return a median of 0");
        }

        private void AnEvenRangeOfValues()
        {
            _rangeOfValues = _someEvenValues;
        }

        private void AnOddRangeOfValues()
        {
            _rangeOfValues = _someOddValues;
        }

        private void AnEmptyRangeOfValues()
        {
            _rangeOfValues = _noValues;
        }

        private void ARangeOfZeroValues()
        {
            _rangeOfValues = _allZeroValues;
        }

        private void CalculateTheMedianValue()
        {
            _medianValue = _processor.GetMedian(_rangeOfValues);
        }

        private void MedianForOddRangeWasReturned()
        {
            _medianValue.ShouldBe(_someOddValueResults);
        }

        private void MedianForEvenRangeWasReturned()
        {
            _medianValue.ShouldBe(_someEvenValueResults);
        }

        private void MedianForNoValueRangeWasReturned()
        {
            _medianValue.ShouldBe(_noValueResults);
        }

        private void MedianForAllZeroRangeWasReturned()
        {
            _medianValue.ShouldBe(_allZeroValueResults);
        }
    }
}
