using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderNewLevels : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    private int _randomLvl;
    private int _lastLvl;

    private void OnEnable()
    {
        _timer.TimeUp += LoadNewLevel;
    }
    private void OnDisable()
    {
        _timer.TimeUp -= LoadNewLevel;
    }

    private void LoadNewLevel()
    {
        _randomLvl = Random.Range(1, 11);
        if (_randomLvl == _lastLvl)
        {
            _randomLvl = Random.Range(1, 11);
        }
        _lastLvl = _randomLvl;

        SceneManager.LoadScene(_randomLvl);
    }
}
