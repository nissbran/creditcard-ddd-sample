using System;
using System.Globalization;
using Bank.Cards.Domain.Model;
using Newtonsoft.Json;

namespace Bank.Cards.Infrastructure.Serialization.Converters
{
    public class MoneyConverter : JsonConverter<Money>
    {
        public override void WriteJson(JsonWriter writer, Money value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToFullPrecisionString(CultureInfo.InvariantCulture));
        }

        public override Money ReadJson(JsonReader reader, Type objectType, Money existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Money.Parse(reader.Value as string, CultureInfo.InvariantCulture);
        }
    }
}