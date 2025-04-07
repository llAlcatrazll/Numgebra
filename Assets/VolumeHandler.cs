using UnityEngine;

public class VolumeHandler : MonoBehaviour
{
    [SerializeField] private GameObject GeneralAudio;
    [SerializeField] private GameObject GeneralAudioMute;
    [SerializeField] private GameObject BackgroundMusic;
    [SerializeField] private GameObject BackgroundMusicMute;

    private const string GENERAL_AUDIO_KEY = "GeneralAudioMuted";
    private const string BGM_AUDIO_KEY = "BGMusicMuted";

    private void Start()
    {
        ApplyAudioSettings();
    }

    public void MuteGameAudio()
    {
        PlayerPrefs.SetInt(GENERAL_AUDIO_KEY, 1);
        ApplyAudioSettings();
    }

    public void UnMuteGameAudio()
    {
        PlayerPrefs.SetInt(GENERAL_AUDIO_KEY, 0);
        ApplyAudioSettings();
    }

    public void MuteBGMusic()
    {
        PlayerPrefs.SetInt(BGM_AUDIO_KEY, 1);
        ApplyAudioSettings();
    }

    public void UnMuteBGMusic()
    {
        PlayerPrefs.SetInt(BGM_AUDIO_KEY, 0);
        ApplyAudioSettings();
    }

    private void ApplyAudioSettings()
    {
        bool isGeneralAudioMuted = PlayerPrefs.GetInt(GENERAL_AUDIO_KEY, 0) == 1;
        bool isBGMMuted = PlayerPrefs.GetInt(BGM_AUDIO_KEY, 0) == 1;

        // Update AudioListener (affects all sounds)
        AudioListener.volume = isGeneralAudioMuted ? 0f : 1f;

        // Update icons
        if (GeneralAudio != null) GeneralAudio.SetActive(!isGeneralAudioMuted);
        if (GeneralAudioMute != null) GeneralAudioMute.SetActive(isGeneralAudioMuted);
        if (BackgroundMusic != null) BackgroundMusic.SetActive(!isBGMMuted);
        if (BackgroundMusicMute != null) BackgroundMusicMute.SetActive(isBGMMuted);
    }
}
