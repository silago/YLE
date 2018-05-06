using System;
using System.Collections;
using System.Collections.Generic;
using Networking;

namespace YLE.API
{
    public class YLERequest
    {
        private readonly YleAPI api;
        private readonly Dictionary<string, string> data;
        private readonly string url;

        protected YLERequest(YleAPI api, string url, Dictionary<string, string> data = null)
        {
            this.url = url;
            this.data = data;
            this.api = api;
        }

        public static YLERequest Init(YleAPI api, string url, Dictionary<string, string> data = null)
        {
            return new YLERequest(api, url, data);
        }

        public IEnumerator Get(Action<string> callback)
        {
            yield return api.Get(url, data, callback);
        }
    }
}