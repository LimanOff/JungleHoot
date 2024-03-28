using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

[Serializable]
public class KeyRebinder
{
    public event Action<string, string> KeyValueChanged;
    public event Action<string> KeyAlreadyBounded;
    public event Action RebindStarted;
    public event Action RebindEnded;

    public Key KeyToRebind;
    public KeySaver KeyToRebindSaver;
    public int KeyToRebindIndex;

    private string _oldKeyValue;
    private List<InputBinding> _oldBindings;

    private PlayerInputController _inputController;

    List<InputBinding> r;

    public void Initialize(PlayerInputController inputController)
    {
        _inputController = inputController;
        KeyToRebind.RebindButton.onClick.AddListener(StartRebinding);

        KeyToRebindSaver.LoadSavedKeyBinding(KeyToRebind, KeyToRebindIndex,ref inputController);

        KeyValueChanged += (text, textToDisplay) =>
        {
            KeyToRebind.SetText(textToDisplay);
            KeyToRebindSaver.SaveKeyBinding(text);
        };
    }

    ~KeyRebinder()
    {
        KeyToRebind.RebindButton.onClick.RemoveListener(StartRebinding);
        KeyValueChanged -= (text, textToDisplay) =>
        {
            KeyToRebind.SetText(textToDisplay);
            KeyToRebindSaver.SaveKeyBinding(text);
        };
    }

    private void StartRebinding()
    {
        _oldKeyValue = KeyToRebind.RebindButtonText.text;

        _oldBindings = _inputController.GameInput.bindings.ToList();

        KeyToRebind.SetText("...");

        _inputController.GameInput.Disable();


        _inputController.GameInput.FindAction(KeyToRebind.RebindInputActionReference.action.name).PerformInteractiveRebinding(KeyToRebindIndex).
                                                                            OnComplete(OnRebindingComplete)
                                                                            .WithTargetBinding(KeyToRebindIndex)
                                                                            .WithControlsExcluding("Mouse")
                                                                            .WithCancelingThrough("<Keyboard>/escape")
                                                                             .Start();
        RebindStarted?.Invoke();
    }

    private void OnRebindingComplete(InputActionRebindingExtensions.RebindingOperation operation)
    {
        string newKeyPath = operation.action.bindings[KeyToRebindIndex].effectivePath;
        string newKeyPathToDisplay = InputControlPath.ToHumanReadableString(
                                                    operation.action.bindings[KeyToRebindIndex].effectivePath,
                                                    InputControlPath.HumanReadableStringOptions.OmitDevice);

        if (!IsKeyAlreadyBound(newKeyPath))
        {
            KeyValueChanged?.Invoke(newKeyPath, newKeyPathToDisplay);
        }
        else
        {
            operation.action.RemoveBindingOverride(KeyToRebindIndex);
            KeyAlreadyBounded?.Invoke($"������� ({newKeyPath}) ��� ������");
            KeyValueChanged?.Invoke(_oldKeyValue,_oldKeyValue);
        }

        operation.Dispose();

        RebindEnded?.Invoke();

        _inputController.GameInput.Enable();
    }

    public void ResetKeyValue()
    {
        _inputController.GameInput.FindAction(KeyToRebind.RebindInputActionReference.action.name).RemoveAllBindingOverrides();
        string key = KeyToRebind.RebindInputActionReference.action.bindings[KeyToRebindIndex].ToDisplayString();
        KeyValueChanged?.Invoke(key, key);
    }

    private bool IsKeyAlreadyBound(string keyPath)
    {
        string key = InputControlPath.ToHumanReadableString(keyPath, InputControlPath.HumanReadableStringOptions.OmitDevice);

        foreach (var keybinding in _oldBindings)
        {
            string keyName = InputControlPath.ToHumanReadableString(
                                                                keybinding.effectivePath,
                                                                InputControlPath.HumanReadableStringOptions.OmitDevice);

            if (keyName == key)
                return true;
        }

        return false;
    }
}
