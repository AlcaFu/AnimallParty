using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquirrelAudioManager : MonoBehaviour
{
    public static SquirrelAudioManager _instance;

    public AudioSource _music;
    public AudioSource _sfx;
    public AudioSource _loop;

    public AudioClip[] _sfxClip;

    [SerializeField] GameObject _offButton;
    [SerializeField] GameObject _onButton;

    [SerializeField] GameObject _offSfx;
    [SerializeField] GameObject _onSfx;

    private Image _offButtonSprite;
    private Image _onButtonSprite;

    private Image _offSfxSprite;
    private Image _onSfxSprite;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _offButtonSprite = _offButton.GetComponent<Image>();
        _onButtonSprite = _onButton.GetComponent<Image>();

        _offSfxSprite = _offSfx.GetComponent<Image>();
        _onSfxSprite = _onSfx.GetComponent<Image>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            _offButtonSprite.color = new Color(_offButtonSprite.color.r, _offButtonSprite.color.g, _offButtonSprite.color.b, 100f / 255f);
            _onButtonSprite.color = new Color(_onButtonSprite.color.r, _onButtonSprite.color.g, _onButtonSprite.color.b, 1f);
            _music.Play();
        }
        else
        {
            _music.Stop();
            _onButtonSprite.color = new Color(_onButtonSprite.color.r, _onButtonSprite.color.g, _onButtonSprite.color.b, 100f / 255f);
            _offButtonSprite.color = new Color(_offButtonSprite.color.r, _offButtonSprite.color.g, _offButtonSprite.color.b, 1f);
        }

        if (PlayerPrefs.GetInt("SfxEnabled", 1) == 1)
        {
            _sfx.mute = false;
            _offSfxSprite.color = new Color(_offSfxSprite.color.r, _offSfxSprite.color.g, _offSfxSprite.color.b, 100f / 255f);
            _onSfxSprite.color = new Color(_onSfxSprite.color.r, _onSfxSprite.color.g, _onSfxSprite.color.b, 1f);
        }
        else
        {
            _sfx.mute = true;
            _onSfxSprite.color = new Color(_onSfxSprite.color.r, _onSfxSprite.color.g, _onSfxSprite.color.b, 100f / 255f);
            _offSfxSprite.color = new Color(_offSfxSprite.color.r, _offSfxSprite.color.g, _offSfxSprite.color.b, 1f);
        }
    }

    public void PlayMusic()
    {
        _music.Play();
    }

    public void PlaySFX(int _sfxIndex)
    {
        if (PlayerPrefs.GetInt("SfxEnabled", 1) == 1)
        {
            _sfx.PlayOneShot(_sfxClip[_sfxIndex]);
        }
    }

    public void LoopSfx(int _sfxIndex)
    {
        _loop.clip = _sfxClip[_sfxIndex];
        _loop.loop = true;
        _loop.Play();
    }

    public void StopLoop()
    {
        if (_loop.isPlaying)
        {
            _loop.Stop();
        }
    }

    public void EnableMusic()
    {
        _music.Play();
        PlayerPrefs.SetInt("MusicEnabled", 1);
        PlayerPrefs.Save();

        _offButtonSprite.color = new Color(_offButtonSprite.color.r, _offButtonSprite.color.g, _offButtonSprite.color.b, 100f / 255f);
        _onButtonSprite.color = new Color(_onButtonSprite.color.r, _onButtonSprite.color.g, _onButtonSprite.color.b, 1f);
    }

    public void DisableMusic()
    {
        _music.Stop();
        PlayerPrefs.SetInt("MusicEnabled", 0);
        PlayerPrefs.Save();

        _onButtonSprite.color = new Color(_onButtonSprite.color.r, _onButtonSprite.color.g, _onButtonSprite.color.b, 100f / 255f);
        _offButtonSprite.color = new Color(_offButtonSprite.color.r, _offButtonSprite.color.g, _offButtonSprite.color.b, 1f);
    }

    public void EnableSFX()
    {
        _sfx.mute = false;
        PlayerPrefs.SetInt("SfxEnabled", 1);
        PlayerPrefs.Save();

        _offSfxSprite.color = new Color(_offSfxSprite.color.r, _offSfxSprite.color.g, _offSfxSprite.color.b, 100f / 255f);
        _onSfxSprite.color = new Color(_onSfxSprite.color.r, _onSfxSprite.color.g, _onSfxSprite.color.b, 1f);
    }

    public void DisableSFX()
    {
        _sfx.mute = true;
        PlayerPrefs.SetInt("SfxEnabled", 0);
        PlayerPrefs.Save();

        _onSfxSprite.color = new Color(_onSfxSprite.color.r, _onSfxSprite.color.g, _onSfxSprite.color.b, 100f / 255f);
        _offSfxSprite.color = new Color(_offSfxSprite.color.r, _offSfxSprite.color.g, _offSfxSprite.color.b, 1f);
    }
}
