using System.Globalization;
using Sampling.Enumerations;
using System.Text;

namespace Sampling.Models;

public record Measurement(DateTime MeasurementTime, double MeasurementValue, MeasurementType Type)
{
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