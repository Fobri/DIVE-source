using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarning : MonoBehaviour {

    Transform player;
    public int maxPlayerWidthArea;
    public GameObject warningObject;

    private void Start()
    {
        player = transform;
    }

    private void LateUpdate()
    {
        if (Mathf.Abs(player.position.x) > maxPlayerWidthArea)
        {
            warningObject.SetActive(true);
        }
        else
            warningObject.SetActive(false);
    }
}
