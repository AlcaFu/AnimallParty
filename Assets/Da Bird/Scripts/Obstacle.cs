using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _leftSprite;
    [SerializeField] private SpriteRenderer _rightSprite;

    [SerializeField] private BoxCollider2D _left;
    [SerializeField] private BoxCollider2D _right;

    [SerializeField] private float _destroyTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxBarSizeX;
    [SerializeField] private float _minBarSizeX;
    [SerializeField] private float _barResizeTime;

    [SerializeField] private Vector3 _spawnPos;
    [SerializeField] private Vector3 _endPos;

    private float _currentSizeX;
    private float _horizontalSpeed;
    private float _elapseTime = 0f;

    private bool isDestroying = false;
    private bool canUpdate;

    private void Awake()
    {
        transform.position = _spawnPos;
        float timetoMove = -(_endPos - _spawnPos).y / _speed;

        _currentSizeX = Random.Range(_minBarSizeX, _maxBarSizeX);

        canUpdate = Random.Range(0, 5) == 0;

        ResizeBox();

        _horizontalSpeed = (_maxBarSizeX - _minBarSizeX) / Random.Range(timetoMove / 2f, timetoMove);
    }

    private void ResizeBox()
    {
        Vector2 tempSize = _leftSprite.size;
        tempSize.y = _currentSizeX;
        _leftSprite.size = tempSize;
        _left.size = tempSize;
        tempSize.y = _maxBarSizeX - _currentSizeX;

        _rightSprite.size = tempSize;
        _right.size = tempSize;

        tempSize = Vector3.zero;
        tempSize.y = _currentSizeX / 2f;
        _left.offset = tempSize;
        tempSize.y = (_maxBarSizeX - _currentSizeX) /2f;
        _right.offset = tempSize;
    }

    private void OnEnable()
    {
        GameManager.Instance.GameEnded += DestroySprite;
    }

    private void OnDisable()
    {
        GameManager.Instance.GameEnded -= DestroySprite;
    }

    private void FixedUpdate()
    {
        if (!canUpdate) 
            return;

        _currentSizeX += _horizontalSpeed * Time.deltaTime;
        ResizeBox();

        if (_currentSizeX > _maxBarSizeX || _currentSizeX < _minBarSizeX)
        {
            _horizontalSpeed *= -1f;
        }
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
        canUpdate = false;
        _left.enabled = false;
        _right.enabled = false;

        isDestroying = true;
        _elapseTime = 0f;
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
