using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class KeySaver
{
    [SerializeField] private string _key;

    public void LoadSavedKeyBinding(ref Key keyToLoad, int keyBindIndex)
    {
        if (PlayerPrefs.HasKey(_key))
        {
            string savedKeyPath = PlayerPrefs.GetString(_key);

            keyToLoad.RebindInputActionReference.action.ApplyBindingOverride(keyBindIndex, savedKeyPath);

            keyToLoad.SetText(keyToLoad.RebindInputActionReference.action.bindings[keyBindIndex].ToDisplayString());
        }
        else
        {
            keyToLoad.SetText(keyToLoad.RebindInputActionReference.action.bindings[keyBindIndex].ToDisplayString());
        }
    }

    public void SaveKeyBinding(string keyPath)
    {
        PlayerPrefs.SetString(_key, keyPath);
        PlayerPrefs.Save();
    }
}
