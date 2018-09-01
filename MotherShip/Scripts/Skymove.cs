using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skymove : MonoBehaviour {

    public Transform player;

    private void LateUpdate()
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }
}
