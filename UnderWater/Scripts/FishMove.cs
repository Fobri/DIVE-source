using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour {

    public float speed;

    void Start(){

        int side = Random.Range(0,2);
        if (side == 0){
           
            transform.rotation = Quaternion.Euler(0f,0f,Random.Range(-30, 31));
        }else{

            transform.rotation= Quaternion.Euler(0f,0f,Random.Range(-220, 221));
        }
    }

    private void FixedUpdate()
    {

        transform.Rotate(0, 0, Random.Range(-0.5f, 0.5f));
        transform.Translate(-transform.right * Time.fixedDeltaTime * speed, Space.World);
        if (transform.rotation.eulerAngles.z > 270f || transform.rotation.eulerAngles.z < 90f){

             GetComponent<SpriteRenderer>().flipY = false;
        }else{

            GetComponent<SpriteRenderer>().flipY = true;
        }
    }
}
