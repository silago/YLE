using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewItem : MonoBehaviour, IReleasable
{
    [SerializeField] private Text _text;

    [SerializeField] private Button Button;

    public IStringIndexed Item;
    private Action<IStringIndexed> clickAction;

    public string Text
    {
        set { _text.text = value; }
    }

    private void Awake()
    {
        if (Button.onClick != null) Button.onClick.AddListener(() => { clickAction(Item); });
    }

    public void BindClickAction(Action<IStringIndexed> action)
    {
        clickAction = action;
    }

    public void Release()
    {
        gameObject.SetActive(false);
        clickAction = null;
    }
}