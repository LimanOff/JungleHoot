using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderNewLevels
{
    private int _randomLvl;
    private static int _lastLvl;

    public void LoadNewLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        do
        {
            _randomLvl = Random.Range(1, 6);
        } while (_randomLvl == currentLevelIndex || _randomLvl == _lastLvl);

        _lastLvl = _randomLvl;

        SceneManager.LoadScene(_randomLvl);
    }
}
