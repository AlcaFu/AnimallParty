using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KepitingMovement : MonoBehaviour
{
    public float speed = 0.01f; // Kecepatan gerak sprite
    public float leftBoundary = -5f; // Batas kiri
    public float rightBoundary = 5f; // Batas kanan
    public Touch _touch;

    public bool canMove = false; // Hanya bergerak setelah permainan dimulai
    public static KepitingMovement _instance;

    private void Update()
    {
        if (!canMove) return;

        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Moved)
            {
                float moveAmount = (_touch.deltaPosition.x / Screen.width) * 10f;

                float newX = transform.position.x + moveAmount;

                newX = Mathf.Clamp(newX, leftBoundary, rightBoundary);

                transform.position = new Vector2(newX, transform.position.y);
            }
        }

        if (PlayerPrefs.GetInt("CrabSfxEnabled", 1) == 1)
        {
            if (!KepitingAudioManager._instance._loop.isPlaying)
            {
                KepitingAudioManager._instance.LoopSfx(3);
            }
        }
        else
        {
            if (KepitingAudioManager._instance._loop.isPlaying)
            {
                KepitingAudioManager._instance.StopLoop();
            }
        }
    }

     public void StartMovement()
    {
        if (PlayerPrefs.GetInt("CrabSfxEnabled", 1) == 1)
        {
            KepitingAudioManager._instance.LoopSfx(3);
        }

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
