using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Clouds : MonoBehaviour {

    public float speed = 1;
    public float randomMultiplier;

    private void Start()
    {
        transform.position = (Vector2)transform.position + new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        speed += Random.Range(0.01f, 0.15f) * randomMultiplier;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "CloudBarrier")
            transform.position = new Vector2(transform.parent.position.x, transform.position.y);
    }
}
