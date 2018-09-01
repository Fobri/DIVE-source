using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Transform player;
    public float followSpeed;
    public float camZoomMultiplier;
    public float camZoomingSpeed;
    public float maxCamSize;
    public float minCamSize;
    Camera cam;
    private float camZoomChange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
        camZoomMultiplier *= 100;
    }

    private void Update()
    {
        camZoomChange = Mathf.LerpUnclamped(0, Input.GetAxisRaw("Mouse ScrollWheel"), camZoomingSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 finalPos = player.position;
        finalPos.z = transform.position.z;
        transform.position = Vector3.SlerpUnclamped(transform.position, finalPos, Time.deltaTime * followSpeed);

        if(cam.orthographicSize < maxCamSize && camZoomChange < 0)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize + Mathf.Abs(camZoomChange) * camZoomMultiplier, Time.deltaTime);
        }
        else if(cam.orthographicSize > minCamSize && camZoomChange > 0)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize - Mathf.Abs(camZoomChange) * camZoomMultiplier, Time.deltaTime);
        }
        if (cam.orthographicSize < minCamSize)
            cam.orthographicSize = minCamSize;
        if (cam.orthographicSize > maxCamSize)
            cam.orthographicSize = maxCamSize;
    }

}
