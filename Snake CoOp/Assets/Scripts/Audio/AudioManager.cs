using UnityEngine;
using System.Collections.Generic;

public enum Audio_SFX
{
    BUTTON_CLICK,
    FOOD_EAT,
    BURNER_EAT,
    GAME_OVER
}

public enum Audio_BGM
{
    GAMEPLAY
}

public class AudioManager : MonoBehaviour
{
    #region --------- Serialized Variables ---------
    [SerializeField] private List<AudioClip> audioSfxList;
    [SerializeField] private List<AudioClip> audioBgmList;
    [SerializeField] private AudioSource audioSfxSource;
    [SerializeField] private AudioSource audioBgmSource;
    #endregion ------------------

    #region --------- Public Variables ---------
    public static AudioManager Instance = null;
    #endregion ------------------

    #region --------- Monobehavior Methods ---------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    #endregion ------------------

    #region --------- Public Variables ---------
    public void PlaySFX(Audio_SFX audio_SFX)
    {
        if (audioSfxList[(int)audio_SFX] != null)
            audioSfxSource.PlayOneShot(audioSfxList[(int)audio_SFX]);
    }

    public void PlayBGM(Audio_BGM audio_BGM)
    {
        if (audioBgmList[(int)audio_BGM] != null)
        {
            audioBgmSource.clip = audioBgmList[(int)audio_BGM];
            audioBgmSource.loop = true;
            audioBgmSource.Play();
        }
    }
    #endregion ------------------
}
