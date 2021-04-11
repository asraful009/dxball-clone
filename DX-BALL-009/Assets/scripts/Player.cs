using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(transform.TransformPoint(Vector3.zero));
        _rigidbody.MovePosition(
            new Vector3(
                Camera.main.ScreenToWorldPoint(
                    new Vector3(Input.mousePosition.x, 0f, 50)).x, -13.5f, 0)
        );
    }
}
