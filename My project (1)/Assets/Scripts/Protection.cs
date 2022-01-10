using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protection : MonoBehaviour
{
    [SerializeField] private bool isDown;
    [SerializeField] private Vector3 delta;
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody;
    
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isDown == false &&
            GameController.Instanse.State == GameController.GameState.PLAY)
        {
            isDown = true;
            delta = transform.position - MousePos();
        }
        if (isDown)
        {
            _rigidbody.MovePosition(delta + MousePos());
        }
        if (Input.GetMouseButtonDown(0) && isDown == true)
        {
            isDown = false;
        }
    }

    private Vector3 MousePos()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward);
    }

}

