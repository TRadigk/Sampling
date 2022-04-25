using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sampling.Tests
{
    [TestClass]
    public class DateTesting
    {
        private MeasurementSampler _sut = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new MeasurementSampler();
        }

        [TestMethod]
        public void TestStartMinute()
        {
            var result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2016-05-11T20:02:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(5, result.Minute);

            result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2016-05-11T20:10:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(15, result.Minute);


            result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2016-05-11T20:53:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(55, result.Minute);
        }

        [TestMethod]
        public void StartMinuteHourRollover()
        {
            var result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2016-05-11T20:56:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(0, result.Minute);
            Assert.AreEqual(21, result.Hour);
        }

        [TestMethod]
        public void StartMinuteDayRollover()
        {
            var result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2016-05-11T23:56:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(0, result.Minute);
            Assert.AreEqual(0, result.Hour);
            Assert.AreEqual(12, result.Day);
        }

        [TestMethod]
        public void StartMinuteMonthRollover()
        {
            var result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2016-05-31T23:56:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(0, result.Minute);
            Assert.AreEqual(0, result.Hour);
            Assert.AreEqual(1, result.Day);
            Assert.AreEqual(6, result.Month);
        }

        [TestMethod]
        public void StartMinuteYearRollover()
        {
            var result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2016-12-31T23:56:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(0, result.Minute);
            Assert.AreEqual(0, result.Hour);
            Assert.AreEqual(1, result.Day);
            Assert.AreEqual(1, result.Month);
            Assert.AreEqual(2017, result.Year);
        }

        [TestMethod]
        public void StartSwitchingDate()
        {
            var result = MeasurementSampler.GetSamplingTimeStamp(DateTime.Parse("2024-02-28T23:56:33"), TimeSpan.FromMinutes(5));
            Assert.AreEqual(0, result.Minute);
            Assert.AreEqual(0, result.Hour);
            Assert.AreEqual(29, result.Day);
            Assert.AreEqual(2, result.Month);
            Assert.AreEqual(2024, result.Year);
        }
    }
}