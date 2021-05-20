using IndianStateCensusAnalyser;
using IndianStateCensusAnalyser.DTO;
using NUnit.Framework;
using System.Collections.Generic;
using static IndianStateCensusAnalyser.CensusAnalyser;

namespace CensusAnalyserTest
{
    public class Tests
    {
        static string indianStateCensusHeaders = "State,Population,AreaInSqKm,DensityPerSqKm";
        static string indianStateCodeHeaders = "SrNo,State Name,TIN,StateCode";
        static string wrongIndianStateCodeHeaders = "SrNo,Stttate neame,TIN,StateCode";
        static string indianStateCensusFilePath = @"C:\Users\prash\source\repos\IndianStateCensusAnalyser\CensusAnalyserTest\CSVfiles\IndiaStateCensusData.csv";
        static string wrongIndianStateCensusFilePath = @"C:\Users\prash\source\repos\IndianStateCensusAnalyser\CensusAnalyserTest\CSVfiles\WrongIndiaStateCensusData.csv";
        static string indianStateCodeFilePath = @"C:\Users\prash\source\repos\IndianStateCensusAnalyser\CensusAnalyserTest\CSVfiles\IndiaStateCode.csv";
        static string wrongIndianStateCodeFilePath = @"C:\Users\prash\source\repos\IndianStateCensusAnalyser\CensusAnalyserTest\WrongIndiaStateCode.csv";
     
        CensusAnalyser censusAnalyser;
        System.Collections.Generic.Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();
        }
        [Test]
        public void GivenIndianCensusDataFile_WhenReaded_ShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(indianStateCensusFilePath, Country.INDIA, indianStateCensusHeaders);
            Assert.AreEqual(29, totalRecord.Count);
        }

        [Test]
        public void GivenWrongIndianCensusDataFile_WhenReaded_Should_INCORRECT_HEADER_Exception()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCensusFilePath, Country.INDIA, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, censusException.eType);
        }
        [Test]
        public void GivenWrongIndianCensusDataFile_WhenReaded_ShouldReturn_FILE_NOT_FOUND_Exception()
        {
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, stateException.eType);
        }
        [Test]
        public void GivenWrongIndianCensusCountryName_WhenReaded_ShouldReturn_NO_SUCH_COUNTRY_Exception()
        {
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(indianStateCodeFilePath, Country.BRAZIL, indianStateCensusHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.NO_SUCH_COUNTRY, stateException.eType);
        }
        [Test]
        public void GivenIndianStateCodeDataFile_WhenReaded_ShouldReturnStateCodeDataCount()
        {
            stateRecord = censusAnalyser.LoadCensusData(indianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders);
            Assert.AreEqual(37, stateRecord.Count);
        }
        [Test]
        public void GivenWrongIndianStateCodeDataFile_WhenReaded_ShouldReturn_FILE_NOT_FOUND_Exception()
        {
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(wrongIndianStateCodeFilePath, Country.INDIA, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, stateException.eType);
        }
        [Test]
        public void GivenWrongIndianStateCodeDataHeader_WhenReaded_ShouldReturn_INCORRECT_HEADER_Exception()
        {
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(indianStateCodeFilePath, Country.INDIA, wrongIndianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, stateException.eType);
        }
        [Test]
        public void GivenWrongIndianStateCodeCountryName_WhenReaded_ShouldReturn_NO_SUCH_COUNTRY_Exception()
        {
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(indianStateCodeFilePath, Country.BRAZIL, wrongIndianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.NO_SUCH_COUNTRY, stateException.eType);
        }
    }
}