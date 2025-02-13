using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] float _destroyTime;
    [SerializeField] float _speed;

    [SerializeField] private Vector3 _spawnPos;
    [SerializeField] private Vector3 _endPos;

    private float _elapseTime = 0f;

    private bool isDestroying = false; 

    private void Awake()
    {
        transform.position = _spawnPos;
        float timetoMove = -(_endPos - _spawnPos).y / _speed;
    }

    private void OnEnable()
    {
        GameManager.Instance.GameEnded += DestroySprite;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameEnded -= DestroySprite;
    }

    private void Update()
    {
        if (!isDestroying)
        {
            MoveTowardsEnd();
        }
        else
        {
            HandleDestroying();
        }
    }

    private void MoveTowardsEnd()
    {
        transform.position = Vector3.MoveTowards(transform.position, _endPos, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _endPos) < 0.01f)
        {
            DestroySprite();
        }
    }

    private void DestroySprite()
    {
        isDestroying = true;
        _elapseTime = 0f;
        GetComponent<Collider2D>().enabled = true;
    }

    private void HandleDestroying()
    {
        _elapseTime += Time.deltaTime;

        float _progress = Mathf.Clamp01(_elapseTime / _destroyTime);

        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _progress);

        if (_progress >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
