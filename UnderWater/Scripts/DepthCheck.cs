using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthCheck : MonoBehaviour {

    public Text text;
    PlayerManager pManager;
    private int maxSafeDepth;

    private void Awake()
    {
        pManager = GetComponent<PlayerManager>();
    }

    private void Start()
    {
        maxSafeDepth = GameManager.instance.baseMaxSafeDepth;
    }

    private void LateUpdate()
    {
        var depth = (int)transform.position.y;
        pManager.curDepth = depth;
        if (depth < maxSafeDepth)
            text.color = Color.red;
        else
            text.color = Color.white;
        text.text = depth.ToString() + " / " + maxSafeDepth.ToString();
    }
}
