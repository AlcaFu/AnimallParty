using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private GameObject _player;
    Vector2 _velocity;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_player.GetComponent<PlayerMovement>().isDead)
            return;

        transform.position = Vector2.SmoothDamp(transform.position, new Vector2(Camera.main.transform.position.x + 0.2f, _player.transform.position.y), ref _velocity, 1f);
    }
}
