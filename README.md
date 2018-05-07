YLE API Usage


Defining endpoints:


A good way to define new endpoint is to implement extesion for YLERequest:

internal static class YleAPIEndpoint
    {
        public static YLERequest Items(this YleAPI api, string param = "")
        {
            var url = "endpoint.json";
            return YLERequest.Init(api, url, new Dictionary<string, string>
            {
                {"param", param}
            });
        }
}

so you can call it this way:

StartCoroutine(Api.Items(param).Get(callback);
