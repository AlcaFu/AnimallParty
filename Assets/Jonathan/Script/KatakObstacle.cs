using UnityEngine;

public class KatakObstacle : MonoBehaviour
{
    private bool pointsOutside = true;

    public void SetDirection(bool isOutside, Vector3 centerPoint)
    {
        pointsOutside = isOutside;

        // Tentukan rotasi berdasarkan posisi relatif terhadap pusat
        Vector3 directionToCenter = (centerPoint - transform.position).normalized;
        if (pointsOutside)
        {
            // Jika di luar, hadap membelakangi pusat
            transform.up = -directionToCenter;
        }
        else
        {
            // Jika di dalam, hadap ke pusat
            transform.up = directionToCenter;
        }
    }

    [System.Obsolete]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            KatakScore katakScore = FindObjectOfType<KatakScore>();
            if (CompareTag("Safe"))
            {
                // Tambahkan skor
                
                if (katakScore != null)
                {
                    katakScore.AddScore();
                    Destroy(gameObject); // Hancurkan objek setelah menambah skor
                }
            }
            else if (CompareTag("Dangerous"))
            {
                katakScore.GameOver();
                Time.timeScale = 0; // Berhenti permainan
            }
        }
    }
}
