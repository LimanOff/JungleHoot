using ModestTree;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsPanel : MonoBehaviour
{
    [Header("AudioMixer Groups")]
    [SerializeField] private AudioMixerGroup _masterAudioMixerGroup;
    [SerializeField] private AudioMixerGroup _soundAudioMixerGroup;
    [SerializeField] private AudioMixerGroup _musicAudioMixerGroup;

    private const string MasterVolume = "MasterVolume";
    private const string SoundVolume = "SoundVolume";
    private const string MusicVolume = "MusicVolume";

    private void Awake()
    {
        ValidateComponents();
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_masterAudioMixerGroup, "(AudioSettingsPanel/ValidateComponents) Группа мастера звуков не задана");
        Assert.IsNotNull(_soundAudioMixerGroup, "(AudioSettingsPanel/ValidateComponents) Группа звука не задана");
        Assert.IsNotNull(_musicAudioMixerGroup, "(AudioSettingsPanel/ValidateComponents) Группа музыки не задана");
    }

    public void ChangeMasterVolume(GameObject sliderGameObject)
    {
        Slider slider = sliderGameObject.GetComponent<Slider>();

        ChangeVolume(slider, _masterAudioMixerGroup, MasterVolume);
    }
    public void ChangeMusicVolume(GameObject sliderGameObject)
    {
        Slider slider = sliderGameObject.GetComponent<Slider>();

        ChangeVolume(slider, _musicAudioMixerGroup, MusicVolume);
    }
    public void ChangeSoundVolume(GameObject sliderGameObject)
    {
        Slider slider = sliderGameObject.GetComponent<Slider>();

        ChangeVolume(slider, _soundAudioMixerGroup,SoundVolume);
    }

    private void ChangeVolume(Slider slider, AudioMixerGroup mixerGroup, string nameOfParameter)
    {
        mixerGroup.audioMixer.SetFloat(nameOfParameter, slider.value);
    }
}
