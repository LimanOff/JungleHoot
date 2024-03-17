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
        int oldFontSize = 46;
        int newFontSize = 114;

        switch (text)
        {
            case "Left Arrow" or "←":
                RebindButtonText.text = "←";
                RebindButtonText.fontSize = newFontSize;
                break;
            case "Right Arrow" or "→":
                RebindButtonText.text = "→";
                RebindButtonText.fontSize = newFontSize;
                break;
            case "Up Arrow" or "↑":
                RebindButtonText.text = "↑";
                RebindButtonText.fontSize = newFontSize;
                break;
            case "Down Arrow" or "↓":
                RebindButtonText.text = "↓";
                RebindButtonText.fontSize = newFontSize;
                break;
            default:
                RebindButtonText.text = text;
                RebindButtonText.fontSize = oldFontSize;
                break;
        }
    }
}
