using Networking;
using UnityEngine;
using YLE.MVC.Model;

namespace YLE.MVC.Controller
{
    public class YLEController
    {
        private readonly YleAPI api;

        private YLEController(YLEView view, MonoBehaviour mono)
        {
            api = YleAPI.Init();
            var model = YleItems.Init(api, mono);
            view.OnBottomReached += model.GetNext;
            //model.OnDataGet += view.OnGetItem;
            model.OnItemsGet += view.OnGetItems;
            view.OnDataRequest += model.Search;
            
            
            //
        }

        public static YLEController Init(YLEView view, MonoBehaviour mono)
        {
            return new YLEController(view, mono);
        }
    }
}