using System.Text.Json.Serialization;
using ColorPicker.Services.SaveLoad.Serializable;

namespace ColorPicker.Services.SaveLoad;

[JsonSourceGenerationOptions(
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
)]
[JsonSerializable(typeof(SerializableUserData))]
[JsonSerializable(typeof(List<SerializableHistoryEntry>))]
public partial class AppJsonContext : JsonSerializerContext;