using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Networking;
using SimpleJSON;
using UnityEngine;


namespace YLE.MVC.Model
{
    public class DataItem : BaseDataModel, IStringIndexed
    {
        private JSONNode node;
        private string title { get; set; }
        private string description { get; set; }
        private string itemTitle { get; set; }
        private string country { get; set; }
        private string creator { get; set; }

        public string this[string index]
        {
            get
            {
                switch (index)
                {
                    case "title":
                        return title;
                    case "description":
                        return description;
                    case "itemTitle":
                        return itemTitle;
                    case "country":
                        return country;
                    case "creator":
                        return creator;
                    default:
                        return node[index];
                }
            }
        }

        public override string ToString()
        {
            return title.Trim('"');
        }

        protected override void Load(JSONNode node)
        {
            this.node = node;
            title = node["title"][BASE_LANG].ToString().Trim('"');
            description = node["description"][BASE_LANG].ToString().Trim('"');
            itemTitle = node["itemTitle"][BASE_LANG].ToString().Trim('"');
            country = node["countryOfOrigin"].ToString().Trim('"');
            creator = node["creator"]["name"].ToString().Trim('"');
        }
    }


    public abstract class YLEModel<DataType> : YLEBaseModel
        where DataType : BaseDataModel, new()
    {
        private string _rawData;
        //public event Action<DataType> OnDataGet;
        public event Action<DataType[]> OnItemsGet;

        protected YLEModel(YleAPI api)
        {
            Api = api;
        }

        protected YLEModel()
        {
        }

        protected Coroutine StartCoroutine(IEnumerator routine)
        {
            return MonoObject.StartCoroutine(routine);
        }

        protected void SetData(string raw)
        {
            var result = JSON.Parse(raw);
            var data = result["data"];
            var items = new List<DataType>();
            foreach (var i in data)
            {
                items.Add(
                    BaseDataModel.Init<DataType>(i.Value)
                );
            }

            OnItemsGet(items.ToArray());
        }
    }
}