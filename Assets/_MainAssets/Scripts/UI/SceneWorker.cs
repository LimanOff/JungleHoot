using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWorker : MonoBehaviour
{
    public void LoadScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }
}
