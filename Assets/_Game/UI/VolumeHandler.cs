using JSAM;
using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;

    [SerializeField] private Slider _soundsSlider;


    private void Start()
    {        
        if (PlayerPrefs.HasKey(StringConsts.MUSIC_VOLUME))
        {
            _musicSlider.value = PlayerPrefs.GetFloat(StringConsts.MUSIC_VOLUME);
        }

        if (PlayerPrefs.HasKey(StringConsts.SOUNDS_VOLUME))
        {
            _soundsSlider.value = PlayerPrefs.GetFloat(StringConsts.SOUNDS_VOLUME);
        }
    }

    public void ChangeMusicVolume()
    {
        AudioManager.SetMusicVolume(_musicSlider.value);
        PlayerPrefs.SetFloat(StringConsts.MUSIC_VOLUME, _musicSlider.value);
    }

    public void ChangeSoundsVolume()
    {
        if (!AudioManager.IsSoundPlaying(Sounds.MM_Button1))
        {
            AudioManager.PlaySound(Sounds.MM_Button1); 
        }
        AudioManager.SetSoundVolume(_soundsSlider.value);
        PlayerPrefs.SetFloat(StringConsts.SOUNDS_VOLUME, _soundsSlider.value);
    }
}
