using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AI;


interface IDataModel
{
}



public class Item
{
	
}

public abstract class YLEBaseModel
{
	protected YleAPI api;
	public static void Init<TChild,T>(YleAPI api) where TChild : YLEModel<T>, new()
	
	{
		new TChild {api = api};
	}
	
}

public abstract class YLEModel<TItem> : YLEBaseModel 
{
	public event Action OnData;
	private TItem _data;
	public event Action<TItem> OnDataGet;
	private string _rawData;
	protected MonoBehaviour _mono;
	protected Coroutine StartCoroutine(IEnumerator routine)
	{
		return _mono.StartCoroutine(routine);
	}
	
	public string RawData
	{
		set
		{
			_rawData = value;
			_data = Decode(_rawData);
			if (OnDataGet != null) OnDataGet.Invoke(_data);
		}
		get { return _rawData; }
	}

	protected TItem Decode(string raw)
	{
		return JsonConvert.DeserializeObject<TItem>(raw);
	}

	public static T Init<T>(YleAPI api, MonoBehaviour mono) where T : YLEModel<TItem>, new()
	{
		return (new T {api = api, _mono = mono});

	}

	protected YLEModel<TItem> WithApi(YleAPI api)
	{
		this.api = api;
		return this;
	}
	
	protected YLEModel(YleAPI api)
	{
		this.api = api;
	}

	public YLEModel()
	{
	}
}

public class YleItems: YLEModel<Item>, IDataModel 
{

	private readonly int limit = 10;
	private int offset = 0;

	public void OnReady()
	{
		GetNext();
	}
	
	public void GetNext()
	{
		StartCoroutine(api.Items(limit, offset).Get(data =>
		{
            offset += limit;
		}));
	}
}