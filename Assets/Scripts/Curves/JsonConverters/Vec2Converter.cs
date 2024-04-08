using Newtonsoft.Json;
using System;
using System.Globalization;
using UnityEngine;

public class Vec2Converter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        if (objectType == typeof(Vector2))
        {
            return true;
        }

        return false;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var str = serializer.Deserialize<string>(reader);
        var t = str.Split(",");

        var x = float.Parse(t[0], CultureInfo.InvariantCulture.NumberFormat);
        var y = float.Parse(t[1], CultureInfo.InvariantCulture.NumberFormat);

        return new Vector2(x,y);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}