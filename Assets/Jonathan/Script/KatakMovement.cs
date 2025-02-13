using UnityEngine;


public class KatakMovement : MonoBehaviour
{
    public Transform circleCenter; // Titik pusat lingkaran
    public float radiusOutside = 2f; // Radius luar lingkaran
    public float radiusInside = 1.5f; // Radius dalam lingkaran
    public float linearSpeed = 2f; // Kecepatan linear yang konstan
    public float transitionSpeed = 5f; // Kecepatan perpindahan radius

    private float targetRadius; // Radius tujuan
    private float currentRadius; // Radius saat ini
    private bool isInside = false; // Apakah berada di dalam lingkaran

    private Vector3 circlePosition; // Cache posisi lingkaran untuk efisiensi

    void Start()
    {
        currentRadius = radiusOutside;
        targetRadius = radiusOutside;

        // Cache posisi lingkaran untuk menghindari pengaksesan transform berulang kali
        circlePosition = circleCenter.position;

        // Daftar event untuk Input System
    }

    void Update()
    {
        // Rotasi mengelilingi lingkaran dengan kecepatan sudut yang disesuaikan
        RotatePlayer();

        // Interpolasi radius dengan transisi halus
        UpdateRadius();

        // Perbarui posisi pemain di lingkaran
        UpdatePosition();
    }

    private void RotatePlayer()
    {
        // Hitung kecepatan sudut agar kecepatan linear tetap konstan
        float angularSpeed = linearSpeed / currentRadius; // ω = v / r

        // Rotasi mengelilingi lingkaran dengan kecepatan sudut yang disesuaikan
        transform.RotateAround(circlePosition, Vector3.forward, -angularSpeed * Mathf.Rad2Deg * Time.deltaTime);
    }

    private void UpdateRadius()
    {
        // Lakukan interpolasi radius menuju target radius
        if (Mathf.Abs(currentRadius - targetRadius) > 0.01f) // Threshold untuk mencegah jitter
        {
            currentRadius = Mathf.Lerp(currentRadius, targetRadius, Time.deltaTime * transitionSpeed);
        }
        else
        {
            currentRadius = targetRadius; // Snap ke target untuk menghindari interpolasi tak berujung
        }
    }

    private void UpdatePosition()
    {
        // Update posisi player berdasarkan radius
        Vector3 relativePosition = (transform.position - circlePosition).normalized;
        transform.position = circlePosition + relativePosition * currentRadius;

        // Cerminkan sprite secara vertikal jika berada di dalam lingkaran
        if (isInside)
        {
            // Jika di bagian dalam, cerminkan sprite
            transform.localScale = new Vector3(0.117f, -0.117f, 0.117f);
        }
        else
        {
            // Jika di bagian luar, kembalikan sprite ke normal
            transform.localScale = new Vector3(0.117f, 0.117f, 0.117f);
        }
    }



}
