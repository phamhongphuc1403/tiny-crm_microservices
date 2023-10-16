using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlock.Application.PipelineBehaviors;

public class NullableEnumConverter<TEnum> : JsonConverter<TEnum?>
    where TEnum : struct, Enum
{
    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String) return JsonSerializer.Deserialize<TEnum?>(ref reader, options);
        var enumValue = reader.GetString();
        return string.IsNullOrWhiteSpace(enumValue) ? null : JsonSerializer.Deserialize<TEnum?>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}
