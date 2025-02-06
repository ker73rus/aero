using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.15f;

    void Start()
    {
    }

    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void Move(float x, float z)
    {
        if (Input.GetKey(KeyCode.LeftShift))
            transform.position += transform.forward * z * speed / 100 * 2;
        else
            transform.position += transform.forward * z * speed / 100;
        transform.position += transform.right * x * speed / 150;
    }
}
