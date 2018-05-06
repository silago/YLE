using System;
using Interfaces;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
using Utils.Pool;
using YLE.MVC.Model;


public class YLEView : MonoBehaviour
{
    [SerializeField] private InputPanel _inputPanel;
    [SerializeField] private ItemInfoView infoView;
    [SerializeField] private ViewItem itemPrefab;
    [SerializeField] private ScrollRect listView;
    [SerializeField] private GameObject WaitIndicator;

    private ObjectPool<ViewItem> pool;
    private bool _waitingForData;

    public event Action<string> OnDataRequest;
    public event Action OnBottomReached;

    private bool WaitingForData
    {
        get { return _waitingForData; }
        set
        {
            WaitIndicator.gameObject.SetActive(value);
            _waitingForData = value;
        }
    }

    private void Awake()
    {
        pool = ObjectPool<ViewItem>.Init(itemPrefab);
        listView.onValueChanged.AddListener(onRectValueChanged);
        _inputPanel.OnSubmit += data =>
        {
            OnSearch();
            OnDataRequest.Invoke(data);
        };
    }

    private void OnSearch()
    {
        pool.FreePool();
        listView.gameObject.SetActive(true);
        infoView.gameObject.SetActive(false);
    }


    private void onRectValueChanged(Vector2 scrollPos)
    {
        if (scrollPos.y == 0 && !WaitingForData)
        {
            if (OnBottomReached != null)
                OnBottomReached();
            WaitingForData = true;
        }
    }


    private void InitItem(ViewItem go, DataItem item)
    {
        go.Text = item.ToString();
        go.gameObject.SetActive(true);
        go.BindClickAction(ShowInfo);
        go.Item = item;
        WaitingForData = false;
    }

    private void ShowInfo(IStringIndexed item)
    {
        listView.gameObject.SetActive(false);
        infoView.gameObject.SetActive(true);
        infoView.Fill(item);
    }

    public void OnGetItem(DataItem item)
    {
        ViewItem go = pool.GetFromPool();
        InitItem(go, item);
    }
    public void OnGetItems(DataItem[] items)
    {
        foreach (DataItem dataItem in items)
        {
            ViewItem go = pool.GetFromPool();
            InitItem(go, dataItem);
        }

        WaitingForData = false;
    }
}