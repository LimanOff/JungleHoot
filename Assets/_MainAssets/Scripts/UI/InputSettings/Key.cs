using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[Serializable]
public struct Key
{
    public Button RebindButton;
    public Text RebindButtonText;
    public InputActionReference RebindInputActionReference;

    public void SetText(string text)
    {
        RebindButtonText.text = text;
    }
}
