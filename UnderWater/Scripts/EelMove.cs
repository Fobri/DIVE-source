using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelMove : MonoBehaviour {

    public float dstToTurn;
    public float speed;
    Vector2 originalPos;
    bool goRight = true;
    SpriteRenderer sr;

    private void Start()
    {
        originalPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (goRight)
        {
            if (Mathf.Abs(originalPos.x - transform.position.x) < dstToTurn)
            {
                sr.flipX = true;
                transform.Translate(transform.right * speed * Time.deltaTime);
            }
            else
                goRight = false;
        }
        else
        {
            sr.flipX = false;
            transform.Translate(-transform.right * speed * Time.deltaTime);
            if (Mathf.Abs(originalPos.x - transform.position.x) < 0.1f)
                goRight = true;
        }
    }


}
