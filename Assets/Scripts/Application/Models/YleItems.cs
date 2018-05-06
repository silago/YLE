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
            StartCoroutine(Api.Items(q, limit, offset).Get(data =>
            {
                SetData(data);
                offset += limit;
            }));
        }
    }
}