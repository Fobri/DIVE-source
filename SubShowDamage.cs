using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerManager))]
public class SubShowDamage : MonoBehaviour {
    
	public GameObject[] damageStagesObjects;
    public float hp;
    private void Start()
    {
        hp = 100;
    }
    // Update is called once per frame
    void LateUpdate () {
		
		if (hp <= 75){

            damageStagesObjects[0].SetActive(true);
            if(hp <= 50)
            {
                damageStagesObjects[1].SetActive(true);
                if (hp <= 25)
                {
                    damageStagesObjects[2].SetActive(true);
                }
            }
		}
	}
}
