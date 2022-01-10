using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        if (_rigidbody.bodyType == RigidbodyType2D.Kinematic && GameController.Instanse.State == GameController.GameState.PLAY)
            transform.localPosition += Vector3.down * Time.deltaTime * 3.5f;

        if (transform.position.y < -20 || transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.CompareTag("Obstacle") &&
            (collision.collider.CompareTag("Obctacle") || collision.collider.CompareTag("Protection")))
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

    
