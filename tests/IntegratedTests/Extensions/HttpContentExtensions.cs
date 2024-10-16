using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IntegratedTests.Extensions
{
    public static class HttpContentExtensions
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter(),
                new TimeSpanConverter(),
                new DateTimeConverter()
            }
        };

        public static async Task<T> ReadContentAs<T>(this HttpContent content)
        {
            var contentValue = await content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(contentValue, Options)!;
        }

        public class TimeSpanConverter : JsonConverter<TimeSpan>
        {
            public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return TimeSpan.Parse(reader.GetString()!);
            }

            public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }

        public class DateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.Parse(reader.GetString()!);
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("o"));
            }
        }
    }
}
