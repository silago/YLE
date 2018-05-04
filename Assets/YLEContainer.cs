using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class YLEContainer : MonoBehaviour
{
    private YLEController _controller;
    [SerializeField] private YLEView _view;


    private void Awake()
    {
        _controller = YLEController.Init(_view, this);
    }
}


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
        var model = YLEModel<Item>.Init<YleItems>(api, mono);
        view.OnBottomReached += model.GetNext;
        model.OnDataGet += view.OnGetItems;
        
        
        model.OnReady();
    }
}