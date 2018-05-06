using System.Collections.Generic;

static class YleAPIItems
{
    public static YleAPI.YLERequest Items(this YleAPI api, string q = "", int limit = 10, int offset = 0)
    {
        var url = string.Format("programs/items.json");
        return YleAPI.YLERequest.Init(api, url, new Dictionary<string, string>()
        {
            
            {"q", q},
            {"limit", limit.ToString()},
            {"offset", offset.ToString()},
            {"language", "fi"},
            {"type", "program"}
        });
    }
}