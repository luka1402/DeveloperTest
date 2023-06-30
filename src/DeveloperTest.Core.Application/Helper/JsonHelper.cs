using System.Text.Json;

namespace DeveloperTest.Core.Application.Helper;

public static class JsonHelper
{
    public static JsonDocument ToJson(this object obj)
    {
        string json = JsonSerializer.Serialize(obj);
        JsonDocument jsonDoc = JsonDocument.Parse(json);
        return jsonDoc;
    }
    public static JsonDocument ToJson(this string s)
    {
        return JsonDocument.Parse(s);

    }
    public static bool IsValidJson(this object obj)
    {
        bool valid = false;
        try
        {
            string json = JsonSerializer.Serialize(obj);
            JsonDocumentOptions options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip
            };
            JsonDocument jsonDoc = JsonDocument.Parse(json, options);
            valid = jsonDoc.RootElement.ValueKind == JsonValueKind.Array || jsonDoc.RootElement.ValueKind == JsonValueKind.Object;
        }
        catch
        {
            valid = false;
        }
        return valid;

    }
}

