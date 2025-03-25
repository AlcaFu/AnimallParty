using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KepitingUiManager : MonoBehaviour
{
    [SerializeField] public GameObject _pausePanelFade;
    [SerializeField] public GameObject _pausePanel;

    private Image _pausePanelFadeSprite;

    private Vector3 _position;
    private Vector3 _startPos;
    private Color _fadeColor = new Color(0, 0, 0, 210);


    private void Start()
    {
        _pausePanelFade.gameObject.SetActive(false);
        _pausePanelFadeSprite = _pausePanelFade.GetComponent<Image>();
        _position = new Vector3(0, 0, 0);
        _startPos = new Vector3(-892, 0, 0);
    }

    public void ReplayButton()
    {
        AdsManager._instance.ShowInterstitialAd(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }

    public void CrabMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("KepitingMain");
    }

    public void CrabPlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameKepiting");
    }

    public void ResumeButton()
    {
        _pausePanel.transform.DOLocalMove(_startPos, .8f, false).SetUpdate(true);
        _pausePanelFadeSprite.DOFade(0f, 1f).SetUpdate(true).OnComplete(() =>
        {
            _pausePanelFade.gameObject.SetActive(false);
                Time.timeScale = 1f;

                if (PlayerPrefs.GetInt("CrabMusicEnabled", 1) == 1)
                {
                    if (!KepitingAudioManager._instance._music.isPlaying)
                    {
                        KepitingAudioManager._instance.PlayMusic();
                    }
                }
                else
                {
                    KepitingAudioManager._instance._music.Stop();
                }


                if (PlayerPrefs.GetInt("CrabSfxEnabled", 1) == 1)
                {
                    KepitingAudioManager._instance._loop.mute = false;
                    KepitingAudioManager._instance._loop.Play();
                    KepitingAudioManager._instance._sfx.mute = false;
                }
                else
                {
                    KepitingAudioManager._instance._loop.mute = true;
                    KepitingAudioManager._instance._loop.Stop();
                    KepitingAudioManager._instance._sfx.mute = true;
                }
            
        });
    }

    public void PauseButton()
    {
        _pausePanelFade.gameObject.SetActive(true);
        Time.timeScale = 0f;
        KepitingAudioManager._instance._music.Stop();
        KepitingAudioManager._instance._sfx.mute = true;
        KepitingAudioManager._instance._loop.mute = true;
        KepitingAudioManager._instance._loop.Stop();

        _pausePanel.transform.DOLocalMove(_position, .8f, false).SetUpdate(true);
        _pausePanelFadeSprite.DOFade(210f / 225f, 1f).SetUpdate(true);
    }

    public void TweenAnimBack()
    {
        _pausePanel.transform.DOLocalMove(_startPos, .8f, false).SetUpdate(true);
    }

    public void BackButton()
    {
        _pausePanel.transform.DOLocalMove(_position, .8f, false).SetUpdate(true);
    }
}
