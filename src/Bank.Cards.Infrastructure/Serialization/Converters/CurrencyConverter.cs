using System;
using System.Globalization;
using Bank.Cards.Domain.Model;
using Newtonsoft.Json;

namespace Bank.Cards.Infrastructure.Serialization.Converters
{
    public class CurrencyConverter : JsonConverter<Currency>
    {
        public override void WriteJson(JsonWriter writer, Currency value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Code);
        }

        public override Currency ReadJson(JsonReader reader, Type objectType, Currency existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Currency.Parse(reader.Value as string);
        }
    }
}