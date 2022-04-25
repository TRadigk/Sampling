using System;
using System.Collections.Generic;
using Sampling.Enumerations;
using Sampling.Models;

namespace Sampling.Tests;

internal class TestData
{
    public static DateTime NewTestData1StartTimeStamp = DateTime.Parse("2017-01-03T01:05:00");

    public static readonly List<Measurement> NewTestData1 = new()
                                                            {
                                                                new Measurement(DateTime.Parse("2016-01-03T10:04:45"),
                                                                                35.79,
                                                                                MeasurementType.Temp),
                                                                new Measurement(DateTime.Parse("2017-01-03T10:09:07"),
                                                                                35.01,
                                                                                MeasurementType.Temp),
                                                                new Measurement(DateTime.Parse("2017-01-03T10:01:18"),
                                                                                98.78,
                                                                                MeasurementType.SpO2),
                                                                new Measurement(DateTime.Parse("2017-01-03T10:02:01"),
                                                                                35.82,
                                                                                MeasurementType.Temp),
                                                                new Measurement(DateTime.Parse("2017-01-03T10:05:00"),
                                                                                97.17,
                                                                                MeasurementType.SpO2),
                                                                new Measurement(DateTime.Parse("2016-01-03T10:05:01"),
                                                                                95.08,
                                                                                MeasurementType.SpO2),
                                                                new Measurement(DateTime.Parse("2017-03-05T06:45:12"),
                                                                                91.01,
                                                                                MeasurementType.SpO2),
                                                                new Measurement(DateTime.Parse("2017-01-03T10:03:34"),
                                                                                96.49,
                                                                                MeasurementType.SpO2),
                                                                new Measurement(DateTime.Parse("2017-01-02T10:05:06"),
                                                                                98.16,
                                                                                MeasurementType.SpO2),
                                                                new Measurement(DateTime.Parse("2017-01-04T04:16:27"),
                                                                                36.12,
                                                                                MeasurementType.Temp),
                                                                new Measurement(DateTime.Parse("2017-01-03T03:53:01"),
                                                                                34.98,
                                                                                MeasurementType.Temp),
                                                                new Measurement(DateTime.Parse("2017-01-03T16:37:31"),
                                                                                89.11,
                                                                                MeasurementType.SpO2)
                                                            };

    public static readonly Dictionary<MeasurementType, List<Measurement>> ExpectedNewTestData1Result = new()
        {
            {
                MeasurementType.Temp, new List<Measurement>
                                      {
                                          new(DateTime.Parse("2017-01-03T03:55:00"), 34.98, MeasurementType.Temp),
                                          new(DateTime.Parse("2017-01-03T10:05:00"), 35.82, MeasurementType.Temp),
                                          new(DateTime.Parse("2017-01-03T10:10:00"), 35.01, MeasurementType.Temp),
                                          new(DateTime.Parse("2017-01-04T04:20:00"), 36.12, MeasurementType.Temp),
                                      }
            },
            {
                MeasurementType.SpO2, new List<Measurement>
                                      {
                                          new(DateTime.Parse("2017-01-03T10:05:00"), 97.17, MeasurementType.SpO2),
                                          new(DateTime.Parse("2017-01-03T16:40:00"), 89.11, MeasurementType.SpO2),
                                          new(DateTime.Parse("2017-03-05T06:50:00"), 91.01, MeasurementType.SpO2),
                                      }
            }
        };
}