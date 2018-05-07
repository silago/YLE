using System.Collections.Generic;
using Networking;
using UnityEngine;
using YLE.API;
using YLE.MVC.Model;

namespace YLE.MVC.Model
{
    public class YleItems : YLEModel<DataItem>
    {
        private readonly int limit = 10;
        private int offset;
        private string q = "";

        private readonly Dictionary<string, string> _requestParams = new Dictionary<string, string>()
        {
            {"language", "fi"},
            {"type", "program"},
        };

        public static YleItems Init(YleAPI api, MonoBehaviour mono)
        {
            return new YleItems {Api = api, MonoObject = mono};
        }


        public void Search(string text)
        {
            q = text;
            offset = 0;
            GetNext();
        }

        public void GetNext()
        {
            StartCoroutine(Api.Items(q, limit, offset, _requestParams).Get(data =>
            {
                SetData(data);
                offset += limit;
            }));
        }
    }
}