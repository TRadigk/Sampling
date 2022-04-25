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

        var measurementsSortedByTimeStamp = from m in measurementsForType orderby m.MeasurementTime ascending select m;

        var sampledElements = FilterMeasurementData(startOfSampling, measurementsSortedByTimeStamp);
        return sampledElements;
    }


    private IEnumerable<Measurement> FilterMeasurementData(DateTime startOfSampling,
                                                           IEnumerable<Measurement> measurements)
    {
        var measurementList = new List<Measurement>(measurements);
        List<Measurement> sampled = new();
        DateTime lastSamplingTimeStamp = DateTime.MinValue;

        foreach (var measurement in measurementList)
        {
            var actualSamplingTimeStamp = GetSamplingTimeStamp(measurement.MeasurementTime, SamplingTimeSpan);
            bool isTooOld = measurement.MeasurementTime < startOfSampling;
            bool isAlreadySampled = actualSamplingTimeStamp == lastSamplingTimeStamp;
            if (isTooOld || isAlreadySampled)
            {
                continue;
            }

            var newMeasurement =
                new Measurement(actualSamplingTimeStamp, measurement.MeasurementValue, measurement.Type);
            sampled.Add(newMeasurement);
            lastSamplingTimeStamp = actualSamplingTimeStamp;
        }

        return sampled;
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