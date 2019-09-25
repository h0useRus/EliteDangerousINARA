using System;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NSW.EliteDangerous.INARA
{
    internal static class Json
    {
        internal static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal, DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ssK"}
            }
        };

        private static readonly Lazy<JsonSerializer> _serializer = new Lazy<JsonSerializer>(() => JsonSerializer.Create(Settings));

        public static string ToJson<T>(T obj, JsonSerializerSettings settings = null)
        {
            if (obj == null) return null;

            var serializer = settings == null ? _serializer.Value : JsonSerializer.Create(settings);
            var stringWriter = new StringWriter(new StringBuilder(256), CultureInfo.InvariantCulture);
            using (var jsonTextWriter = new JsonTextWriter(stringWriter) { CloseOutput = false })
            {
                serializer.Serialize(jsonTextWriter, obj, typeof(T));
            }
            return stringWriter.ToString();
        }

        internal static T FromJson<T>(TextReader textReader, JsonSerializerSettings settings = null)
        {
            var serializer = settings == null ? _serializer.Value : JsonSerializer.Create(settings);
            using (var jsonTextReader = new JsonTextReader(textReader) { SupportMultipleContent = true, CloseInput = false })
                return serializer.Deserialize<T>(jsonTextReader);
        }

        internal static T FromJson<T>(Stream jsonStream, JsonSerializerSettings settings = null)
        {
            using (var streamReader = new StreamReader(jsonStream))
                return FromJson<T>(streamReader, settings);
        }

        internal static T FromJson<T>(string jsonString, JsonSerializerSettings settings = null)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return default;

            using (var streamReader = new StringReader(jsonString))
                return FromJson<T>(streamReader, settings);
        }
    }
}