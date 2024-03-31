using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class KeySaver
{
    [SerializeField] private string _key;

    public void LoadSavedKeyBinding(Key keyToLoad, int keyBindIndex,ref PlayerInputController inputController)
    {
        if (PlayerPrefs.HasKey(_key))
        {
            string savedKeyPath = PlayerPrefs.GetString(_key);

            switch (savedKeyPath)
            {
                case "←":
                    savedKeyPath = "<Keyboard>/leftArrow";
                    break;
                case "→":
                    savedKeyPath = "<Keyboard>/rightArrow";
                    break;
                case "↑":
                    savedKeyPath = "<Keyboard>/upArrow";
                    break;
                case "↓":
                    savedKeyPath = "<Keyboard>/downArrow";
                    break;
            }
            inputController.GameInput.FindAction(keyToLoad.RebindInputActionReference.action.name).ApplyBindingOverride(keyBindIndex, savedKeyPath);
            keyToLoad.SetText(inputController.GameInput.FindAction(keyToLoad.RebindInputActionReference.action.name).bindings[keyBindIndex].ToDisplayString());
        }
        else
        {
            keyToLoad.SetText(inputController.GameInput.FindAction(keyToLoad.RebindInputActionReference.action.name).bindings[keyBindIndex].ToDisplayString());
        }
    }

    public void SaveKeyBinding(string keyPath)
    {
        switch (keyPath)
        {
            case "←":
                keyPath = "<Keyboard>/leftArrow";
                break;
            case "→":
                keyPath = "<Keyboard>/rightArrow";
                break;
            case "↑":
                keyPath = "<Keyboard>/upArrow";
                break;
            case "↓":
                keyPath = "<Keyboard>/downArrow";
                break;
        }
        PlayerPrefs.SetString(_key, keyPath);
        PlayerPrefs.Save();
    }
}
