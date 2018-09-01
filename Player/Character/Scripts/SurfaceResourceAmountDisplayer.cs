using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceResourceAmountDisplayer : MonoBehaviour {

    public GameObject[] resourceDisplaySlots = new GameObject[4];

    private void Start()
    {
        UpdateResourceAmount();
    }

    public void UpdateResourceAmount()
    {
        for(int i = 0; i < resourceDisplaySlots.Length; i++)
        {
            resourceDisplaySlots[i].transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = GameManager.instance.resources[i].amount.ToString();
        }
    }
}
