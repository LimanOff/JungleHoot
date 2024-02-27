using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLoopManager : MonoBehaviour
{
    public List<AudioClip> LoopMusic = new List<AudioClip>();
    public AudioSource LoopMusicAudioSource;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += delegate { SetMusicBasedOnCurrentLevel(); };
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= delegate { SetMusicBasedOnCurrentLevel(); };
    }

    private void SetMusicBasedOnCurrentLevel()
    {
        int currentLevelBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentLevelBuildIndex < LoopMusic.Count)
            LoopMusicAudioSource.clip = LoopMusic[currentLevelBuildIndex];
        else
            LoopMusicAudioSource.clip = null;

        if (LoopMusicAudioSource.clip != null)
            LoopMusicAudioSource.Play();
        else
            LoopMusicAudioSource.Stop();
    }
}
