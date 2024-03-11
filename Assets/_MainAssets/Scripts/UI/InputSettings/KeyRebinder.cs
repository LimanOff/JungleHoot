using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

[Serializable]
public class KeyRebinder
{
    public event Action<string> KeyValueChanged;
    public event Action<string> KeyAlreadyBounded;
    public event Action RebindStarted;
    public event Action RebindEnded;

    public Key KeyToRebind;
    public KeySaver KeyToRebindSaver;
    public int KeyToRebindIndex;

    private string _oldKeyValue;
    private List<InputBinding> _oldBindings;

    public void Initialize()
    {
        KeyToRebind.RebindButton.onClick.AddListener(StartRebinding);

        KeyToRebindSaver.LoadSavedKeyBinding(ref KeyToRebind, KeyToRebindIndex);

        KeyValueChanged += (text) =>
        {
            KeyToRebind.SetText(text);
            KeyToRebindSaver.SaveKeyBinding(text);
        };
    }

    ~KeyRebinder()
    {
        KeyToRebind.RebindButton.onClick.RemoveListener(StartRebinding);
        KeyValueChanged -= (text) =>
        {
            KeyToRebind.SetText(text);
            KeyToRebindSaver.SaveKeyBinding(text);
        };
    }

    private void StartRebinding()
    {
        _oldKeyValue = KeyToRebind.RebindButtonText.text;
        _oldBindings = PlayerInputController.GameInput.bindings.ToList();

        KeyToRebind.SetText("...");

        PlayerInputController.GameInput.Disable();


        PlayerInputController.GameInput.FindAction(KeyToRebind.RebindInputActionReference.action.name).PerformInteractiveRebinding(KeyToRebindIndex).
                                                                            OnComplete(OnRebindingComplete)
                                                                            .WithTargetBinding(KeyToRebindIndex)
                                                                            .WithControlsExcluding("Mouse")
                                                                            .WithCancelingThrough("<Keyboard>/escape")
                                                                             .Start();
        RebindStarted?.Invoke();
    }

    private void OnRebindingComplete(InputActionRebindingExtensions.RebindingOperation operation)
    {
        string newKeyPath = InputControlPath.ToHumanReadableString(
                                                    operation.action.bindings[KeyToRebindIndex].effectivePath,
                                                    InputControlPath.HumanReadableStringOptions.OmitDevice);

        if (!IsKeyAlreadyBound(newKeyPath))
        {
            KeyToRebindSaver.SaveKeyBinding(newKeyPath);

            KeyValueChanged?.Invoke(newKeyPath);
        }
        else
        {
            operation.action.RemoveBindingOverride(KeyToRebindIndex);
            KeyAlreadyBounded?.Invoke($"Клавиша ({newKeyPath}) уже занята");
            KeyValueChanged?.Invoke($"{_oldKeyValue}");
        }

        operation.Dispose();

        RebindEnded?.Invoke();

        PlayerInputController.GameInput.Enable();
    }

    public void ResetKeyValue()
    {
        PlayerInputController.GameInput.FindAction(KeyToRebind.RebindInputActionReference.action.name).RemoveAllBindingOverrides();
        KeyValueChanged?.Invoke(KeyToRebind.RebindInputActionReference.action.bindings[KeyToRebindIndex].ToDisplayString());
    }

    private bool IsKeyAlreadyBound(string keyPath)
    {
        foreach (var keybinding in _oldBindings)
        {
            string keyName = InputControlPath.ToHumanReadableString(
                                                                keybinding.effectivePath,
                                                                InputControlPath.HumanReadableStringOptions.OmitDevice);
            if (keyName == keyPath)
                return true;
        }

        return false;
    }
}
