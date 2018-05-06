using System;
using System.Collections;
using System.Collections.Generic;
using Utils.Config;


namespace Networking
{
    public class YleAPI
    {
        private static string _url;

        static YleAPI()
        {
            var config = ConfigLoader.LoadConfig();

            app_id = config.Get("app_id");
            app_key = config.Get("app_key");
            secret = config.Get("secret");
            base_url = config.Get("base_url");
            version = config.Get("version");
        }

        private YleAPI()
        {
        }

        private static string app_id { get; set; }
        private static string app_key { get; set; }
        private static string secret { get; set; }
        private static string base_url { get; set; }
        private static string version { get; set; }

        private static string Url
        {
            get { return _url ?? (_url = base_url + "/" + version + "/"); }
        }


        public static YleAPI Init()
        {
            return new YleAPI();
        }


        public IEnumerator Get(string url_part, Dictionary<string, string> options = null,
            Action<string> callbackAction = null)
        {
            var url = Url + url_part;
            var base_params = new Dictionary<string, string>
            {
                {"app_id", app_id},
                {"app_key", app_key}
            };

            var request = WebRequest.Get(
                url, base_params, options
            );
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
                throw new Exception("could not get data" + request.error);

            callbackAction(request.downloadHandler.text);
            yield return request.downloadHandler.text;
        }

        public IEnumerator Get(string url_part, Action<string> callbackAction)
        {
            return Get(url_part, null, callbackAction);
        }
    }
}