using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float movSpeed;
	public float maxSpeed;
	public int movSystem;
	private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		float xMov = Input.GetAxis("Horizontal");
		
		//First movement system
		if (movSystem == 1){
		
			if (rb2D.velocity.x < maxSpeed && rb2D.velocity.x > -maxSpeed){
			
				rb2D.AddForce(new Vector2(xMov * movSpeed, 0f));
			}
		}

		//Second movement system
		if (movSystem == 2){
		
			rb2D.velocity = new Vector2(maxSpeed * xMov, rb2D.velocity.y);
		}

		//Third movement system
		if (movSystem == 3){
			
			if (Input.GetKey(KeyCode.A)){

				rb2D.velocity = new Vector2(-maxSpeed, rb2D.velocity.y);
			}
			if (Input.GetKey(KeyCode.D)){

				rb2D.velocity = new Vector2(maxSpeed, rb2D.velocity.y);
			}
		}

		//Debug.Log(rb2D.velocity.x);
	}
}
