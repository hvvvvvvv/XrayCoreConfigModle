﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;

namespace XrayCoreConfigModle.JsonConverters
{
    public class ConfigurationConverter : JsonConverter<InboundConfigurationObject>
    {
        public override InboundConfigurationObject? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<NoneConfigurationObject>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, InboundConfigurationObject value, JsonSerializerOptions options)
        {
            Type writeType = InboundConfigurationObject.GetInstanceType(value);
            JsonSerializer.Serialize(writer, value, writeType, options);
        }
    }
}
