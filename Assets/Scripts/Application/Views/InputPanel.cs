using System;
using UnityEngine;
using UnityEngine.UI;

public class InputPanel : MonoBehaviour
{
    public Text InputField;
    public Button SubmitBtn;
    public event Action<string> OnSubmit;


    private void Awake()
    {
        SubmitBtn.onClick
            .AddListener(
                () =>
                {
                    if (OnSubmit != null) OnSubmit.Invoke(InputField.text);
                });
    }
}