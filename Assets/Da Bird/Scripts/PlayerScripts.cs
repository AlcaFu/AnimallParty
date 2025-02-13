using System.Collections;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _moveTime;

    private float _speed;
    private bool _canClick;
    private bool _canMove;

    private void Awake()
    {
        transform.position = _startPos;
        _canClick = false;
        _canMove = false;
        _speed = (_maxX - _minX) / _moveTime;
    }

    public void GameStarted()
    {
        _canMove = true;
        _canClick = true;
    }

    private void Update()
    {
        if (!_canClick) return;

        if (Input.GetMouseButtonDown(0))
        {
            _speed *= -1f;
        }
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;

        transform.Translate(_speed * Time.fixedDeltaTime * Vector3.right);

        if (transform.position.x < _minX || transform.position.x > _maxX)
        {
            _speed *= -1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Tags.SCORE))
        {
            GameManager.Instance?.UpdateScore();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.CompareTag(Constants.Tags.OBSTACLE))
        {
            GameManager.Instance?.EndGame();
            GetComponent<Collider2D>().enabled = false;

            _canMove = false;
            _canClick = false;
        }
    }
}
