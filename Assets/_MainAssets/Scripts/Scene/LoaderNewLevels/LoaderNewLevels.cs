using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderNewLevels
{
    private int _randomLvl;
    private static int _lastLvl;

    public void LoadNewLevel()
    {
        _randomLvl = Random.Range(1, 6);
        if (_randomLvl == _lastLvl)
        {
            _randomLvl = Random.Range(1, 6);
        }
        _lastLvl = _randomLvl;

        SceneManager.LoadScene(_randomLvl);
    }
}
