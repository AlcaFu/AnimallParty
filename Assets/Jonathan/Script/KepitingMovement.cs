using UnityEngine;

public class KepitingMovement : MonoBehaviour
{
    public float speed = 5f; // Kecepatan gerak sprite
    public float leftBoundary = -5f; // Batas kiri
    public float rightBoundary = 5f; // Batas kanan

    private Vector2 direction = Vector2.right; // Arah awal gerakan (ke kanan)
    private bool canMove = false; // Hanya bergerak setelah permainan dimulai

    private void Update()
    {
         // Periksa apakah pemain boleh bergerak
        if (!canMove) return;

        // Pergerakan sprite
        transform.Translate(direction * speed * Time.deltaTime);

        // Periksa batas dan ubah arah jika mencapai batas
        if (transform.position.x >= rightBoundary && direction == Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (transform.position.x <= leftBoundary && direction == Vector2.left)
        {
            direction = Vector2.right;
        }
    }

     public void StartMovement()
    {
        AudioManager.Instance.PlayLoopedSFX(3);
        // Memulai pergerakan setelah game dimulai
        canMove = true;
    }

    private void OnEnable()
    {
        // Tambahkan event listener untuk input klik
    }

    private void OnDisable()
    {
        // Hapus event listener
    }
}
