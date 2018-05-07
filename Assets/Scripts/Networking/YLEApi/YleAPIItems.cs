using System.Collections.Generic;
using Networking;


namespace YLE.API
{
    internal static class YleAPIItems
    {
        public static YLERequest Items(this YleAPI api, string q = "", int limit = 10, int offset = 0,
            Dictionary<string, string> options = null)
        {
            var url = "programs/items.json";
            var request_params = new Dictionary<string, string>()
            {
                {"q", q},
                {"limit", limit.ToString()},
                {"offset", offset.ToString()},
            };
            
            if (options != null)
            {
                foreach (KeyValuePair<string, string> option in options)
                {
                    request_params[option.Key] = option.Value;
                }
            }


            return YLERequest.Init(api, url, request_params);
        }
    }
}