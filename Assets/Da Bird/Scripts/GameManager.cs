using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _endScoreText;

    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _scorePrefab;
    [SerializeField] private GameObject _obstaclePrefab;
    [SerializeField] private Transform _player;

    public UnityAction GameStarted;
    public UnityAction GameEnded;

    private int score;
    private bool hasGameFinished;

    [SerializeField] private float _spawnInterval;

    private PlayerScripts _playerScripts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameStarted += StartSpawn;
    }

    private void OnDisable()
    {
        GameStarted -= StartSpawn;
    }

    private void Start()
    {
        score = 0;
        _scoreText.text = score.ToString();
        hasGameFinished = false;

        _playerScripts = _player.GetComponent<PlayerScripts>();
        if (_playerScripts != null)
        {
            _playerScripts.GameStarted();
        }

        GameStarted?.Invoke(); 
    }

    private void StartSpawn()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (!hasGameFinished)
        {
            Instantiate(Random.Range(0, 6) == 0 ? _scorePrefab : _obstaclePrefab);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
    }

    public void EndGame()
    {
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        hasGameFinished = true;
        GameEnded?.Invoke();

        _endPanel.SetActive(true);
        _endScoreText.text = score.ToString();

        yield return new WaitForSeconds(0.5f);
    }
}
