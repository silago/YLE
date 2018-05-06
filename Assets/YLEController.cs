using UnityEngine;

public class YLEController
{
    YleAPI api;
    public static YLEController Init(YLEView view, MonoBehaviour mono)
    {
        return new YLEController(view, mono);
    }

    YLEController(YLEView view, MonoBehaviour mono)
    {
        api = YleAPI.Init();
         
        YleItems model = YleItems.Init(api, mono);
        //var model = YLEModel<Item>.Init<YleItems>(api, mono);
        view.OnBottomReached += model.GetNext;
        model.OnDataGet += view.OnGetItems;
        view.OnDataRequest += model.Search;
        //model.OnReady();
    }
}