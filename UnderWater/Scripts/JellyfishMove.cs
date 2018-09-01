using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMove : MonoBehaviour {

    public float moveForce;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void MoveJelly()
    {
        transform.Rotate(0, 0, Random.Range(-20, 20));
        rb.AddForce(transform.up * moveForce, ForceMode2D.Impulse);
    }

}
