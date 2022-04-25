using Sampling.Enumerations;
using Sampling.Models;

// ReSharper disable IdentifierTypo

namespace Sampling;

public class MeasurementSampler
{
    // ReSharper disable once MemberCanBePrivate.Global
    public int SamplingTimeSpanMinutes { get; set; } = 5;
    private TimeSpan SamplingTimeSpan => TimeSpan.FromMinutes(SamplingTimeSpanMinutes);

    public Dictionary<MeasurementType, List<Measurement>> Sample(DateTime startOfSampling,
                                                                 List<Measurement> unsampledMeasurements)
    {
        var outputDictionary = new Dictionary<MeasurementType, List<Measurement>>();
        var measurementTypes = Enum.GetValues(typeof(MeasurementType));

        foreach (MeasurementType measurementType in measurementTypes)
        {
            var sampledMeasurements = SampleMeasurementData(startOfSampling, unsampledMeasurements, measurementType);

            outputDictionary.Add(measurementType, sampledMeasurements.ToList());
        }

        return outputDictionary;
    }

    private IEnumerable<Measurement> SampleMeasurementData(DateTime startOfSampling,
                                                           List<Measurement> unsampledMeasurements,
                                                           MeasurementType measurementType)
    {
        var measurementsForType = unsampledMeasurements.Where(measurement => measurement.Type == measurementType);

        var measurementsSortedByTimeStamp = from measurement in measurementsForType
                                            orderby measurement.MeasurementTime descending
                                            select measurement;

        var sampledElements = FilterMeasurementData(startOfSampling, measurementsSortedByTimeStamp);

        return sampledElements;
    }


    private IEnumerable<Measurement> FilterMeasurementData(DateTime startOfSampling,
                                                           IEnumerable<Measurement> measurements)
    {
        var measurementList = new List<Measurement>(measurements);
        List<Measurement> sampledData = new();
        var lastSamplingTimeStamp = DateTime.MinValue;

        foreach ((var measurementTime, double measurementValue, var measurementType) in measurementList)
        {
            var actualSamplingTimeStamp = GetSamplingTimeStamp(measurementTime, SamplingTimeSpan);
            bool isTooOld = measurementTime < startOfSampling;
            bool isAlreadySampled = actualSamplingTimeStamp == lastSamplingTimeStamp;

            if (isTooOld || isAlreadySampled)
            {
                continue;
            }

            lastSamplingTimeStamp = actualSamplingTimeStamp;
            var measurementWithNewTimeStamp =
                new Measurement(actualSamplingTimeStamp, measurementValue, measurementType);
            sampledData.Insert(0, measurementWithNewTimeStamp);
        }

        return sampledData;
    }

    internal static DateTime GetSamplingTimeStamp(DateTime timeStamp, TimeSpan gridSpan)
    {
        if (timeStamp.Second > 0)
        {
            timeStamp += gridSpan;
        }

        int newOffsetMinutes = timeStamp.Minute - timeStamp.Minute % gridSpan.Minutes;

        return new DateTime(timeStamp.Year,
                            timeStamp.Month,
                            timeStamp.Day,
                            timeStamp.Hour,
                            newOffsetMinutes,
                            0);
    }
}