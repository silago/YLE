using System.Collections.Generic;
using Networking;


namespace YLE.API
{
    internal static class YleAPIItems
    {
        public static YLERequest Items(this YleAPI api, string q = "", int limit = 10, int offset = 0)
        {
            var url = "programs/items.json";
            return YLERequest.Init(api, url, new Dictionary<string, string>
            {
                {"q", q},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
                {"language", "fi"},
                {"type", "program"}
            });
        }
    }
}