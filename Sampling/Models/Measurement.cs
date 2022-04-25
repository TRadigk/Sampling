using System.Globalization;
using Sampling.Enumerations;
using System.Text;

namespace Sampling.Models;

public record Measurement
{
    public DateTime MeasurementTime { get; }
    public Double MeasurementValue { get; }
    public MeasurementType Type { get; }

    public Measurement(DateTime measurementTime, double measurementValue, MeasurementType type)
    {
        MeasurementTime = measurementTime;
        MeasurementValue = measurementValue;
        Type = type;
    }


    /// <inheritdoc />
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append('{');
        builder.Append($"{MeasurementTime:s}");
        builder.Append(", ");
        builder.Append(Type.ToString().ToUpper());
        builder.Append(", ");
        builder.Append($"{MeasurementValue.ToString("F2",CultureInfo.InvariantCulture)}");
        builder.Append('}');
        return builder.ToString();
    }
}