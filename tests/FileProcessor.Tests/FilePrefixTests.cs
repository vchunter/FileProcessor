using FileProcessor.Core;
using FileProcessor.Core.Files;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace FileProcessor.Tests
{
    [TestFixture]
    public class FilePrefixTests
    {
        private IFileParser _fileParser;
        private Processor _processor;
        private string _lpFileName;
        private string _touFileName;
        private string _unknownFileName;
        private string _lpFilePrefix;
        private string _touFilePrefix;
        private string _unknownFilePrefix;
        private string _fileName;
        private string _filePrefix;


        [SetUp]
        public void Init()
        {
            _fileParser = Substitute.For<IFileParser>();
            _processor = new Processor(_fileParser);

            _lpFileName = "LP_210095893_20150901T011608049.csv";
            _touFileName = "TOU_212621145_20150911T022358.csv";
            _unknownFileName = "212621145_20150911T022358.csv";

            _lpFilePrefix = "LP";
            _touFilePrefix = "TOU";
            _unknownFilePrefix = "UNKNOWN";
        }

        [Test]
        public void WhenGivenAnLpFileGetLpFilePrefix()
        {
            var spec = this.Given(s => s.AnLpFile())
                .When(s => s.GettingTheFilePrefix())
                .Then(s => s.LpFilePrefixWasReturned());

            spec.BDDfy("Given a file test that file prefix contains LP");
        }

        [Test]
        public void WhenGivenAnTouFileGetTouFilePrefix()
        {
            var spec = this.Given(s => s.ATouFile())
                .When(s => s.GettingTheFilePrefix())
                .Then(s => s.TouFilePrefixWasReturned());

            spec.BDDfy("Given a file test that file prefix contains Tou");
        }

        [Test]
        public void WhenGivenAnUnknownFileGetUnknownFilePrefix()
        {
            var spec = this.Given(s => s.ABadFile())
                .When(s => s.GettingTheFilePrefix())
                .Then(s => s.UnknownFilePrefixWasReturned());

            spec.BDDfy("Given a file test that file prefix is Unknown");
        }

        private void AnLpFile()
        {
            _fileName = _lpFileName;
        }

        private void ATouFile()
        {
            _fileName = _touFileName;
        }

        private void ABadFile()
        {
            _fileName = _unknownFileName;
        }

        private void GettingTheFilePrefix()
        {
            _filePrefix = _processor.GetFilePrefix(_fileName);
        }

        private void LpFilePrefixWasReturned()
        {
            _filePrefix.ShouldBe(_lpFilePrefix);
        }

        private void TouFilePrefixWasReturned()
        {
            _filePrefix.ShouldBe(_touFilePrefix);
        }

        private void UnknownFilePrefixWasReturned()
        {
            _filePrefix.ShouldBe(_unknownFilePrefix);
        }
    }
}
