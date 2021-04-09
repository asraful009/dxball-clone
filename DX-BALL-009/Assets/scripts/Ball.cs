using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float _speed = 40f;
    Vector3 _velocity;
    Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector3.down);
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = Vector3.up * _speed;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;

    }

    void OnCollisionEnter(Collision collision) 
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
