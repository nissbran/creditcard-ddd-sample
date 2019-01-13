using System;
using Bank.Cards.Domain.Model;
using Newtonsoft.Json;

namespace Bank.Cards.Infrastructure.Serialization.Converters
{
    public class AccountIdConverter : JsonConverter<AccountId>
    {
        public override void WriteJson(JsonWriter writer, AccountId value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override AccountId ReadJson(JsonReader reader, Type objectType, AccountId existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return AccountId.Parse(reader.Value as string);
        }
    }
}