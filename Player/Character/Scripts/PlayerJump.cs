using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

	public Vector2 jumpForce;
	public bool moveMidair;
	public bool onGround;
	private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Space) && onGround){

			rb2D.AddForce(jumpForce, ForceMode2D.Impulse);
		}

		if (!onGround){

			if(!moveMidair){

				rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
			}
		}else{

			rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}
}
