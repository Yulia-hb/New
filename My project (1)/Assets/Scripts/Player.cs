using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Vector3 _startPos;

    public System.Action OnGameOver;

    void Start()
    {
        _startPos = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = (CircleCollider2D)GetComponent<Collider2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle") && GameController.Instanse.State == GameController.GameState.PLAY)
        {
            OnGameOver?.Invoke();
            _collider.isTrigger = true;
            _rigidbody2D.isKinematic = true;
        }
    }

    public void Reset()
    {
        _collider.isTrigger = false;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        transform.position = _startPos;

    }
}
