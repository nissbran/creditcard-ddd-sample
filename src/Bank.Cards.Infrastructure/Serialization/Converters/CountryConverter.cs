using System;
using System.Globalization;
using Bank.Cards.Domain.Model;
using Newtonsoft.Json;

namespace Bank.Cards.Infrastructure.Serialization.Converters
{
    public class CountryConverter : JsonConverter<Country>
    {
        public override void WriteJson(JsonWriter writer, Country value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Code);
        }

        public override Country ReadJson(JsonReader reader, Type objectType, Country existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Country.Parse(reader.Value as string);
        }
    }
}