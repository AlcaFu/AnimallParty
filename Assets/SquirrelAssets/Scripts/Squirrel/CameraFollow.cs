using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector2 _velocity;
    public GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_player.GetComponent<PlayerMovement>().isDead)
            return;

        Vector2 target = new Vector2(_player.transform.position.x + 2f, _player.transform.position.y);
        transform.position = Vector2.SmoothDamp(transform.position, target, ref _velocity, 0.5f);
    }
}
