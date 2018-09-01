using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerTechManager : MonoBehaviour {

	
	public Canvas techCanvas;
    Collider2D col;
    public Collider2D playerCol;
    public GameObject blocker;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        BlockBoughtUpgrades();
    }

    void BlockBoughtUpgrades()
    {
        bool[] slotsFilled = new bool[8];
        if (techCanvas.name == "CoreTechCanvas")
            slotsFilled = GameManager.instance.coreTechCanvasIndexes;
        else
            slotsFilled = GameManager.instance.experimentalTechCanvasIndexes;
        for(int i = 0; i < slotsFilled.Length; i++)
        {
            if(slotsFilled[i] == true)
            {
                var t = techCanvas.transform.GetChild(i).transform;
                Instantiate(blocker, t);
            }
        }
    }

    void Update () {
        if (Physics2D.IsTouching(col, playerCol)){
            if (Input.GetKeyDown(KeyCode.E)) {

                if (!GameManager.instance.techOpen) {

                    GameManager.instance.techOpen = true;
                    techCanvas.enabled = true;
                    
                    playerCol.gameObject.GetComponent<PlayerMovement>().enabled = false;
                } else {

                    GameManager.instance.techOpen = false;
                    techCanvas.enabled = false;
                    playerCol.gameObject.GetComponent<PlayerMovement>().enabled = true;
                }
            }
        }
    }
}
