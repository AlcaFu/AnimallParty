using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject _pausePanelFade;
    [SerializeField] public GameObject _pausePanel;

    private Image _pausePanelFadeSprite;

    private Vector3 _position;
    private Vector3 _startPos;
    private Color _fadeColor = new Color(0, 0, 0, 210);

    public GameObject _mobileController;

    private void Start()
    {
        _pausePanelFade.gameObject.SetActive(false);
        _pausePanelFadeSprite = _pausePanelFade.GetComponent<Image>();
        _position = new Vector3(0, 0, 0);
        _startPos = new Vector3(-892, 0, 0);
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SquirrelMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SquirrelMain");
    }

    public void SquirrelPlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SquirrelHopper");
    }

    public void ResumeButton()
    {
        _mobileController.gameObject.SetActive(true);
        _pausePanel.transform.DOLocalMove(_startPos, .8f, false).SetUpdate(true);
        _pausePanelFadeSprite.DOFade(0f, 1f).SetUpdate(true).OnComplete(() =>
        { 
          _pausePanelFade.gameObject.SetActive(false);
          Time.timeScale = 2f;

            if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
            {
                if (!SquirrelAudioManager._instance._music.isPlaying) 
                {
                    SquirrelAudioManager._instance.PlayMusic();
                }
            }
            else
            {
                SquirrelAudioManager._instance._music.Stop();
            }


            if (PlayerPrefs.GetInt("SfxEnabled", 1) == 1)
            {
                SquirrelAudioManager._instance._sfx.mute = false;
            }
            else
            {
                SquirrelAudioManager._instance._sfx.mute = true;
            }
        });
    }

    public void PauseButton()
    {
        _mobileController.gameObject.SetActive(false);
        _pausePanelFade.gameObject.SetActive(true);
        Time.timeScale = 0f;
        SquirrelAudioManager._instance._music.Stop();
        SquirrelAudioManager._instance._sfx.mute = true;

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
