using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class KepitingAudioManager : MonoBehaviour
{
    public static KepitingAudioManager _instance;

    public AudioSource _music;
    public AudioSource _sfx;
    public AudioSource _loop;

    public AudioClip[] _sfxClip;

    [SerializeField] GameObject _offButton;
    [SerializeField] GameObject _onButton;

    [SerializeField] GameObject _offSfx;
    [SerializeField] GameObject _onSfx;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("CrabMusicEnabled", 1) == 1)
        {
            _offButton.gameObject.SetActive(false);
            _onButton.gameObject.SetActive(true);
            _music.Play();
        }
        else
        {
            _music.Stop();
            _onButton.gameObject.SetActive(false);
            _offButton.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("CrabSfxEnabled", 1) == 1)
        {
            _loop.Play();
            _sfx.mute = false;
            _offSfx.gameObject.SetActive(false);
            _onSfx.gameObject.SetActive(true);
        }
        else
        {
            _loop.Stop();
            _sfx.mute = true;
            _onSfx.gameObject.SetActive(false);
            _offSfx.gameObject.SetActive(true);
        }
    }

    public void PlayMusic()
    {
        _music.Play();
    }

    public void PlaySFX(int _sfxIndex)
    {
        if (PlayerPrefs.GetInt("CrabSfxEnabled", 1) == 1)
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
        PlayerPrefs.SetInt("CrabMusicEnabled", 1);
        PlayerPrefs.Save();

        _offButton.gameObject.SetActive(false);
        _onButton.gameObject.SetActive(true);
    }

    public void DisableMusic()
    {
        _music.Stop();
        PlayerPrefs.SetInt("CrabMusicEnabled", 0);
        PlayerPrefs.Save();

        _onButton.gameObject.SetActive(false);
        _offButton.gameObject.SetActive(true);
    }

    public void EnableSFX()
    {
        _sfx.mute = false;
        PlayerPrefs.SetInt("CrabSfxEnabled", 1);
        PlayerPrefs.Save();

        _offSfx.gameObject.SetActive(false);
        _onSfx.gameObject.SetActive(true);

        if (KepitingMovement._instance != null && KepitingMovement._instance.canMove)
        {
            KepitingAudioManager._instance.LoopSfx(3);
        }
    }

    public void DisableSFX()
    {
        _loop.Stop();
        _sfx.mute = true;
        PlayerPrefs.SetInt("CrabSfxEnabled", 0);
        PlayerPrefs.Save();

        _onSfx.gameObject.SetActive(false);
        _offSfx.gameObject.SetActive(true);
    }
}
