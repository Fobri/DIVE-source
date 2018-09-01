using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMovement : MonoBehaviour {

    Rigidbody2D rb;
    private float forwardMultiplier;
    private float backMultiplier;
    private bool canUseBoost;

    public bool keepsCurrentDepth;
    SubRotation subRotation;
    public bool isBoosting;

    public float stopSpeed;

	public int boostModifier = 2;

    private float curVel = 0;

    private void Start()
    {
        subRotation = GetComponent<SubRotation>();
        rb = GetComponent<Rigidbody2D>();
        forwardMultiplier = GameManager.instance.baseSubSpeed;
        backMultiplier = GameManager.instance.baseSubReverseSpeed;
        canUseBoost = GameManager.instance.canUseTurbo;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) && canUseBoost)
            {
                rb.AddForce(transform.right * forwardMultiplier * boostModifier, ForceMode2D.Force);
                keepsCurrentDepth = false;
                isBoosting = false;
            }
            else
            {
                rb.AddForce(transform.right * forwardMultiplier, ForceMode2D.Force);
                keepsCurrentDepth = false;
                isBoosting = true;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.right * backMultiplier, ForceMode2D.Force);
            keepsCurrentDepth = false;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            keepsCurrentDepth = true;
            subRotation.canRotate = false;
            isBoosting = false;
        }
        else
            subRotation.canRotate = true;
        if (keepsCurrentDepth)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.SmoothDamp(rb.velocity.y, 0, ref curVel, Time.deltaTime * stopSpeed));
            //rb.AddForce(Vector2.up + (Vector2)transform.position * floatMultiplier, ForceMode2D.Force);
    }
}
