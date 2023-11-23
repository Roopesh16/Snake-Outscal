using UnityEngine;
using System.Collections.Generic;

public enum Audio_SFX
{
    BUTTON_CLICK,
    FOOD_EAT,
    BURNER_EAT,
    POWERUP,
    GAME_OVER
}

public enum Audio_BGM
{
    GAMEPLAY
}

public class AudioManager : MonoBehaviour
{
    #region --------- Serialized Variables ---------
    [SerializeField] private AudioSource audioSfxSource;
    [SerializeField] private AudioSource audioBgmSource;
    [SerializeField] private List<AudioClip> audioSfxList;
    [SerializeField] private List<AudioClip> audioBgmList;
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

    private void Start()
    {
        PlayBGM(Audio_BGM.GAMEPLAY);
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
            audioBgmSource.playOnAwake = true;
            audioBgmSource.loop = true;
            audioBgmSource.Play();
        }
    }
    #endregion ------------------
}
