using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject dangerousPrefab;
    public GameObject safePrefab;
    public Transform player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI menuText;
    public GameObject gameOverUI;
    public GameObject Tutor;

    public float spawnRate = 1f;
    public float spawnRangeX = 3f;
    public float spawnHeight = 5f;
    public float objectSpeed = 3f;

    public KepitingMovement playerMovement; // Referensi ke skrip movement

    private int score = 0;
    private bool gameStarted = false;

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // Menyimpan daftar obstacle yang di-spawn

    void Start()
    {
        // Tunggu sampai pemain mengetap untuk memulai permainan
        AudioManager.Instance.PlayMusic(0);
        Time.timeScale = 0;
    }

    void Update()
    {
        // Periksa input tap pertama untuk memulai permainan
        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            gameStarted = true;
            Time.timeScale = 1; // Mulai waktu permainan
            InvokeRepeating(nameof(SpawnObstacle), 0f, spawnRate);

            playerMovement.StartMovement();

            Tutor.gameObject.SetActive(false);
        }
    }

    void SpawnObstacle()
    {
        // Tentukan apakah objek yang di-spawn berbahaya atau aman dengan rate spawn 1/5
        GameObject prefabToSpawn = Random.Range(0, 5) == 0 ? safePrefab : dangerousPrefab;

        // Posisi spawn acak di sepanjang sumbu X dan tetap pada ketinggian tertentu
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnHeight, 0);

        // Spawn objek
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Rotasi objek ke arah player
        Vector3 direction = (player.position - spawnedObject.transform.position).normalized;
        spawnedObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        // Tambahkan komponen ObstacleLaut untuk perilaku objek
        ObstacleLaut obstacle = spawnedObject.AddComponent<ObstacleLaut>();
        obstacle.Initialize(objectSpeed, this);

        // Tambahkan obstacle ke daftar
        spawnedObstacles.Add(spawnedObject);

        Debug.Log($"Obstacle spawned: {spawnedObject.name} at {spawnPosition}");
    }

    public void AddScore(int amount)
    {
        AudioManager.Instance.PlaySFX(2);

        // Menambahkan skor dan memperbarui UI skor
        score += amount;
        scoreText.text = $"{score}";

        // Jika skor kelipatan 10, destroy salah satu obstacle
        if (score % 10 == 0)
        {
            DestroyObstacle();
            Debug.Log("Destroy");
        }
    }

    private void DestroyObstacle()
    {
        // Pilih obstacle secara acak untuk di-destroy
        int randomIndex = Random.Range(0, spawnedObstacles.Count);
        GameObject obstacleToDestroy = spawnedObstacles[randomIndex];

        // Log informasi sebelum menghapus obstacle
        Debug.Log($"Destroying obstacle: {obstacleToDestroy.name}");

        // Hapus dari daftar dan destroy dari scene
        spawnedObstacles.RemoveAt(randomIndex);
        Destroy(obstacleToDestroy);

        Debug.Log($"Obstacle destroyed. Remaining obstacles: {spawnedObstacles.Count}");
    }

    public void GameOver()
    {
        // Menampilkan UI Game Over dan menghentikan waktu permainan
        gameOverUI.SetActive(true);
        menuText.text = score.ToString();
        scoreText.text = "";
        Time.timeScale = 0;
        AudioManager.Instance.StopLoopedSFX();
    }
}
