using UnityEngine;
using UnityEngine.UI;

public class YLEView : MonoBehaviour
{
	public ScrollRect myScrollRect;

	private void Update()
	{
		Debug.Log(myScrollRect.normalizedPosition);
	}
}