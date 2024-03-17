using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWorker : MonoBehaviour
{
    public void LoadScene(string nameOfScene)
    {
        SceneManager.LoadSceneAsync(nameOfScene);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
