using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class YLEContainer : MonoBehaviour
{
	private YLEController _controller;
	[SerializeField]
	private YLEView _view;
	
	
	private void Awake()
	{
		_controller = YLEController.Init(_view);
		
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public class YLEController
{
	public static YLEController Init(YLEView view)
	{
		return new YLEController(view);
	}
	
	YLEController(YLEView view)
	{
		
	}
	
}


public class YLEModel
{
	
}

