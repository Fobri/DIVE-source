using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRotation : MonoBehaviour {
    
    private float rotationSpeed;
    public bool canRotate;
    SpriteFlipper flipper;
    [Tooltip("0 = null; < 0 = starts slowing down at that angle difference; < = first")]
    public float minAngleDifference;
    [Tooltip("360 = null; < 360 = starts slowing down at that angle difference; < = first")]
    public float minMaxAngleDifference;
    [Tooltip("The speed of the rotation when the angle difference is triggered")]
    private float slowRotationSpeed;

    private void Start()
    {
        flipper = GetComponent<SpriteFlipper>();
        rotationSpeed = GameManager.instance.baseSubFastRotSpeed;
        slowRotationSpeed = GameManager.instance.baseSubSlowRotSpeed;

    }

    private void Update()
    {
        if (canRotate)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(mousePos);
            /*if (!flipper.facingRight)
                screenPos.x = screenPos.x;*/
                
            Vector3 rotationDifference = screenPos - transform.position;
            float rotZ = Mathf.Atan2(rotationDifference.y, rotationDifference.x) * Mathf.Rad2Deg;
            if (!flipper.facingRight)
                rotZ = -rotZ + 180f;

            float angleDifference = transform.rotation.eulerAngles.z - rotZ;
            if (angleDifference < minAngleDifference || angleDifference > 0f && angleDifference < minMaxAngleDifference){

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, rotZ), rotationSpeed * Time.deltaTime);
            }else{
                
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, rotZ), slowRotationSpeed * Time.deltaTime);
            }
            
            //transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, rotZ), rotationSpeed / 25f * Time.deltaTime);
        }
    }
}
