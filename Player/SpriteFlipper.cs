using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour {

    SpriteRenderer sr;
    Rigidbody2D rb;
    public bool manualFlip;
    public float rotationSpeed;
    public bool facingRight = true;
    public GameObject[] lights;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = GetComponentInParent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!manualFlip)
        {
            float velX = rb.velocity.x;
            if (velX < 0)
                sr.flipX = true;
            else if (velX > 0)
                sr.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var thing in lights)
            {
                thing.transform.SetParent(null);
            }
            StartCoroutine(SmoothFlip());
        }
    }

    IEnumerator SmoothFlip()
    {
        if (facingRight)
        {
            while (transform.rotation.eulerAngles.y > -180)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, -180, transform.rotation.eulerAngles.z), rotationSpeed * Time.deltaTime);
                if (transform.rotation.eulerAngles.y > 179)
                    break;
                yield return null;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
            facingRight = false;
        }
        else
        {
            while(transform.rotation.eulerAngles.y != 0)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z), rotationSpeed * Time.deltaTime);
                if (transform.rotation.eulerAngles.y < 2)
                    break;
                yield return null;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
            facingRight = true;
        }
        foreach(var thing in lights)
        {
            thing.transform.SetParent(transform);
            thing.transform.rotation = transform.rotation;
            thing.transform.position = transform.position;
        }
    }

    public void FlipSprite()
    {
        sr.flipX = !sr.flipX;
    }
}
