using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRotorSpeedScript : MonoBehaviour {

    public AnimationClip anim;
    public Rigidbody2D rb;
    public int animSpeed;

    private void LateUpdate()
    {
        int fr = (int)rb.velocity.magnitude * animSpeed;
        if(fr != 0)
            anim.frameRate = fr;
    }
}
