using UnityEngine;
using TMPro;

public class KatakScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject itemPrefab;
    public GameObject[] obstaclePrefabs;  // Array of obstacle prefabs
    public Transform circleCenter;
    public Transform player;  // Reference to the player object
    public float radiusOutside = 2f;
    public float radiusInside = 1.5f;
    public float minDistanceFromPlayer = 2f;  // Minimum distance from player

    public GameObject gameOverMenu;

    public int score = 0;
    private int obstacleCount = 0;

    // Store references to active obstacles
    private GameObject[] currentObstacles;

    void Start()
    {
        AudioManager.Instance.PlayMusic(1);
        currentObstacles = new GameObject[0];
        SpawnItem(); // Spawn initial item
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
        AudioManager.Instance.PlaySFX(6);
        // Check for multiples of score to spawn obstacle
        if (score % 4 == 0)
        {
            SpawnObstacle();
            obstacleCount++;
        }

        // Destroy one obstacle if score is a multiple of 12
        if (score % 12 == 0)
        {
            DestroyOneObstacle();
        }

        // Spawn a new item
        SpawnItem();
    }

    void SpawnItem()
    {
        Vector3 spawnPosition;
        bool isValidPosition;

        // Find a valid spawn position
        do
        {
            // Randomly choose inside or outside the circle
            bool spawnInside = Random.value > 0.5f;
            float spawnRadius = spawnInside ? radiusInside : radiusOutside;

            // Calculate random position
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            spawnPosition = circleCenter.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadius;

            // Validate distance from existing obstacles
            isValidPosition = true;
            foreach (var obstacle in currentObstacles)
            {
                if (obstacle != null && Vector3.Distance(spawnPosition, obstacle.transform.position) < 0.5f) // Minimum 0.5f distance
                {
                    isValidPosition = false;
                    break;
                }
            }
        } while (!isValidPosition);

        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    void SpawnObstacle()
    {
        // Expand obstacle array
        int newArraySize = currentObstacles.Length + 1;
        GameObject[] newObstacles = new GameObject[newArraySize];

        // Copy old obstacles to the new array
        for (int i = 0; i < currentObstacles.Length; i++)
        {
            newObstacles[i] = currentObstacles[i];
        }

        // Find a valid spawn position for the new obstacle
        Vector3 spawnPosition;
        bool isValidPosition;
        float spawnRadius;  // Declare spawnRadius here to make it accessible throughout the method

        do
        {
            // Calculate position and spawn radius
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            spawnRadius = Random.value > 0.5f ? radiusOutside : radiusInside;
            spawnPosition = circleCenter.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * spawnRadius;

            // Validate distance from existing obstacles
            isValidPosition = true;
            foreach (var obstacle in currentObstacles)
            {
                if (obstacle != null && Vector3.Distance(spawnPosition, obstacle.transform.position) < 1f) // Minimum 1f distance between obstacles
                {
                    isValidPosition = false;
                    break;
                }
            }

            // Validate distance from the player
            if (Vector3.Distance(spawnPosition, player.position) < minDistanceFromPlayer)
            {
                isValidPosition = false;
            }
        } while (!isValidPosition);

        // Randomly select an obstacle prefab from the array
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject selectedObstaclePrefab = obstaclePrefabs[randomIndex];

        // Spawn the new obstacle at the valid position
        GameObject newObstacle = Instantiate(selectedObstaclePrefab, spawnPosition, Quaternion.identity);

        // Set the direction and rotation of the obstacle
        bool pointsOutside = spawnRadius == radiusOutside;
        newObstacle.GetComponent<KatakObstacle>().SetDirection(pointsOutside, circleCenter.position);

        // Add the new obstacle to the array
        newObstacles[newObstacles.Length - 1] = newObstacle;

        // Update the reference array
        currentObstacles = newObstacles;
    }

    void DestroyOneObstacle()
    {
        // Find and destroy one obstacle
        for (int i = 0; i < currentObstacles.Length; i++)
        {
            if (currentObstacles[i] != null)
            {
                Destroy(currentObstacles[i]);
                currentObstacles[i] = null; // Remove reference
                break; // Destroy only one obstacle
            }
        }
    }

    public void GameOver()
    {
        AudioManager.Instance.PlaySFX(5);
        gameOverMenu.SetActive(true);
        gameOverText.text = $"{score}";
    }
}
