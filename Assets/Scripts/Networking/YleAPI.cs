using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Policy;
using JetBrains.Annotations;
using Networking;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.XR.WSA.Persistence;

public class YleAPI
{
    public class ConfigLoader
    {
        private Dictionary<string, string> data;

        private ConfigLoader(Dictionary<string, string> data)
        {
            this.data = data;
        }

        public string this[string key]
        {
            get { return data[key]; }
        }

        public string Get(string key)
        {
            return this[key];
        }


        //TODO:: support load from env
        public static ConfigLoader LoadConfig(string path = "Assets/Resources/yle.config")
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var parts = line.Split('=');
                result.Add(parts[0], parts[1]);
            }

            reader.Close();
            return new ConfigLoader(result);
        }
    }

    private static string app_id { get; set; }
    private static string app_key { get; set; }
    private static string secret { get; set; }
    private static string base_url { get; set; }
    private static string version { get; set; }

    private static string _url = null;

    private static string Url
    {
        get
        {
            if (_url == null)
            {
                _url = base_url + "/" + version + "/";
            }

            return _url;
        }
    }

    static YleAPI()
    {
        ConfigLoader config = ConfigLoader.LoadConfig();

        app_id = config.Get("app_id");
        app_key = config.Get("app_key");
        secret = config.Get("secret");
        base_url = config.Get("base_url");
        version = config.Get("version");
    }

    private YleAPI()
    {
    }


    public static YleAPI Init()
    {
        return new YleAPI();
    }


    public IEnumerator Get(string url_part, Action<string> callbackAction)
    {
        return Get(url_part, null, callbackAction);
    }


    public IEnumerator Get(string url_part, Dictionary<string, string> options = null,
        Action<string> callbackAction = null)
    {
        var url = Url + url_part;
        var base_params = new Dictionary<string, string>()
        {
            {"app_id",  app_id},
            {"app_key", app_key},
        };

        var request = WebRequest.Get(
            url, base_params, options
        );
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            throw new Exception("could not get data" + request.error);
        }
        else
        {
            
            Debug.Log("qwe");
            callbackAction(request.downloadHandler.text);
            yield return request.downloadHandler.text;
        }
    }

    public class YLERequest
    {
        private readonly string url;
        private readonly Dictionary<string, string> data;
        private readonly YleAPI api;

        public static YLERequest Init(YleAPI api, string url, Dictionary<string, string> data = null)
        {
            return new YLERequest(api, url, data);
        }

        protected YLERequest(YleAPI api, string url, Dictionary<string, string> data = null)
        {
            this.url = url;
            this.data = data;
            this.api = api;
        }

        public IEnumerator Get(Action<string> callback)
        {
            yield return api.Get(url, data, callback);
        }

    }
}

static class YleAPIProgram
{
    public static YleAPI.YLERequest Program(this YleAPI api, int id)
    {
        var url = string.Format("programs/items{0}.json", id);
        return YleAPI.YLERequest.Init(api, null);
    }
}

static class YleAPIItems
{
    public static YleAPI.YLERequest Items(this YleAPI api, int limit = 10, int offset = 0)
    {
        var url = string.Format("programs/items.json");
        return YleAPI.YLERequest.Init(api, url, new Dictionary<string, string>()
        {
            {"limit", limit.ToString()},
            {"offset", offset.ToString()},
        });
    }
}