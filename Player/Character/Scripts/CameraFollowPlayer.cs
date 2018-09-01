using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

	public float movSpeed;
	public Transform playerTransform;
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position != playerTransform.position){

			transform.position = Vector3.Lerp(transform.position, playerTransform.position, movSpeed * Time.deltaTime);
			transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
		}
	}
}
