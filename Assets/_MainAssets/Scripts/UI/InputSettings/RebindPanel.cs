using ModestTree;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RebindPanel : MonoBehaviour
{
    [SerializeField] private List<KeyRebinder> _keyRebinders = new List<KeyRebinder>();
    [SerializeField] private MessageDisplayer _messageDisplayer;
    [SerializeField] private GameObject _notTouchArea;

    [Inject]
    public void Initialize(PlayerInputController inputController)
    {
        ValidateComponents();

        foreach (KeyRebinder keyRebinder in _keyRebinders)
        {
            keyRebinder.Initialize(inputController);
            keyRebinder.KeyAlreadyBounded += _messageDisplayer.ShowInfoMessage;
            keyRebinder.RebindStarted += () => _notTouchArea.SetActive(true);
            keyRebinder.RebindEnded += () => _notTouchArea.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        foreach (KeyRebinder keyRebinder in _keyRebinders)
        {
            keyRebinder.KeyAlreadyBounded -= _messageDisplayer.ShowInfoMessage;
            keyRebinder.RebindStarted -= () => _notTouchArea.SetActive(true);
            keyRebinder.RebindEnded -= () => _notTouchArea.SetActive(false);
        }
    }

    public void ResetKeys()
    {
        _keyRebinders.ForEach(k => k.ResetKeyValue());
        DropSavedKeys();
    }

    private void DropSavedKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_notTouchArea, "(RebindPanel/ValidateComponents) NotTouchArea не задана");
        Assert.IsNotNull(_messageDisplayer, "(RebindPanel/ValidateComponents) MessageDisplayer не задан");
    }
}
