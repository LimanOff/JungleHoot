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

    [Header("Sliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private const string MasterVolume = "MasterVolume";
    private const string SoundVolume = "SoundVolume";
    private const string MusicVolume = "MusicVolume";

    private void Awake()
    {
        ValidateComponents();
        InitializeSliders();
    }

    private void ValidateComponents()
    {
        Assert.IsNotNull(_masterAudioMixerGroup, "(_audioSettingsPanel/ValidateComponents) Группа мастера звуков не задана");
        Assert.IsNotNull(_soundAudioMixerGroup, "(_audioSettingsPanel/ValidateComponents) Группа звука не задана");
        Assert.IsNotNull(_musicAudioMixerGroup, "(_audioSettingsPanel/ValidateComponents) Группа музыки не задана");

        Assert.IsNotNull(_masterSlider, "(_audioSettingsPanel/ValidateComponents) Слайдер мастера звуков не задан");
        Assert.IsNotNull(_soundSlider, "(_audioSettingsPanel/ValidateComponents) Слайдер звука не задан");
        Assert.IsNotNull(_musicSlider, "(_audioSettingsPanel/ValidateComponents) Слайдер музыки не задан");
    }

    private void InitializeSliders()
    {
        InitializeSlider(_masterSlider,MasterVolume, _masterAudioMixerGroup);
        InitializeSlider(_soundSlider, SoundVolume, _soundAudioMixerGroup);
        InitializeSlider(_musicSlider, MusicVolume, _musicAudioMixerGroup);
    }

    private void InitializeSlider(Slider slider, string nameOfParameter, AudioMixerGroup audioMixerGroup)
    {
        float newSliderValue = 1f;

        audioMixerGroup.audioMixer.GetFloat(nameOfParameter, out newSliderValue);
        slider.value = newSliderValue;
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
