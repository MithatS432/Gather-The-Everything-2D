using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider musicSlider;

    void Start()
    {
        musicSlider.value = musicSource.volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
