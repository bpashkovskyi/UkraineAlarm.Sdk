using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>
/// Reads a <see cref="TimeSpan"/> from either a string (e.g. "00:30:00"), a number of ticks,
/// or an object exposing a "ticks" property, matching the various shapes the API may return.
/// </summary>
internal sealed class FlexibleTimeSpanConverter : JsonConverter<TimeSpan?>
{
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null:
                return null;

            case JsonTokenType.String:
                string? text = reader.GetString();
                return string.IsNullOrEmpty(text)
                    ? null
                    : TimeSpan.Parse(text, CultureInfo.InvariantCulture);

            case JsonTokenType.Number:
                return TimeSpan.FromTicks(reader.GetInt64());

            case JsonTokenType.StartObject:
                return ReadFromObject(ref reader);

            default:
                throw new JsonException($"Unexpected token '{reader.TokenType}' when reading a TimeSpan.");
        }
    }

    private static TimeSpan? ReadFromObject(ref Utf8JsonReader reader)
    {
        long? ticks = null;
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }

            bool isTicks = reader.ValueTextEquals("ticks");
            reader.Read();
            if (isTicks && reader.TokenType == JsonTokenType.Number)
            {
                ticks = reader.GetInt64();
            }
            else
            {
                reader.Skip();
            }
        }

        return ticks is null ? null : TimeSpan.FromTicks(ticks.Value);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStringValue(value.Value.ToString("c", CultureInfo.InvariantCulture));
    }
}
