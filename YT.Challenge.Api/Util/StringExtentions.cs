using Newtonsoft.Json;

namespace YT.Challenge.Api.Util
{
    public static class StringExtentions
    {
        public static string ToJsonString<T>(this T obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}