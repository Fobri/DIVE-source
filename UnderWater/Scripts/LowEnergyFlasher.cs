using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LowEnergyFlasher : MonoBehaviour {
    
    public float speed;
    Text text;
    bool fadingIn = true;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (fadingIn)
        {
            float opacity = Mathf.Lerp(text.color.a, 1, Time.deltaTime * speed);
            var col = text.color;
            col.a = opacity;
            text.color = col;
            if (opacity >= 0.99f)
                fadingIn = false;
        }
        else
        {
            float opacity = Mathf.Lerp(text.color.a, 0, Time.deltaTime * speed);
            var col = text.color;
            col.a = opacity;
            text.color = col;
            if (opacity <= 0.01f)
                fadingIn = true;
        }

    }


}
