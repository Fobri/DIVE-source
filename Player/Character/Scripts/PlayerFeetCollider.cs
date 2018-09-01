using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeetCollider : MonoBehaviour {

    List<Collider2D> cols = new List<Collider2D>();

	void OnTriggerEnter2D(Collider2D collision2D){

        cols.Add(collision2D);
        SetJumpStatus();
	}
	void OnTriggerExit2D(Collider2D collision2D){

        cols.Remove(collision2D);
        SetJumpStatus();
	}

    void SetJumpStatus()
    {
        if(cols.Count > 0)
            GetComponentInParent<PlayerJump>().onGround = true;
        else
            GetComponentInParent<PlayerJump>().onGround = false;
    }
}
