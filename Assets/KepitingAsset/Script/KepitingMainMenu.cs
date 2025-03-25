using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KepitingMainMenu : MonoBehaviour
{
    [SerializeField] AudioSource _source;
    [SerializeField] AudioSource _sfxSource;

    [SerializeField] GameObject _offButton;
    [SerializeField] GameObject _onButton;

    [SerializeField] GameObject _offSfx;
    [SerializeField] GameObject _onSfx;

    private void Start()
    {
        if (PlayerPrefs.GetInt("CrabMusicEnabled", 1) == 1)
        {
            _offButton.gameObject.SetActive(false);
            _onButton.gameObject.SetActive(true); ;
            _source.Play();
        }
        else
        {
            _source.Stop();
            _onButton.gameObject.SetActive(false);
            _offButton.gameObject.SetActive(true); ;
        }

        if (PlayerPrefs.GetInt("CrabSfxEnabled", 1) == 1)
        {
            _sfxSource.mute = false;
            _offSfx.gameObject.SetActive(false);
            _onSfx.gameObject.SetActive(true); ;
        }
        else
        {
            _sfxSource.mute = true;
            _onSfx.gameObject.SetActive(false);
            _offSfx.gameObject.SetActive(true) ;
        }
    }

    public void EnableMusic()
    {
        _source.Play();
        PlayerPrefs.SetInt("CrabMusicEnabled", 1);
        PlayerPrefs.Save();

        _offButton.gameObject.SetActive(false);
        _onButton.gameObject.SetActive(true);
    }

    public void DisableMusic()
    {
        _source.Stop();
        PlayerPrefs.SetInt("CrabMusicEnabled", 0);
        PlayerPrefs.Save();

        _onButton.gameObject.SetActive(false);
        _offButton.gameObject.SetActive(true);
    }

    public void EnableSFX()
    {
        _sfxSource.mute = false;
        PlayerPrefs.SetInt("CrabSfxEnabled", 1);
        PlayerPrefs.Save();

        _offSfx.gameObject.SetActive(false);
        _onSfx.gameObject.SetActive(true);
    }

    public void DisableSFX()
    {
        _sfxSource.mute = true;
        PlayerPrefs.SetInt("CrabSfxEnabled", 0);
        PlayerPrefs.Save();

        _onSfx.gameObject.SetActive(false);
        _offSfx.gameObject.SetActive(true);
    }
}
