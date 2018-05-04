using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class YLEView : MonoBehaviour
{
	[SerializeField]
	private ScrollRect myScrollRect;

	public event Action OnBottomReached;
	public event Action OnDataReceived;
	public event Action<int> OnProgramRequest;
	
	private void Awake()
	{
		myScrollRect.onValueChanged.AddListener(onRectValueChanged);
	}

	private void onRectValueChanged(Vector2 scrollPos)
	{
		if (scrollPos.y == 0)
		{
			if (OnBottomReached != null) OnBottomReached();
		}
	}

	public void OnGetItems(Item item)
	{
		Debug.Log(item);
	}

}

