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
        Vector3 angleV = new Vector3(0, 0, 0);
        if(collision.collider.name == "Player") 
        {
            Vector3 width = collision.gameObject.GetComponent<Collider>().bounds.size;
            angleV.x = 
                Mathf.Abs(
                    transform.TransformPoint(Vector3.zero).x 
                    - 
                    collision.gameObject.transform.TransformPoint(Vector3.zero).x
                )/width.x;
            print(angleV);
            //Instantiate(collision, pos, rot);
        }
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal + angleV);
    }
}
