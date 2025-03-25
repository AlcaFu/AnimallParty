using UnityEngine;

public class ObstacleLaut : MonoBehaviour
{
    private float speed;
    private ObstacleSpawn spawner;

    public void Initialize(float moveSpeed, ObstacleSpawn spawner)
    {
        this.speed = moveSpeed;
        this.spawner = spawner;

        // Hapus objek setelah 5 detik jika tidak bertabrakan
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        // Gerakkan objek ke depan (ke arah rotasi)
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Jika objek berbahaya, tampilkan Game Over
            if (CompareTag("Dangerous"))
            {
                KepitingAudioManager._instance.PlaySFX(0);
                spawner.GameOver();
            }
            // Jika objek aman, tambahkan skor
            else if (CompareTag("Safe"))
            {
                spawner.AddScore(1);
                Destroy(gameObject);
            }
        }
    }
}
