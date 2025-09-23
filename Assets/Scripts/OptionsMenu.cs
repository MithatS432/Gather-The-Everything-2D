using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;

    void Awake()
    {
        if (musicSlider != null)
        {
            musicSlider.minValue = 0.0001f;
            musicSlider.maxValue = 1f;
            musicSlider.value = 1f;

            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
    }

    public void SetMusicVolume(float value)
    {
        if (mixer != null)
        {
            float dB = Mathf.Log10(value) * 20f;
            mixer.SetFloat("MusicVolume", dB);
        }
    }
}
