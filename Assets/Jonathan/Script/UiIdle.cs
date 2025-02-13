using UnityEngine;
using DG.Tweening; // Pastikan Anda memiliki DoTween dan menggunakan namespace ini

public class UiIdle : MonoBehaviour
{
    [System.Serializable]
    public class RotationSettings
    {
        public RectTransform target; // Objek UI yang akan berotasi
        public float rotationAngle = 15f; // Sudut rotasi ke kiri dan kanan
        public float duration = 2f; // Durasi rotasi ke satu arah
    }

    public RotationSettings[] rotationObjects; // Array untuk mengatur objek UI
    public RectTransform[] buttonObjects; // Array untuk tombol level selection

    public float buttonAnimationDuration = 0.5f; // Durasi animasi scale
    public float buttonDelay = 0.2f; // Delay antar tombol

    void Start()
    {
        AudioManager.Instance.PlayMusic(0);
        // Memulai animasi rotasi untuk objek UI
        foreach (var rotation in rotationObjects)
        {
            if (rotation.target != null)
            {
                StartRotation(rotation);
            }
        }

        // Memulai animasi tombol level selection
        AnimateButtons();
    }

    private void StartRotation(RotationSettings settings)
    {
        // Membuat rotasi kiri dan kanan dengan DoTween secara loop
        settings.target
            .DOLocalRotate(new Vector3(0, 0, settings.rotationAngle), settings.duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void AnimateButtons()
    {
        for (int i = 0; i < buttonObjects.Length; i++)
        {
            RectTransform button = buttonObjects[i];
            if (button != null)
            {
                button.localScale = Vector3.zero; // Memulai dengan scale 0

                button
                    .DOScale(Vector3.one, buttonAnimationDuration) // Animasi scale ke ukuran asli
                    .SetEase(Ease.OutBack)
                    .SetDelay(i * buttonDelay); // Delay berdasarkan urutan tombol
            }
        }
    }
}
