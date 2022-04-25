using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sampling.Enumerations;
using Sampling.Models;

namespace Sampling.Tests
{
    [TestClass]
    public class TestOfSampling
    {
        private readonly List<Measurement> _testDataSet1 = new()
                                                           {
                                                               new Measurement(DateTime.Parse("2017-01-03T10:04:45"),
                                                                               35.79,
                                                                               MeasurementType.Temp),
                                                               new Measurement(DateTime.Parse("2017-01-03T10:01:18"),
                                                                               98.78,
                                                                               MeasurementType.SpO2),
                                                               new Measurement(DateTime.Parse("2017-01-03T10:09:07"),
                                                                               35.01,
                                                                               MeasurementType.Temp),
                                                               new Measurement(DateTime.Parse("2017-01-03T10:03:34"),
                                                                               96.49,
                                                                               MeasurementType.SpO2),
                                                               new Measurement(DateTime.Parse("2017-01-03T10:02:01"),
                                                                               35.82,
                                                                               MeasurementType.Temp),
                                                               new Measurement(DateTime.Parse("2017-01-03T10:05:00"),
                                                                               97.17,
                                                                               MeasurementType.SpO2),
                                                               new Measurement(DateTime.Parse("2017-01-03T10:05:01"),
                                                                               95.08,
                                                                               MeasurementType.SpO2)
                                                           };

        private readonly Dictionary<MeasurementType, List<Measurement>> _expectedResultData1 = new()
            {
                {
                    MeasurementType.Temp, new List<Measurement>
                                          {
                                              new(DateTime.Parse("2017-01-03T10:05:00"), 35.79, MeasurementType.Temp),
                                              new(DateTime.Parse("2017-01-03T10:10:00"), 35.01, MeasurementType.Temp),
                                          }
                },
                {
                    MeasurementType.SpO2, new List<Measurement>
                                          {
                                              new(DateTime.Parse("2017-01-03T10:05:00"), 97.17, MeasurementType.SpO2),
                                              new(DateTime.Parse("2017-01-03T10:10:00"), 95.08, MeasurementType.SpO2)
                                          }
                }
            };


        private MeasurementSampler _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new MeasurementSampler();
        }

        [TestMethod]
        public void TestSampling()
        {
            var result = _sut.Sample(DateTime.Parse("2017-01-03T10:04:44"), _testDataSet1);

            var measurementTypes = Enum.GetValues(typeof(MeasurementType));
            var resultKeys = result.Keys;

            foreach (var resultKey in result.Keys)
            {
                CollectionAssert.AreEqual(_expectedResultData1[resultKey], result[resultKey]);
            }

            CollectionAssert.AreEqual(measurementTypes, resultKeys);
        }

        [TestMethod]
        public void TestSamplingWithDebugOutput()
        {
            var result = _sut.Sample(DateTime.Parse("2017-01-03T10:04:44"), _testDataSet1);

            var measurementTypes = Enum.GetValues(typeof(MeasurementType));
            var resultKeys = result.Keys;
            string inputDataString = String.Join($"{Environment.NewLine}", _testDataSet1);
            Debug.WriteLine("INPUT:");
            Debug.WriteLine(inputDataString);
            foreach (var resultKey in result.Keys)
            {
                CollectionAssert.AreEqual(_expectedResultData1[resultKey], result[resultKey]);
            }

            CollectionAssert.AreEqual(measurementTypes, resultKeys);


            Debug.WriteLine("OUTPUT:");
            foreach (string outputDataString in result.Values.Select(sampledOutput =>
                                                                         String.Join($"{Environment.NewLine}",
                                                                          sampledOutput)))
            {
                Debug.WriteLine(outputDataString);
            }
        }
    }
}